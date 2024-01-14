using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Health_Manager : MonoBehaviour
{
    List<Toggle> MaxHealth_CheckBoxes = new List<Toggle>();
    List<FourStatesCheckBox> CurHealth_CheckBoxes = new List<FourStatesCheckBox>();
    [SerializeField] private Health ConnectedData;
    private RectTransform MaxHealth_Layout;
    private RectTransform CurHealth_Layout;
    public void Init(Health Connect)
    {
        ConnectedData = Connect;
        VerticalLayoutGroup temp = gameObject.AddComponent<VerticalLayoutGroup>(); // .spacing = 20
        temp.childControlHeight = false;
        temp.childControlWidth = false;
        
        gameObject.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        Generate();
    }
    private RectTransform GenerateHorizontalLayout(string name)
    {
        GameObject temp = new GameObject(name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        HorizontalLayoutGroup tempLayout = temp.AddComponent<HorizontalLayoutGroup>();
        tempLayout.childControlHeight = false;
        tempLayout.childControlWidth = false;
        ContentSizeFitter tempFitter = temp.AddComponent<ContentSizeFitter>();
        tempFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        tempFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        tempRectTransform.SetParent(transform);

        return tempRectTransform;
    }
    private void GenerateText()
    {
        GameObject temp_Text = new GameObject("Health" + "_Text");
        RectTransform tempRectTransform = temp_Text.AddComponent<RectTransform>();
        tempRectTransform.SetParent(transform);
        tempRectTransform.anchorMin = new Vector2(0.5f, 1);
        tempRectTransform.anchorMax = new Vector2(0.5f, 1);
        TMPro.TextMeshProUGUI temp = temp_Text.AddComponent<TMPro.TextMeshProUGUI>();
        temp.text = ConnectedData._name;
        temp.font = Prefab_Manager._executor.Font;
        temp.fontSizeMax = 36;
        temp.enableAutoSizing = true;
        temp.alignment = TMPro.TextAlignmentOptions.MidlineLeft;
        tempRectTransform.sizeDelta = Prefab_Manager._executor.TextBox_SizeSettings;
    }

    [ContextMenu("Generate Checkboxes")]
    public void Generate()
    {
        Clear();
        GenerateText();
        MaxHealth();
        CurrentHealth();
    }
    private void MaxHealth()
    {
        var MaxHealth = ConnectedData._max_Health;
        if (MaxHealth_Layout == null) MaxHealth_Layout = GenerateHorizontalLayout("MaxHealth_Layout");
        for (int i = 0; i < MaxHealth._maxValue; ++i)
        {

            GameObject temp = Instantiate(Prefab_Manager._executor.CheckBox_Base, MaxHealth_Layout);
            temp.name = MaxHealth._attributes[0].name_spec.fixedData + "_Checkbox_" + i;
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            Toggle temp_Toggle = temp.GetComponent<Toggle>();

            temp_Toggle.isOn = (i < MaxHealth._attributes[0].curValue);
            temp_Toggle.onValueChanged.AddListener(delegate
            {
                ReSetCurrentValue(temp_Toggle);
            }
                );
            MaxHealth_CheckBoxes.Add(temp_Toggle);
        }
    }
    private void CurrentHealth()
    {
        var CurHealth = ConnectedData._cur_Health;
        if(CurHealth_Layout == null) CurHealth_Layout = GenerateHorizontalLayout("CurHealth_Layout");
        if (ConnectedData._max_Health._attributes[0].curValue+1 >= CurHealth_CheckBoxes.Count) 
        {
            
            for (int i = 0; ConnectedData._max_Health._attributes[0].curValue + 1 > CurHealth_CheckBoxes.Count; ++i)
            {
                Counter currentCounter = CurHealth._attributes[i];
                GameObject temp = Instantiate(Prefab_Manager._executor.CheckBox_FourState, CurHealth_Layout);
                temp.name = "CurrentHealth_4State_Checkbox_" + CurHealth_CheckBoxes.Count;
                RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
                FourStatesCheckBox temp_Toggle = temp.GetComponent<FourStatesCheckBox>();

                temp_Toggle.SetState(CurHealth._attributes[CurHealth_CheckBoxes.Count+i].curValue);
                temp_Toggle.onValueChanged.AddListener( currentCounter.SetValue );
                CurHealth_CheckBoxes.Add(temp_Toggle);
            }
        }
        else
        {
            while(ConnectedData._max_Health._attributes[0].curValue+1 < CurHealth_CheckBoxes.Count )
            {
                DestroyImmediate(CurHealth_CheckBoxes[CurHealth_CheckBoxes.Count-1].gameObject);
                CurHealth_CheckBoxes.Remove(CurHealth_CheckBoxes[CurHealth_CheckBoxes.Count - 1]);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(CurHealth_Layout);
    }
    public void Clear()
    {
        if (MaxHealth_CheckBoxes.Count == 0) return;
        foreach (var cur in MaxHealth_CheckBoxes)
        {
            DestroyImmediate(cur.gameObject);
        }
        MaxHealth_CheckBoxes.Clear();
    }
    public void ReSetCurrentValue(Toggle Value)
    {
        for (int i = 0; i < MaxHealth_CheckBoxes.Count; ++i)
        {
            if (Value == MaxHealth_CheckBoxes[i])
            {
                ConnectedData._max_Health._attributes[0].SetValue(i);
                break;
            }
        }
        for (int i = 0; i < MaxHealth_CheckBoxes.Count; ++i)
        {
            Toggle temp = MaxHealth_CheckBoxes[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = i <= ConnectedData._max_Health._attributes[0].curValue;
            temp.onValueChanged.AddListener(delegate
            {
                ReSetCurrentValue(temp);
            }
                );
        }
        CurrentHealth();
    }
}
