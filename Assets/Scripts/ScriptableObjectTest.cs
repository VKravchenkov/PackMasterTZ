using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Configure Layer", menuName = "Configure Layer", order = 51)]
public class ScriptableObjectTest : ScriptableObject
{
    private string nameSO;
    private float damage;
    private int id;

    public void Init(string name, float damage, int id)
    {
        this.nameSO = name;
        this.damage = damage;
        this.id = id;
    }

    public static ScriptableObjectTest CreateInstance(string name, float damage, int id)
    {
        ScriptableObjectTest scriptableObjectTest = CreateInstance<ScriptableObjectTest>();
        scriptableObjectTest.Init(name, damage, id);
        return scriptableObjectTest;
    }
}
