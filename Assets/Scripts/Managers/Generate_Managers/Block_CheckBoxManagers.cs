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
        gameObject.AddComponent<VerticalLayoutGroup>().spacing = 50;
        ContentSizeFitter temp = gameObject.AddComponent<ContentSizeFitter>();
        temp.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        temp.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
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
            Vector2 newPosition = i == 0 ?
                new Vector2(0, Prefab_Manager.Shift() * -2)
                : new Vector2(0, Block[Block.Count - 1].GetComponent<RectTransform>().anchoredPosition.y - Prefab_Manager.Shift() * 2);
            tempRectTransform.anchoredPosition = newPosition;
            CheckBox_Manager temp_Manager = temp.AddComponent<CheckBox_Manager>();
            temp_Manager.Init(ConnectedData._attributes[i]);
            Block.Add(temp_Manager);
        }
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
