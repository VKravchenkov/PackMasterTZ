using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Configure Case", menuName = "Configure Case", order = 51)]
public class ConfigureCase : ScriptableObject
{
    [SerializeField] private Sprite iconPiople;
    [SerializeField] private Sprite iconLand;
    [SerializeField] private ModeSizeCase modeSizeCase;
    [SerializeField] private int price;
    [SerializeField] private int countItems;

    public Sprite IconPiople => iconPiople;
    public Sprite IconLand => iconLand;

    public ModeSizeCase ModeSizeCase => modeSizeCase;
    public int Price => price;
    public int CountItems => countItems;
}
