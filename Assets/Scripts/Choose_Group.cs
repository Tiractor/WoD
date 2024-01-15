using UnityEngine;

[System.Serializable]
public class Group_Choose : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI Name;
    [SerializeField] TMPro.TextMeshProUGUI Code;
    [SerializeField] GroupChooseData Data;
    public void Init(GroupChooseData Target)
    {
        Data = Target;
        Name.text = Target.GroupName;
        Code.text = Target.ConnectCode;
    }
    public void CopyCode()
    {
        Utility.CopyToClipboard(Data.ConnectCode);
    }
    public void InitLoad()
    {
        Group_Manager._executor.CurrentGroup = Data;
        GroupCharacterRequest data = new GroupCharacterRequest(Data);
        Char_Manager._executor.GroupCharactersServer(data);
    }
}

[System.Serializable]
public class GroupChooseDataArray
{
    public GroupChooseData[] characters;
}
[System.Serializable]
public class GroupChooseData
{
    public string GroupName;
    public string idUserGroup;
    public string ConnectCode;
}

[System.Serializable]
public class GroupCharacterRequest
{
    public string idUserGroup;
    public GroupCharacterRequest(GroupChooseData Value)
    {
        idUserGroup = Value.idUserGroup;
    }
}