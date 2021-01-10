using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test : MonoBehaviour
{
    public GameObject prefab;

    public void SceneReset()
    {
        SceneManager.LoadScene("GameScene");
    }


    public void CreatePrefab()
    {
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneReset();
    }
}
