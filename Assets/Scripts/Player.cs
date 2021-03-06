﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera myCamera;
    public float Speed;
    public float JumpStrength;
    public float Sensitivity;
    public float waitime;

    public GameObject BluePortal;
    public GameObject OrangePortal;

    private Vector2 myMouse;
    private Rigidbody myRigidbody;
    private bool isGrounded;
    private bool canTeleport = true;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myRigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        myMouse.x = Input.GetAxis("Mouse X");
        myMouse.y = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(0.0f, myMouse.x * Sensitivity * Time.deltaTime, 0.0f);
        Vector3 targetRotation = new Vector3(-myMouse.y * Sensitivity * Time.deltaTime, 0.0f, 0.0f);
        if(targetRotation.x + myCamera.transform.eulerAngles.x < 90 || targetRotation.x + myCamera.transform.eulerAngles.x > 270)
        {
            myCamera.transform.eulerAngles += targetRotation;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0.0f, 0.0f, Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0.0f, 0.0f, -Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Speed * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Speed * Time.deltaTime, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            myRigidbody.AddForce(0.0f, JumpStrength, 0.0f, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void Teleport(GameObject portal)
    {
        if (canTeleport)
        {

            GameObject otherPortal;
            if (portal.name == "BluePortal")
            {
                otherPortal = GameObject.Find("OrangePortal");
            }
            else
            {
                otherPortal = GameObject.Find("BluePortal");
            }
            if (otherPortal)
            {
                transform.position = portal.transform.position;

                float magnitude = myRigidbody.velocity.magnitude;
                myRigidbody.velocity = otherPortal.transform.forward * magnitude;
                canTeleport = false;
                StartCoroutine(TeleportCooldown());
            }

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else if(collision.gameObject.tag == "Portal")
    }
    IEnumerator TeleportCooldown()
    {
       yield return new WaitForSeconds(waitTime);
        canTeleport = true;
    }
}
