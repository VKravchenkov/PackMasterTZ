using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCase : Item, IDropHandler
{
    [SerializeField] private bool isBusy;
    [SerializeField] private int index;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image border;

    private ModeItemCase modeItemCase = ModeItemCase.Located;

    public bool Busy => isBusy;
    public int Index => index;

    public ModeItemCase ModeItemCase => modeItemCase;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (!isBusy)
            {
                SetBusy(true);

                ItemThings itemThings = eventData.pointerDrag.GetComponent<ItemThings>();

                itemThings.SetParent(rectTransform);
                itemThings.SetItemCase(this);
                //?
                SetModeItemCase(ModeItemCase.Added);

                // Update ListItemThings
                EventManager.OnUpdateListThings();

            }
        }
    }

    public void SetBusy(bool isBusy)
    {
        this.isBusy = isBusy;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void SetImage(Sprite sprite)
    {
        border.sprite = sprite;
    }

    public void SetModeItemCase(ModeItemCase modeItemCase)
    {
        this.modeItemCase = modeItemCase;
    }

    public void Back()
    {
        SetBusy(false);
        SetModeItemCase(ModeItemCase.Located);
    }

}
