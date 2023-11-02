using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Spawns a Ragdoll when the unit dies
public class UnitRagdollSpawner : MonoBehaviour
{
    // Getting the ragdoll prefab
    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private Transform originalRootBone;

    // Reference to the health system of the unit
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.onDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Transform ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
        unitRagdoll.Setup(originalRootBone);
    }
}
