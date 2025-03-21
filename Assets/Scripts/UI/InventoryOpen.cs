using UnityEngine;

class InventoryOpen : MonoBehaviour, IPointerClickHolder
{
    [SerializeField]
    private BagUIVew bagUI;
    public void OnPointerUp()
    {
        bagUI.Opened(true);
    }

    public void OnPointerDown()
    {
        bagUI.Opened(false);

    }
}