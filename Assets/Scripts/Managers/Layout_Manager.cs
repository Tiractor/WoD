using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Layout_Manager : MonoBehaviour
{
    [Header("Base Elements")]
    [SerializeField] private GameObject BaseData;
    [SerializeField] private GameObject Attributes;
    [SerializeField] private GameObject Skills;

    [Header("Other Traits")]
    [SerializeField] private GameObject Merits;
    [SerializeField] private GameObject LowData;
    [SerializeField] private GameObject ImportantCounters;
    [SerializeField] private GameObject Conditions;
    [SerializeField] private GameObject Aspirations;


    [Header("Equipment")]
    [SerializeField] private GameObject Attack;
    [SerializeField] private GameObject Equipment;

    public void Refresh()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Skills.GetComponentInParent<RectTransform>());
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(BaseData.GetComponentInParent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(Skills.transform.parent.GetComponentInParent<RectTransform>());
    }
    public void addElement_BaseData(str_Data Connected)
    {
        GameObject temp = new GameObject(Connected.fixedData);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(BaseData.transform);
        temp.AddComponent<str_Data_Manager>().Init(Connected);
    }
    public void addElement_Attributes(GroupCounters Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(Attributes.transform);
        tempRectTransform.sizeDelta = new Vector2(600, 250);
        temp.AddComponent<Block_CheckBoxManagers>().Init(Connected);
    }
    public void addElement_Skills(GroupCounters Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(Skills.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 250);
        temp.AddComponent<Block_CheckBoxManagers>().Init(Connected);
    }
    public void addElement_Merits(GroupCounters Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(Merits.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 250);
        temp.AddComponent<Block_CheckBoxManagers>().Init(Connected);
    }
    public void addElement_LowData(str_Data Connected)
    {
        GameObject temp = new GameObject(Connected.fixedData);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(LowData.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 50);
        temp.AddComponent<str_Data_Manager>().Init(Connected);
    }
    public void addElement_ImportantCounters(Health Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(ImportantCounters.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 50);
        temp.AddComponent<Health_Manager>().Init(Connected);
    }
    public void addElement_ImportantCounters(GroupCounters Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(ImportantCounters.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 50);
        temp.AddComponent<Will_Manager>().Init(Connected);
    }
    public void addElement_Sanity(GroupCounters Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(ImportantCounters.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 50);
        temp.AddComponent<Block_CheckBoxManagers>().Init(Connected);
    }

    public void addElement_Conditions(GroupCounters Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(Conditions.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 50);
        temp.AddComponent<Block_CheckBoxManagers>().Init(Connected);
    }
    public void addElement_Aspirations(GroupCounters Connected)
    {
        GameObject temp = new GameObject(Connected._name);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        tempRectTransform.SetParent(Aspirations.transform);
        tempRectTransform.sizeDelta = new Vector2(500, 50);
        temp.AddComponent<Block_CheckBoxManagers>().Init(Connected);
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        var children = new List<GameObject>();
        foreach (Transform child in BaseData.transform) children.Add(child.gameObject);
        foreach (Transform child in Attributes.transform) children.Add(child.gameObject);
        foreach (Transform child in Skills.transform) children.Add(child.gameObject);

        foreach (Transform child in Merits.transform) children.Add(child.gameObject);
        foreach (Transform child in LowData.transform) children.Add(child.gameObject);
        foreach (Transform child in Conditions.transform) children.Add(child.gameObject);
        foreach (Transform child in Aspirations.transform) children.Add(child.gameObject);
        foreach (Transform child in ImportantCounters.transform) children.Add(child.gameObject);

        foreach (Transform child in Attack.transform) children.Add(child.gameObject);
        foreach (Transform child in Equipment.transform) children.Add(child.gameObject);

        children.ForEach(child => DestroyImmediate(child));
    }
}
