using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    bool startedMoving = false;

    [HideInInspector]
    public bool allowedToMove = true;

    // Update is called once per frame
    void Update()
    {
        if (allowedToMove) {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * y;


            //handle step Sound
            if (move != Vector3.zero && !startedMoving) {
                AudioManager.instance.Play("footSteps");
                startedMoving = true;
            }
            else if (move == Vector3.zero && startedMoving) {
                AudioManager.instance.Stop("footSteps");
                startedMoving = false;
            }

            controller.Move(move * speed * Time.deltaTime);
        }
    }
}
