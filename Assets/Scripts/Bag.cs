using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;
internal class Bag : MonoBehaviour, IPointerDownHolder
{
    /// <summary>
    /// Stors all collected items by id
    /// </summary>
    Dictionary<int, ItemData> putedItems = new();

    private void Awake()
    {
        BagEvents.SubscribeOn(BagEvents.EventType.removeFromBag, RemoveFromBag);
    }

    /// <summary>
    /// Puts given item in bag and invokes event putInBag
    /// </summary>
    /// <param name="item"></param>
    public void OnPointerUp(ItemData item)
    {
        if (item == null) return;
        if (putedItems.ContainsKey(item.Id)) return;

        putedItems.Add(item.Id, item);

        BagEvents.Invoke(BagEvents.EventType.putInBag, item.Id);
    }

    public ItemData GetItem(int id)
    {
        return putedItems[id];
    }

    /// <summary>
    /// Removes item from dictionary
    /// </summary>
    public void RemoveFromBag(int id)
    {
        if (!putedItems.ContainsKey(id)) return;
        putedItems.Remove(id);
    }

    private void OnDestroy()
    {
        BagEvents.Unsubscribe(BagEvents.EventType.removeFromBag, RemoveFromBag);
    }
}
