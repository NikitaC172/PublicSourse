using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector2 inputVector;
    private float horizontalMove;
    private float verticalMove;
    private const string horizontalMoveAxis = "Horizontal";
    private const string verticalMoveAxis = "Vertical";

    public float HorizontalMove => horizontalMove;
    public float VerticalMove => verticalMove;

    private void Update()
    {
        inputVector = new Vector2(Input.GetAxis(horizontalMoveAxis), Input.GetAxis(verticalMoveAxis));
        if (inputVector != Vector2.zero)
        {
            float angle = Vector2.Angle(inputVector, Vector2.up);
            horizontalMove = (float)(Math.Sin(Mathf.Deg2Rad * angle) * Math.Sign(inputVector.x));
            verticalMove = (float)(Math.Cos(Mathf.Deg2Rad * angle)) ;
            Debug.Log(horizontalMove + "   " + verticalMove);
        }
        else
        {
            horizontalMove = 0;
            verticalMove = 0;
        }
    }
}
