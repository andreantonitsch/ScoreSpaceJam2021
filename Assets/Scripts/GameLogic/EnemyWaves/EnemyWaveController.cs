using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyWave
{
    public ShootingPattern sp;
    public float Duration;
    
}

public class EnemyWaveController : MonoBehaviour
{
    public List<EnemyWave> Waves;
    public int Repetitions;

    public bool Ready = true;

    public int iteration = 0;


    public void Update()
    {
        if (Ready) StartSection();

    }

    public void StartSection()
    {
        if(iteration < Repetitions)
            StartCoroutine(Section());
        iteration++;
    }

    public IEnumerator Section()
    {
        Ready = false;
        foreach(var wave in Waves)
        {
            wave.sp.Shoot();
            yield return new WaitForSeconds(wave.Duration);
        }
        
        Ready = true;
        yield return null;
    }


}
