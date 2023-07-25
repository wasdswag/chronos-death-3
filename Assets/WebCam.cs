using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture(devices[0].name);
        webcamTexture.requestedFPS = 30;
        webcamTexture.requestedHeight = 480;
        webcamTexture.requestedWidth = 640;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

  
}
