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

    //TODO
    [SerializeField] private List<ItemCase> listItemCasesDown;

    [SerializeField] private bool[] arrayLeft;
    [SerializeField] private bool[] arrayRight;

    //TODO
    [SerializeField] private bool[] arrayDown;

    [SerializeField] private RectTransform rectTransformLeftBlock;
    [SerializeField] private RectTransform rectTransformRightBlock;

    //TODO
    [SerializeField] private RectTransform rectTransformDownBlock;

    [SerializeField] private RectTransform rectCheck;

    private bool isRotate = false;

    //TODO
    private bool isBlocker = false;

    private List<ItemCheck> itemChecks;
    private ConfigureCase currentConfigure;

    public bool[] ArrayLeft => arrayLeft;
    public bool[] ArrayRight => arrayRight;

    //TODO
    public bool[] ArrayDown => arrayDown;
    public List<ItemCase> ListItemCasesLeft => listItemCasesLeft;
    public List<ItemCase> ListItemCasesRight => listItemCasesRight;

    //TODo
    public List<ItemCase> ListItemCasesDown => listItemCasesDown;

    private void Awake()
    {
        //EventManager.CreateCase += GenerateCase;
        //EventManager.CheckSuitCaseFill += () => isRotate = true;
    }

    private void OnDestroy()
    {
        //EventManager.CreateCase -= GenerateCase;
        //EventManager.CheckSuitCaseFill -= () => isRotate = true;
    }

    private void FixedUpdate()
    {
        if (isRotate)
        {
            if (currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2W2H2
                || currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2W2H3
                || currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2W2H4)
            {
                if (rectTransformLeftBlock.rotation != Quaternion.AngleAxis(180, Vector3.up))
                {
                    rectTransformLeftBlock.rotation *= Quaternion.AngleAxis(10f, Vector3.up);
                }
                else
                {
                    isRotate = false;
                    InitArray();
                    CheckCase();
                }
            }

            if (currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2x2W2H2)
            {
                if (rectTransformDownBlock.rotation != Quaternion.AngleAxis(180, Vector3.right)
                    && !isBlocker)
                {
                    rectTransformDownBlock.rotation *= Quaternion.AngleAxis(10f, Vector3.right);
                }
                else
                {
                    if (!isBlocker)
                    {
                        rectTransformDownBlock.pivot = new Vector2(1f, 1f);
                        rectTransformDownBlock.localPosition = new Vector3(0f, -205f);
                    }
                    if (rectTransformDownBlock.rotation != Quaternion.Euler(-180f, -180f, 0f)
                        && rectTransformLeftBlock.rotation != Quaternion.AngleAxis(180, Vector3.up))
                    {
                        isBlocker = true;
                        rectTransformLeftBlock.rotation *= Quaternion.AngleAxis(10f, Vector3.up);
                        rectTransformDownBlock.rotation *= Quaternion.AngleAxis(-10f, Vector3.up);
                    }
                    else
                    {
                        isRotate = false;

                        //InitArray();

                        if (CheckLUD())
                        {
                            arrayLeft = UnionArraysLLD(arrayLeft, arrayDown);
                            CheckCase();
                        }
                        else
                        {
                            BackAll();
                        }


                        
                    }
                }
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

        //TODo
        if (currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2x2W2H2)
        {
            foreach (var item in listItemCasesDown)
            {
                arrayDown[item.Index] = item.Busy;
            }
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
            //TODo
            if (currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2x2W2H2)
                arrayDown[i] = listItemCasesDown[i].Busy;
        }
    }

    public void CheckCase()
    {
        bool isCheck = true;

        for (int i = 0; i < arrayLeft.Length; i++)
        {
            if (arrayLeft[i] == true && arrayRight[i] == true)
            {
                itemChecks.Find(item => item.Index == i).ShowCheck(false);
                isCheck = false;
                print("Failed");
                BackAll();
                return;
            }

        }

        arrayLeft = UnionArraysLR(arrayLeft, arrayRight);

        for (int i = 0; i < arrayLeft.Length; i++)
        {
            if (arrayLeft[i] == true)
            {
                itemChecks[i].ShowCheck(true);
            }
        }

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
            case ModeSizeCase.Mode2x2W2H2:
                Case2x2 case2X2 = new Case2x2();

                itemDataStrategy = case2X2.Generate(configureCase.CountItems);
                arrayLeft = itemDataStrategy.list[0];
                arrayRight = itemDataStrategy.list[1];

                break;
            case ModeSizeCase.Mode2x2W2H3:
                Case2x3 case2X3 = new Case2x3();

                itemDataStrategy = case2X3.Generate(configureCase.CountItems);
                arrayLeft = itemDataStrategy.list[0];
                arrayRight = itemDataStrategy.list[1];
                break;
            case ModeSizeCase.Mode2x2W2H4:
                Case2x4 case2X4 = new Case2x4();

                itemDataStrategy = case2X4.Generate(configureCase.CountItems);
                arrayLeft = itemDataStrategy.list[0];
                arrayRight = itemDataStrategy.list[1];
                break;
            case ModeSizeCase.Mode2x2x2W2H2:
                Case2x2x2 case2X2X2 = new Case2x2x2();

                itemDataStrategy = case2X2X2.Generate(configureCase.CountItems);
                arrayLeft = itemDataStrategy.list[0];
                arrayRight = itemDataStrategy.list[1];
                arrayDown = itemDataStrategy.list[2];

                //TODO

                //сдвиг по позиции 

                //x = -205
                //y = -205
                //Pivot x = 0.5f y = 1

                rectTransformDownBlock.localPosition = new Vector3(-205f, -205f);

                break;
            
            default:
                break;


        }

        listItemCasesLeft = new List<ItemCase>();
        listItemCasesRight = new List<ItemCase>();

        InitBlock(listItemCasesLeft, rectTransformLeftBlock);
        InitBlock(listItemCasesRight, rectTransformRightBlock);

        InitBlockCheck();

        //TODo
        if (itemDataStrategy.list.Count == 3)
        {
            listItemCasesDown = new List<ItemCase>();
            InitBlock(listItemCasesDown, rectTransformDownBlock);

            //TODO
            for (int i = 0; i < arrayDown.Length; i++)
            {
                if (arrayDown[i])
                {
                    listItemCasesDown[i].SetBusy(true);
                    listItemCasesDown[i].SetModeItemCase(ModeItemCase.Located);
                    listItemCasesDown[i].SetImage(IconManager.GetRandomSprite1x1());
                }
            }
        }

        SetFillCase(itemDataStrategy.list[0], itemDataStrategy.list[1]);

        UpdateArrays();

        // TODO items error?
        // TODO this TxT
        EventManager.OnInitThings(itemDataStrategy.list[0].Length - configureCase.CountItems);

        //TODO this TxTxT
        // ?

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

        if (currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2x2W2H2)
        {
            rectTransformDownBlock.rotation = Quaternion.identity;
            rectTransformDownBlock.pivot = new Vector2(0.5f, 1f);
            rectTransformDownBlock.localPosition = new Vector3(-205f, -205f);
            isBlocker = false;

            for (int i = 0; i < listItemCasesLeft.Count; i++)
            {
                if (listItemCasesDown[i].ModeItemCase == ModeItemCase.Added)
                {
                    listItemCasesDown[i].Back();
                }
            }

        }

        EventManager.OnBackToPoolThings();
        EventManager.OnShowButtonGo(false);
    }

    //TODO
    private bool CheckLUD()
    {
        InitArray();

        for (int i = 0; i < arrayLeft.Length; i++)
        {
            int tmp = (i + 2) % arrayLeft.Length;

            if (arrayLeft[i] == true && arrayDown[tmp] == true)
            {
                return false;
            }
        }
        return true;
    }

    private bool[] UnionArraysLR(bool[] array1, bool[] array2)
    {
        for (int i = 0; i < array1.Length; i++)
        {
            if (array2[i] == true)
            {
                array1[i] = true;
            }
        }
        return array1;
    }

    private bool[] UnionArraysLLD(bool[] array1, bool[] array2)
    {
        for (int i = 0; i < array1.Length; i++)
        {
            int tmp = (i + 2) % arrayLeft.Length;
            if (array2[tmp] == true)
            {
                array1[i] = true;
            }
        }
        return array1;
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

        if (currentConfigure.ModeSizeCase == ModeSizeCase.Mode2x2x2W2H2)
        {
            rectTransformDownBlock.rotation = Quaternion.identity;
            rectTransformDownBlock.pivot = new Vector2(0.5f, 1f);
            rectTransformDownBlock.localPosition = new Vector3(-205f, -205f);
            isBlocker = false;
            listItemCasesDown.ForEach(item => Destroy(item.gameObject));
        }
    }
}
