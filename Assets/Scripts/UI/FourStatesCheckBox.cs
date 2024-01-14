using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StateCounter : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite[] stateSprites;

    public UnityEvent<int> OnValueChange;

    [SerializeField] private int currentState = 0;
    [SerializeField] private Image imageComponent;

    void Start()
    {
        if(imageComponent == null) imageComponent = GetComponent<Image>();
        if (imageComponent == null || stateSprites == null || stateSprites.Length == 0)
        {
            Debug.LogWarning("StateCounter requires an Image component and at least one sprite to function properly.");
            return;
        }
        imageComponent.sprite = stateSprites[0];
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            DecrementState();
        }
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            IncrementState();
        }
    }
    private void IncrementState()
    {
        currentState = (currentState + 1) % stateSprites.Length;
        imageComponent.sprite = stateSprites[currentState];
        OnValueChange?.Invoke(currentState);
    }
    private void DecrementState()
    {
        currentState = (currentState - 1) < 0 ? stateSprites.Length - 1 : (currentState - 1) % stateSprites.Length;
        imageComponent.sprite = stateSprites[currentState];
        OnValueChange?.Invoke(currentState);
    }
}