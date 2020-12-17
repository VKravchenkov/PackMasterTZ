using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackModal : BaseView
{
    public static PackModal Instance { get; private set; }

    [SerializeField] private Button buttonGo;

    [SerializeField] private Canvas canvas;

    public Canvas Canvas => canvas;

    protected override void Init()
    {
        base.Init();

        Instance = this;

        // OnClick

        Subscribe(buttonGo, () => EventManager.OnCheckSuitCaseFill());
        EventManager.ShowButtonGo += (isShow) => buttonGo.gameObject.SetActive(isShow);
        EventManager.OnShowButtonGo(false);
    }

    protected void OnDestroy()
    {
        UnSubscribe(buttonGo, () => EventManager.OnCheckSuitCaseFill());
        EventManager.ShowButtonGo -= (isShow) => buttonGo.gameObject.SetActive(isShow);
    }
}
