using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedShotTrigger : ShotTrigger
{
    public float timer = 0.0f;
    public float Cooldown = 4.0f;
    public float BurstDuration = 2.0f;
    public float BurstInterval = 0.1f;

    public float burst_duration = 0;

    public override void Init()
    {
        base.Init();
        timer = Cooldown;

    }

    public IEnumerator ShootBurst()
    {
        while (burst_duration > 0.0f)
        {
            Trigger();
            yield return new WaitForSeconds(BurstInterval);
        }
    }

    public void FixedUpdate()
    {
        timer -= ScaledTime.fixedDeltaTime;
        burst_duration -= ScaledTime.fixedDeltaTime;
        if (timer <= 0)
        {
            burst_duration = BurstDuration;
            StartCoroutine(ShootBurst());
            timer = Cooldown;
        }

    }
}
