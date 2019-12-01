using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerModelToTexture : MonoBehaviour
{
    [SerializeField] private RenderTexture _emptyTexture;
    [SerializeField] private RenderTexture _chadTexture;
    [SerializeField] private RenderTexture _virginTexture;
    [SerializeField] private RawImage _uiImage;

    public void SetEmptyTexture()
    {
        _uiImage.texture = _emptyTexture;
    }

    public void SetChadTexture()
    {
        _uiImage.texture = _chadTexture;
    }

    public void SetVirginTexture()
    {
        _uiImage.texture = _virginTexture;
    }
}
