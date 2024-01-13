using UnityEngine;
[System.Serializable]
public class Character : MonoBehaviour
{
    public CharacterData _data;
    public GameObject ConnectedButton;
    public Layout_Manager ConnectedOutput;
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
        BaseData();
        Attributes();
        Skills();
        //RectTransform temp4 = Advantages().GetComponent<RectTransform>();
        transform.localScale = new Vector3(0.8f, 0.8f);
    }
    private void BaseData()
    {
        foreach (var cur in _data.BaseData)
        {
            ConnectedOutput.addElement_BaseData(cur);
        }
    }
    private void Attributes()
    {
        foreach (var cur in _data.Attributes)
        {
            ConnectedOutput.addElement_Attributes(cur);
        }
    }
    private void Skills()
    {
        foreach (var cur in _data.Skills)
        {
            ConnectedOutput.addElement_Skills(cur);
        }
    }
    /*private GameObject Advantages()
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
    }*/
    /*private GameObject Sk1ills()
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
    }*/

    [ContextMenu("Clear List")]
    public void ClearList()
    {
        ConnectedOutput.Clear();
    }

    [ContextMenu("ReCreate List")]
    public void ReGenerate()
    {
        ClearList();
        _data = new CharacterData();
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