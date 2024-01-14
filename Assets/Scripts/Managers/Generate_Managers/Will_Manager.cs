using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Will_Manager : MonoBehaviour
{
    List<Toggle> Max_CheckBoxes = new List<Toggle>();
    List<Toggle> Cur_CheckBoxes = new List<Toggle>();
    [SerializeField] private GroupCounters ConnectedData;
    private RectTransform Max_Layout;
    private RectTransform Cur_Layout;
    public void Init(GroupCounters Connect)
    {
        ConnectedData = Connect;
        VerticalLayoutGroup temp = gameObject.AddComponent<VerticalLayoutGroup>(); // .spacing = 20
        temp.childControlHeight = false;
        temp.childControlWidth = false;

        ContentSizeFitter tempFitter = gameObject.AddComponent<ContentSizeFitter>();
        tempFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        tempFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
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
        GameObject temp_Text = new GameObject("Will" + "_Text");
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
        MaxWill();
        CurrentWill();
    }
    private void MaxWill()
    {
        var MaxWill = ConnectedData._attributes[0];
        if (Max_Layout == null) Max_Layout = GenerateHorizontalLayout("MaxHealth_Layout");
        for (int i = 0; i < MaxWill.ParentCounters._maxValue; ++i)
        {

            GameObject temp = Instantiate(Prefab_Manager._executor.CheckBox_Base, Max_Layout);
            temp.name = MaxWill.name_spec.fixedData + "_Checkbox_" + i;
            RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
            Toggle temp_Toggle = temp.GetComponent<Toggle>();

            temp_Toggle.isOn = (i < MaxWill.curValue);
            temp_Toggle.onValueChanged.AddListener(delegate
            {
                ReSetCurrentValue(temp_Toggle);
            }
                );
            Max_CheckBoxes.Add(temp_Toggle);
        }
    }
    private void CurrentWill()
    {
        var CurWill = ConnectedData._attributes[1];
        if(Cur_Layout == null) Cur_Layout = GenerateHorizontalLayout("CurHealth_Layout");
        if (ConnectedData._attributes[0].curValue+1 >= Cur_CheckBoxes.Count) 
        {
            int start = Cur_CheckBoxes.Count;
            for (int i = 0; ConnectedData._attributes[0].curValue + 1 > Cur_CheckBoxes.Count; ++i)
            {
                int shift = start + i;
                GameObject temp = Instantiate(Prefab_Manager._executor.CheckBox_Square, Cur_Layout);
                temp.name = ConnectedData._name + "_Checkbox_" + Cur_CheckBoxes.Count;
                RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
                Toggle temp_Toggle = temp.GetComponent<Toggle>();

                temp_Toggle.isOn = (shift < CurWill.curValue);
                Cur_CheckBoxes.Add(temp_Toggle);


                
                temp_Toggle.onValueChanged.AddListener(delegate
                {
                    CurWill.SetValue(shift);
                    ReverseConnect(temp_Toggle);
                }
                );
            }
        }
        else
        {
            while(ConnectedData._attributes[0].curValue+1 < Cur_CheckBoxes.Count )
            {
                DestroyImmediate(Cur_CheckBoxes[Cur_CheckBoxes.Count-1].gameObject);
                Cur_CheckBoxes.Remove(Cur_CheckBoxes[Cur_CheckBoxes.Count - 1]);
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(Cur_Layout);
    }
    public void Clear()
    {
        if (Max_CheckBoxes.Count == 0) return;
        foreach (var cur in Max_CheckBoxes)
        {
            DestroyImmediate(cur.gameObject);
        }
        Max_CheckBoxes.Clear();
    }
    public void ReSetCurrentValue(Toggle Value)
    {
        for (int i = 0; i < Max_CheckBoxes.Count; ++i)
        {
            if (Value == Max_CheckBoxes[i])
            {
                ConnectedData._attributes[0].SetValue(i);
                break;
            }
        }
        for (int i = 0; i < Max_CheckBoxes.Count; ++i)
        {
            Toggle temp = Max_CheckBoxes[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = i <= ConnectedData._attributes[0].curValue;
            temp.onValueChanged.AddListener
                (delegate
                    {
                        ReSetCurrentValue(temp);
                    }
                );
        }
        CurrentWill();
    }
    public void ReverseConnect(Toggle Value)
    {
        if(Cur_CheckBoxes[0] == Value)
        {
            ConnectedData._attributes[1].SetValue(-1);
        }
        for (int i = 0; i < Cur_CheckBoxes.Count; ++i)
        {

            Toggle temp = Cur_CheckBoxes[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = i <= ConnectedData._attributes[1].curValue;
            if (Cur_CheckBoxes[0] == Value && Value == temp) Value.isOn = false;
            int tempint = i; 
            temp.onValueChanged.AddListener
                (delegate
                    {
                        ConnectedData._attributes[1].SetValue(tempint);
                        ReverseConnect(temp);
                    }
                );
        }
        
    }

}
