using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

class ItemUIView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Bag bag;
    private int id = 0;

    [SerializeField]
    private TMP_Text itemName;

    private bool pointerIn = false;
    public bool PointerIn { get { return pointerIn; } }

    public void DeleteItemFromBag()
    {
        bag.RemoveFromBag(id);
        BagEvents.Invoke(BagEvents.EventType.removeFromBag, id);
    }

    public void Init(ItemData item, Bag _bag)
    {
        id = item.Id;
        itemName.text = item.Name;
        bag = _bag;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerIn = false;
    }

}
