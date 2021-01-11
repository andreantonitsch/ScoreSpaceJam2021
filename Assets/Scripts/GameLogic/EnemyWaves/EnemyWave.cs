using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(ShootingPattern))]
public class EnemyWave : MonoBehaviour
{
    [SerializeField]
    private ShootingPattern sp;
    [SerializeField]
    public float Duration;      //Become End Trigger
    public bool EndWhenAllDead; // same as above

    public ShootingPattern Sp { get {
            if (sp == null)
                sp = GetComponent<ShootingPattern>();
            return sp;
        }
    }

    public void Start()
    {
        sp = GetComponent<ShootingPattern>();
    }

}
