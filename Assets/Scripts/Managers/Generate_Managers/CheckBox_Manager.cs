using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CheckBox_Manager : MonoBehaviour
{
    List <Toggle> CheckBoxes = new List<Toggle>();
    [SerializeField] private Counter ConnectedData;
    public void Init(Counter Connect)
    {
        ConnectedData = Connect;
        HorizontalLayoutGroup temp = gameObject.AddComponent<HorizontalLayoutGroup>(); // .spacing = 20
        temp.childControlHeight = false;
        temp.childControlWidth = false;
        
        gameObject.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        Generate();
    }
    [ContextMenu("Generate Checkboxes")]
    public void Generate()
    {
        Clear();
        if (!ConnectedData.ParentCounters._isMerit) GenerateText();
        if (!ConnectedData.ParentCounters._isAttribute) CreateIF();
        GenerateCheckBoxes();
        
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
                if(ConnectedData.ParentCounters._isAttribute) ConnectedData.SetValue(i+1);
                else ConnectedData.SetValue(i);
                break;
            }
        }
        for(int i =0; i < CheckBoxes.Count;++i)
        {
            Toggle temp = CheckBoxes[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = ConnectedData.ParentCounters._isAttribute ? (i <= ConnectedData.curValue -1) : (i <= ConnectedData.curValue);
            temp.onValueChanged.AddListener(delegate
             {
                 ReSetCurrentValue(temp);
             }
                );
        }
        
    }

    private void CreateIF()
    {
        GameObject newObject = Instantiate(Prefab_Manager._executor.InputField, transform);
        newObject.name = ConnectedData.name_spec.fixedData + "_InputField";
        TMPro.TMP_InputField temp_IF = newObject.GetComponent<TMPro.TMP_InputField>();
        RectTransform newObjectRect = newObject.GetComponent<RectTransform>();
        newObjectRect.localScale = Vector3.one;
        newObjectRect.sizeDelta = new Vector2(150,40);

        temp_IF.onValueChanged.AddListener(delegate
            {
                ConnectedData.name_spec.ApplyData(temp_IF);
            }
        );
    }
}
