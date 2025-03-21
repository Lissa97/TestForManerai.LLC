using UnityEngine;
using UnityEngine.Events;

class BagEventsHandler : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onItemAdd;

    [SerializeField]
    private UnityEvent onItemDelete;

    private void Awake()
    {
        BagEvents.SubscribeOn(BagEvents.EventType.putInBag, OnItemAdd);
        BagEvents.SubscribeOn(BagEvents.EventType.removeFromBag, OnItemDelete);
    }

    private void OnDestroy()
    {
        BagEvents.Unsubscribe(BagEvents.EventType.putInBag, OnItemAdd);
        BagEvents.Unsubscribe(BagEvents.EventType.removeFromBag, OnItemDelete);
    }

    private void OnItemDelete(int id)
    {
        onItemDelete?.Invoke();
    }

    private void OnItemAdd(int id)
    {
        onItemAdd?.Invoke();
    }

}
