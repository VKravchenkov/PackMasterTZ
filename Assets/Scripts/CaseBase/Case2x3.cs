using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x3 : CaseBase, IStrategy
{
    public ItemDataStrategy Generate(int items)
    {
        ItemDataStrategy itemDataStrategy = new ItemDataStrategy();

        itemDataStrategy.list = new List<bool[]>();

        bool[] tmp1 = new bool[6];
        bool[] tmp2 = new bool[6];

        if (!CheckCountInItemCase(items, tmp1))
        {
            items = 1;
        }

        if (items == 1)
        {
            OneItemInCase(12, tmp1, tmp2);
        }

        if (items == 2)
        {
            TwoItemInCase(6, 6, tmp1, tmp2);
        }

        if (items == 3)
        {
            string str = GenerationStr("123", 4, "000");
            FillArray(str, tmp1, tmp2);
        }

        if (items == 4)
        {
            string str = GenerationStr("1234", 5, "00");
            FillArray(str, tmp1, tmp2);
        }

        if (items == 5)
        {
            string str = GenerationStr("12345", 6, "0");
            FillArray(str, tmp1, tmp2);
        }

        itemDataStrategy.list.Add(tmp1);
        itemDataStrategy.list.Add(tmp2);

        return itemDataStrategy;
    }
}
