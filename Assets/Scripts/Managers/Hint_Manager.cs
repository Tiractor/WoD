using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hint_Manager : TaskExecutor<Hint_Manager>
{
    public GameObject hintPrefab; // Префаб подсказки
    public Canvas canvas; // Канвас для родительского объекта
    private void Awake()
    {
        Denote();
    }
    public void CreateHint(string message)
    {
        // Создаем префаб на сцене
        GameObject hint = Instantiate(hintPrefab, canvas.transform);

        // Получаем компоненты Text и Scrollbar
        ScrollRect scrollRect = hint.GetComponentInChildren<ScrollRect>();
        Text hintText = scrollRect.content.GetComponent<Text>();
        Scrollbar scrollbar = hint.GetComponentInChildren<Scrollbar>();

        // Подгоняем размер контента под текст
        StartCoroutine(AdjustContentSize(hintText, scrollRect, scrollbar));

        // Заполняем текстовое поле
        hintText.text = message;

        // Добавление компонента Draggable.cs к окну подсказки
        hint.AddComponent<Draggable>();

        // Позиционирование подсказки, если необходимо
        hint.transform.localPosition = Vector2.zero;

        // Настройка кнопки закрытия
        Button closeButton = hint.GetComponentInChildren<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => Destroy(hint));
        }
    }

    private IEnumerator AdjustContentSize(Text textComponent, ScrollRect scrollRect, Scrollbar scrollbar)
    {
        // Ожидаем следующего кадра до обновления, чтобы UI успел подготовиться
        yield return null;

        // Регулируем размер контента, если текст больше области просмотра
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, textComponent.preferredHeight);

        // Включаем или отключаем прокрутку, основываясь на высоте текста
        scrollbar.gameObject.SetActive(scrollRect.content.sizeDelta.y > scrollRect.viewport.sizeDelta.y);

        // Возвращаем скроллбар в "верхнее" положение
        scrollbar.value = 1;
    }
}