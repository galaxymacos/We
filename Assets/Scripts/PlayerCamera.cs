using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;

    public float horizontalSensitivity = 15f;
    public float verticalSensitivity = 2f;

    // Update is called once per frame
    void Update()
    {    
        
        freeLookCam.m_XAxis.Value += RewiredInput.instance.RightJoystickInput.x * horizontalSensitivity * Time.deltaTime;
        freeLookCam.m_YAxis.Value +=
            RewiredInput.instance.RightJoystickInput.y * verticalSensitivity * Time.deltaTime * -1;

    }
}
