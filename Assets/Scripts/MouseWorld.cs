using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{

    private static MouseWorld instance;
    //Gets the layer that the mouse pointer should hit
    [SerializeField]private LayerMask mousePlaneLayerMask;

    private void Awake() {
        instance = this;
    }
    
    // Gets the mouse position on the screen
   public static Vector3 GetPosition()
   {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask));
        return raycastHit.point;
   }
}
