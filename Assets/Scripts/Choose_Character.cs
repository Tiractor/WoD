using UnityEngine;



[System.Serializable]
public class Character_Choose : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Name;
    [SerializeField] ChooseData Data;
    public bool FromServer;
    public void Init(ChooseData Target, bool _FromServer )
    {
        Data = Target;
        FromServer = _FromServer;
        Name.text = Target.CharacterName;
    }
    public void InitLoad()
    {
        Char_Manager.LoadChar(Data);
    }
    public void InitJoin()
    {
        Char_Manager.JoinChar(Data);
    }
}

[System.Serializable]
public class ChooseDataArray
{
    public ChooseData[] characters;
}
[System.Serializable]
public class ChooseData
{
    public string CharacterName;
    public string idPlayerCharacter;
}

[System.Serializable]
public class CharacterJoinData
{
    public string Login;
    public string idPlayerCharacter;
    public string idUserGroup;
    public CharacterJoinData(ChooseData Value)
    {
        idPlayerCharacter = Value.idPlayerCharacter;
    }
}