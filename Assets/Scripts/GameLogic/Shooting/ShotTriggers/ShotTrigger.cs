using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShotTrigger : MonoBehaviour
{
    public ShootingPattern Member;

    public virtual void Init() { }

    public void Sub(ShootingPattern sp)
    {
        Member = sp;
    }

    // Should call sp.Shoot()
    public void Trigger() { Member.Shoot(); }

}


