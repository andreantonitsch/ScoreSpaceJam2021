using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExploder : MonoBehaviour
{
    public HP enemy;
    public GameObject Explosion;

    Animation anim;
    public void Start()
    {
        enemy.HPZeroedEvent += SpawnExplosion;
    }

    public void SpawnExplosion()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }

}
