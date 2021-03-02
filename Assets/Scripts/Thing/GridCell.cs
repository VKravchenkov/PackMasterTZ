using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridCell
{
    private ThingCell thingCell;

    private Vector2 coordinates;

    private bool isOccupied;

    private bool nonDelete;

    public ThingCell ThingCell => thingCell;
    public Vector2 Coordinates => coordinates;
    public bool IsOccupied => isOccupied;

    public Bounds Bounds { get => thingCell.boxCollider.bounds; }

    public void Init(Vector2 coordinates)
    {
        this.coordinates = coordinates;
    }

    public void SetOccupied(ThingCell thingCell)
    {
        if (nonDelete)
            return;

        this.thingCell = thingCell;

        if (thingCell != null)
            isOccupied = true;
    }

    public void Feed(bool isFeed)
    {
        if (nonDelete)
            return;

        isOccupied = !isFeed;
        thingCell = null;
    }

    public void SetNonDelete()
    {
        nonDelete = true;
    }
}
