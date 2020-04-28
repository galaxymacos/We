using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class RewiredTester : MonoBehaviour
{
    private Player player;

    public Vector2 movement;
    public Vector2 cameraMovement;
    public bool aButtonPressing;
    public bool bButtonPressing;
    private void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        movement.x = player.GetAxis("Move Horizontal");
        movement.y = player.GetAxis("Move Vertical");
        cameraMovement.x = player.GetAxis("Camera Horizontal Movement");
        cameraMovement.y = player.GetAxis("Camera Vertical Movement");
        aButtonPressing = player.GetButton("A");
        bButtonPressing = player.GetButton("B");
        print($"Camera Movement: {cameraMovement}");
    }

}
