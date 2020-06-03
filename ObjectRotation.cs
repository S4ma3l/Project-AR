using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{

    public Transform RotatableH;
    public float RotationSpeed = .1f;

    bool ismouseheld;
    Vector2 currentMousePosition;
    Vector2 mouseDeltaPosition;
    Vector2 lastMousePosition;
    bool istouchpadactive;

    private void Start()
    {
        ResetMousePosition();
    }

    public void ResetMousePosition()
    {
        currentMousePosition = Input.mousePosition;
        lastMousePosition = currentMousePosition;
        mouseDeltaPosition = currentMousePosition - lastMousePosition;
    }

    void FixedUpdate()
    {
        if (istouchpadactive)
        {

            currentMousePosition = Input.mousePosition;
            mouseDeltaPosition = currentMousePosition - lastMousePosition;

            if (RotatableH != null)
                RotatableH.transform.Rotate(0f, mouseDeltaPosition.x * RotationSpeed, 0f);
           
            lastMousePosition = currentMousePosition;


        }

    }

    public void ActivateTouchpad()
    {
        ResetMousePosition();
        istouchpadactive = true;
    }

    public void DeactivateTouchpad()
    {
        istouchpadactive = false;
    }
}