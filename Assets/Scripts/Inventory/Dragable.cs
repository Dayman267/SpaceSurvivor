using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dragable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Inventory inventory;
    private Vector2 firstPosition;
    private RectTransform ownRect;

    public static Action<bool> OnSeedDragged;
    public static Action OnSeedReleased;
    
    private void Start()
    {
        ownRect = GetComponent<RectTransform>();
    }

    public void OnPointerUp(PointerEventData arg0)
    {
        ownRect.anchoredPosition = firstPosition;
        OnSeedDragged?.Invoke(false);
        if (transform.Find("image").GetComponent<Image>().sprite.name.Contains("Seed"))
        {
            OnSeedReleased?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        firstPosition = GetComponent<RectTransform>().anchoredPosition;
        if (transform.Find("image").GetComponent<Image>().sprite.name.Contains("Seed"))
        {
            OnSeedDragged?.Invoke(true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
}
