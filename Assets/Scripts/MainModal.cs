using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainModal : BaseView
{
    public static MainModal Instance { get; private set; }

    [SerializeField] private Button buttonSkip;
    [SerializeField] private Button buttonGo;
    [SerializeField] private Button buttonSettings;

    [SerializeField] private TMP_Text textMoneyUser;
    [SerializeField] private TMP_Text textMoneyTourist;
    [SerializeField] private Image iconland;
    [SerializeField] private Image iconPeople;

    [SerializeField] private GeneratorCase generatorCase;

    private int money;

    protected override void Init()
    {
        base.Init();

        Instance = this;

        Subscribe(buttonSkip, () => EventManager.OnSkip());
        Subscribe(buttonGo,
            () =>
            {
                Close();
                PackModal.Instance.Show();
                generatorCase.CreateCase();
            });

        EventManager.UpdateMoneyUser += (value) => SetMoneyUser(value);
        EventManager.UpdateDataTourist += (value) => SetDataTourist(value);

        SetMoneyUser(0);

    }

    protected void OnDestroy()
    {
        UnSubscribe(buttonSkip, () => EventManager.OnSkip());
        UnSubscribe(buttonGo, () => generatorCase.CreateCase());
        EventManager.UpdateMoneyUser -= (value) => SetMoneyUser(value);
        EventManager.UpdateDataTourist -= (value) => SetDataTourist(value);
    }

    private void SetMoneyUser(int money)
    {
        this.money += money;
        textMoneyUser.text = this.money.ToString();
    }

    private void SetDataTourist(ConfigureCase configureCase)
    {
        textMoneyTourist.text = configureCase.Price.ToString();
        iconland.sprite = configureCase.IconLand;
        iconPeople.sprite = configureCase.IconPiople;
    }
}
