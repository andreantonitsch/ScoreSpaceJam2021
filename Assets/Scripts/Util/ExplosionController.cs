using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExplosionController : MonoBehaviour
{
    public Renderer explosionRenderer;

    public float T;
    MaterialPropertyBlock props;
    public void Start()
    {
        props = new MaterialPropertyBlock();
    }
    public void Update()
    {
        explosionRenderer.GetPropertyBlock(props);
        props.SetFloat("_DifractionStrength", T);
        explosionRenderer.SetPropertyBlock(props);
    }

}
