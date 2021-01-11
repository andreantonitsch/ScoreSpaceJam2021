using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class EnemyWaveController : MonoBehaviour
{
    public List<EnemyWave> Waves;
    public int Repetitions;

    public bool Ready = true;
    public bool Pause = true;

    public int iteration = 0;

    public EnemyWave current_wave;

    public WaveGenerator wg;

    public void Update()
    {
        if (Ready && !Pause) PlaySection();

    }

    public void PrepareSection()
    {

    }

    public void PlaySection()
    {
        if(iteration < Repetitions)
        {
            StartCoroutine(Section());
            iteration++;
        }
    }

    public IEnumerator Section()
    {
        Ready = false;
        foreach(var wave in Waves)
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
            
            
        }
        
        Ready = true;
        yield return null;
    }


}
