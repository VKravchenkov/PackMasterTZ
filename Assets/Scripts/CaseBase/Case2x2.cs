using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Case2x2 : CaseBase, IStrategy
{
    public ItemDataStrategy Generate(int items)
    {
        ItemDataStrategy itemDataStrategy = new ItemDataStrategy();

        itemDataStrategy.list = new List<bool[]>();

        bool[] tmp1 = new bool[4];
        bool[] tmp2 = new bool[4];

        if (!CheckCountInItemCase(items, tmp1))
        {
            items = 1;
        }

        if (items == 1)
        {
            OneItemInCase(8, tmp1, tmp2);
        }

        if (items == 2)
        {
            TwoItemInCase(4, 4, tmp1, tmp2);
        }

        if (items == 3)
        {
            string str = GenerationStr("123", 4, "0");

            FillArray(str, tmp1, tmp2);
        }

        itemDataStrategy.list.Add(tmp1);
        itemDataStrategy.list.Add(tmp2);

        return itemDataStrategy;
    }
}
