using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x2N3 : CaseBaseN
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void OneThingsInCases1x2(int indexGridBlock = 0, string str = default)
    {
        rnd = Random.Range(0, 2);

        if (str == default)
            str = (rnd % 2 == 0) ? "100100" : "001001";

        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 3);
            J = i % 3;

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
            str = GenerateStr("00000", 6, "1");

        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 3);
            J = i % 3;

            if (isRandom)
            {
                indexGridBlock = i % gridBlocks.Count;

                if (i % gridBlocks.Count == 0)
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
                        //print($"str[{i}] -> [{(I + 1) % 2},{J}] ->GridBlock{indexGridBlock}");

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
        rnd = Random.Range(0, 4);

        if (str == default)
        {
            if (rnd == 0)
            {
                str = "110000";
            }
            if (rnd == 1)
            {
                str = "000110";
            }
            if (rnd == 2)
            {
                str = "011000";
            }
            if (rnd == 3)
            {
                str = "000011";
            }
        }

        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 3);
            J = i % 3;

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
        testlayerDatas.Add(new TestlayerData("Layer_8", Conf_9));
        testlayerDatas.Add(new TestlayerData("Layer_8", Conf_10));
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
        AppendByTypeInListPack(TypeThing.Type2x1);
    }

    private void Conf_2()
    {
        OneThingsInCases1x2();

        rnd = Random.Range(0, 4);

        if (rnd == 0)
        {
            AppendByTypeInListPack(TypeThing.Type1x2, 2);

        }

        if (rnd == 1)
        {
            AppendByTypeInListPack(TypeThing.Type2x1, 2);
        }

        if (rnd == 2)
        {
            AppendByTypeInListPack(TypeThing.Type1x2);
            AppendByTypeInListPack(TypeThing.Type1x1);
        }
        if (rnd == 3)
        {
            AppendByTypeInListPack(TypeThing.Type2x1);
            AppendByTypeInListPack(TypeThing.Type1x1);
        }

    }

    private void Conf_3()
    {
        OneThingsInCases1x2(0, "010010");
        rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            AppendByTypeInListPack(TypeThing.Type1x2, 2);
        }

        if (rnd == 1)
        {
            AppendByTypeInListPack(TypeThing.Type1x2);
            AppendByTypeInListPack(TypeThing.Type1x1);
        }
    }

    private void Conf_4()
    {
        OneThingsInCases2x1();
        rnd = Random.Range(0, 3);
        if (rnd == 0)
        {
            AppendByTypeInListPack(TypeThing.Type1x2);
            AppendByTypeInListPack(TypeThing.Type2x1);
        }
        if (rnd == 1)
        {
            AppendByTypeInListPack(TypeThing.Type1x2);
            AppendByTypeInListPack(TypeThing.Type1x1, 2);
        }
        if (rnd == 2)
        {
            AppendByTypeInListPack(TypeThing.Type2x1);
            AppendByTypeInListPack(TypeThing.Type1x1, 2);
        }
    }

    private void Conf_5()
    {
        // 3 Cell occupied

        OneThingsInCases1x2(0, "100100");

        rnd = Random.Range(0, 4);

        string str = string.Empty;

        if (rnd == 0)
        {
            str = "010000";
        }
        if (rnd == 1)
        {
            str = "001000";
        }

        if (rnd == 2)
        {
            str = "000010";
        }
        if (rnd == 3)
        {
            str = "000001";
        }

        OneThingsInCases1x1(0, str);

        AppendByTypeInListPack(TypeThing.Type2x1);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_6()
    {
        // 3 Cell occupied

        OneThingsInCases1x2(0, "001001");

        rnd = Random.Range(0, 4);

        string str = string.Empty;

        if (rnd == 0)
        {
            str = "100000";
        }
        if (rnd == 1)
        {
            str = "010000";
        }

        if (rnd == 2)
        {
            str = "000100";
        }
        if (rnd == 3)
        {
            str = "000010";
        }

        OneThingsInCases1x1(0, str);

        AppendByTypeInListPack(TypeThing.Type1x2);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_7()
    {
        // 3 Cell occupied
        OneThingsInCases1x2(0, "010010");

        rnd = Random.Range(0, 4);

        string str = string.Empty;

        if (rnd == 0)
        {
            str = "100000";
        }
        if (rnd == 1)
        {
            str = "001000";
        }

        if (rnd == 2)
        {
            str = "000100";
        }
        if (rnd == 3)
        {
            str = "000001";
        }

        OneThingsInCases1x1(0, str);

        AppendByTypeInListPack(TypeThing.Type1x2);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_8()
    {
        // 3 Cell occupied

        rnd = Random.Range(0, 2);

        if (rnd == 0)
        {
            OneThingsInCases2x1(0, "110000");
            OneThingsInCases1x1(0, "001000");
        }
        if (rnd == 1)
        {
            OneThingsInCases2x1(0, "000110");
            OneThingsInCases1x1(0, "001000");
        }

        AppendByTypeInListPack(TypeThing.Type2x1);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_9()
    {
        OneThingsInCases1x1();

        AppendByTypeInListPack(TypeThing.Type2x2);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_10()
    {
        OneThingsInCases1x1();

        AppendByTypeInListPack(TypeThing.Type3x1);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }
}
