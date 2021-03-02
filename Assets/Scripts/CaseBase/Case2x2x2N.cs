using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x2x2N : CaseBaseN
{
    [SerializeField] private RectTransform BackgroundLeftDown;
    [SerializeField] private bool isBlocker = false;

    private bool isChangePivot = false;

    private Vector3 startLocalPosition;
    private Vector2 startPivot;
    private Quaternion startQuaternion;

    protected override void Start()
    {
        base.Start();

        startLocalPosition = BackgroundLeftDown.localPosition;
        startPivot = BackgroundLeftDown.pivot;
        startQuaternion = BackgroundLeftDown.localRotation;
    }

    protected override void OneThingsInCases1x2(int indexGridBlock = 0, string str = default)
    {
        rnd = Random.Range(0, 2);

        if (str == default)
            str = (rnd % 2 == 0) ? "1010" : "0101";

        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 2);
            J = i % 2;

            if (str[i] == '1')
            {
                //print($"str[{i}] -> [{I},{J}] ->GridBlock{indexGridBlock}");
                dataCellTmp.Add(new DataIndex(I, J));
            }
        }

        ThingFull thingFull = Instantiate(poolThings.DictionaryThings[TypeThing.Type1x2]);
        AddedThingsFixed1x2Or2x1(dataCellTmp, thingFull, indexGridBlock);
        dataCellTmp.Clear();
    }

    protected override void OneThingsInCases1x1(int indexGridBlock = 0, string str = default, bool isRandom = true)
    {
        if (str == default)
            str = GenerateStr("000", 4, "1");

        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 2);
            J = i % 2;

            if (isRandom)
            {
                indexGridBlock = i % gridBlocks.Count;

                if (i % 2 == 0)
                {
                    if (str[i] == '1')
                    {
                        //print($"str[{i}] -> [{I},{J}] ->GridBlock{indexGridBlock}");

                        ThingFull thingFull = Instantiate(poolThings.DictionaryThings[TypeThing.Type1x1]);
                        AddedThingsFixed1x1(I, J, thingFull, indexGridBlock);
                    }
                }
                else
                {
                    if (str[i] == '1')
                    {
                        //print($"str[{i}] -> [{I},{J}] ->GridBlock{indexGridBlock}");

                        ThingFull thingFull = Instantiate(poolThings.DictionaryThings[TypeThing.Type1x1]);
                        AddedThingsFixed1x1((I + 1) % 2, J, thingFull, indexGridBlock);
                    }
                }
            }
            else
            {
                if (str[i] == '1')
                {
                    //print($"str[{i}] -> [{I},{J}] ->GridBlock{indexGridBlock}");

                    ThingFull thingFull = Instantiate(poolThings.DictionaryThings[TypeThing.Type1x1]);
                    AddedThingsFixed1x1(I, J, thingFull, indexGridBlock);
                }
            }

        }
    }

    protected override void OneThingsInCases2x1(int indexGridBlock = 0, string str = default)
    {
        rnd = Random.Range(0, 2);

        if (str == default)
            str = (rnd % 2 == 0) ? "0011" : "1100";

        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 2);
            J = i % 2;

            if (str[i] == '1')
            {
                //print($"str[{i}] -> [{I},{J}] ->GridBlock{indexGridBlock}");
                dataCellTmp.Add(new DataIndex(I, J));
            }
        }

        ThingFull thingFull = Instantiate(poolThings.DictionaryThings[TypeThing.Type2x1]);
        AddedThingsFixed1x2Or2x1(dataCellTmp, thingFull, indexGridBlock);
        dataCellTmp.Clear();
    }

    public override void InitConfigure()
    {
        testlayerDatas.Add(new TestlayerData("Layer_1", Conf_1));
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    protected override void Rotation()
    {
        if (IsRotate)
        {
            if (!isBlocker)
            {
                isBlocker = !IsRotateLeftDown();
            }
            else
            {
                if (IsChangeRotateLeftDown())
                {
                    IsRotateLeft();
                }
                else
                {
                    if (IsPossibleClose())
                    {
                        IsRotate = false;

                        Clear();
                    }
                    else
                    {
                        OnReset();
                    }
                }
            }
        }
    }

    private bool IsRotateLeft()
    {
        if (gridBlocks[0].RectTransformPar != null
            && gridBlocks[0].RectTransformPar.rotation != Quaternion.AngleAxis(180, Vector3.up))
        {
            BackgroundLeft.rotation *= Quaternion.AngleAxis(10f, Vector3.up);

            gridBlocks[0].RectTransformPar.rotation *= Quaternion.AngleAxis(10f, Vector3.up);

            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsRotateLeftDown()
    {
        if (BackgroundLeftDown.rotation != Quaternion.AngleAxis(180, Vector3.right))
        {
            BackgroundLeftDown.rotation *= Quaternion.AngleAxis(10f, Vector3.right);
            gridBlocks[2].RectTransformPar.rotation *= Quaternion.AngleAxis(10f, Vector3.right);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsChangeRotateLeftDown()
    {
        if (BackgroundLeftDown.rotation != Quaternion.Euler(-180f, 180f, 0f))
        {
            if (!isChangePivot)
            {
                BackgroundLeftDown.pivot = new Vector2(1f, 0.5f);
                BackgroundLeftDown.localPosition = new Vector3(0f, 210f);

                gridBlocks[2].RectTransformPar.pivot = new Vector2(1f, 0.5f);
                gridBlocks[2].RectTransformPar.localPosition = new Vector2(0f, 210f);

                isChangePivot = true;
            }

            BackgroundLeftDown.rotation *= Quaternion.AngleAxis(10f, Vector3.up);
            gridBlocks[2].RectTransformPar.rotation *= Quaternion.AngleAxis(10f, Vector3.up);
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override bool IsPossibleClose()
    {
        for (int i = 0; i < gridBlocks[0].Width; i++)
        {
            for (int j = 0; j < gridBlocks[0].Height; j++)
            {
                if ((IsOne(gridBlocks[0].gridCells[i, j], gridBlocks[1].gridCells[(i + 1) % 2, j]) && gridBlocks[0].gridCells[i, j].IsOccupied == true)
                    || (IsOne(gridBlocks[0].gridCells[i, j], gridBlocks[2].gridCells[i, (j + 1) % 2]) && gridBlocks[0].gridCells[i, j].IsOccupied == true)
                    || (IsOne(gridBlocks[1].gridCells[i, j], gridBlocks[2].gridCells[(i + 1) % 2, (j + 1) % 2]) && gridBlocks[1].gridCells[i, j].IsOccupied == true))
                {
                    return false;
                }
            }
        }

        return true;
    }

    protected override void OnReset()
    {
        IsRotate = false;

        gridBlocks[0].RectTransformPar.rotation = Quaternion.AngleAxis(0f, Vector3.up);
        BackgroundLeft.rotation = Quaternion.AngleAxis(0f, Vector3.up);

        gridBlocks[2].RectTransformPar.rotation = startQuaternion;
        gridBlocks[2].RectTransformPar.pivot = startPivot;
        gridBlocks[2].RectTransformPar.localPosition = startLocalPosition;

        BackgroundLeftDown.rotation = startQuaternion;
        BackgroundLeftDown.pivot = startPivot;
        BackgroundLeftDown.localPosition = startLocalPosition;

        isChangePivot = false;
        isBlocker = false;

        for (int i = 0; i < gridBlocks[0].ListAddedThings.Count; i++)
        {
            if (gridBlocks[0].ListAddedThings.Count != 0)
                gridBlocks[0].ListAddedThings[i].GetComponent<ThingDragNDrop>().OnReset();
        }

        for (int j = 0; j < gridBlocks[1].ListAddedThings.Count; j++)
        {
            if (gridBlocks[1].ListAddedThings.Count != 0)
                gridBlocks[1].ListAddedThings[j].GetComponent<ThingDragNDrop>().OnReset();
        }

        for (int i = 0; i < gridBlocks[2].ListAddedThings.Count; i++)
        {
            if (gridBlocks[2].ListAddedThings.Count != 0)
                gridBlocks[2].ListAddedThings[i].GetComponent<ThingDragNDrop>().OnReset();
        }

        gridBlocks[0].ListAddedThings.Clear();
        gridBlocks[1].ListAddedThings.Clear();
        gridBlocks[2].ListAddedThings.Clear();

        gridBlocks[0].Clear();
        gridBlocks[1].Clear();
        gridBlocks[2].Clear();
    }

    private void Conf_1()
    {
        OneThingsInCases1x1();

        AppendByTypeInListPack(TypeThing.Type1x2);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

}
