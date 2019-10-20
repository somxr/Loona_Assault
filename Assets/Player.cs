using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

   [Tooltip("In m/s")] [SerializeField] float xSpeed = 25f;

    [Tooltip("In meters")] [SerializeField] float xRange = 10.5f;

    [Tooltip("In m/s")] [SerializeField] float ySpeed = 25f;

    [Tooltip("In meters")] [SerializeField] float yRange = 4f;

    [SerializeField] float positionPitchFactor = -4f;
    [SerializeField] float throwPitchFactor = -10f;

    [SerializeField] float yawPositionFactor = 4f;

    [SerializeField] float rollThrowFactor = -10f;

    float xThrow, yThrow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        processTranslation();
        processRotation();
    }

    private void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToThrow = yThrow * throwPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToThrow;

        float yaw = transform.localPosition.x * yawPositionFactor;

        float roll = xThrow * rollThrowFactor; 

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void processTranslation()
    {
        xTranslation();
        yTranslation();
    }

    private void yTranslation()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPosition = yOffset + transform.localPosition.y;

        float yPositionClamp = Mathf.Clamp(rawYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x, yPositionClamp, transform.localPosition.z);
    }

    private void xTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPosition = xOffset + transform.localPosition.x;

        float xPositionClamp = Mathf.Clamp(rawXPosition, -xRange, xRange);

        transform.localPosition = new Vector3(xPositionClamp, transform.localPosition.y, transform.localPosition.z);
    }
}


