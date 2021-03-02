using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class ThingDragNDrop : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    public ThingFull Thing;

    [SerializeField] private Transform transformParentDef;
    [SerializeField] private RectTransform rectTransform;

    private bool IsWithinBorders() => Thing.thingCells.All(cell => Thing.GridBlock.IsWithinBorders(cell));
    private bool IsNotInersecting() => Thing.thingCells.Any(cell => !Thing.GridBlock.IsCellIntersectGrid(cell));
    private bool IsAllCellOccupied() => Thing.thingCells.All(cell => !Thing.GridBlock.IsNextCellOccupied(cell));

    //TODO
    private Canvas canvas;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();

        rectTransform = transform.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Thing.FixedPosition)
            return;

        rectTransform.SetParent(canvas.transform);

        foreach (ThingCell item in Thing.thingCells)
        {
            if (item.dataCell != null && Thing.GridBlock != null)
            {
                if (Thing.GridBlock.gridCells[(int)item.dataCell.Coordinates.x, (int)item.dataCell.Coordinates.y].ThingCell == item)
                {
                    Thing.GridBlock.gridCells[(int)item.dataCell.Coordinates.x, (int)item.dataCell.Coordinates.y].Feed(true);
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Thing.FixedPosition)
            return;

        Thing.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (Thing.FixedPosition)
            return;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position, this.transform.position + Vector3.forward * 10, 10f);

        if (raycastHit2D.collider != null)
        {
            if (Thing.GridBlock != null && Thing.GridBlock != raycastHit2D.collider.GetComponent<GridBlock>())
            {
                Thing.GridBlock.ListAddedThings.Remove(Thing);
            }

            Thing.Init(raycastHit2D.collider.GetComponent<GridBlock>());

            TryToApplyFigure();

        }
        else
        {
            TryToClearListAddedThings();

            OnReset();
        }
    }

    public void TryToApplyFigure()
    {
        if (IsWithinBorders() && IsNotInersecting() && IsAllCellOccupied())
        {
            Thing.GridBlock.AddThisToGrid(Thing);

            PoolThings.Instance.RemoveToListPack(Thing);

            // Check Update List pack
            PoolThings.Instance.IsEmptyList();
        }
        else
        {
            TryToClearListAddedThings();

            OnReset();
        }
    }

    public void OnReset()
    {
        Thing.Init(null);
        rectTransform.SetParent(transformParentDef);

        if (PoolThings.Instance.ListPoolSelectedPack.Any(item => item == Thing))
            return;

        PoolThings.Instance.AddToListPack(Thing);
        // Check Update List pack
        PoolThings.Instance.IsEmptyList();
    }

    private void TryToClearListAddedThings()
    {
        if (Thing.GridBlock != null && Thing.GridBlock.ListAddedThings.Any(item => item == Thing))
        {
            Thing.GridBlock.ListAddedThings.Remove(Thing);
        }
    }

    public void SetTransformParentDef(Transform transform)
    {
        transformParentDef = transform;
    }
}
