using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolThings : MonoBehaviour
{
    public static PoolThings Instance { get; private set; }

    public List<ThingFull> ListPoolInstance;
    public List<ThingFull> ListPoolSelectedPack;

    public Dictionary<TypeThing, ThingFull> DictionaryThings = new Dictionary<TypeThing, ThingFull>();

    private void Awake()
    {
        Instance = this;

        ListPoolSelectedPack = new List<ThingFull>();

        InitDictionary();
    }

    private void InitDictionary()
    {
        foreach (TypeThing typeThing in (TypeThing[])System.Enum.GetValues(typeof(TypeThing)))
        {
            ThingFull thingFull = ListPoolInstance.Find(item => item.name.Contains(typeThing.ToString().Substring(4)));

            DictionaryThings.Add(typeThing, thingFull);
        }
    }

    public void AddToListPack(ThingFull thingFull)
    {
        ListPoolSelectedPack.Add(thingFull);

        thingFull.transform.SetParent(transform);

        thingFull.GetComponent<ThingDragNDrop>().SetTransformParentDef(transform);

        thingFull.transform.localScale = Vector3.one;
    }

    public void RemoveToListPack(ThingFull thingFull)
    {
        ListPoolSelectedPack.Remove(thingFull);
    }

    public void IsEmptyList()
    {
        if (ListPoolSelectedPack.Count == 0)
        {
            EventManager.OnShowButtonGo(true);
        }
        else
        {
            EventManager.OnShowButtonGo(false);
        }
    }

    public void RemoteAllToListPack()
    {
        foreach (ThingFull item in ListPoolSelectedPack)
        {
            ListPoolSelectedPack.Remove(item);
            Destroy(item.gameObject);
        }
    }
}

public enum TypeThing
{
    Type1x1 = 0,
    Type1x2 = 1,
    Type2x1 = 2,
    Type1x2L = 3,
    Type2x2 = 4,
    Type3x1 = 5,
    Other = 6
}
