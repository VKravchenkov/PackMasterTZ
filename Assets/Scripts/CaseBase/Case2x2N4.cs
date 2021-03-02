using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x2N4 : CaseBaseN
{
    protected override void Start()
    {
        base.Start();
    }


    protected override void OneThingsInCases1x2(int indexGridBlock, string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 4);
            J = i % 4;

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
            str = GenerateStr("0000000", 8, "1");

        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 4);
            J = i % 4;

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

    protected override void OneThingsInCases2x1(int indexGridBlock, string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            I = Mathf.CeilToInt(i / 4);
            J = i % 4;

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
        testlayerDatas.Add(new TestlayerData("Layer_6", Conf_7));
        testlayerDatas.Add(new TestlayerData("Layer_6", Conf_8));
        testlayerDatas.Add(new TestlayerData("Layer_6", Conf_9));
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
        // 1 Cell Occupied
        OneThingsInCases1x1();

        rnd = Random.Range(0, 2);

        if (rnd == 0)
        {
            AppendByTypeInListPack(TypeThing.Type1x1);
            AppendByTypeInListPack(TypeThing.Type1x2, 3);
        }
        if (rnd == 1)
        {
            AppendByTypeInListPack(TypeThing.Type1x1);
            AppendByTypeInListPack(TypeThing.Type2x1, 3);
        }
    }

    private void Conf_2()
    {
        // 2 Cell Occupied
        rnd = Random.Range(0, 4);

        if (rnd == 0)
        {
            OneThingsInCases2x1(0, "11000000");
        }
        if (rnd == 1)
        {
            OneThingsInCases2x1(0, "00001100");
        }
        if (rnd == 2)
        {
            OneThingsInCases2x1(0, "00110000");
        }
        if (rnd == 3)
        {
            OneThingsInCases2x1(0, "00000011");
        }

        AppendByTypeInListPack(TypeThing.Type1x2, 2);
        AppendByTypeInListPack(TypeThing.Type1x1);

    }

    private void Conf_3()
    {
        rnd = Random.Range(0, 4);
        if (rnd == 0)
        {
            OneThingsInCases1x1(0, "10010000");
        }
        if (rnd == 1)
        {
            OneThingsInCases1x1(0, "00001001");
        }
        if (rnd == 2)
        {
            OneThingsInCases1x1(0, "10000001");
        }
        if (rnd == 3)
        {
            OneThingsInCases1x1(0, "00011000");
        }

        AppendByTypeInListPack(TypeThing.Type1x2, 2);
        AppendByTypeInListPack(TypeThing.Type1x1, 2);
    }

    private void Conf_4()
    {
        rnd = Random.Range(0, 4);
        if (rnd == 0)
        {
            OneThingsInCases1x1(0, "10100000");
        }
        if (rnd == 1)
        {
            OneThingsInCases1x1(0, "00001010");
        }
        if (rnd == 2)
        {
            OneThingsInCases1x1(0, "01010000");
        }
        if (rnd == 3)
        {
            OneThingsInCases1x1(0, "00000101");
        }

        AppendByTypeInListPack(TypeThing.Type1x2, 2);
        AppendByTypeInListPack(TypeThing.Type1x1, 2);
    }

    private void Conf_5()
    {
        rnd = Random.Range(0, 6);
        if (rnd == 0)
        {
            OneThingsInCases1x1(0, "10000100");
        }
        if (rnd == 1)
        {
            OneThingsInCases1x1(0, "01000010");
        }
        if (rnd == 2)
        {
            OneThingsInCases1x1(0, "00100001");
        }
        if (rnd == 3)
        {
            OneThingsInCases1x1(0, "01001000");
        }
        if (rnd == 4)
        {
            OneThingsInCases1x1(0, "00100100");
        }
        if (rnd == 5)
        {
            OneThingsInCases1x1(0, "00010010");
        }

        AppendByTypeInListPack(TypeThing.Type1x2);
        AppendByTypeInListPack(TypeThing.Type2x1);
        AppendByTypeInListPack(TypeThing.Type1x1, 2);

    }

    private void Conf_6()
    {
        rnd = Random.Range(0, 4);
        if (rnd == 0)
        {
            OneThingsInCases1x1(0, "10010100");
        }
        if (rnd == 1)
        {
            OneThingsInCases1x1(0, "10000101");
        }
        if (rnd == 2)
        {
            OneThingsInCases1x1(0, "10010010");
        }
        if (rnd == 3)
        {
            OneThingsInCases1x1(0, "00011010");
        }

        AppendByTypeInListPack(TypeThing.Type1x2);
        AppendByTypeInListPack(TypeThing.Type1x1, 2);
    }

    private void Conf_7()
    {
        AppendByTypeInListPack(TypeThing.Type2x2, 2);
    }

    private void Conf_8()
    {
        OneThingsInCases2x1(0, "11000000");
        AppendByTypeInListPack(TypeThing.Type2x2);
        AppendByTypeInListPack(TypeThing.Type1x1);
    }

    private void Conf_9()
    {
        OneThingsInCases1x1(0, "10000001", false);
        AppendByTypeInListPack(TypeThing.Type3x1);
    }
}
