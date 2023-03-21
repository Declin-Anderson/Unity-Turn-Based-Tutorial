using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2022.1.23f1 
*/

//* Used to get the mouse position in the world
public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    //Gets the layer that the mouse pointer should hit
    [SerializeField] private LayerMask mousePlaneLayerMask;

    //* Called when the script instance is being loaded
    private void Awake()
    {
        instance = this;
    }

    //* Gets the mouse position on the screen
    public static Vector3 GetPosition()
    {
        // Raycasts to see where the mouse is in the world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
