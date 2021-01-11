using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndController : MonoBehaviour
{


    public GameObject Button;
    public float Timer = 5.0f;


    public IEnumerator EnableRestartButton()
    {
        yield return new WaitForSeconds(Timer);
        Button.SetActive(true);
    }

    public void Start()
    {
        
    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");

    }



}
