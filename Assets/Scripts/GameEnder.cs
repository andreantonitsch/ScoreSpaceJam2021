using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    public float end_timer = 5.0f;

    public void Start()
    {
        var hp = PlayerController.Player.GetComponent<HP>();
        hp.HPZeroedEvent += EndGame;

    }


    public IEnumerator DelayedEnd()
    {
        yield return new WaitForSeconds(end_timer);
        DontDestroyOnLoad(ScoreController.Instance);
        SceneManager.LoadScene("EndScreen");
    }

    public void EndGame()
    {
        StartCoroutine(DelayedEnd());
    }



}
