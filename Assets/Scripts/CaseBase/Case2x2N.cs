using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x2N : CaseBaseN
{

    protected override void Start()
    {
        base.Start();
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
        testlayerDatas.Add(new TestlayerData("Layer_2", Conf_2));
        testlayerDatas.Add(new TestlayerData("Layer_3", Conf_3));
        testlayerDatas.Add(new TestlayerData("Layer_4", Conf_4));
        testlayerDatas.Add(new TestlayerData("Layer_5", Conf_5));
        testlayerDatas.Add(new TestlayerData("Layer_6", Conf_6));
        testlayerDatas.Add(new TestlayerData("Layer_7", Conf_7));
        testlayerDatas.Add(new TestlayerData("Layer_8", Conf_8));
        testlayerDatas.Add(new TestlayerData("Layer_9", Conf_9));
        testlayerDatas.Add(new TestlayerData("Layer_10", Conf_10));
        testlayerDatas.Add(new TestlayerData("Layer_11", Conf_11));
        testlayerDatas.Add(new TestlayerData("Layer_12", Conf_12));
    }


    private void FixedUpdate()
    {
        Rotation();
    }

    protected override void Rotation()
    {
        if (IsRotate && gridBlocks[0].RectTransformPar != null
            && gridBlocks[0].RectTransformPar.rotation != Quaternion.AngleAxis(180, Vector3.up))
        {
            gridBlocks[0].RectTransformPar.rotation *= Quaternion.AngleAxis(10f, Vector3.up);
        }
        else if (IsRotate && gridBlocks[0].RectTransformPar.rotation == Quaternion.AngleAxis(180, Vector3.up))
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

        if (IsRotate && BackgroundLeft != null && BackgroundLeft.rotation != Quaternion.AngleAxis(180, Vector3.up))
        {
            BackgroundLeft.rotation *= Quaternion.AngleAxis(10f, Vector3.up);
        }

    }

    protected override void OnReset()
    {
        IsRotate = false;

        gridBlocks[0].RectTransformPar.rotation = Quaternion.AngleAxis(0f, Vector3.up);
        BackgroundLeft.rotation = Quaternion.AngleAxis(0f, Vector3.up);

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

        gridBlocks[0].ListAddedThings.Clear();
        gridBlocks[1].ListAddedThings.Clear();

        gridBlocks[0].Clear();
        gridBlocks[1].Clear();
    }

    private void Conf_1()
    {
        OneThingsInCases1x1();

        AppendByTypeInListPack(TypeThing.Type1x1);

        AppendByTypeInListPack(TypeThing.Type1x2);
    }

    private void Conf_2()
    {
        OneThingsInCases1x1();

        AppendByTypeInListPack(TypeThing.Type1x1);

        AppendByTypeInListPack(TypeThing.Type2x1);
    }

    private void Conf_3()
    {
        OneThingsInCases1x1(0, "0001");
        AppendByTypeInListPack(TypeThing.Type1x2L);
    }

    private void Conf_4()
    {
        OneThingsInCases1x1(0, "1100");
        AppendByTypeInListPack(TypeThing.Type2x1);
    }

    private void Conf_5()
    {
        OneThingsInCases2x1();
        AppendByTypeInListPack(TypeThing.Type2x1);
    }

    private void Conf_6()
    {
        OneThingsInCases1x2();
        AppendByTypeInListPack(TypeThing.Type1x2);
    }

    private void Conf_7()
    {
        OneThingsInCases1x1(0, "1001");
        AppendByTypeInListPack(TypeThing.Type1x1, 2);
    }

    private void Conf_8()
    {
        OneThingsInCases1x1(0, "0110");
        AppendByTypeInListPack(TypeThing.Type1x1, 2);
    }

    private void Conf_9()
    {
        OneThingsInCases1x2(0, "1010");
        OneThingsInCases1x1(0, "0100");
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_10()
    {
        OneThingsInCases1x2(0, "0101");
        OneThingsInCases1x1(0, "1000");
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_11()
    {
        OneThingsInCases2x1(0, "0011");
        OneThingsInCases1x1(0, "0100");
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_12()
    {
        OneThingsInCases2x1(0, "1100");
        OneThingsInCases1x1(0, "0001");
        AppendByTypeInListPack(TypeThing.Type1x1);
    }
}


