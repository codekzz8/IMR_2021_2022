using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInput : MonoBehaviour
{
    public GameObject ball;
    public float throwingForce = 1000f;
    public GameObject playerCamera;
    public InputActionReference toggleReference = null;
    public InputActionReference gripReference = null;
    private bool isGripped = false;
    public GameObject rightHand;
    public GameObject leftHand;
    public HoverEnterEvent hand;

    private void Awake()
    {
        toggleReference.action.started += Toggle;
        gripReference.action.started += Gripped;
        gripReference.action.canceled += UnGripped;
        
    }

    private void Gripped(InputAction.CallbackContext context)
    {
        isGripped = true;
    }
    private void UnGripped(InputAction.CallbackContext context)
    {
        isGripped = false;
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        if (isGripped)
        {
            gripReference.action.Disable();
            ball.GetComponent<Rigidbody>().AddForce(rightHand.transform.forward * throwingForce);
            gripReference.action.Enable();
        }
    }
}
