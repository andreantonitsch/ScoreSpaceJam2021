using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemySpawnData
{
    public GameObject prefab;
    public Vector3 spawn_location;
    public float z_rotation;
    public int spawn_quantity;

}

[System.Serializable]
public struct SectionWavePossibilities
{
    public List<int> indexes;
}

public class WaveGenerator : MonoBehaviour
{
    public EnemyWaveController wc;
    public List<EnemyWaveDescriptor> WaveDescritors;
    public GameObject BaseWave;
    public int Wave = 10;

    public EnemyWaveDescriptor SpecialWave;
    public bool SpecialSectionIsAGo = false;
    public List<EnemyWaveDescriptor> CurrentWavePossibilities = new List<EnemyWaveDescriptor>();

    public List<SectionWavePossibilities> SectionPossibilities;


    public void Start()
    {
        wc.SectionEndedEvent += SectionEnded;
    }

    public void SectionEnded(int current_section)
    {
        AdvanceSectionWaves(current_section);
        GenerateSection();
    }

    public void AdvanceSectionWaves(int current_section)
    {
        if (current_section == SectionPossibilities.Count)
        {
            SpecialSectionIsAGo = true;

            return;
        }

        var l = new List<EnemyWaveDescriptor>();

        foreach (var i in SectionPossibilities[current_section].indexes)
        {
            l.Add(WaveDescritors[i]);
        }

        CurrentWavePossibilities.AddRange(l);
    }

    public void GenerateSpecialSection()
    {
        var w = GenerateWave(SpecialWave);
        wc.Waves.Add(w);
        w.transform.parent = this.transform;
    }

    public void GenerateSection()
    {
        if (SpecialSectionIsAGo)
        {
            GenerateSpecialSection();
            return;
        }

        wc.Waves.Clear();
        foreach (Transform t in transform)
            Destroy(t.gameObject);

        for (int i = 0; i < Wave; i++)
        {
            int ix = Random.Range(0, CurrentWavePossibilities.Count - 1);
            var w = GenerateWave(CurrentWavePossibilities[ix]);
            wc.Waves.Add(w);
            w.transform.parent = this.transform;
        }
    }

    public EnemyWave GenerateWave(EnemyWaveDescriptor wave_descriptor)
    {
        var wave = Instantiate(BaseWave).GetComponent<EnemyWave>();
        var wave_sp = wave.GetComponent<ShootingPattern>();

        wave.EndWhenAllDead = !wave_descriptor.DurationOnly;
        wave.Duration = Random.Range(wave_descriptor.DurationRange.x,
                                     wave_descriptor.DurationRange.y);

        var available_enemies = wave_descriptor.enemies_available.EnemyDescriptors;
        var ix = Random.Range(0, available_enemies.Count - 1);
        var enemy_descriptor = available_enemies[ix];

        var data = BuildEnemyPrefab(enemy_descriptor);


        // Add Shooting Behavior
        var available_shots = wave_descriptor.shooting_behaviors.shooting_descriptors;
        ix = Random.Range(0, available_shots.Count - 1);
        var shot = available_shots[ix].GetSample();
        wave_sp.shot = shot;
        //

        wave.transform.position = data.spawn_location;
        wave.transform.rotation = Quaternion.Euler(0,0, data.z_rotation);
        wave_sp.ProjectilePrefab = data.prefab;

        wave_sp.shot.BulletQuantity = data.spawn_quantity;
        return wave;
    }



    public EnemySpawnData BuildEnemyPrefab(EnemyDescriptor enemy_descriptor)
    {
        var data = new EnemySpawnData();

        var enemy = Instantiate(enemy_descriptor.base_prefab);
        var sp = enemy.GetComponent<ShootingPattern>();
        var mp = enemy.GetComponent<MovementPattern>();
        var ts = enemy.GetComponent<TimedShotTrigger>();
        var am = enemy.GetComponent<ArcadeMovement>();

        var possible_spawn = enemy_descriptor.spawn_group.spawn_descriptors;
        var ix = Random.Range(0, possible_spawn.Count - 1);
        var spawn = possible_spawn[ix];
        data.spawn_location = spawn.Position;
        data.z_rotation = spawn.Z_rotation;

        ts.Cooldown = Random.Range(enemy_descriptor.ShootingCooldownRange.x,
                                   enemy_descriptor.ShootingCooldownRange.y);
        ts.BurstDuration = Random.Range(enemy_descriptor.BurstDurationRange.x,
                                   enemy_descriptor.BurstDurationRange.y);
        ts.BurstInterval = Random.Range(enemy_descriptor.BurstIntervalRange.x,
                                   enemy_descriptor.BurstIntervalRange.y);

        am.BaseSpeed = Random.Range(enemy_descriptor.BaseSpeedRange.x,
                                   enemy_descriptor.BaseSpeedRange.y);


        var spawn_quantity = Random.Range(enemy_descriptor.QuantityRange.x,
                                          enemy_descriptor.QuantityRange.y);


        var movement_quantity = enemy_descriptor.MovementElements;

        for (int i = 0; i < movement_quantity; i++)
        {
            var mov_group = enemy_descriptor.movement_group.movement_descriptors;
            ix = Random.Range(0, mov_group.Count - 1);
            var mb = mov_group[ix].GetSample();
            mp.AddBehaviour(mb);
        }


        var shot_group = enemy_descriptor.shooting_group.shooting_descriptors;
        ix = Random.Range(0, shot_group.Count - 1);
        var sb = shot_group[ix].GetSample();
        sp.shot = sb;

        sp.shot.BulletQuantity = Random.Range(enemy_descriptor.BulletQuiantity.x,
                                              enemy_descriptor.BulletQuiantity.y);
        //sp.shot.BulletQuantity = Random.Range(5, 15);


        data.spawn_quantity = spawn_quantity;


        data.prefab = enemy;
        enemy.SetActive(false);
        return data;
    }


}
