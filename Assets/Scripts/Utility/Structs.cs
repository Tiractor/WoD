using UnityEngine;

[System.Serializable]
public class Counter
{
    [System.NonSerialized] public GroupCounters ParentCounters;
    public str_Data name_spec;
    public int curValue;
    //public string description;
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
    public bool _notNeedInputField = false;
    public bool _notNeedFixedText = false;
    public bool _notNeedCheckBoxes = false;
    public int _maxValue;
    public string _name;
    public Counter[] _attributes;
    public GroupCounters(string Name)
    {
        _name = Name;
        _maxValue = 5;
        _attributes = new Counter[0];
    }
    public GroupCounters(string Name, int CountOfCounters, int MaxValue)
    {
        _name = Name;
        _maxValue = MaxValue;
        _attributes = new Counter[CountOfCounters];
        for (int i = 0; i < CountOfCounters; ++i)
            _attributes[i] = new Counter(this, i.ToString());

    }
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
    public GroupCounters(string Name, int CountOfCounters, string[] NamesOfCounters, int BaseValue, int MaxValue)
    {
        if (NamesOfCounters.Length != CountOfCounters) Debug.LogError("Count of Names != Count of Counters");
        _name = Name;
        _maxValue = MaxValue;
        _attributes = new Counter[CountOfCounters];
        for (int i = 0; i < CountOfCounters; ++i)
        {
            _attributes[i] = new Counter(this, NamesOfCounters[i]);
            _attributes[i].SetValue(BaseValue);
        }
    }
    public void addNewAttribute()
    {
        Counter[] temp = new Counter[_attributes.Length + 1];
        for(int i = 0; i < _attributes.Length; ++i)
        {
            temp[i] = _attributes[i];
        }
        temp[_attributes.Length] = new Counter(this, "");
        _attributes = temp;
    }
}
[System.Serializable]
public class Attack
{
    public string Name;
    public string Dmg;
    public string Range;
    public string Clip;
    public string Init;
    public string Str;
    public string Size;
    public void SetData(Attacks_UI Data)
    {
        Name = Data.Name.text;
        Dmg = Data.Dmg.text;
        Range = Data.Range.text;
        Clip = Data.Clip.text;
        Init = Data.Init.text;
        Str = Data.Str.text;
        Size = Data.Size.text;
    }
}
[System.Serializable]
public class Equipment
{
    public string Name;
    public string Durability;
    public string Structure;
    public string Size;
    public string Cost;
    public void SetData(Equipments_UI Data)
    {
        Name = Data.Name.text;
        Durability = Data.Durability.text;
        Structure = Data.Structure.text;
        Size = Data.Size.text;
        Cost = Data.Cost.text;
    }
}
[System.Serializable]
public class Datas<T> where T : new()
{
    public T[] _data;
    public Datas()
    {
        _data = new T[0];
    }
    public void AddNewElement()
    {
        Debug.Log(_data.GetType() + " " + _data.Length);
        T[] temp = new T[_data.Length + 1];
        for (int i = 0; i < _data.Length; ++i)
        {
            temp[i] = _data[i];
        }
        temp[_data.Length] = new T();
        _data = temp;
    }
}
[System.Serializable]
public class Health 
{
    public string _name;
    public GroupCounters _max_Health;
    public GroupCounters _cur_Health;
    public Health()
    {
        _max_Health = new GroupCounters("Max_Health", 1, new string[] { "Max_Health" },0,12);
        _cur_Health = new GroupCounters("Cur_Health", 12, 3);
    }
    public Health(string Name)
    {
        _name = Name;
        _max_Health = new GroupCounters("Max_Health", 1, new string[] { "Max_Health" }, 0, 12);
        _cur_Health = new GroupCounters("Cur_Health", 12, 3);
    }
    public void addNewAttribute()
    {
        _max_Health.addNewAttribute();
        _cur_Health.addNewAttribute();
    }
}

[System.Serializable]
public class Expirience
{
    public GroupCounters Beats;
    public str_Data Exp;
    public Expirience()
    {
        Beats = new GroupCounters(BaseNameLib.EOtherTraits[6], 1, 5);
        Beats._notNeedFixedText = true;
        Beats._notNeedInputField = true;
        Exp = new str_Data(BaseNameLib.EOtherTraits[7]);
    }
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
        "Интеллект",
        "Сообразительность",
        "Решительность",
    };
    public static string[] EAttributes_Physical = new string[]
    {
        "Сила",
        "Ловкость",
        "Выносливость",
    };
    public static string[] EAttributes_Social = new string[]
    {
        "Внушительность",
        "Манипулировние",
        "Самообладание"
    };

    public static string[][] EAttributes = new string[][]
    {
        EAttributes_Mental,
        EAttributes_Physical,
        EAttributes_Social
    };

    public static string[] ESkills_Mental = new string[]
    {
       "Образование",
       "Компьютер",
       "Ремесло" ,
       "Расследование" ,

       "Медицина" ,
       "Оккультизм" ,
       "Политика" ,
       "Наука"
    };
    public static string[] ESkills_Physical = new string[]
    {
        "Атлетика",
        "Драка",
        "Вождение",
        "Стрельба",

        "Кража",
        "Скрытность",
        "Выживание",
        "Холодное Оружие",
    };
    public static string[] ESkills_Social = new string[]
    {
        "Знание Животных",
        "Эмпатия",
        "Экспрессия",
        "Запугивание",

        "Убеждение",
        "Коммуникабельность",
        "Знание Улиц",
        "Обман",
    };

    public static string[][] ESkills = new string[][]
    {
        ESkills_Mental,
        ESkills_Physical,
        ESkills_Social
    };

    public static string[] EBase = new string[]
    {
        "Имя",
        "Игрок",
        "Возраст",

        "Добродетель",
        "Порок",
        "Концепция",

        "Хроника",
        "Фракция",
        "Группа"
    };

    public static string[] ECategories = new string[]
    {
        "Ментальные",
        "Физические",
        "Социальные"
    };
    public static string[] ELowData = new string[]
    {
        "Размер",
        "Скорость",
        "Защита",
        "Броня",
        "Инициатива"
    };
    public static string[] EOtherTraits = new string[]
    {
        "Преимущества",
        "Здоровье",
        "Воля",
        "Целостность",
        "Состояния",
        "Стремления",
        "Вехи",
        "Опыт"
    };
    public static string[] EAttack = new string[]
    {
        "Атака",
        "Урон",
        "Дистанция",
        "Магазин",
        "Инициатива",
        "Сила",
        "Размер",
        
    };
    public static string[] EEquipment = new string[]
    {
        "Экипировка",
        "Прочность",
        "Структура",
        "Размер",
        "Цена",
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

    public static void CopyToClipboard(string text)
    {
        TextEditor te = new TextEditor();
        te.text = text;
        te.SelectAll();
        te.Copy();
    }
}