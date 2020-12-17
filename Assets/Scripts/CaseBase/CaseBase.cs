using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseBase
{
    protected virtual void OneItemInCase(int maxIndex, bool[] tmp1, bool[] tmp2)
    {
        int rnd = Random.Range(0, maxIndex);

        if (rnd > tmp1.Length - 1)
        {
            tmp2[rnd % tmp2.Length] = true;
        }
        else
        {
            tmp1[rnd] = true;
        }
    }

    protected virtual void TwoItemInCase(int maxIndex1, int maxIndex2, bool[] tmp1, bool[] tmp2)
    {
        int index1 = Random.Range(0, maxIndex1);
        int index2 = Random.Range(0, maxIndex2);
        if (index1 == index2)
        {
            if (index1 + 1 > tmp1.Length - 1)
            {
                index1 -= 1;
            }
            else
            {
                index1 += 1;
            }

            tmp1[index1] = true;
            tmp2[index2] = true;
        }
        else
        {
            tmp1[index1] = true;
            tmp2[index2] = true;
        }
    }

    protected virtual void FillArray(string str, bool[] tmp1, bool[] tmp2)
    {
        for (int i = 0; i < tmp2.Length; i++)
        {
            if (i % 2 == 0)
            {
                if (str[i] != '0')
                {
                    tmp1[i] = true;
                }
                else
                {
                    tmp1[i] = false;
                }
            }
            else
            {
                if (str[i] != '0')
                {
                    tmp2[i] = true;
                }
                else
                {
                    tmp2[i] = false;
                }
            }
        }
    }

    protected virtual string GenerationStr(string strDefault, int indexMax, string strInsert)
    {
        int index = Random.Range(0, indexMax);
        return strDefault.Insert(index, strInsert);
    }

    protected virtual bool CheckCountInItemCase(int count, bool[] tmp1)
    {
        return (count < tmp1.Length) ? true : false;
    }
}
