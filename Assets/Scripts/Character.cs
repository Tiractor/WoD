using UnityEngine;
[System.Serializable]
public class Character : MonoBehaviour
{
    public CharacterData _data;
    public GameObject ConnectedButton;
    public Character()
    {
        _data = new CharacterData();
    }
    private void Start()
    {
        ReGenerate();
    }
    [ContextMenu("Generate List")]
    private void GenerateList()
    {
        if (Prefab_Manager._executor == null)
        {
            Debug.LogError("Îבתÿגט Ïנופאב Ìוםוהזונ");
            return;
        }
       
        transform.localScale = new Vector3(1f, 1f);
        RectTransform temp = BaseData().GetComponent<RectTransform>();
        RectTransform temp2 = Attributes().GetComponent<RectTransform>();
        RectTransform temp3 = Skills().GetComponent<RectTransform>();
        RectTransform temp4 = Advantages().GetComponent<RectTransform>();
        temp2.anchoredPosition = new Vector2(0, Utility.FindLowestRectTransform(temp).y + Prefab_Manager.Shift()*-2);
        Debug.Log(temp3.anchoredPosition);
        temp3.anchoredPosition = new Vector2(0, -80);
        Debug.Log(temp3.anchoredPosition);
        transform.localScale = new Vector3(0.8f, 0.8f);
       
    }
    private GameObject BaseData()
    {
        float width = 0;
        GameObject _BaseData = new GameObject("BaseData");
        RectTransform _BaseData_transform = _BaseData.AddComponent<RectTransform>();
        _BaseData_transform.SetParent(transform);
        _BaseData_transform.anchoredPosition = Vector2.zero;
        int count = 0;
        int bigCount = -1;
        for (int i = 0; i < _data.BaseData.Length; ++i)
        {
            if(Mathf.Abs(count) % 3 == 0)
            {
                count = 0;
                bigCount++;
            }
            var cur = _data.BaseData[i];
            GameObject temp = new GameObject(cur.fixedData);
            RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
            tempRectTransform.SetParent(_BaseData_transform);
            temp.AddComponent<str_Data_Manager>().Init(cur);
            Vector2 newPosition = new Vector2(Prefab_Manager._executor.TextBox_SizeSettings.x * bigCount * 2, Prefab_Manager._executor.TextBox_SizeSettings.y * count);
            tempRectTransform.anchoredPosition = newPosition;
            width += tempRectTransform.anchoredPosition.x;
            count--;
        }
        return _BaseData;
    }
    private GameObject Attributes()
    {
        float width = 0;
        GameObject _Attributes = new GameObject("Attributes");
        RectTransform _Attributes_transform = _Attributes.AddComponent<RectTransform>();
        _Attributes_transform.SetParent(transform);
        _Attributes_transform.anchoredPosition = Vector2.zero;
        for (int i = 0; i < _data.Attributes.Length; ++i)
        {
            var cur = _data.Attributes[i];
            cur._isAttribute = true;
            GameObject temp = new GameObject(cur._name);
            RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
            tempRectTransform.SetParent(_Attributes_transform);
            Vector2 Shift = temp.AddComponent<Block_CheckBoxManagers>().Init(cur).Max;
            Vector2 newPosition = i == 0 ?
                Vector2.zero
                : new Vector2(width + Prefab_Manager._executor.TextBox_SizeSettings.x * 2.5f, 0);
            tempRectTransform.anchoredPosition = newPosition;
            width += tempRectTransform.anchoredPosition.x;
        }
        return _Attributes;
    }
    private GameObject Advantages()
    {
        float width = 0;
        GameObject _Advantages = new GameObject("Advantages");
        RectTransform _Advantages_transform = _Advantages.AddComponent<RectTransform>();
        _Advantages_transform.SetParent(transform);
        _Advantages_transform.anchoredPosition = Vector2.zero;
        for (int i = 0; i < _data.Advantages.Length; ++i)
        {
            var cur = _data.Advantages[i];
            GameObject temp = new GameObject(cur._name);
            RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
            tempRectTransform.SetParent(_Advantages_transform);
            Vector2 Shift = temp.AddComponent<Block_CheckBoxManagers>().Init(cur).Max;
            Vector2 newPosition = i == 0 ?
                Vector2.zero
                : new Vector2(width + Prefab_Manager._executor.TextBox_SizeSettings.x * 2.5f, 0);
            tempRectTransform.anchoredPosition = newPosition;
            width += tempRectTransform.anchoredPosition.x;
        }
        return _Advantages;
    }
    private GameObject Skills()
    {
        float height = 0;
        GameObject _Skills = new GameObject("Skills");
        RectTransform _Skills_transform = _Skills.AddComponent<RectTransform>();
        _Skills_transform.SetParent(transform);
        _Skills_transform.anchoredPosition = Vector2.zero;
        for (int i = 0; i < _data.Skills.Length; ++i)
        {
            var cur = _data.Skills[i];
            GameObject temp = new GameObject(cur._name);
            RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
            tempRectTransform.SetParent(_Skills_transform);
            float delt = (cur._attributes.Length + 1) / 2f;
            Vector2 Shift = temp.AddComponent<Block_CheckBoxManagers>().Init(cur).Max;
            Vector2 newPosition = new Vector2(0, height - Prefab_Manager._executor.TextBox_SizeSettings.y * delt);
            tempRectTransform.anchoredPosition = newPosition;
            height -= Prefab_Manager._executor.TextBox_SizeSettings.y * delt;
        }
        return _Skills;
    }

    [ContextMenu("Clear List")]
    public void ClearList()
    {
        var children = new System.Collections.Generic.List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => DestroyImmediate(child));
    }

    [ContextMenu("ReCreate List")]
    public void ReGenerate()
    {
        ClearList();
        GenerateList();
    }

    public Character ApplyData(CharacterData Data)
    {
        _data = Data;
        return this;
    }
}

[System.Serializable]
public class CharacterData
{
    public str_Data[] BaseData;
    public GroupCounters[] Attributes;
    public GroupCounters[] Skills;
    public GroupCounters[] Advantages;
    // Çהמנמגüו????
    public Stats stats;

    public CharacterData()
    {
        Attributes = new GroupCounters[BaseNameLib.EAttributes.Length];
        for (int i = 0; i < BaseNameLib.EAttributes.Length; ++i)
        {
            Attributes[i] = new GroupCounters(BaseNameLib.ECategories[i], BaseNameLib.EAttributes[i].Length, BaseNameLib.EAttributes[i], 1);
        }
        Skills = new GroupCounters[BaseNameLib.ESkills.Length];
        for (int i = 0; i < BaseNameLib.ESkills.Length; ++i)
        {
            Skills[i] = new GroupCounters(BaseNameLib.ECategories[i], BaseNameLib.ESkills[i].Length, BaseNameLib.ESkills[i]);
        }
        BaseData = new str_Data[BaseNameLib.EBase.Length];
        for (int i = 0; i < BaseNameLib.EBase.Length; ++i)
        {
            BaseData[i] = new str_Data(BaseNameLib.EBase[i]);
        }
    }
    public CharacterData(CharacterData Another)
    {
        Attributes = Another.Attributes;
        Skills = Another.Skills;
        BaseData = Another.BaseData;
    }
    public CharacterData SetData(CharacterData Data)
    {
        Attributes = Data.Attributes;
        Skills = Data.Skills;
        BaseData = Data.BaseData;
        return this;
    }
}