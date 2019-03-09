using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera myCamera;
    public float Speed;
    public float Sencitivity;

    private Vector2 myMouse;

	// Use this for initialization
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update ()
    {
        myMouse.x = Input.GetAxis("Mouse X");
        myMouse.y = Input.GetAxis("Mouse Y");

        transform.eulerAngles += new Vector3(0.0f, myMouse.x * Sencitivity * Time.deltaTime, 0.0f);

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0.0f, 0.0f,   Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0.0f, 0.0f,   -Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Speed * Time.deltaTime, 0.0f, 0.0f   );
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Speed * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}
