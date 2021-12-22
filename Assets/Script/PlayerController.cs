using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class PlayerController : MonoBehaviour
{
    bool _isWalking = false;


    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;

    [HideInInspector]
    public bool allowedToMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (allowedToMove)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
            if ((input.axis != Vector2.zero) && _isWalking == false)
            {
                _isWalking = true;
                AudioManager.instance.Play("footSteps");
            }
            if ((input.axis == Vector2.zero) && _isWalking == true)
            {
                _isWalking = false;
                AudioManager.instance.Stop("footSteps");
            }
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }
    }
}