using System.Collections.Generic;
using UnityEngine;

class BagUIVew : MonoBehaviour
{
    [SerializeField]
    private Bag bag;

    [SerializeField]
    private ItemUIView sampleItemUiView;

    [SerializeField]
    private Transform inventory;

    private List<ItemUIView> items = new();
    private void Start()
    {
        BagEvents.SubscribeOn(BagEvents.EventType.putInBag, AddNewItem);
    }

    public void AddNewItem(int id)
    {
        var newItemUI = Instantiate(sampleItemUiView, inventory);
        newItemUI.GetComponent<ItemUIView>().Init(bag.GetItem(id), bag);
        items.Add(newItemUI);
    }

    /// <summary>
    /// Open and Close bag, and delete the item, if pointer was on one
    /// </summary>
    /// <param name="state"></param>
    public void Opened(bool state)
    {
        //if open, just open
        if(state)
        {
            inventory.gameObject.SetActive(true);
            return;
        }

        //if close, firstly check what item will be deleted
        ItemUIView delete = null;

        foreach (var item in items)
        {
            if (!item.PointerIn) continue;
            delete = item;
            break;
        }

        //if we found deleted item delete it
        if (delete != null)
        {
            delete.DeleteItemFromBag();
            items.Remove(delete);
            Destroy(delete.gameObject);
        }

        inventory.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        BagEvents.Unsubscribe(BagEvents.EventType.putInBag, AddNewItem);
    }
}
