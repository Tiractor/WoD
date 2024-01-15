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
public class Char_Manager : MonoBehaviour
{
    [SerializeField] private GameObject WhereInst;
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
    public void UserCharactersServer()
    {
        RequestData data = new RequestData();
        data.Login = Authorization._executor.UserData.Login;
        string temp = Connector.Request_UserCharacters(data);
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
    public void GroupCharactersServer()
    {
        RequestData data = new RequestData();
        data.Login = Authorization._executor.UserData.Login;
        string temp = Connector.Request_GroupCharacters(data);
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
    public void UserCharactersLocal()
    {
        
    }
}