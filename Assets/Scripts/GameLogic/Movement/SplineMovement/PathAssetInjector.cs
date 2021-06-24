using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAssetInjector : MonoBehaviour
{
    public List<PathAsset> assets;
    SplineFollowerComponent comp;


    public void Start()
    {
        comp = GetComponent<SplineFollowerComponent>();

        comp.Init();
        foreach(var asset in assets)
            comp.InitializeMovement(asset);

    }
}
