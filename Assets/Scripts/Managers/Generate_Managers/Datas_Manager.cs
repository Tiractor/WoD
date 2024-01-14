using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Attacks_UI 
{
    public TMPro.TMP_InputField Name;
    public TMPro.TMP_InputField Dmg;
    public TMPro.TMP_InputField Range;
    public TMPro.TMP_InputField Clip;
    public TMPro.TMP_InputField Init;
    public TMPro.TMP_InputField Str;
    public TMPro.TMP_InputField Size;
}
public class Equipments_UI
{
    public TMPro.TMP_InputField Name;
    public TMPro.TMP_InputField Durability;
    public TMPro.TMP_InputField Structure;
    public TMPro.TMP_InputField Size;
    public TMPro.TMP_InputField Cost;
}

public class Datas_Manager : MonoBehaviour
{
    List<Attacks_UI> Attacks_IF = new List<Attacks_UI>();
    List<Equipments_UI> Equipments_IF = new List<Equipments_UI>();
    [SerializeField] private Datas<Attack> Attacks;
    [SerializeField] private Datas<Equipment> Equipments;
    GridLayoutGroup Layout;
    public void Init(Datas<Attack> Connect)
    {
        Attacks = Connect;
        Layout = gameObject.AddComponent<GridLayoutGroup>();
        Layout.cellSize = Prefab_Manager._executor.TableCellSize_SizeSettings;
        Layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        Layout.constraintCount = 7;
        Layout.spacing = new Vector2(20, 20);
        ContentSizeFitter tempFitter = gameObject.AddComponent<ContentSizeFitter>();
        tempFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        tempFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        GenerateText(BaseNameLib.EAttack);
        AddButton_NewAttack();
    }
    public void Init(Datas<Equipment> Connect)
    {
        Equipments = Connect;
        Layout = gameObject.AddComponent<GridLayoutGroup>();
        Layout.cellSize = Prefab_Manager._executor.TableCellSize_SizeSettings;
        Layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        Layout.constraintCount = 5;
        Layout.spacing = new Vector2(50,50);
        ContentSizeFitter tempFitter = gameObject.AddComponent<ContentSizeFitter>();
        tempFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        tempFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        GenerateText(BaseNameLib.EEquipment);
        AddButton_NewEquipment();
    }

    void GenerateText(string[] Names)
    {
        foreach (var cur in Names)
        {
            GenerateTextSolo(cur);
        }
    }

    private void GenerateTextSolo(string Name)
    {
        GameObject temp_Text = new GameObject(Name + "_Text");
        RectTransform tempRectTransform = temp_Text.AddComponent<RectTransform>();
        tempRectTransform.SetParent(transform);
        tempRectTransform.anchorMin = Prefab_Manager._executor.AnchorsText[0];
        tempRectTransform.anchorMax = Prefab_Manager._executor.AnchorsText[1];
        TMPro.TextMeshProUGUI temp = temp_Text.AddComponent<TMPro.TextMeshProUGUI>();
        temp.text = Name;
        temp.fontSizeMax = 36;
        temp.enableAutoSizing = true;
        temp.font = Prefab_Manager._executor.Font;
        temp.alignment = TMPro.TextAlignmentOptions.MidlineLeft;
        temp.enableWordWrapping = false;
        tempRectTransform.sizeDelta = Prefab_Manager._executor.TextBox_SizeSettings;
        tempRectTransform.anchoredPosition = Vector2.zero;
    }
    public void CreateNewAttack()
    {
        Attacks.AddNewElement();
        Attacks_UI tempAttack = new Attacks_UI();
        RectTransform tempParentRectTransform = GetComponent<RectTransform>();
        int temp_int = Attacks._data.Length - 1;
        tempAttack.Name = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        Debug.Log(tempAttack.Name);
        tempAttack.Name
            .onValueChanged.AddListener
            (delegate
                {
                    Attacks._data[temp_int].SetData(tempAttack);
                }
            );
        tempAttack.Dmg = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempAttack.Dmg.onValueChanged.AddListener
            (delegate
            {
                Attacks._data[temp_int].SetData(tempAttack);
            }
            );
        tempAttack.Range = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempAttack.Range.onValueChanged.AddListener
            (delegate
            {
                Attacks._data[temp_int].SetData(tempAttack);
            }
            );
        tempAttack.Clip = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempAttack.Clip.onValueChanged.AddListener
            (delegate
            {
                Attacks._data[temp_int].SetData(tempAttack);
            }
            );
        tempAttack.Init = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempAttack.Init.onValueChanged.AddListener
            (delegate
            {
                Attacks._data[temp_int].SetData(tempAttack);
            }
            );
        tempAttack.Str = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempAttack.Str.onValueChanged.AddListener
            (delegate
            {
                Attacks._data[temp_int].SetData(tempAttack);
            }
            );
        tempAttack.Size = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempAttack.Size.onValueChanged.AddListener
            (delegate
            {
                Attacks._data[temp_int].SetData(tempAttack);
            }
            );
        Attacks_IF.Add(tempAttack);
    }

    public void CreateNewEquipment()
    {
        Debug.Log(name);
        Debug.Log(Equipments);
        Equipments.AddNewElement();
        Equipments_UI tempUI = new Equipments_UI();
        RectTransform tempParentRectTransform = GetComponent<RectTransform>();


        tempUI.Name = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempUI.Name.onValueChanged.AddListener
            (delegate
            {
                Equipments._data[Equipments._data.Length - 1].SetData(tempUI);
            }
            );
        tempUI.Durability = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempUI.Durability.onValueChanged.AddListener
            (delegate
            {
                Equipments._data[Equipments._data.Length - 1].SetData(tempUI);
            }
            );
        tempUI.Structure = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempUI.Structure.onValueChanged.AddListener
            (delegate
            {
                Equipments._data[Equipments._data.Length - 1].SetData(tempUI);
            }
            );
        tempUI.Size = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempUI.Size.onValueChanged.AddListener
            (delegate
            {
                Equipments._data[Equipments._data.Length - 1].SetData(tempUI);
            }
            );
        tempUI.Cost = Instantiate(Prefab_Manager._executor.InputField, tempParentRectTransform).GetComponent<TMPro.TMP_InputField>();
        tempUI.Cost.onValueChanged.AddListener
            (delegate
            {
                Equipments._data[Equipments._data.Length - 1].SetData(tempUI);
            }
            );

        Equipments_IF.Add(tempUI);
    }

    public void AddButton_NewAttack()
    {
        Button temp = Instantiate(Prefab_Manager._executor.Button_AddCounter, transform.parent)
            .GetComponent<Button>();

        temp.onClick.AddListener
            (delegate
                {
                    CreateNewAttack();
                    RebuildLayout._executor.Rebuild();
                }
            );
    }
    public void AddButton_NewEquipment()
    {
        Button temp = Instantiate(Prefab_Manager._executor.Button_AddCounter, transform.parent)
            .GetComponent<Button>();

        temp.onClick.AddListener
            (delegate
            {
                CreateNewEquipment();
                RebuildLayout._executor.Rebuild();
            }
            );
    }
}
