using UnityEngine;

[System.Serializable]
public class Counter
{
    [System.NonSerialized] public GroupCounters ParentCounters;
    public str_Data name_spec;
    public int curValue;
    public string description;
    public Counter(GroupCounters parent, string Name)
    {
        ParentCounters = parent;
        name_spec = new str_Data(Name);
        curValue = 0;
    }

    public void SetValue(int Value)
    {
        if (ParentCounters._maxValue < Value) Debug.LogWarning("Trying set Value bigger than Max - " + Value);
        curValue = Mathf.Clamp(Value, 0, ParentCounters._maxValue);
    }
}
[System.Serializable]
public class GroupCounters
{
    public bool _isAttribute = false;
    public int _maxValue;
    public string _name;
    public Counter[] _attributes;
    public GroupCounters(string Name, int CountOfCounters, string[] NamesOfCounters)
    {
        if(NamesOfCounters.Length != CountOfCounters) Debug.LogError("Count of Names != Count of Counters");
        _name = Name;
        _maxValue = 5;
        _attributes = new Counter[CountOfCounters];
        for (int i = 0; i < CountOfCounters; ++i)
            _attributes[i] = new Counter(this, NamesOfCounters[i]);
        
    }
    public GroupCounters(string Name, int CountOfCounters, string[] NamesOfCounters, int BaseValue)
    {
        if (NamesOfCounters.Length != CountOfCounters) Debug.LogError("Count of Names != Count of Counters");
        _name = Name;
        _maxValue = 5;
        _attributes = new Counter[CountOfCounters];
        for (int i = 0; i < CountOfCounters; ++i) 
        {
            _attributes[i] = new Counter(this, NamesOfCounters[i]);
            _attributes[i].SetValue(BaseValue);
        }
    }
}

public class Stats
{
    [Header("Base")]
    public int max_Health;
    public int[] cur_Health;
    public int Will;
    public int Sanity;
    [Header("Fight Stats")]
    public int Size;
    public int Speed;
    public int Defense;
    public int Armor;
    public int Initiative;
    [Header("Exp")]
    public int Mark;
    public int Exp;
}

[System.Serializable]
public class str_Data
{
    public string fixedData;
    public string changeableData;
    public str_Data(string FixedData)
    {
        fixedData = FixedData;
    }
    public void ApplyData(TMPro.TMP_InputField FromWhat)
    {
        changeableData = FromWhat.text;
    }
}
class BaseNameLib {
    public static string[] EAttributes_Mental = new string[]
    {
        "���������",
        "�����������������",
        "�������������",
    };
    public static string[] EAttributes_Physical = new string[]
    {
        "����",
        "��������",
        "������������",
    };
    public static string[] EAttributes_Social = new string[]
    {
        "��������������",
        "��������������",
        "�������������"
    };

    public static string[][] EAttributes = new string[][]
    {
        EAttributes_Mental,
        EAttributes_Physical,
        EAttributes_Social
    };

    public static string[] ESkills_Mental = new string[]
    {
       "�����������",
       "���������",
       "�������" ,
       "�������������" ,

       "��������" ,
       "����������" ,
       "��������" ,
       "�����"
    };
    public static string[] ESkills_Physical = new string[]
    {
        "��������",
        "�����",
        "��������",
        "��������",

        "�����",
        "����������",
        "���������",
        "�������� ������",
    };
    public static string[] ESkills_Social = new string[]
    {
        "������ ��������",
        "�������",
        "����������",
        "�����������",

        "���������",
        "������������������",
        "������ ����",
        "�����",
    };

    public static string[][] ESkills = new string[][]
    {
        ESkills_Mental,
        ESkills_Physical,
        ESkills_Social
    };

    public static string[] EBase = new string[]
    {
        "���",
        "�����",
        "�������",

        "�����������",
        "�����",
        "���������",

        "�������",
        "�������",
        "������"
    };

    public static string[] ECategories = new string[]
    {
        "����������",
        "����������",
        "����������"
    };
}

public static class Utility
{
    public static Vector2 FindLowestRectTransform(RectTransform Target)
    {
        return FindLowestRectTransform(Target.gameObject);
    }
    public static Vector2 FindLowestRectTransform(GameObject Target)
    {
        RectTransform lowest = null;
        float lowestY = float.PositiveInfinity;

        // Iterate through all children with RectTransform component
        foreach (RectTransform rectTransform in Target.GetComponentsInChildren<RectTransform>())
        {
            // Check the Y position of the lower edge of the RectTransform
            float currentY = rectTransform.rect.y + rectTransform.anchoredPosition.y;

            // Update the lowest RectTransform found
            if (currentY < lowestY)
            {
                lowestY = currentY;
                lowest = rectTransform;
            }
        }

        // Check if a RectTransform was found and return its lower boundary coordinates
        if (lowest != null)
        {
            return new Vector2(lowest.anchoredPosition.x, lowestY);
        }

        // Default return value, in case no RectTransforms are found
        return Vector2.zero;
    }
}