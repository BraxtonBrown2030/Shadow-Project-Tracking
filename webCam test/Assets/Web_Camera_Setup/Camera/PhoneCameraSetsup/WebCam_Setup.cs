using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WebCam_Setup : MonoBehaviour
{
    public WebCamTexture webcamTexture;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            WebCamDevice myWebcam = devices[0];
            webcamTexture = new WebCamTexture(myWebcam.name);
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = webcamTexture;
            webcamTexture.Play();

            if (!webcamTexture.isPlaying)
            {
                Debug.LogError("Couldn't start the webcam stream!");
            }
        }
        else
        {
            Debug.LogError("No webcam devices found!");
        }
    }
}
