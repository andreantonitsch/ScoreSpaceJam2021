using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject prefab;


    public void CreatePrefab()
    {
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}
