using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAnimation : MonoBehaviour
{
    public GameObject TeleportPrefab;
    public Material TeleportMaterial;

    public float t;

    public void Update()
    {
        t += ScaledTime.deltaTime;
    }

    public void Spawn(Vector2 position)
    {
        Instantiate(TeleportPrefab, position, Quaternion.identity);
        t = 0;
        StartCoroutine(HandleT());
    }

    public IEnumerator HandleT()
    {
        while (t < 1.1f)
        {
            TeleportMaterial.SetFloat("_T", t);
            yield return null;
        }
    }


}
