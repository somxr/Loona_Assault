using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [Header("General")]
    [Tooltip("In m/s")] [SerializeField] float xSpeed = 25f;
    [Tooltip("In meters")] [SerializeField] float xRange = 10.5f;
    [Tooltip("In m/s")] [SerializeField] float ySpeed = 25f;
    [Tooltip("In meters")] [SerializeField] float yRange = 4f;

    [Header("Screen-Position Based Parameters")]
    [SerializeField] float pitchPositionFactor = -4f;
    [SerializeField] float yawPositionFactor = 4f;

    [Header("Control-Throw Based Parameters")]
    [SerializeField] float pitchThrowFactor = -10f;
    [SerializeField] float rollThrowFactor = -10f;

    [SerializeField] ParticleSystem[] guns;

    float xThrow, yThrow;

    bool controlsEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (controlsEnabled)
        {
            ProcessInput(controlsEnabled);
        }
    }

    private void ProcessInput(bool controlsEnabled)
    {
        ProcessTranslation();
        ProcessRotation();
        ProccessShooting();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchPositionFactor;
        float pitchDueToThrow = yThrow * pitchThrowFactor;
        float pitch = pitchDueToPosition + pitchDueToThrow;

        float yaw = transform.localPosition.x * yawPositionFactor;

        float roll = xThrow * rollThrowFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        XTranslation();
        YTranslation();
    }

    private void YTranslation()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPosition = yOffset + transform.localPosition.y;

        float yPositionClamp = Mathf.Clamp(rawYPosition, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x, yPositionClamp, transform.localPosition.z);
    }

    private void XTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPosition = xOffset + transform.localPosition.x;

        float xPositionClamp = Mathf.Clamp(rawXPosition, -xRange, xRange);

        transform.localPosition = new Vector3(xPositionClamp, transform.localPosition.y, transform.localPosition.z);
    }

    public void OnPlayerDeath() //Called by STRING REFERENCE. Careful if changing method name, it will not be called 
    {                             //from the other script CollisionHandler
        controlsEnabled = false;
    }

    private void ProccessShooting()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (ParticleSystem gun in guns)
        {
            var em = gun.emission;
            em.enabled = isActive;
        }
    }

}