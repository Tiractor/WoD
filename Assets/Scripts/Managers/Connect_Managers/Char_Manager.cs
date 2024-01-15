using UnityEngine;
[System.Serializable]
public class UploadCharacterData
{
    public string Login;
    public string CharacterName;
    public int CharacterId;
    public CharacterData CharacterData;
}
[System.Serializable]
public class GettedCharacterData
{
    public string CharacterName;
    public int CharacterId;
    public CharacterData CharacterData;
}

[System.Serializable]
public class DataToGetCharacter : ChooseData
{
    public string Login;
    public DataToGetCharacter(ChooseData Data)
    {
        CharacterName = Data.CharacterName;
        idPlayerCharacter = Data.idPlayerCharacter;
    }
}
[System.Serializable]
public class Request
{
    public string Login;
    public string CharacterName;
    public CharacterData CharacterData;
}
public class Char_Manager : TaskExecutor<Char_Manager>
{
    [SerializeField] private GameObject WhereInst;
    [SerializeField] private GameObject WhereInst_Group;
    private void Awake()
    {
        Denote();
    }
    public static void PrepareCharData(CharacterData Upload)
    {
        UploadCharacterData Char = new UploadCharacterData();
        Char.CharacterName = Upload.BaseData[0].changeableData;
        Char.Login = Authorization._executor.UserData.Login;
        Char.CharacterData = Upload;
        Char.CharacterId = Upload.idBase;
        Connector.Request_UploadCharacter(Char);
    }
    public static void LoadChar(ChooseData Which)
    {
        DataToGetCharacter tempdata = new DataToGetCharacter(Which);
        tempdata.Login = Authorization._executor.UserData.Login;
        string temp = Connector.Request_GetCharacter(tempdata);
        Debug.Log(temp);
        GettedCharacterData data = JsonUtility.FromJson<GettedCharacterData>(temp);
        Debug.Log(data.CharacterData);
        Character_Manager._executor.CreateCharacter(data.CharacterData);
    }
    public static void JoinChar(ChooseData Which)
    {
        CharacterJoinData tempdata = new CharacterJoinData(Which);
        tempdata.Login = Authorization._executor.UserData.Login;
        tempdata.idUserGroup = Group_Manager._executor.CurrentGroup.idUserGroup;
        Connector.Request_JoinCharacter(tempdata);
    }
    public void UserCharactersServer()
    {
        RequestData data = new RequestData();
        data.Login = Authorization._executor.UserData.Login;
        string temp = Connector.Request_UserCharacters(data);
        if (temp.Contains("[]")) return;
        ChooseDataArray chooseDataArray = JsonUtility.FromJson<ChooseDataArray>("{\"characters\":" + temp + "}");
        
        if (chooseDataArray.characters != null)
        {
            foreach (var chooseData in chooseDataArray.characters)
            {
                Character_Choose characterChoose = Instantiate(Prefab_Manager._executor.Button_ChooseCharacter, WhereInst.transform).GetComponent<Character_Choose>();
                characterChoose.Init(chooseData, true);
            }
        }
        else
        {
            Debug.LogError("Ошибка: массив characters не удалось десериализовать или temp не является валидным JSON.");
        }
    }
    public void UserCharactersServerForJoin()
    {
        RequestData data = new RequestData();
        data.Login = Authorization._executor.UserData.Login;
        string temp = Connector.Request_UserCharacters(data);
        ChooseDataArray chooseDataArray = JsonUtility.FromJson<ChooseDataArray>("{\"characters\":" + temp + "}");
        if (temp.Contains("[]")) return;
        if (chooseDataArray.characters != null)
        {
            foreach (var chooseData in chooseDataArray.characters)
            {
                Character_Choose characterChoose = Instantiate(Prefab_Manager._executor.Button_JoinCharacter, WhereInst.transform).GetComponent<Character_Choose>();
                characterChoose.Init(chooseData, true);
            }
        }
        else
        {
            Debug.LogError("Ошибка: массив characters не удалось десериализовать или temp не является валидным JSON.");
        }
    }
    public void GroupCharactersServer(GroupCharacterRequest data)
    {
        string temp = Connector.Request_GroupCharacters(data);
        Group_Manager._executor.WhereChar.SetActive(true);
        Debug.Log(temp);
        if (temp == "") return;
        ChooseDataArray chooseDataArray = JsonUtility.FromJson<ChooseDataArray>("{\"characters\":" + temp + "}");

        if (chooseDataArray.characters != null)
        {
            foreach (var chooseData in chooseDataArray.characters)
            {
                Character_Choose characterChoose = Instantiate(Prefab_Manager._executor.Button_ChooseCharacter, WhereInst_Group.transform).GetComponent<Character_Choose>();
                characterChoose.Init(chooseData, true);
            }
        }
        else
        {
            Debug.LogError("Ошибка: массив characters не удалось десериализовать или temp не является валидным JSON.");
        }
    }
    public void UserCharactersLocal()
    {
        
    }
}