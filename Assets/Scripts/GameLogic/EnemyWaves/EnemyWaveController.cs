using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class EnemyWaveController : MonoBehaviour
{
    public delegate void WaveEndedHandler(int wave_number);
    public event WaveEndedHandler WaveEndedEvent;

    public delegate void SectionEndedHandler(int section_number);
    public event SectionEndedHandler SectionEndedEvent;

    public List<EnemyWave> Waves;
    public int Repetitions;

    public bool Ready = true;
    public bool Pause = false;

    public int iteration = 0;

    public EnemyWave current_wave;

    public WaveGenerator wg;


    public int SectionNumber = 0;
    public int WaveNumber = 0;

    public void Update()
    {
        if (Ready && !Pause) PlaySection();

    }

    public void AskForNewSection()
    {
        Waves.Clear();
        wg.GenerateSection();
        Ready = true;
    }

    public void PauseWaves()
    {
        SectionNumber += 1;
        iteration = 0;
        Pause = true;
        SectionEndedEvent.Invoke(SectionNumber);
    }

    public void UnpauseWaves()
    {
        AskForNewSection();
        Pause = false;
        iteration = 0;
    }


    public void PlaySection()
    {
        if(iteration < Repetitions)
        {
            StartCoroutine(Section());
            iteration++;
        }
        if(iteration == Repetitions && Ready)
            PauseWaves();
    }

    public IEnumerator Section()
    {
        Ready = false;
        var current_waves = new List<EnemyWave>(Waves);
        foreach(var wave in current_waves)
        {
            current_wave = wave;
            float duration = wave.Duration;
            wave.Sp.Shoot();
            while (duration > 0.0f)
            {
                if ((wave.Duration - duration > 2.0f) && 
                    wave.EndWhenAllDead && 
                    EnemyTracker.tracked_enemies.Count == 0)
                    break;

                duration -= 1.0f;
                yield return new WaitForSeconds(1f);
            }

            WaveNumber += 1;
            WaveEndedEvent?.Invoke(WaveNumber);


        }

        yield return null;
        Ready = true;
    }


}
