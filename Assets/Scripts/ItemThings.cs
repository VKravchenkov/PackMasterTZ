using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemThings : Item, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image image;
    [SerializeField] private RectTransform rectTransform;

    private Canvas canvas;
    private Transform transformParentDefault;
    private ItemCase itemCaseParent;

    public ItemCase ItemCaseParent => itemCaseParent;

    private void Awake()
    {
        canvas = PackModal.Instance.Canvas;
        transformParentDefault = rectTransform.parent;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemCaseParent != null)
        {
            itemCaseParent.SetBusy(false);
            itemCaseParent.SetModeItemCase(ModeItemCase.Located);
        }

        canvasGroup.blocksRaycasts = false;
        rectTransform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (rectTransform.parent != canvas.transform)
        {
            SetParent(rectTransform);
        }
        else
        {
            itemCaseParent = null;
            // Added to list
            EventManager.OnAddedItemThings(this);
            SetParent(transformParentDefault.GetComponent<RectTransform>());
        }

    }

    public void SetParent(RectTransform rectTransform)
    {
        this.rectTransform.SetParent(rectTransform);
        this.rectTransform.localPosition = Vector3.zero;
    }

    public void SetParentDefault()
    {
        itemCaseParent = null;
        SetParent(transformParentDefault.GetComponent<RectTransform>());
    }

    public void SetItemCase(ItemCase itemCase)
    {
        itemCaseParent = itemCase;
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

}
