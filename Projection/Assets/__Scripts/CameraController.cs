using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float mouseSensitivity;
    public float clampAngle;
    public CharacterController player;
    public bool look = true;

    //private Transform parent;
    private float verticalRotation;
    private float horizontalRotation;

    // Start is called before the first frame update
    void Start()
    {
        //parent = transform.parent;
        horizontalRotation = player.transform.eulerAngles.y;
        verticalRotation = transform.localEulerAngles.x; 
        //cameraTranform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (look)
        {
            CameraMovement();
        }
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red);

        // If the player presses ESC while in the game, it unlocks the cursor
        if (look && Input.GetKeyDown(KeyCode.Escape))
        {
            look = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0) && !look)
        {
            look = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void CameraMovement()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //verticalRotation += mouseY ; 
        //verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        //transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        //parent.Rotate(Vector3.up, mouseX);
        //parent.Rotate(Vector3.right, Mathf.Clamp(mouseY, -clampAngle, clampAngle));

        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //parent.Rotate(mouseY, 0, 0);

        //float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //parent.localEulerAngles = new Vector3(Mathf.Clamp(parent.localEulerAngles.x, -viewRange, viewRange), 0, 0);
        //parent.Rotate(Vector3.right, mouseY);

        float _mouseVertical = -Input.GetAxis("Mouse Y");
        float _mouseHorizontal = Input.GetAxis("Mouse X");

        verticalRotation += _mouseVertical * mouseSensitivity * Time.deltaTime;
        horizontalRotation += _mouseHorizontal * mouseSensitivity * Time.deltaTime;

        verticalRotation = Mathf.Clamp(verticalRotation, -clampAngle, clampAngle);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

    }
}
