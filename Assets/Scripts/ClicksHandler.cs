using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClicksHandler : MonoBehaviour
{
    private RaycastHit[] results;
    private int n = 0;
    private void Awake()
    {
        results = new RaycastHit[10];
    }

    private void Update()
    {
        //if left mouse button stays unhold just quit
        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0)) return;
        
        //if mouse button up
        if(Input.GetMouseButtonUp(0))
        {
            n = Physics.RaycastNonAlloc(Camera.main.ScreenPointToRay(Input.mousePosition), results);

            for (var i = 0; i < n; i++)
            {
                if (results[i].collider == null) continue;
                if (results[i].collider.GetComponent<IPointerDownHolder>() == null) continue;

                PutInContainer(results[i].collider);
                break;
            }

            Release();
            return;
        }

        //if mouse button down
        n = Physics.RaycastNonAlloc(Camera.main.ScreenPointToRay(Input.mousePosition), results);

        for (var i = 0; i < n; i++)
        {
            if (results[i].collider == null) continue;
            if(results[i].collider.GetComponent<IPointerClickHolder>() == null) continue;

            Hold(results[i].collider);
            break;
        }
    }

    private void PutInContainer(Collider collider)
    {
        if (lastHoldedObject == null) return;

        var container = collider.GetComponent<IPointerDownHolder>();
        var item = lastHoldedObject.GetComponent<ItemData>();

        container.OnPointerUp(item);
    }

    /// <summary>
    /// Releases last holded objects
    /// </summary>
    private void Release()
    {
        if(lastHoldedObject is null) return;

        var draggable = lastHoldedObject.GetComponent<IPointerClickHolder>();
        draggable.OnPointerDown();
    }

    private GameObject lastHoldedObject;

    /// <summary>
    /// Hold object
    /// </summary>
    private void Hold(Collider collider)
    {
        var draggable = collider.GetComponent<IPointerClickHolder>();
        draggable.OnPointerUp();

        lastHoldedObject = collider.gameObject;
    }
}
