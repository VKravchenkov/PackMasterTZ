using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStrategy
{
    ItemDataStrategy Generate(int items);
}
