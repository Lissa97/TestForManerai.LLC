using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

internal class TypeHolder : MonoBehaviour
{
    [SerializeField]
    private ItemData.ItemType type;

    [SerializeField]
    private Bag bag;

    private List<ItemData> items = new();

    private float currentLength = 0;
    [SerializeField]
    private float speed = 67;
    private const float delta = 0.05f;

    private void Awake()
    {
        BagEvents.SubscribeOn(BagEvents.EventType.putInBag, Put);
        BagEvents.SubscribeOn(BagEvents.EventType.removeFromBag, Remove);
    }

    /// <summary>
    /// Puts the object with id on "shelf", if it type the same as "self" type
    /// </summary>
    /// <param name="id"></param>
    public void Put(int id)
    {
        var item = bag.GetItem(id);

        if (item.Type != type) return;

        items.Add(item);
        item.GetComponent<RigidbodySwitcher>()?.RigidbodyTurned(false);
        
        StartCoroutine(
                BringItemToPoint(
                    transform.position + Vector3.right * currentLength,
                    item.transform
                    )
            );

        currentLength += 1;
    }

    /// <summary>
    /// Remove the object from "shelf" and rearranges the remains
    /// </summary>
    /// <param name="id"></param>
    public void Remove(int id)
    {
        StopAllCoroutines();

        if (items.Find(x => x.Id == id) is null) return;

        var length = 0;

        for(var i = 0; i < items.Count; i++)
        {
            if (items[i].Id == id)
            {
                items[i].GetComponent<RigidbodySwitcher>()?.RigidbodyTurned(true);
                continue;
            }

            StartCoroutine(
                BringItemToPoint(
                    transform.position + Vector3.right * length,
                    items[i].transform
                    )
            );

            length += 1;
        }

        items.RemoveAll(x => x.Id == id);
        currentLength = length;
    }

    /// <summary>
    /// Move item on "shelf"
    /// </summary>
    IEnumerator BringItemToPoint(Vector3 position, Transform item)
    {
        while (Vector3.Distance(position, item.position) > delta)
        {
            item.position = Vector3.Lerp(item.position, position, Time.fixedDeltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDestroy()
    {
        BagEvents.Unsubscribe(BagEvents.EventType.putInBag, Put);
        BagEvents.Unsubscribe(BagEvents.EventType.removeFromBag, Remove);
    }
}
