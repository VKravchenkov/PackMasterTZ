using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CaseController : MonoBehaviour
{
    [SerializeField] private ItemCase prefabCase;
    [SerializeField] private ItemCheck prefabCheck;

    [SerializeField] private List<ItemCase> listItemCasesLeft;
    [SerializeField] private List<ItemCase> listItemCasesRight;

    [SerializeField] private bool[] arrayLeft;
    [SerializeField] private bool[] arrayRight;

    [SerializeField] private RectTransform rectTransformLeftBlock;
    [SerializeField] private RectTransform rectTransformRightBlock;
    [SerializeField] private RectTransform rectCheck;

    private bool isRotate = false;

    private List<ItemCheck> itemChecks;
    private ConfigureCase currentConfigure;

    public bool[] ArrayLeft => arrayLeft;
    public bool[] ArrayRight => arrayRight;
    public List<ItemCase> ListItemCasesLeft => listItemCasesLeft;
    public List<ItemCase> ListItemCasesRight => listItemCasesRight;

    private void Awake()
    {
        EventManager.CreateCase += GenerateCase;
        EventManager.CheckSuitCaseFill += () => isRotate = true;
    }

    private void OnDestroy()
    {
        EventManager.CreateCase -= GenerateCase;
        EventManager.CheckSuitCaseFill -= () => isRotate = true;
    }

    private void FixedUpdate()
    {
        if (isRotate)
        {
            if (rectTransformLeftBlock.rotation != Quaternion.AngleAxis(180, Vector3.up))
            {
                rectTransformLeftBlock.rotation *= Quaternion.AngleAxis(10f, Vector3.up);
            }
            else
            {
                isRotate = false;

                CheckCase();
            }
        }
    }

    private void InitArray()
    {
        foreach (var item in listItemCasesLeft)
        {
            arrayLeft[item.Index] = item.Busy;
        }

        foreach (var item in listItemCasesRight)
        {
            arrayRight[item.Index] = item.Busy;
        }
    }

    private void InitBlock(List<ItemCase> itemCases, RectTransform rectTransform)
    {
        for (int i = 0; i < arrayLeft.Length; i++)
        {
            ItemCase itemCase = Instantiate(prefabCase, rectTransform);
            itemCase.name = $"ItemCase -> {i}";
            itemCase.SetIndex(i);
            itemCases.Add(itemCase);
        }
    }

    private void InitBlockCheck()
    {
        itemChecks = new List<ItemCheck>();
        for (int i = 0; i < arrayLeft.Length; i++)
        {
            ItemCheck itemCheck = Instantiate(prefabCheck, rectCheck);
            itemCheck.SetIndex(i);
            itemChecks.Add(itemCheck);
        }
    }
    private void UpdateArrays()
    {
        for (int i = 0; i < arrayLeft.Length; i++)
        {
            arrayLeft[i] = listItemCasesLeft[i].Busy;
            arrayRight[i] = listItemCasesRight[i].Busy;
        }
    }

    public void CheckCase()
    {
        bool isCheck = true;

        InitArray();

        List<ItemCase> itemCasesL = listItemCasesLeft.FindAll(item => item.ModeItemCase == ModeItemCase.Added);
        List<ItemCase> itemCasesR = listItemCasesRight.FindAll(item => item.ModeItemCase == ModeItemCase.Added);
        itemCasesL = itemCasesL.Concat(itemCasesR).ToList();

        for (int i = 0; i < arrayLeft.Length; i++)
        {
            //print($"ArrayLift[{i}] = {ArrayLeft[i]} " + $"ArrayRight[{i}] = {ArrayRight[i]}");

            if (ArrayLeft[i] == false & ArrayRight[i] == false)
            {
                // return;
            }
            else if (ArrayLeft[i] == ArrayRight[i])
            {
                itemChecks.Find(item => item.Index == i).ShowCheck(false);
                isCheck = false;
                print("Failed");
                BackAll();
                return;
            }
        }

        itemCasesL.ForEach(item => itemChecks[item.Index].ShowCheck(true));

        if (isCheck)
        {
            StartCoroutine(Clear());
            EventManager.OnUpdateMoney(currentConfigure.Price);
            EventManager.OnSkip();
        }

    }

    public void GenerateCase(ConfigureCase configureCase)
    {
        currentConfigure = configureCase;
        ItemDataStrategy itemDataStrategy = new ItemDataStrategy();

        switch (configureCase.ModeSizeCase)
        {
            case ModeSizeCase.Mode2x2:
                Case2x2 case2X2 = new Case2x2();

                itemDataStrategy = case2X2.Generate(configureCase.CountItems);
                arrayLeft = itemDataStrategy.list[0];
                arrayRight = itemDataStrategy.list[1];

                break;
            case ModeSizeCase.Mode2x3:
                Case2x3 case2X3 = new Case2x3();

                itemDataStrategy = case2X3.Generate(configureCase.CountItems);
                arrayLeft = itemDataStrategy.list[0];
                arrayRight = itemDataStrategy.list[1];
                break;
            case ModeSizeCase.Mode2x4:
                Case2x4 case2X4 = new Case2x4();

                itemDataStrategy = case2X4.Generate(configureCase.CountItems);
                arrayLeft = itemDataStrategy.list[0];
                arrayRight = itemDataStrategy.list[1];
                break;
            case ModeSizeCase.Mode2x2x2:
                break;
            case ModeSizeCase.Mode2x2x2x2:
                break;
            default:
                break;


        }

        listItemCasesLeft = new List<ItemCase>();
        listItemCasesRight = new List<ItemCase>();

        InitBlock(listItemCasesLeft, rectTransformLeftBlock);
        InitBlock(listItemCasesRight, rectTransformRightBlock);

        InitBlockCheck();

        SetFillCase(itemDataStrategy.list[0], itemDataStrategy.list[1]);

        UpdateArrays();

        // TODO items error?
        EventManager.OnInitThings(itemDataStrategy.list[0].Length - configureCase.CountItems);
    }

    private void SetFillCase(bool[] arrayleft, bool[] arrayRight)
    {
        for (int i = 0; i < arrayleft.Length; i++)
        {
            if (arrayleft[i])
            {
                listItemCasesLeft[i].SetBusy(true);
                listItemCasesLeft[i].SetModeItemCase(ModeItemCase.Located);
                listItemCasesLeft[i].SetImage(IconManager.GetRandomSprite1x1());
            }
            if (arrayRight[i])
            {
                listItemCasesRight[i].SetBusy(true);
                listItemCasesRight[i].SetModeItemCase(ModeItemCase.Located);
                listItemCasesRight[i].SetImage(IconManager.GetRandomSprite1x1());
            }
        }
    }

    private void BackAll()
    {
        rectTransformLeftBlock.rotation = Quaternion.AngleAxis(0f, Vector3.up);

        itemChecks.ForEach(item => item.Hide());

        for (int i = 0; i < listItemCasesLeft.Count; i++)
        {
            if (listItemCasesLeft[i].ModeItemCase == ModeItemCase.Added)
            {
                listItemCasesLeft[i].Back();
            }

            if (listItemCasesRight[i].ModeItemCase == ModeItemCase.Added)
            {
                listItemCasesRight[i].Back();
            }
        }

        EventManager.OnBackToPoolThings();
        EventManager.OnShowButtonGo(false);
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(0.5f);

        PackModal.Instance.Close();
        MainModal.Instance.Show();

        rectTransformLeftBlock.rotation = Quaternion.AngleAxis(0f, Vector3.up);

        listItemCasesLeft.ForEach(item => Destroy(item.gameObject));
        listItemCasesRight.ForEach(item => Destroy(item.gameObject));

        itemChecks.ForEach(item => Destroy(item.gameObject));
        EventManager.OnShowButtonGo(false);
    }
}
