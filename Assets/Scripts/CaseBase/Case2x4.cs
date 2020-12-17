using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x4 : CaseBase, IStrategy
{
    public ItemDataStrategy Generate(int items)
    {
        ItemDataStrategy itemDataStrategy = new ItemDataStrategy();

        itemDataStrategy.list = new List<bool[]>();

        bool[] tmp1 = new bool[8];
        bool[] tmp2 = new bool[8];

        if (!CheckCountInItemCase(items, tmp1))
        {
            items = 1;
        }

        if (items == 1)
        {
            OneItemInCase(16, tmp1, tmp2);
        }

        if (items == 2)
        {
            TwoItemInCase(8, 8, tmp1, tmp2);
        }

        if (items == 3)
        {
            string str = GenerationStr("123", 4, "00000");
            FillArray(str, tmp1, tmp2);
        }

        if (items == 4)
        {
            string str = GenerationStr("1234", 5, "0000");
            FillArray(str, tmp1, tmp2);
        }

        if (items == 5)
        {
            string str = GenerationStr("12345", 6, "000");
            FillArray(str, tmp1, tmp2);
        }

        if (items == 6)
        {
            string str = GenerationStr("123456", 7, "00");
            FillArray(str, tmp1, tmp2);
        }

        if (items == 7)
        {
            string str = GenerationStr("1234567", 8, "0");
            FillArray(str, tmp1, tmp2);
        }

        itemDataStrategy.list.Add(tmp1);
        itemDataStrategy.list.Add(tmp2);

        return itemDataStrategy;
    }
}
