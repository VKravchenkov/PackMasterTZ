using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    public static Dictionary<string, BaseView> StaticViews = new Dictionary<string, BaseView>();

    protected override void Awake()
    {
        base.Awake();

        StaticViews.Clear();

        List<BaseView> views = FindObjectsOfType<BaseView>().ToList();

        foreach (var item in views)
        {
            StaticViews[item.GetType().Name] = item;
        }

        // init views
        foreach (var view in views)
        {
            view.Initialize();
        }

        // show views
        foreach (var view in views.Where(i => i.ShowAtStart))
        {
            Show(view, null);
        }
    }

    public static void Show(BaseView view, BaseView owner)
    {
        if (owner == view)
        {
            // Error
            owner = null;
        }

        // set visible state
        view.gameObject.SetActive(true);
    }

    public static void Close(BaseView view)
    {
        view.gameObject.SetActive(false);
    }

    public static void Hide(BaseView view)
    {

    }
}
