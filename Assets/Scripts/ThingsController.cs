using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingsController : MonoBehaviour
{
    [SerializeField] private ItemThings prefabItem;
    [SerializeField] private List<ItemThings> listItemThings;
    [SerializeField] private List<ItemThings> listPool;

    private void Awake()
    {
        EventManager.InitThings += InitItemsThings;
        EventManager.UpdateListThings += UpdateList;
        EventManager.AddedItemThings += Add;
        EventManager.BackToPoolThings += BackToPool;
    }

    private void OnDestroy()
    {
        EventManager.InitThings -= InitItemsThings;
        EventManager.UpdateListThings -= UpdateList;
        EventManager.AddedItemThings -= Add;
        EventManager.BackToPoolThings -= BackToPool;
    }

    private void InitItemsThings(int count)
    {
        listItemThings = new List<ItemThings>();

        for (int i = 0; i < count; i++)
        {
            ItemThings itemThings = Instantiate(prefabItem, transform);
            itemThings.SetImage(IconManager.GetRandomSprite1x1());
            listItemThings.Add(itemThings);
        }

        listPool = new List<ItemThings>(listItemThings);
    }

    private void UpdateList()
    {
        ItemThings itemThings = listPool.Find(item => item.ItemCaseParent != null);

        if (itemThings != null)
            listPool.Remove(itemThings);

        if (listPool.Count == 0)
        {
            //TODO
            EventManager.OnShowButtonGo(true);
        }
        else
        {
            EventManager.OnShowButtonGo(false);
        }
    }

    private void Add(ItemThings itemThings)
    {
        if (listPool.Find(item => item == itemThings) == null)
            listPool.Add(itemThings);

        UpdateList();
    }

    private void BackToPool()
    {
        listItemThings.ForEach(item => listPool.Add(item));

        listPool.ForEach(item => item.SetParentDefault());
    }
}
