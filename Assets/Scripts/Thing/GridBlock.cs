using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridBlock : MonoBehaviour
{
    [SerializeField] public int Width;
    [SerializeField] public int Height;

    [SerializeField] private RectTransform rectTransformPar;

    public GridCell[,] gridCells;

    private RectTransform rectTransformBlock;
    private Vector3[] vector3s = new Vector3[4];

    private DataCell dataCell;

    public RectTransform RectTransformPar => rectTransformPar;

    public List<ThingFull> ListAddedThings = new List<ThingFull>();

    private void Awake()
    {
        rectTransformBlock = transform.GetComponent<RectTransform>();
        rectTransformBlock.GetWorldCorners(vector3s);

        InitGridCell();
    }

    public void InitGridCell()
    {
        gridCells = new GridCell[Width, Height];

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                gridCells[i, j] = new GridCell();

                if (Height == 2)
                {
                    gridCells[i, j].Init(
                    new Vector2(
                    -rectTransformBlock.rect.width / (Width + Height) + i * rectTransformBlock.rect.width / Width,
                    rectTransformBlock.rect.height / (Width + Height) - j * rectTransformBlock.rect.height / Height
                    ));
                }
                if (Height == 3)
                {
                    gridCells[i, j].Init(
                    new Vector2(
                    -rectTransformBlock.rect.width / (Width * 2) + i * rectTransformBlock.rect.width / Width,
                    rectTransformBlock.rect.height / (Height) - j * rectTransformBlock.rect.height / Height
                    ));
                }
                if (Height == 4)
                {
                    gridCells[i, j].Init(
                    new Vector2(
                    -rectTransformBlock.rect.width / (Width * 2) + i * rectTransformBlock.rect.width / Width,
                    rectTransformBlock.rect.height / (Height) + rectTransformBlock.rect.height / (Height * 2) - j * rectTransformBlock.rect.height / Height
                    ));
                }

            }
        }
    }

    public bool IsWithinBorders(ThingCell cell)
    {
        Vector2 minBorder = new Vector2(vector3s[0].x, vector3s[0].y);
        Vector2 maxBorder = new Vector2(vector3s[2].x, vector3s[2].y);

        return Mathf.Round(cell.boxCollider.bounds.min.x) > minBorder.x
            && Mathf.Round(cell.boxCollider.bounds.min.y) > minBorder.y
            && Mathf.Round(cell.boxCollider.bounds.max.x) < maxBorder.x
            && Mathf.Round(cell.boxCollider.bounds.max.y) < maxBorder.y;
    }

    public bool IsCellIntersectGrid(ThingCell cell)
    {
        foreach (GridCell gridCell in gridCells)
        {
            if (gridCell.IsOccupied)
                if (cell.boxCollider.bounds.Intersects(gridCell.Bounds))
                    return true;
        }
        return false;
    }

    public bool IsNextCellOccupied(ThingCell cell)
    {
        cell.transform.SetParent(gameObject.transform);

        List<DataCell> distances = new List<DataCell>();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                distances.Add(new DataCell()
                {
                    Distance = Vector3.Distance(gridCells[i, j].Coordinates, cell.transform.localPosition),
                    Coordinates = new Vector2(i, j)
                });

            }
        }

        dataCell = GetMinDictance(distances);

        cell.CenterInGrids = gridCells[(int)dataCell.Coordinates.x, (int)dataCell.Coordinates.y].Coordinates;
        cell.dataCell = dataCell;

        cell.transform.SetParent(cell.thingFullParent.transform);
        cell.transform.localPosition = cell.StartPosition;

        return gridCells[(int)dataCell.Coordinates.x, (int)dataCell.Coordinates.y].IsOccupied;
    }

    public void AddThisToGrid(ThingFull thingFull)
    {
        float x = 0;
        float y = 0;

        //TODO
        // 1x1, 1x2,2x1 this

        if (thingFull.TypeThing != TypeThing.Type1x2L)
        {
            for (int i = 0; i < thingFull.thingCells.Count; i++)
            {
                x += thingFull.thingCells[i].CenterInGrids.x;
                y += thingFull.thingCells[i].CenterInGrids.y;

                gridCells[(int)thingFull.thingCells[i].dataCell.Coordinates.x, (int)thingFull.thingCells[i].dataCell.Coordinates.y]
                    .SetOccupied(thingFull.thingCells[i]);
            }

            thingFull.transform.SetParent(gameObject.transform);
            thingFull.transform.localPosition = new Vector3(x / thingFull.thingCells.Count, y / thingFull.thingCells.Count, 0);
        }
        //TODO
        // 1x2L

        if (thingFull.TypeThing == TypeThing.Type1x2L)
        {
            for (int i = 1; i < thingFull.thingCells.Count; i++)
            {
                x += thingFull.thingCells[i].CenterInGrids.x;
                y += thingFull.thingCells[i].CenterInGrids.y;

                gridCells[(int)thingFull.thingCells[i].dataCell.Coordinates.x, (int)thingFull.thingCells[i].dataCell.Coordinates.y]
                    .SetOccupied(thingFull.thingCells[i]);
            }

            thingFull.transform.SetParent(gameObject.transform);
            thingFull.transform.localPosition = new Vector3(x / 2, y / 2, 0);
        }

        if (!ListAddedThings.Any(item => item == thingFull))
            ListAddedThings.Add(thingFull);

        thingFull.transform.localScale = Vector3.one;
    }

    private DataCell GetMinDictance(List<DataCell> distances)
    {
        float min = distances.Min(dataCell => dataCell.Distance);

        return distances.Find(item => item.Distance == min);
    }

    public void Clear()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                gridCells[i, j].Feed(true);
                gridCells[i, j].SetOccupied(null);
            }
        }
    }
}

public class DataCell
{
    public float Distance;
    public Vector2 Coordinates;
}
