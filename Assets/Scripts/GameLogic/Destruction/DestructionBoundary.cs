using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionBoundary : MonoBehaviour
{

    public void OnTriggerExit2D(Collider2D collision)
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var v = collision.attachedRigidbody.gameObject.GetComponent<Destructor>();
        if (v != null)
            v.Destroy();
    }

}
