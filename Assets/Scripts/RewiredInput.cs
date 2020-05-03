using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class RewiredInput : MonoBehaviour
{
    private Player player;

    public Vector2 LeftJoystickInput => leftJoystickInput;
    private Vector2 leftJoystickInput;

    public event Action leftJoystickReset;

    public Vector2 RightJoystickInput => rightJoystickInput;
    private Vector2 rightJoystickInput;

    public bool AButtonPressing => aButtonPressing;
    private bool aButtonPressing;

    public bool BButtonPressing => bButtonPressing;
    private bool bButtonPressing;

    public static RewiredInput instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        player = ReInput.players.GetPlayer(0);
        
        
    }

    private void Update()
    {
        leftJoystickInput.x = player.GetAxis("Move Horizontal");
        leftJoystickInput.y = player.GetAxis("Move Vertical");

        if (Math.Abs(player.GetAxis("Move Horizontal")) < Mathf.Epsilon && player.GetAxis("Move Vertical") < Mathf.Epsilon &&
            player.GetAxisPrev("Move Horizontal") >= Mathf.Epsilon && player.GetAxisPrev("Move Vertical") >= Mathf.Epsilon)
        {
            leftJoystickReset?.Invoke();
        }
        
        
        rightJoystickInput.x = player.GetAxis("Camera Horizontal Movement");
        rightJoystickInput.y = player.GetAxis("Camera Vertical Movement");
        aButtonPressing = player.GetButtonDown("A");
        bButtonPressing = player.GetButtonDown("B");
    }

}
