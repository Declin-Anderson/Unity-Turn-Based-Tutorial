using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    // Target Spot for the model to move to
    private Vector3 targetPosition;

    // On update the charcter will move to a set position when T is pressed according to our movespeed
    private void Update()
    {
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float moveSpeed = 4f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Move(new Vector3(4, 0, 4));
        }
    }

    // Changes the target vector for the model to move to
    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
