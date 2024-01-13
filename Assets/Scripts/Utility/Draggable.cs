using UnityEngine;
using UnityEngine.EventSystems; // Нужен для интерфейсов EventSystems

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Vector2 offset;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos))
        {
            rectTransform.position = globalMousePos - (Vector3)offset;
        }
    }
}