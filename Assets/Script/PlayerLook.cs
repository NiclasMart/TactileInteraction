using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensibility = 1f;

    public Transform player;

    float _mouseY = 0f;
    float _mouseX = 0f;

    float _yPlayerRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseY = Input.GetAxis("Mouse Y");
        _mouseX = Input.GetAxis("Mouse X");

    }

    private void FixedUpdate()
    {
        //loock up/down
        _yPlayerRotation -= _mouseY * mouseSensibility;
        _yPlayerRotation = Mathf.Clamp(_yPlayerRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_yPlayerRotation , 0f, 0f);

        //rotation
        player.Rotate(Vector3.up * _mouseX * mouseSensibility);
    }
}
