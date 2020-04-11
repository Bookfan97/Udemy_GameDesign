using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnStart : MonoBehaviour
{
    public float shakeAmount;
    private CameraController theCamera;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraController>();
        theCamera.ScreenShake(shakeAmount);
    }
}
