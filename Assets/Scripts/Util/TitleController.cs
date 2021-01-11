using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleController : MonoBehaviour
{

    public List<GameObject> Steps;

    public int step_count = 0;

    public float Timer = 1.0f;
    public float timer = 0.0f;

    public void Update()
    {
        if (timer < 0.0f)
            if (Input.GetButton("Fire1") || 
                Input.GetButton("Jump") ||
                Input.GetButton("Fire2"))
            {
                AdvanceMenu();
            }
        timer -= Time.deltaTime;
    }


    public void AdvanceMenu()
    {
        timer = 1.0f;
        step_count++;

        if (step_count >= Steps.Count)
            SceneManager.LoadScene("GameScene");
        else
        {
            Steps[step_count - 1].SetActive(false);
            Steps[step_count].SetActive(true);

        }



    }



}
