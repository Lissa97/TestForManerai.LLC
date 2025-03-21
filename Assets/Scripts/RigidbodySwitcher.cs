using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
class RigidbodySwitcher : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private new Collider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    /// <summary>
    /// Add(true)\Remove(false) pysic calculations from the object
    /// </summary>
    /// <param name="state"></param>
    public void RigidbodyTurned(bool state)
    {
        if(state)
        {
            rigidbody.isKinematic = false;
            collider.enabled = true;
            return;
        }

        rigidbody.isKinematic = true;
        collider.enabled = false;
    }
}
