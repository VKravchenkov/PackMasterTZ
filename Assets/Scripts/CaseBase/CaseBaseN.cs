using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CaseBaseN : MonoBehaviour
{
    // TODO Mode
    [SerializeField] protected ModeSizeCase modeSizeCase;

    [SerializeField] protected List<GridBlock> gridBlocks;

    [SerializeField] protected RectTransform BackgroundLeft;
    [SerializeField] protected RectTransform BackgroundRight;

    [SerializeField] protected bool IsRotate = false;

    [SerializeField] protected PoolThings poolThings;

    public List<TestlayerData> testlayerDatas = new List<TestlayerData>();

    public ModeSizeCase ModeSizeCase => modeSizeCase;

    protected List<DataIndex> dataCellTmp = new List<DataIndex>();

    protected int I;
    protected int J;
    protected int rnd;

    protected virtual void Start()
    {
        //InitConfigure();
    }

    protected abstract void OneThingsInCases1x2(int indexGridBlock, string str);
    protected abstract void OneThingsInCases1x1(int indexGridBlock, string str, bool isRandom);
    protected abstract void OneThingsInCases2x1(int indexGridBlock = 0, string str = default);
    protected abstract void Rotation();
    protected abstract void OnReset();
    public abstract void InitConfigure();

    public void SetPool(PoolThings poolThings)
    {
        this.poolThings = poolThings;
    }

    public void SetRotate(bool isRotate)
    {
        this.IsRotate = isRotate;
    }

    protected string GenerateStr(string strDefault, int indexMax, string strInsert)
    {
        int index = Random.Range(0, indexMax);
        return strDefault.Insert(index, strInsert);
    }

    protected void AppendByTypeInListPack(TypeThing typeThing, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            ThingFull thingFull = Instantiate(poolThings.DictionaryThings[typeThing]);

            //switch (typeThing)
            //{
            //    case TypeThing.Type1x1:
            //        thingFull.SetBack(IconManager.GetRandomSprite1x1());
            //        break;
            //    case TypeThing.Type1x2:
            //        thingFull.SetBack(IconManager.GetRandomSprite1x2());
            //        break;
            //    case TypeThing.Type2x1:
            //        thingFull.SetBack(IconManager.GetRandomSprite2x1());
            //        break;
            //    case TypeThing.Other:
            //        break;
            //    default:
            //        break;
            //}

            poolThings.AddToListPack(thingFull);
        }
    }

    protected void AddedThingsFixed1x1(int indexI, int indexJ, ThingFull thingFull, int indexGridBlock)
    {
        //TODO
        //thingFull.SetBack(IconManager.GetRandomSprite1x1());

        thingFull.Init(gridBlocks[indexGridBlock]);

        thingFull.GridBlock.gridCells[indexI, indexJ].SetOccupied(thingFull.thingCells[0]);

        thingFull.GridBlock.gridCells[indexI, indexJ].SetNonDelete();

        thingFull.SetFix();

        thingFull.transform.SetParent(thingFull.GridBlock.transform);

        thingFull.transform.localPosition = thingFull.GridBlock
            .gridCells[indexI, indexJ]
            .Coordinates;

        thingFull.transform.localScale = Vector3.one;
    }

    protected void AddedThingsFixed1x2Or2x1(List<DataIndex> dataIndices, ThingFull thingFull, int indexGridBlock)
    {
        //if (thingFull.TypeThing== TypeThing.Type1x2)
        //{
        //    thingFull.SetBack(IconManager.GetRandomSprite1x2());
        //}

        //if (thingFull.TypeThing==TypeThing.Type2x1)
        //{
        //    thingFull.SetBack(IconManager.GetRandomSprite2x1());
        //}

        thingFull.Init(gridBlocks[indexGridBlock]);

        Vector2 Center = Vector2.zero;

        for (int i = 0; i < dataIndices.Count; i++)
        {
            thingFull.GridBlock.gridCells[dataIndices[i].indexI, dataIndices[i].indexJ].SetOccupied(thingFull.thingCells[i]);

            thingFull.GridBlock.gridCells[dataIndices[i].indexI, dataIndices[i].indexJ].SetNonDelete();

            Center += thingFull.GridBlock
            .gridCells[dataIndices[i].indexI, dataIndices[i].indexJ]
            .Coordinates;
        }

        thingFull.SetFix();

        thingFull.transform.SetParent(thingFull.GridBlock.transform);

        thingFull.transform.localPosition = Center / dataIndices.Count;

        thingFull.transform.localScale = Vector3.one;
    }

    protected virtual bool IsPossibleClose()
    {
        for (int i = 0; i < gridBlocks[0].Width; i++)
        {
            for (int j = 0; j < gridBlocks[0].Height; j++)
            {
                if ((IsOne(gridBlocks[0].gridCells[i, j], gridBlocks[1].gridCells[(i + 1) % 2, j])
                    && gridBlocks[0].gridCells[i, j].IsOccupied == true))
                {
                    return false;
                }
            }
        }
        return true;

    }

    protected bool IsOne(GridCell gridCell1, GridCell gridCell2)
    {
        return gridCell1.IsOccupied == gridCell2.IsOccupied;
    }

    protected void Clear()
    {
        PackModal.Instance.Close();
        MainModal.Instance.Show();

        EventManager.OnShowButtonGo(false);

        EventManager.OnUpdateMoney(GeneratorCase.Instance.ConfigureCase.Price);
        EventManager.OnSkip();

        CaseManager.Instance.DestroyCurrentCase();
    }

}
public class DataIndex
{
    public int indexI;
    public int indexJ;

    public DataIndex(int indexI, int indexJ)
    {
        this.indexI = indexI;
        this.indexJ = indexJ;
    }
}

public class TestlayerData
{
    public string Name;
    public System.Action currentAlg;

    public TestlayerData(string name, System.Action currentAlg)
    {
        Name = name;
        this.currentAlg = currentAlg;
    }
}
