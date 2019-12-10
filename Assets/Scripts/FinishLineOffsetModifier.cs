using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineOffsetModifier : MonoBehaviour
{
    private Material _finishLineMaterial;
    
    private void Awake()
    {
        _finishLineMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _finishLineMaterial.mainTextureOffset = new Vector2(_finishLineMaterial.mainTextureOffset.x + 0.01f, _finishLineMaterial.mainTextureOffset.y);
    }
}
