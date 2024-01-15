using UnityEngine;
[System.Serializable]
public class InitGroup
{
    public string Name;
    public string Login;
}
[System.Serializable]
public class ConnectGroup
{
    public string Code;
    public string Login;
}
[System.Serializable]
public class Group_Data
{
    public string GroupName;
    public string idUserGroup;
    public string ConnectCode;
}

public class Group_Manager : TaskExecutor<Group_Manager>
{
    [SerializeField] private Group_Data[] Groups;
    private void Awake()
    {
        Denote();
    }
    public void CreateGroup(TMPro.TMP_InputField name)
    {
        InitGroup Group = new InitGroup();
        Group.Name = name.text;
        Group.Login = Authorization._executor.UserData.Login;
        Debug.Log(JsonUtility.ToJson(Group));
        Connector.Request_CreateGroup(Group);
    }
    public void UserGroups()
    {
        RequestData Group = new RequestData();
        Group.Login = Authorization._executor.UserData.Login;
        Debug.Log(JsonUtility.ToJson(Group));
        Connector.Request_UserGroups(Group);
    }
    public void TryJoin(TMPro.TMP_InputField code)
    {
        ConnectGroup Group = new ConnectGroup();
        Group.Code = code.text;
        Group.Login = Authorization._executor.UserData.Login;
        Debug.Log(JsonUtility.ToJson(Group));
        Connector.Request_JoinGroup(Group);
    }

}