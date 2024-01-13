using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Manager : TaskExecutor<Character_Manager>
{
    [SerializeField] private List <Character> Characters;
    [SerializeField] private List<Button> Button_Characters;
    [SerializeField] private GameObject WhereInst;
    [SerializeField] private GameObject WhereButtons;
    [SerializeField] private Character Open_Character;
    private void Awake()
    {
        Denote();
        if (WhereInst == null) Debug.LogError("WhereInst not Choosen");
    }
    [ContextMenu("Create new blank Character")]
    public Character CreateCharacter()
    {
        GameObject temp = new GameObject("Character_" + Characters.Count);
        RectTransform tempRectTransform = temp.AddComponent<RectTransform>();
        
        tempRectTransform.anchorMax = new Vector2(0, 1);
        tempRectTransform.anchorMin = new Vector2(0, 1);
        tempRectTransform.SetParent(WhereInst.transform);
        tempRectTransform.anchoredPosition = new Vector2(450,-50);
        Character tempChar = temp.AddComponent<Character>();
        Characters.Add(tempChar);
        OpenCharacter(tempChar);
        tempChar.ConnectedButton = GenerateButton(tempChar);
        
        return tempChar;
    }
    private GameObject GenerateButton(Character Target)
    {
        Character tempChar = Target;
        GameObject temp = Instantiate(Prefab_Manager._executor.Button_Character, WhereButtons.transform);
        temp.name = "ButtonOfCharacter_" + (Characters.Count-1);
        Button tempButton = temp.GetComponent<Button>();
        RectTransform tempRectTransform = temp.GetComponent<RectTransform>();
        temp.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Target._data.BaseData[0].changeableData;
        tempRectTransform.anchoredPosition = Prefab_Manager._executor.Button;
        tempRectTransform.anchoredPosition -= new Vector2 (0, Prefab_Manager.Shift() + Button_Characters.Count * (150 + Prefab_Manager.Shift()));
        Debug.Log(tempChar + " " + temp);
        GenerateCloseButton(tempChar, temp);
        tempButton.onClick.AddListener(delegate {
            OpenCharacter(tempChar);
        }
        );
        Button_Characters.Add(tempButton);
        return temp;
    }
    private void GenerateCloseButton(Character Target, GameObject parent)
    {
        Character tempChar = Target;
        GameObject temp = Instantiate(Prefab_Manager._executor.Button_CloseCharacter, parent.transform);
        temp.name = "Close_ButtonOfCharacter_" + (Characters.Count-1);
        temp.GetComponentInChildren<Button>().onClick.AddListener(delegate {
            CloseCharacter(tempChar);
        }
        );
    }
    public void CreateCharacter(CharacterData Load)
    {
        CreateCharacter().ApplyData(Load);
    }
    [ContextMenu("Clear Character List")]
    public void ClearCharacters()
    {
        foreach(var cur in Characters)
        {
            DestroyImmediate(cur.gameObject);
        }
        Characters.Clear();
        foreach (var cur in Button_Characters)
        {
            DestroyImmediate(cur.gameObject);
        }
        Button_Characters.Clear();
    }
    [ContextMenu("Save all Characters")]
    public void SaveCharacters()
    {
        foreach (var cur in Characters)
        {
            FileManager.SaveToFile(cur.gameObject.name, cur._data);
        }
    }
    [ContextMenu("Load all Characters")]
    public void LoadCharacters()
    {
        string[] files = FileManager.GetAllCharacterFiles();

        foreach (var filePath in files)
        {
            CharacterData loadedCharacter = FileManager.LoadFromFile<CharacterData>(System.IO.Path.GetFileNameWithoutExtension(filePath));
            if (loadedCharacter != null)
            {
                CreateCharacter(loadedCharacter);
            }
        }
    }
    public void OpenCharacter(Character Target)
    {
        if (Target == Open_Character || Target == null) return;
        if (Open_Character != null) Open_Character.gameObject.SetActive(false);
        Open_Character = Target;
        Open_Character.gameObject.SetActive(true);
        Open_Character.ReGenerate();
    }
    public void CloseCharacter(Character Target)
    {
        bool OpeningNC = false;
        if (Open_Character == Target) OpeningNC = true;
        DestroyImmediate(Target.ConnectedButton);
        Characters.Remove(Target);
        DestroyImmediate(Target.gameObject);


        if (OpeningNC) OpenCharacter(Characters[0]);
    }
}
