using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    public bool ShowAtStart;


    public event Action Shown;
    public event Action Closed;
    public event Action Hidden;


    internal virtual void Initialize()
    {
        gameObject.SetActive(false);
        Init();
    }

    protected virtual void Init()
    {

    }

    public void Show(BaseView owner = null)
    {
        UIManager.Show(this, owner);
    }

    public void Close()
    {
        UIManager.Close(this);
    }

    public void Hide()
    {
        UIManager.Hide(this);
    }

    protected virtual void OnShown()
    {
        Shown?.Invoke();
    }

    protected virtual void OnClosed()
    {
        Closed?.Invoke();
    }

    protected virtual void OnHidden()
    {
        Hidden?.Invoke();
    }

    protected virtual void Subscribe(Button button, Action action)
    {
        button?.onClick.AddListener(() => action?.Invoke());
    }
    protected virtual void UnSubscribe(Button button, Action action)
    {
        button?.onClick.RemoveListener(() => action?.Invoke());
    }
}
