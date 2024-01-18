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
        ImportantCounters();
        Textes();
        Equip();
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
        ConnectedOutput.addElement_Expirirnce(_data.Exp);
    }
    private void ImportantCounters()
    {
        ConnectedOutput.addElement_ImportantCounters(_data.Health);
        ConnectedOutput.addElement_ImportantCounters(_data.Will);
        ConnectedOutput.addElement_Sanity(_data.Sanity);
    }
    private void Textes()
    {
        ConnectedOutput.addElement_Conditions(_data.Conditions);
        ConnectedOutput.addElement_Aspirations(_data.Aspirations);
    }
    private void Equip()
    {
        ConnectedOutput.addElement_Equipment(_data.Attacks, _data.Equipments);
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
        GenerateList();
    }

    public Character ApplyData(CharacterData Data)
    {
        _data = Data;
        ReGenerate();
        return this;
    }
}

[System.Serializable]
public class CharacterData
{
    public int idBase;
    public str_Data[] BaseData;
    public GroupCounters[] Attributes;
    public GroupCounters[] Skills;
    public GroupCounters Merits;
    public Health Health;
    public GroupCounters Will;
    public GroupCounters Sanity;
    public GroupCounters Conditions;
    public GroupCounters Aspirations;
    public Expirience Exp;
    public str_Data[] LowData;
    public Datas<Attack> Attacks;
    public Datas<Equipment> Equipments;

    public CharacterData()
    {
        idBase = -1;
        Attributes = new GroupCounters[BaseNameLib.EAttributes.Length];
        for (int i = 0; i < BaseNameLib.EAttributes.Length; ++i)
        {
            Attributes[i] = new GroupCounters(BaseNameLib.ECategories[i], BaseNameLib.EAttributes[i].Length, BaseNameLib.EAttributes[i], 1);
            Attributes[i]._notNeedInputField = true;
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
        Merits = new GroupCounters(BaseNameLib.EOtherTraits[0]);
        Merits._notNeedFixedText = true;
        Health = new Health(BaseNameLib.EOtherTraits[1]);
        Will = new GroupCounters(BaseNameLib.EOtherTraits[2], 2,10);
        Sanity = new GroupCounters(BaseNameLib.EOtherTraits[3], 1, 10);
        Sanity._notNeedFixedText = true;
        Sanity._notNeedInputField = true;
        Conditions = new GroupCounters(BaseNameLib.EOtherTraits[4], 1, 10);
        Conditions._notNeedFixedText = true;
        Conditions._notNeedCheckBoxes = true;
        Aspirations = new GroupCounters(BaseNameLib.EOtherTraits[5], 1, 10);
        Aspirations._notNeedFixedText = true;
        Aspirations._notNeedCheckBoxes = true;
        Exp = new Expirience();
        Attacks = new Datas<Attack>();
        Equipments = new Datas<Equipment>();
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
        Will = Data.Will;
        Exp = Data.Exp;
        Health = Data.Health;
        Sanity = Data.Sanity;
        LowData = Data.LowData;

        return this;
    }
}