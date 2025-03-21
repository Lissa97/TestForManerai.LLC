using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class DragAndDropItem : MonoBehaviour, IPointerClickHolder
{
    [SerializeField]
    private LayerMask visibleAreaMask;

    [SerializeField, Range(0, 10)]
    private float sensetivity = 10f;

    private const float delta = 0.05f;
    new Rigidbody rigidbody;

    private bool holded = false;
    private const float cameraDistance = 5; //convenient constant close enougth to camera

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPointerUp()
    {   
        holded = true;
        rigidbody.useGravity = false;
    }

    public void OnPointerDown()
    {
        holded = false;
        rigidbody.useGravity = true;
    }

    /// <summary>
    /// Drags object if it was holded
    /// </summary>
    private void FixedUpdate()
    {
        if (!holded) return;

        //calculate point to move
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cameraDistance;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //check if we already near the point
        if (Vector3.Distance(transform.position, worldPosition) < delta) return;
        //check if the object stays inside of visibleArea
        if (
            Physics.OverlapSphere(
                Vector3.Lerp(transform.position, worldPosition, Time.fixedDeltaTime * sensetivity), 
                0.01f, 
                visibleAreaMask
                ).Length == 0
            ) return;
          
        rigidbody.MovePosition(Vector3.Lerp(transform.position, worldPosition, Time.fixedDeltaTime * sensetivity));
    }
}
