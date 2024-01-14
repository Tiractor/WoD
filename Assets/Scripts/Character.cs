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
            Debug.LogError("Объяви Префаб Менеджер");
            return;
        }
       
        transform.localScale = new Vector3(1f, 1f);
        BaseData();
        Attributes();
        Skills();
        Merits();
        LowData();
        Health();
        transform.localScale = new Vector3(0.8f, 0.8f);
        ConnectedOutput.Refresh();
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
    private void Merits()
    {
        
        ConnectedOutput.addElement_Merits(_data.Merits);
    }
    private void LowData()
    {
        foreach (var cur in _data.LowData)
        {
            ConnectedOutput.addElement_LowData(cur);
        }
    }
    private void Health()
    {
            ConnectedOutput.addElement_ImportantCounters(_data.Health);
    }
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
    public GroupCounters Merits;
    public Health Health;
    public Stats stats;
    public str_Data[] LowData;


    public CharacterData()
    {
        
        Attributes = new GroupCounters[BaseNameLib.EAttributes.Length];
        for (int i = 0; i < BaseNameLib.EAttributes.Length; ++i)
        {
            Attributes[i] = new GroupCounters(BaseNameLib.ECategories[i], BaseNameLib.EAttributes[i].Length, BaseNameLib.EAttributes[i], 1);
            Attributes[i]._isAttribute = true;
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
        LowData = new str_Data[BaseNameLib.ELowData.Length];
        for (int i = 0; i < BaseNameLib.ELowData.Length; ++i)
        {
            LowData[i] = new str_Data(BaseNameLib.ELowData[i]);
        }
        Merits = new GroupCounters("Преимущества");
        Merits._isMerit = true;
        Health = new Health("Здоровье");
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
        Merits = Data.Merits;
        stats = Data.stats;
        return this;
    }
}