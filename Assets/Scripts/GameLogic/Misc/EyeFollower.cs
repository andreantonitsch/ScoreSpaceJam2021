using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 BasePosition;
    public Transform player;
    public float offset;

    public void Start()
    {
        player = PlayerController.Player?.transform;
        BasePosition = transform.localPosition;
    }

    public void Update()
    {
        if (player)
        {
            var t = (player.position - transform.position ).normalized;

            Vector3 v =  t * offset;
            v.y = -v.y;
            transform.localPosition = BasePosition +v;

        }
    }
}
