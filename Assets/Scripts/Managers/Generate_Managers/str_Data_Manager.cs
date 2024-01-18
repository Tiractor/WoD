using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class str_Data_Manager : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI fixedData_Output;
    [SerializeField] private str_Data ConnectedData;
    [HideInInspector] public float Max_X;
    public void Init(str_Data Connect)
    {
        ConnectedData = Connect;
        Generate();
    }
    [ContextMenu("Generate")]
    public void Generate()
    {
        GenerateText();
        CreateIF();
    }

    private void GenerateText()
    {
        GameObject temp_Text = new GameObject(ConnectedData.fixedData + "_Text");
        RectTransform tempRectTransform = temp_Text.AddComponent<RectTransform>();
        tempRectTransform.SetParent(transform);
        tempRectTransform.anchorMin = Prefab_Manager._executor.AnchorsText[0];
        tempRectTransform.anchorMax = Prefab_Manager._executor.AnchorsText[1];
        TMPro.TextMeshProUGUI temp = temp_Text.AddComponent<TMPro.TextMeshProUGUI>();
        temp.text = ConnectedData.fixedData;
        temp.fontSizeMax = 36;
        temp.enableAutoSizing = true;
        temp.font = Prefab_Manager._executor.Font;
        temp.alignment = TMPro.TextAlignmentOptions.MidlineLeft;
        temp.enableWordWrapping = false;
        tempRectTransform.sizeDelta = Prefab_Manager._executor.TextBox_SizeSettings;
        tempRectTransform.anchoredPosition = Vector2.zero;
        fixedData_Output = temp;
    }

    private void CreateIF()
    {
        RectTransform rectTransform1 = fixedData_Output.GetComponent<RectTransform>();

        GameObject newObject = Instantiate(Prefab_Manager._executor.InputField, transform);
        newObject.name = ConnectedData.fixedData + "_InputField";
        TMPro.TMP_InputField temp_IF = newObject.GetComponent<TMPro.TMP_InputField>();
        RectTransform tempRectTransform = newObject.GetComponent<RectTransform>();
        tempRectTransform.anchorMin = Prefab_Manager._executor.AnchorsIF[0];
        tempRectTransform.anchorMax = Prefab_Manager._executor.AnchorsIF[1];
        tempRectTransform.anchoredPosition = Vector2.zero;
        temp_IF.text = ConnectedData.changeableData;
        temp_IF.onValueChanged.AddListener(delegate
            {
                ConnectedData.ApplyData(temp_IF);
            }
        );
    }
}
