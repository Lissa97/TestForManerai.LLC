using UnityEngine;
using UnityEngine.UI;

class ItemData : MonoBehaviour
{
    [SerializeField]
    private float weight = 0;

    [SerializeField]
    private string itemName = "DefaultName";

    [SerializeField]
    private int id = 0;

    [SerializeField]
    private ItemType type = 0;

    public enum ItemType
    {
        Type1,
        Type2,
        Type3
    }

    public int Id {get { return id; }}
    public string Name {get { return itemName; } }

    public ItemType Type {get { return type; }}
}
