using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckBox_Manager : MonoBehaviour
{
    List <Toggle> CheckBoxes = new List<Toggle>();
    [SerializeField] private Counter ConnectedData;
    [HideInInspector] public float Max_X;
    public void Init(Counter Connect)
    {
        ConnectedData = Connect;
        Generate();
    }
    [ContextMenu("Generate Checkboxes")]
    public void Generate()
    {
        Clear();
        GameObject text = GenerateText();
        GenerateCheckBoxes();
        if (!ConnectedData.ParentCounters._isAttribute) CreateIF(text, CheckBoxes[0].gameObject);
        Max_X = CheckBoxes[CheckBoxes.Count - 1].GetComponent<RectTransform>().anchoredPosition.x;
    }

    private GameObject GenerateText()
    {
        GameObject temp_Text = new GameObject(ConnectedData.name_spec.fixedData + "_Text");
        RectTransform tempRectTransform = temp_Text.AddComponent<RectTransform>();
        tempRectTransform.SetParent(transform);
        TMPro.TextMeshProUGUI temp = temp_Text.AddComponent<TMPro.TextMeshProUGUI>();
        temp.text = ConnectedData.name_spec.fixedData;
        temp.font = Prefab_Manager._executor.Font;
        temp.fontSizeMax = 36;
        temp.enableAutoSizing = true;
        temp.alignment = TMPro.TextAlignmentOptions.MidlineLeft;
        temp.enableWordWrapping = false;
        tempRectTransform.sizeDelta = Prefab_Manager._executor.TextBox_SizeSettings;
        tempRectTransform.anchoredPosition = Vector2.zero;
        return temp_Text;
    }

    private void ClearButton()
    {
        GameObject temp = Instantiate(Prefab_Manager._executor.CheckBox_Clear, transform);
        temp.name = ConnectedData.name_spec.fixedData + "_Clear";
        RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
        Vector2 newPosition = new Vector2(Prefab_Manager._executor.TextBox_SizeSettings.x + Prefab_Manager.Shift() * 0.5f, 0);
                
        tempRectTransform.anchoredPosition = newPosition;
        Toggle temp_Toggle = temp.GetComponent<Toggle>();
        temp_Toggle.onValueChanged.AddListener(delegate
            {
                ReSetCurrentValue(temp_Toggle);
            }
                );
        CheckBoxes.Add(temp_Toggle);
    }
    private void GenerateCheckBoxes()
    {
        if(!ConnectedData.ParentCounters._isAttribute) ClearButton();
        for (int i = 0; i < ConnectedData.ParentCounters._maxValue; ++i)
        {
            GameObject temp = Instantiate(Prefab_Manager._executor.CheckBox_Base, transform);
            temp.name = ConnectedData.name_spec.fixedData + "_Checkbox_" + (CheckBoxes.Count+1);
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            Vector2 newPosition = CheckBoxes.Count == 0 ?
                new Vector2(Prefab_Manager._executor.TextBox_SizeSettings.x + Prefab_Manager.Shift()*0.5f, 0)
                : new Vector2(CheckBoxes[CheckBoxes.Count - 1].GetComponent<RectTransform>().anchoredPosition.x + Prefab_Manager.Shift()*2f, 0);
            tempRectTransform.anchoredPosition = newPosition;
            Toggle temp_Toggle = temp.GetComponent<Toggle>();
            
            temp_Toggle.isOn = (i < ConnectedData.curValue);
            temp_Toggle.onValueChanged.AddListener(delegate 
                    {
                        ReSetCurrentValue(temp_Toggle);
                    }
                );
            CheckBoxes.Add(temp_Toggle);
        }
    }
    [ContextMenu("Clear Data")]
    public void Clear()
    {
        if (CheckBoxes.Count == 0) return;
        foreach (var cur in CheckBoxes)
        {
            DestroyImmediate(cur.gameObject);
        }
        CheckBoxes.Clear();
    }
    public void ReSetCurrentValue(Toggle Value)
    {
        for (int i = 0; i < CheckBoxes.Count; ++i)
        {
            if (Value == CheckBoxes[i])
            {
                ConnectedData.SetValue(i+1);
                break;
            }
        }
        for(int i =0; i < CheckBoxes.Count;++i)
        {
            Toggle temp = CheckBoxes[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = (i <= ConnectedData.curValue);
            temp.onValueChanged.AddListener(delegate
             {
                 ReSetCurrentValue(temp);
             }
                );
        }
        
    }

    private void CreateIF(GameObject object1, GameObject object2)
    {
        RectTransform rectTransform1 = object1.GetComponent<RectTransform>();
        RectTransform rectTransform2 = object2.GetComponent<RectTransform>();

        GameObject newObject = Instantiate(Prefab_Manager._executor.InputField, transform);
        newObject.name = ConnectedData.name_spec.fixedData + "_InputField";
        TMPro.TMP_InputField temp_IF = newObject.GetComponent<TMPro.TMP_InputField>();
        RectTransform newObjectRect = newObject.GetComponent<RectTransform>();
        float temp_height =newObjectRect.sizeDelta.y;
        // Установка новых параметров для RectTransform созданного объекта.
        // Следуем за RectTransform первого объекта.
        newObjectRect.localScale = Vector3.one;
        newObjectRect.sizeDelta = rectTransform1.sizeDelta;
        newObjectRect.anchorMin = new Vector2(0.5f, rectTransform1.anchorMin.y);
        newObjectRect.anchorMax = new Vector2(0.5f, rectTransform1.anchorMax.y);
        newObjectRect.pivot = new Vector2(0.5f, 0.5f);

        // Вычисляем позиции X окончаний объекта 1 и начала объекта 2 в мировом пространстве.
        Vector3 object1EndPosition = rectTransform1.TransformPoint(new Vector3(rectTransform1.rect.max.x, 0, 0));
        Vector3 object2StartPosition = rectTransform2.TransformPoint(new Vector3(rectTransform2.rect.min.x, 0, 0));

        // Располагаем наш объект в середине между двумя данными объектами.
        newObjectRect.position = (object1EndPosition + object2StartPosition) * 0.5f;

        // Вычисляем ширину нового объекта.
        float newWidth = Vector3.Distance(object1EndPosition, object2StartPosition);

        // Задаем расчетные размеры.
        newObjectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        newObjectRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rectTransform1.rect.height); // Возьмем высоту от object1.

        newObjectRect.sizeDelta = new Vector2(newObjectRect.sizeDelta.x, temp_height);
        temp_IF.onValueChanged.AddListener(delegate
            {
                ConnectedData.name_spec.ApplyData(temp_IF);
            }
        );
    }
}
