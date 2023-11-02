using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//* Handles the motion of the bullet to it target
public class BulletProjectile : MonoBehaviour
{
    // Reference to trail renderer on prefab
    [SerializeField] private TrailRenderer trailRenderer;
    // Reference to the VFX prefab for bullet hit
    [SerializeField] private Transform bulletHitVFXPrefab;

    // Where bullet will go
    private Vector3 targetPosition;
    //* Setup up where the bullet will start
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        // Calculating the bullet movespeed
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        float moveSpeed = 200f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        // Bullet has reached its target
        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = targetPosition;

            trailRenderer.transform.parent = null;

            Destroy(gameObject);

            Instantiate(bulletHitVFXPrefab, targetPosition, Quaternion.identity);
        }
    }
}
