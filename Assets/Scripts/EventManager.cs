using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class EventManager
{
    public static event Action<ConfigureCase> CreateCase;
    public static event Action<int> InitThings;
    public static event Action CheckSuitCaseFill;

    public static event Action UpdateListThings;
    public static event Action<ItemThings> AddedItemThings;
    public static event Action BackToPoolThings;

    public static event Action<bool> ShowButtonGo;
    public static event Action Skip;

    public static event Action<int> UpdateMoneyUser;
    public static event Action<ConfigureCase> UpdateDataTourist;

    public static void OnCreateCase(ConfigureCase configureCase)
    {
        Action<ConfigureCase> tmp = CreateCase;// Volatile.Read(ref CreateCase);
        tmp?.Invoke(configureCase);
    }

    public static void OnInitThings(int count)
    {
        Action<int> tmp = InitThings;
        tmp?.Invoke(count);
    }

    public static void OnCheckSuitCaseFill()
    {
        Action tmp = CheckSuitCaseFill;
        tmp?.Invoke();
    }

    public static void OnUpdateListThings()
    {
        Action tmp = UpdateListThings;
        tmp?.Invoke();
    }

    public static void OnAddedItemThings(ItemThings itemThings)
    {
        Action<ItemThings> tmp = AddedItemThings;
        tmp?.Invoke(itemThings);
    }

    public static void OnShowButtonGo(bool isShow)
    {
        Action<bool> tmp = ShowButtonGo;
        tmp?.Invoke(isShow);
    }

    public static void OnBackToPoolThings()
    {
        Action tmp = BackToPoolThings;
        tmp?.Invoke();
    }

    public static void OnUpdateMoney(int money)
    {
        Action<int> tmp = UpdateMoneyUser;
        tmp?.Invoke(money);
    }

    public static void OnUpdateMoneyTourist(ConfigureCase configureCase)
    {
        Action<ConfigureCase> tmp = UpdateDataTourist;
        tmp?.Invoke(configureCase);
    }

    public static void OnSkip()
    {
        Action tmp = Skip;
        tmp?.Invoke();
    }
}
