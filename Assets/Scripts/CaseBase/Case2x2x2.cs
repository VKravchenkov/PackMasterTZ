using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x2x2 : CaseBase, IStrategy
{
    public ItemDataStrategy Generate(int items)
    {
        ItemDataStrategy itemDataStrategy = new ItemDataStrategy();

        itemDataStrategy.list = new List<bool[]>();

        bool[] tmp1 = new bool[4];
        bool[] tmp2 = new bool[4];
        bool[] addedTmp3 = new bool[4];

        if (!CheckCountInItemCase(items, tmp1))
        {
            items = 1;
        }

        if (items == 1)
        {
            // 1 вариант
            // все addedTmp3 false
            OneItemInCase(16, tmp1, tmp2);

            // 2 вариант
            // все tmp1 false
            // OneItemInCase(16, addedTmp3, tmp2);

            // 3 вариант
            // все tmp2 false
            // OneItemInCase(16, tmp1, addedTmp3);
        }

        if (items == 2)
        {
            //1 вариант
            // все addedTmp3 false
            TwoItemInCase(8, 8, tmp1, tmp2);

            //2 вариант
            // все tmp1 false
            TwoItemInCase(8, 8, addedTmp3, tmp2);

            //3 вариант
            // все tmp2 false
            TwoItemInCase(8, 8, tmp1, addedTmp3);

            // 4-6 варианты смешивания не буду реализовывать

        }

        if (items == 3)
        {
            // 1 вариант
            // все addedTmp3 false
            string str = GenerationStr("123", 4, "00000");
            FillArray(str, tmp1, tmp2);

            // 2 вариант
            // все tmp1 false
            str = GenerationStr("123", 4, "00000");
            FillArray(str, addedTmp3, tmp2);


            // 3 вариант
            // все tmp2 false
            str = GenerationStr("123", 4, "00000");
            FillArray(str, tmp1, addedTmp3);

            // 4- .. варианты не будут реализованы
            
        }

        itemDataStrategy.list.Add(tmp1);
        itemDataStrategy.list.Add(tmp2);
        itemDataStrategy.list.Add(addedTmp3);


        return itemDataStrategy;
    }

    //use default GenerateStr
    //private void GenerateStr()
    //{

    //}
}
