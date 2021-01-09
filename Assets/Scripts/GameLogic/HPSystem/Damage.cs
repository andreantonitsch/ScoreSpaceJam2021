using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int Quantity = 1;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var v = collision.gameObject.GetComponent<HP>();
        if (v != null)
            v.Take(this);
    }
}
