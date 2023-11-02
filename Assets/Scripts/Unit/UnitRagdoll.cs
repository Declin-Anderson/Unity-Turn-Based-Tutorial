using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Handles the ragdoll physics
public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone;

    public void Setup(Transform originalRootBane)
    {
        MatchAllChildTransforms(originalRootBane, ragdollRootBone);

        ApplyExplosionToRagdoll(ragdollRootBone, 300f, transform.position, 10f);
    }

    //* Goes through all of the bones in the ragdoll model and connects to the bones in the unit model
    private void MatchAllChildTransforms(Transform root, Transform clone)
    {
        // recursive loop to go through all of the children in each child of the root transform
        foreach (Transform child in root)
        {
            Transform cloneChild = clone.Find(child.name);
            if (cloneChild != null)
            {
                cloneChild.position = child.position;
                cloneChild.rotation = child.rotation;

                MatchAllChildTransforms(child, cloneChild);
            }
        }
    }

    //* Applies a explosion to the ragdoll to make it fall with more impact
    private void ApplyExplosionToRagdoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidbody))
            {
                childRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
            }

            ApplyExplosionToRagdoll(child, explosionForce, explosionPosition, explosionRange);
        }
    }
}
