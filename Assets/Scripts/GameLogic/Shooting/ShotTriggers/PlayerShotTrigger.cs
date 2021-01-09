using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotTrigger : ShotTrigger
{
    public void Update()
    {
        if (Input.GetButton("Fire1"))
            Trigger();
    }

}
