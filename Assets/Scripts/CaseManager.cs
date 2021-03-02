using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public static CaseManager Instance { get; private set; }

    [SerializeField] private List<CaseBaseN> caseBaseNs;
    [SerializeField] private PoolThings poolThings;

    private CaseBaseN caseBaseNCurrent;

    private void Awake()
    {
        Instance = this;

        EventManager.CreateCase += (confCase) => InitCases(confCase);
        EventManager.CheckSuitCaseFill += () => caseBaseNCurrent.SetRotate(true);
    }

    private void OnDestroy()
    {
        EventManager.CreateCase -= (confCase) => InitCases(confCase);
        EventManager.CheckSuitCaseFill -= () => caseBaseNCurrent.SetRotate(true);
    }

    private void InitCases(ConfigureCase configureCase)
    {
        CaseBaseN caseBaseN = caseBaseNs.Find(item => item.ModeSizeCase == configureCase.ModeSizeCase);

        caseBaseNCurrent = Instantiate(caseBaseN, transform.parent);

        caseBaseNCurrent.SetPool(poolThings);

        caseBaseNCurrent.InitConfigure();

        int rnd = Random.Range(0, caseBaseNCurrent.testlayerDatas.Count);

        caseBaseNCurrent.testlayerDatas[rnd].currentAlg();
    }

    public void DestroyCurrentCase()
    {
        Destroy(caseBaseNCurrent.gameObject);
    }
}
