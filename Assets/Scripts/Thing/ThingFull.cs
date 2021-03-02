using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThingFull : MonoBehaviour
{
    [SerializeField] private Image imageBack;
    [SerializeField] private TypeThing typeThing;

    public List<ThingCell> thingCells = new List<ThingCell>();

    public GridBlock GridBlock;

    private bool fixedPosition;
    public bool FixedPosition => fixedPosition;
    public TypeThing TypeThing => typeThing;

    public void Init(GridBlock grid)
    {
        GridBlock = grid;
    }

    public void SetFix() => fixedPosition = true;

    public void SetBack(Sprite sprite)
    {
        imageBack.sprite = sprite;
    }

}
