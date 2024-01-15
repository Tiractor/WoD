using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Authorization_Data
{
    public string Login;
    public string Password;
}

public class Authorization : TaskExecutor<Authorization>
{
    [SerializeField] private TMPro.TMP_InputField AuthLogin;
    [SerializeField] private TMPro.TMP_InputField AuthPassword;
    [SerializeField] private TMPro.TMP_InputField RegLogin;
    [SerializeField] private TMPro.TMP_InputField RegPassword;
    public Authorization_Data UserData;
    [SerializeField] private Group_Manager GroupData;
    private void Awake()
    {
        Denote();
        Load();
    }
    public void Save()
    {
        string temp_Data = JsonUtility.ToJson(UserData);
        PlayerPrefs.SetString("UserData", temp_Data);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        string temp_Data;
        if (PlayerPrefs.HasKey("UserData"))
        {
            temp_Data = PlayerPrefs.GetString("UserData");
            UserData = JsonUtility.FromJson<Authorization_Data>(temp_Data);
            Connector.Request_Auth(UserData);
        }
    }
    public void TryAuth()
    {
        UserData.Login = AuthLogin.text;
        UserData.Password = AuthPassword.text;
        if(Connector.Request_Auth(UserData)) SceneManager.LoadScene("Core");
    }
    public void TryReg()
    {
        
        UserData.Login = RegLogin.text;
        UserData.Password = RegPassword.text;
        if(Connector.Request_Reg(UserData)) SceneManager.LoadScene("Core");
    }
    public void Ignore()
    {
        Connector.isAuthorized = false;
        SceneManager.LoadScene("Core");
        Destroy(gameObject);
    }
}
