using System.Collections;
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
    [SerializeField] private GameObject Low_Data;
    [SerializeField] private GameObject Conditions;
    [SerializeField] private GameObject Aspirations;
    [SerializeField] private GameObject Counters;
    [Header("Equipment")]
    [SerializeField] private GameObject Attack;
    [SerializeField] private GameObject Equipment;
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
    [ContextMenu("Clear")]
    public void Clear()
    {
        var children = new System.Collections.Generic.List<GameObject>();
        foreach (Transform child in BaseData.transform) children.Add(child.gameObject);
        foreach (Transform child in Attributes.transform) children.Add(child.gameObject);
        foreach (Transform child in Skills.transform) children.Add(child.gameObject);

        /*foreach (Transform child in Merits.transform) children.Add(child.gameObject);
        foreach (Transform child in Low_Data.transform) children.Add(child.gameObject);
        foreach (Transform child in Conditions.transform) children.Add(child.gameObject);
        foreach (Transform child in Aspirations.transform) children.Add(child.gameObject);
        foreach (Transform child in Counters.transform) children.Add(child.gameObject);

        foreach (Transform child in Attack.transform) children.Add(child.gameObject);
        foreach (Transform child in Equipment.transform) children.Add(child.gameObject);*/

        children.ForEach(child => DestroyImmediate(child));
    }
}
