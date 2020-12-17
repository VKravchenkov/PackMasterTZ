using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConfigureLayer : EditorWindow
{
    private string nameConf;
    private float damage;
    private int id;


    [MenuItem("Window/Configure Layer")]
    public static void ShowWindow()
    {
        GetWindow<ConfigureLayer>("Configure Layer");
    }
    private void OnGUI()
    {
        GUILayout.Label("Name");
        nameConf = EditorGUILayout.TextField("Name :", nameConf);
        damage = EditorGUILayout.FloatField(damage);
        id = EditorGUILayout.IntField(id);

        if (GUILayout.Button("Create Configure Layer"))
        {
            ScriptableObjectTest scriptableObjectTest = ScriptableObjectTest.CreateInstance(nameConf, damage, id);
        }
    }
}


