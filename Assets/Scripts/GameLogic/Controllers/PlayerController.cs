using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static GameObject player;
    public static float WarpCooldown = 0.75f;

    public static GameObject Player { get { if (player == null) player = FindObjectOfType<PlayerController>()?.gameObject; return player; } }
}
