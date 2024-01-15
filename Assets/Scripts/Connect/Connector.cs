using UnityEngine.Networking;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Collections;
[System.Serializable]
public struct ServerResponse 
{
    public string result;
    public string response;
}
[System.Serializable]
public struct ServerError
{
    public string errorcode;
    public string reason;
}
[System.Serializable]
public class RequestData
{
    public string Login;
}

public class Connector : TaskExecutor<Connector>
{
    public static bool PHPisOnline = true;

    [SerializeField] private string Link;
    [SerializeField] private Error_Display Error;
    static public bool isAuthorized;
    private static string link;
    private string secretKey = "tP7vbJLf2WNYiRaMvlhfA9p8uQlcwmyZ";

    [ContextMenu("Forced set static data by local data")]
    public void ForcedLinked()
    {
        link = Link;
    }
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Link.Contains("https"))
        {
            link = Link;
        }
        else
        {
            link = string.Empty;
            PHPisOnline = false;
            return;
        }
#endif
        Denote();
        PHPisOnline = true;
        link = Link;
    }
    private static void ErrorProcessor(string error)
    {
        Debug.Log(error);
        Debug.Log(JsonUtility.FromJson<ServerError>(error));
        _executor.Error.gameObject.SetActive(true);
        _executor.Error.SetData(JsonUtility.FromJson<ServerError>(error));
        
        Debug.LogWarning("Server Error: " + error);
        if (error == "Cannot connect to destination host")
        {
            PHPisOnline = false;
        }
    }
    public static string CalculateSHA256(string text)
    {
        string hashString;

        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(text + _executor.secretKey));
            hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        return hashString;
    }
    private static string Request(string Json, string TargetLink)
    {
        WWWForm form = new WWWForm();
        
        string LinkSend = link + TargetLink;
        Debug.Log(Json + " - sended to " + LinkSend);
        form.AddField("hash", CalculateSHA256(Json));
        form.AddField("data_to_send", Json);
        using (UnityWebRequest www = UnityWebRequest.Post(LinkSend, form))
        {
            www.SendWebRequest();
            float startTime = Time.time;
            while (!www.isDone && Time.time - startTime < 2f) { }
            if (www.result != UnityWebRequest.Result.Success && www.isDone)
            {
                ErrorProcessor(www.error);
                return www.error;
            }
            else
            {
                Debug.Log(TargetLink + " Server response: " + www.downloadHandler.text);
                ServerResponse temp = JsonUtility.FromJson<ServerResponse>(www.downloadHandler.text);
                Debug.Log(temp.result);
                if (temp.result == "error")
                {
                    ErrorProcessor(temp.response);
                    return "";
                }
                else return temp.response;
            }
        }
    }
    private static IEnumerator RequestCoroutine(string json, string targetLink, Action<string> callback)
    {
        WWWForm form = new WWWForm();

        string linkSend = link + targetLink;
        Debug.Log(json + " - sended to " + linkSend);
        form.AddField("hash", CalculateSHA256(json));
        form.AddField("data_to_send", json);
        using (UnityWebRequest www = UnityWebRequest.Post(linkSend, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                ErrorProcessor(www.error);
                callback?.Invoke(null);
            }
            else
            {
                Debug.Log(targetLink + " Server response: " + www.downloadHandler.text);
                ServerResponse temp = JsonUtility.FromJson<ServerResponse>(www.downloadHandler.text);
                Debug.Log(temp.result);
                if (temp.result == "error")
                {
                    ErrorProcessor(temp.response);
                    callback?.Invoke(null);
                }
                else
                {
                    callback?.Invoke(temp.response);
                }
            }
        }
    }
    public static bool Request_Reg(Authorization_Data Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "Registration.php";
        string res = Request(Json, Target);
        return res != "";
    }
    public static bool RequestAuth(Authorization_Data Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "Authorization.php";
        string res = Request(Json, Target);
        return res != "";
    }
    public static void Request_Auth(Authorization_Data data, Action<string> callback)
    {
        string json = JsonUtility.ToJson(data);
        string target = "Authorization.php";
        string res = "";
        _executor.StartCoroutine(RequestCoroutine(json, target, callback));
    }
    public static bool Request_UploadCharacter(UploadCharacterData Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "UploadCharacter.php";
        string res = Request(Json, Target);
        return res != "";
    }
    public static string Request_UserCharacters(RequestData Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "UserCharacters.php";
        string res = Request(Json, Target);
        return res;
    }
    public static string Request_GetCharacter(DataToGetCharacter Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "GetCharacter.php";
        string res = Request(Json, Target);
        Debug.Log(res);
        return res;
    }


    public static bool Request_CreateGroup(InitGroup Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "CreateGroup.php";
        string res = Request(Json, Target);
        return res != null;
    }
    public static string Request_GroupCharacters(GroupCharacterRequest Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "GroupCharacters.php";
        string res = Request(Json, Target);
        return res;
    }
    public static bool Request_JoinGroup(ConnectGroup Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "JoinGroup.php";
        string res = Request(Json, Target);
        return res != null;
    }
    public static string Request_UserGroups(RequestData Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "UserGroups.php";
        string res = Request(Json, Target);
        return res;
    }
    public static string Request_JoinCharacter(CharacterJoinData Data)
    {
        string Json = JsonUtility.ToJson(Data);
        string Target = "JoinCharacter.php";
        string res = Request(Json, Target);
        return res;
    }
}

