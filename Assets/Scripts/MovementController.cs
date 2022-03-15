using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    /* PC controls
    [SerializeField]
    InputActionReference movementInput_Left;
    [SerializeField]
    InputActionReference movementInput_Right;
    */

    [SerializeField]
    MovementButtonController leftControl;
    [SerializeField]
    MovementButtonController rightControl;

    [SerializeField]
    float moveSpeed = 100f;

    bool movingLeft = false;
    bool movingRight = false;

    [SerializeField]
    float turnSpeed = 5f;

    private void Start()
    {
        /* PC controls
        if (movementInput_Left)
        {
            movementInput_Left.action.started += StartMovement;
            movementInput_Left.action.canceled += EndMovement;
        }

        if (movementInput_Right)
        {
            movementInput_Right.action.started += StartMovement;
            movementInput_Right.action.canceled += EndMovement;
        }
        */
    }

    private void Update()
    {
        if (leftControl)
            movingLeft = leftControl.buttonPressed;
        if (rightControl)
            movingRight = rightControl.buttonPressed;

        //Check if both keys are pressed, if so don't move
        if (!(movingLeft && movingRight))
        {
            if (movingLeft)
                MovingLeft();
            else if (movingRight)
                MovingRight();
            else
            {
                Quaternion rot = gameObject.transform.rotation;
                gameObject.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, Mathf.LerpAngle(rot.eulerAngles.z, 0, turnSpeed * Time.deltaTime));  //Back to neutral rotation
            }
        }
    }

    void MovingLeft()
    {
        if (gameObject.transform.position.x > -60)
        {
            gameObject.transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            Quaternion rot = gameObject.transform.rotation;
            gameObject.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, Mathf.LerpAngle(rot.eulerAngles.z, 45, turnSpeed * Time.deltaTime));     //Roll ship left
        }
    }

    void MovingRight()
    {
        if (gameObject.transform.position.x < 60)
        {
            gameObject.transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            Quaternion rot = gameObject.transform.rotation;
            gameObject.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, rot.eulerAngles.y, Mathf.LerpAngle(rot.eulerAngles.z, -45, turnSpeed * Time.deltaTime));    //Roll ship right
        }
    }

    /* PC controls
    private void StartMovement(InputAction.CallbackContext obj)
    {
        switch (obj.action.name)
        {
            case "Move_Left":
                movingLeft = true;
                break;
            case "Move_Right":
                movingRight = true;
                break;
            default:
                break;
        }
    }

    private void EndMovement(InputAction.CallbackContext obj)
    {
        switch (obj.action.name)
        {
            case "Move_Left":
                movingLeft = false;
                break;
            case "Move_Right":
                movingRight = false;
                break;
            default:
                break;
        }
    }
    */
}
