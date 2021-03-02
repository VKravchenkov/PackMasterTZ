using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingCell : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public ThingFull thingFullParent;

    //TODo
    public Vector2 CenterInGrids;

    public DataCell dataCell;

    public Vector2 StartPosition;

    private void Awake()
    {
        StartPosition = transform.localPosition;
    }
}
