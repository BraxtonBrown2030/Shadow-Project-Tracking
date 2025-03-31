using System;
using UnityEngine;
using UnityEngine.UI;

public class RawImageWebCamera : MonoBehaviour
{     
    public RawImage webCamDisplay;
    private WebCamTexture webCamTexture;

    void Start()
    {
        if (webCamDisplay == null)
        {
            Debug.LogError("RawImage component is not assigned.");
            return;
        }

        webCamTexture = new WebCamTexture();
        webCamDisplay.texture = webCamTexture;

        if (webCamTexture != null)
        {
            webCamTexture.Play();
            Debug.Log("WebCamTexture started.");
        }
        else
        {
            Debug.LogError("Failed to initialize WebCamTexture.");
        }
    }
}
