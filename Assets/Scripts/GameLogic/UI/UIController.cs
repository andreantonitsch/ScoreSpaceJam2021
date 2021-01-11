using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text score_text;
    public TMP_Text hp_text;
    public TMP_Text wave_text;

    public ScoreController sc;
    public ArcadeMovement am;
    public EnemyWaveController wc;

    public GameObject WarpReady;
    public GameObject WarpNotReady;



    public void Start()
    {
        sc = ScoreController.Instance;
        sc.ScoreChangedEvent += UpdateScore;
        var player_hp = PlayerController.Player.GetComponent<HP>();
        var am = PlayerController.Player.GetComponent<ArcadeMovement>();


        wc = FindObjectOfType<EnemyWaveController>();
        wc.WaveEndedEvent += UpdateWaves;


        UpdateHP(player_hp.Max);
        player_hp.HPChangedEvent += UpdateHP;
        am.TeleportTriggeredEvent += UpdateWarp;

        

    }

    public void UpdateScore(int quantity, int delta)
    {
        score_text.text = $"{quantity:00000}";
    }

    public void UpdateHP(int quantity)
    {
        if (quantity > 0)
            hp_text.text = new string('I', quantity);
    }

    public void UpdateWaves(int quantity)
    {
        wave_text.text = $"{wc.SectionNumber} : {wc.WaveNumber}";
    }



    public IEnumerator WarpTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        WarpReady.SetActive(true);
        WarpNotReady.SetActive(false);
    }
    public void UpdateWarp()
    {
        WarpReady.SetActive(false);
        WarpNotReady.SetActive(true);
        StartCoroutine(WarpTimer(PlayerController.WarpCooldown));

    }

}
