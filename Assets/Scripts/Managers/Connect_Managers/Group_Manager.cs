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
public class Group_Manager : TaskExecutor<Group_Manager>
{
    [SerializeField] private Clear WhereInst;
    public GameObject WhereChar;
    public GroupChooseData CurrentGroup;
    private void Awake()
    {
        Denote();
    }
    public void CreateGroup(TMPro.TMP_InputField name)
    {
        InitGroup Group = new InitGroup();
        Group.Name = name.text;
        Group.Login = Authorization._executor.UserData.Login;
        Connector.Request_CreateGroup(Group);
    }
    public void UserGroups()
    {
        WhereInst.Clearing();
        RequestData data = new RequestData();
        data.Login = Authorization._executor.UserData.Login;
        string temp = Connector.Request_UserGroups(data);
        GroupChooseDataArray chooseDataArray = JsonUtility.FromJson<GroupChooseDataArray>("{\"characters\":" + temp + "}");

        if (chooseDataArray.characters != null)
        {
            foreach (var chooseData in chooseDataArray.characters)
            {
                Group_Choose characterChoose = Instantiate(Prefab_Manager._executor.Button_ChooseGroup, WhereInst.transform).GetComponent<Group_Choose>();
                characterChoose.Init(chooseData);
            }
        }
        else
        {
            Debug.LogError("Ошибка: массив characters не удалось десериализовать или temp не является валидным JSON.");
        }
    }
    public void TryJoin(TMPro.TMP_InputField code)
    {
        ConnectGroup Group = new ConnectGroup();
        Group.Code = code.text;
        Group.Login = Authorization._executor.UserData.Login;
        Connector.Request_JoinGroup(Group);
    }

}