using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Block_CheckBoxManagers : MonoBehaviour
{
    
    List<CheckBox_Manager> Block = new List<CheckBox_Manager>();
    [SerializeField] GroupCounters ConnectedData;
    public Block_CheckBoxManagers Init(GroupCounters Connect)
    {
        ConnectedData = Connect;
        VerticalLayoutGroup tempLayout = gameObject.AddComponent<VerticalLayoutGroup>();
        tempLayout.childControlHeight = false;
        tempLayout.childControlWidth = false;
        tempLayout.spacing = 50;
        if (ConnectedData._notNeedFixedText) tempLayout.childScaleWidth = true;
        ContentSizeFitter tempFitter = gameObject.AddComponent<ContentSizeFitter>();
        tempFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        tempFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        Generate();
        return this;
    }

    private void GenerateText()
    {
        GameObject temp_Text = new GameObject(ConnectedData._name + "_Text");
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
        for (int i = 0; i < ConnectedData._attributes.Length; ++i)
        {
            GameObject temp = new GameObject(ConnectedData._attributes[i].name_spec.fixedData + "_Checkbox_Manager_" + Block.Count);
            RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
            tempRectTransform.SetParent(transform);
            tempRectTransform.sizeDelta = Prefab_Manager._executor.SizeCounters;
            CheckBox_Manager temp_Manager = temp.AddComponent<CheckBox_Manager>();
            temp_Manager.Init(ConnectedData._attributes[i]);
            Block.Add(temp_Manager);
        }
        if (ConnectedData._notNeedFixedText && !ConnectedData._notNeedInputField) AddButton_NewCounter();
    }
    public void AddButton_NewCounter()
    {
        Button temp = Instantiate(Prefab_Manager._executor.Button_AddCounter, transform)
            .GetComponent<Button>();

        temp.onClick.AddListener
            (delegate 
                {
                    AddNewCounter(temp);
                }
            );

    }
    public void AddNewCounter(Button objectToMove)
    {
        ConnectedData.addNewAttribute();
        GameObject temp = new GameObject("New" + "_Checkbox_Manager_" + Block.Count);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(transform);
        tempRectTransform.sizeDelta = Prefab_Manager._executor.SizeCounters;
        tempRectTransform.localScale = Vector3.one;
        CheckBox_Manager temp_Manager = temp.AddComponent<CheckBox_Manager>();
        temp_Manager.Init(ConnectedData._attributes[ConnectedData._attributes.Length-1]);
        Block.Add(temp_Manager);
        objectToMove.transform.SetAsLastSibling();
        LayoutRebuilder.ForceRebuildLayoutImmediate(objectToMove.transform.parent.GetComponent<RectTransform>());
    }
    [ContextMenu("Clear Data")]
    public void Clear()
    {
        if (Block.Count == 0) return;
        foreach (var cur in Block)
        {
            DestroyImmediate(cur.gameObject);
        }
        Block.Clear();
    }
}
