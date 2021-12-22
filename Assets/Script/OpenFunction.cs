using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

enum DoorState
{
    OPEN,
    CLOSE,
    OPENING,
    CLOSING
}

public class OpenFunction : MonoBehaviour
{
    DoorState _state = DoorState.CLOSE;
    float _moveDirection;
    float _openXPosition = -1.6f;
    float _closeXPosition;

    public MeshCollider col;
    public SteamVR_Action_Boolean triggerDoor;

    public float doorSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _closeXPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if ((Input.GetKeyDown(KeyCode.E) || triggerDoor.stateDown) && col.Raycast(ray, out hit, 2f)) { 
            if (_state == DoorState.CLOSE) {
                AudioManager.instance.Play("door");
                _moveDirection = -1;
                _state = DoorState.OPENING;
            }
            else if (_state == DoorState.OPEN) {
                AudioManager.instance.Play("door");
                _moveDirection = 1;
                _state = DoorState.CLOSING;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_state == DoorState.OPENING) {
            MoveDoor();
            if (transform.position.x <= _openXPosition) {
                _moveDirection = 0;
                _state = DoorState.OPEN;
            }
        }
        else if (_state == DoorState.CLOSING) {
            MoveDoor();
            if (transform.position.x >= _closeXPosition) {
                _moveDirection = 0;
                _state = DoorState.CLOSE;
            }
        }
    }

    void MoveDoor()
    {
        Vector3 pos = transform.position;
        pos.x += _moveDirection * doorSpeed;
        transform.position = pos;
    }
}
