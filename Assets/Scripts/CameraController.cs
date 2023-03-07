using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Controls the camera movement and the inputs for being able to move the camera
public class CameraController : MonoBehaviour
{

    // Minimum speed that the camera will zoom
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    // Maximum speed that the camera will zoom
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    // The Cinemachine camera that is used
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    // The position that the camera will move to
    private Vector3 targetFollowOffset;
    // Transposer component to be able to adjust the offset
    private CinemachineTransposer cinemachineTransposer;

    //* Start is called before the first frame update
    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    //* Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    //* Handling the movement of the camera through WASD
    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }

        // Speed of the camera
        float moveSpeed = 10f;

        // The movement of the camera
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    //* Handling the rotation of camera through QE
    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        // Rotation Speed
        float rotationSpeed = 100f;

        // Generating the rotation
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    //* Handles of the zoom of the camera using the scroll wheel
    private void HandleZoom()
    {
        float zoomAmount = 1f;
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }

        // The position it will zoom into
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        // Speed of the zoom
        float zoomSpeed = 5f;

        // Generating the zoom
        cinemachineTransposer.m_FollowOffset =
            Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }
}
