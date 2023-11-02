using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Animator script for units
public class UnitAnimator : MonoBehaviour
{
    //* Reference to animator
    [SerializeField] private Animator animator;
    //* Reference to bullet prefab
    [SerializeField] private Transform bulletProjectilePrefab;
    //* Reference to the point where the bullet comes out
    [SerializeField] private Transform shootPointTransform;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
    }

    //* listener for the OnStartMoving Event in MoveAction Script
    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", true);
    }

    //* listener for the OnStopMoving Event in MoveAction Script
    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", false);
    }

    //* listener for the OnShoot Event in ShootAction Script
    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        animator.SetTrigger("Shoot");

        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        Vector3 targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
        targetUnitShootAtPosition.y = shootPointTransform.position.y;
        
        bulletProjectile.Setup(targetUnitShootAtPosition);
    }
}
