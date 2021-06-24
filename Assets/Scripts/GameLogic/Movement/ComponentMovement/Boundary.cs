using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Vector4 Rect;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(Rect.x, Rect.y, 0), new Vector3(Rect.x, Rect.w, 0));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(Rect.x, Rect.w, 0), new Vector3(Rect.z, Rect.w, 0));
        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3(Rect.z, Rect.w, 0), new Vector3(Rect.z, Rect.y, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(Rect.z, Rect.y, 0), new Vector3(Rect.x, Rect.y, 0));
    }
}
