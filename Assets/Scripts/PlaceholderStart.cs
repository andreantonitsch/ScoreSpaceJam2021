using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderStart : MonoBehaviour
{
    EnemyWaveController wc;


    public IEnumerator DelayedStart(float wait)
    {
        yield return new WaitForSeconds(wait);
        wc.AskForNewSection();
        
    }
    public void Start()
    {
        wc = FindObjectOfType<EnemyWaveController>();

        StartCoroutine(DelayedStart(2));
    }
}
