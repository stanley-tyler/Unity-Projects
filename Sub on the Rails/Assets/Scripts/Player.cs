using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Header("General")]
    [Tooltip("In meters per second")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In meters")] [SerializeField] float yRange = 20f;
    [Tooltip("In meters")][SerializeField] float xRange = 14f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -.05f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -20f;


    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update ()
    {
        if (isControlEnabled == true)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
        
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffsetThisFrame = xThrow * controlSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffsetThisFrame;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffsetThisFrame = yThrow * controlSpeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffsetThisFrame;
        float clampedYPos = Mathf.Clamp(rawYPos, -14f, 6f);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    
    void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;

    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor; //x
        float yaw = transform.localPosition.x * positionYawFactor; //y
        float roll = xThrow * controlRollFactor; //z

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DeactiveateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void DeactiveateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    
}
