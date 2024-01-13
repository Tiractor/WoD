using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hint_Manager : TaskExecutor<Hint_Manager>
{
    public GameObject hintPrefab; // ������ ���������
    public Canvas canvas; // ������ ��� ������������� �������
    private void Awake()
    {
        Denote();
    }
    public void CreateHint(string message)
    {
        // ������� ������ �� �����
        GameObject hint = Instantiate(hintPrefab, canvas.transform);

        // �������� ���������� Text � Scrollbar
        ScrollRect scrollRect = hint.GetComponentInChildren<ScrollRect>();
        Text hintText = scrollRect.content.GetComponent<Text>();
        Scrollbar scrollbar = hint.GetComponentInChildren<Scrollbar>();

        // ��������� ������ �������� ��� �����
        StartCoroutine(AdjustContentSize(hintText, scrollRect, scrollbar));

        // ��������� ��������� ����
        hintText.text = message;

        // ���������� ���������� Draggable.cs � ���� ���������
        hint.AddComponent<Draggable>();

        // ���������������� ���������, ���� ����������
        hint.transform.localPosition = Vector2.zero;

        // ��������� ������ ��������
        Button closeButton = hint.GetComponentInChildren<Button>();
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(() => Destroy(hint));
        }
    }

    private IEnumerator AdjustContentSize(Text textComponent, ScrollRect scrollRect, Scrollbar scrollbar)
    {
        // ������� ���������� ����� �� ����������, ����� UI ����� �������������
        yield return null;

        // ���������� ������ ��������, ���� ����� ������ ������� ���������
        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, textComponent.preferredHeight);

        // �������� ��� ��������� ���������, ����������� �� ������ ������
        scrollbar.gameObject.SetActive(scrollRect.content.sizeDelta.y > scrollRect.viewport.sizeDelta.y);

        // ���������� ��������� � "�������" ���������
        scrollbar.value = 1;
    }
}