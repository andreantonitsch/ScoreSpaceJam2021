using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpgradeSystem : MonoBehaviour
{
    public GameObject UpgradeCanvas;
    public TMP_Text text;
    public GameObject TextCanvas;

    public GameObject player;
    public GameObject PlayerBulletPrefab;
    public ShotBehavior sb;

    public EnemyWaveController wc;

    public void Start()
    {
        wc = FindObjectOfType<EnemyWaveController>();
        player = PlayerController.Player;
        var sp = player.GetComponent<ShootingPattern>();
        sb = Instantiate(sp.shot);
        sp.shot = sb;
        wc.SectionEndedEvent += ActivateUpgrades;
    }


    public IEnumerator DelayedUpgradeActivate()
    {
        var count = 3;
        yield return new WaitForSeconds(1);

        for (int i = 0; i <= count; i++)
        {
            text.text = $"Pick your Upgrades\nBelow In...\n{3-i}...";
            yield return new WaitForSeconds(1);
        }

        UpgradeCanvas.SetActive(true);

    }


    public void ActivateUpgrades(int section)
    {
        TextCanvas.SetActive(true);
        StartCoroutine(DelayedUpgradeActivate());

    }


    public void ProjectilesPlus()
    {
        sb.BulletQuantity += 1;
        UnpauseWaves();
    }

    public void PiercePlus()
    {
        PlayerBulletPrefab.GetComponent<DestroyOnCollision>().CollisionCount += 1;
        UnpauseWaves();
    }

    public void DamagePlus()
    {
        PlayerBulletPrefab.GetComponent<Damage>().Quantity += 1;
        UnpauseWaves();
    }

    public void AttackSpeedPlus()
    {
        player.GetComponent<ShootingPattern>().Cooldown-= 0.2f;
        UnpauseWaves();
    }

    public void HealHP()
    {
        var hp = player.GetComponent<HP>();
        hp.Current = hp.Max;
        UnpauseWaves();
    }

    public void UnpauseWaves()
    {
        TextCanvas.SetActive(false);
        UpgradeCanvas.SetActive(false);
        wc.UnpauseWaves();
    }

}