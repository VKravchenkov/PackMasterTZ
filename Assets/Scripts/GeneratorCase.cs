using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneratorCase : MonoBehaviour
{
    //??
    public static GeneratorCase Instance { get; private set; }

    [SerializeField] private List<ConfigureCase> listConfigureCases;

    [SerializeField] private ConfigureCase currentSelectConfigureCase;

    public List<ConfigureCase> ListConfigureCases => listConfigureCases;

    public ConfigureCase ConfigureCase => currentSelectConfigureCase;

    private void Awake()
    {
        Instance = this;
    
        LoadConfigureCases();

        ChangeCurrentConfigureCase();

        EventManager.Skip += ChangeCurrentConfigureCase;
    }

    private void Start()
    {
        EventManager.OnUpdateMoneyTourist(currentSelectConfigureCase);
    }

    private void OnDestroy()
    {
        EventManager.Skip -= ChangeCurrentConfigureCase;
    }

    public void CreateCase()
    {
        EventManager.OnCreateCase(currentSelectConfigureCase);
    }

    public void ChangeCurrentConfigureCase()
    {
        currentSelectConfigureCase = GetRandomConfigure();
        EventManager.OnUpdateMoneyTourist(currentSelectConfigureCase);
    }

    private ConfigureCase GetRandomConfigure()
    {
        int index = Random.Range(0, listConfigureCases.Count);

        return listConfigureCases[index];
    }

    private void LoadConfigureCases()
    {
        listConfigureCases = Resources.LoadAll<ConfigureCase>("ScriptableObjects").ToList();
    }
}
