using UnityEngine.Networking;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;
public class Connector : TaskExecutor<Connector>
{
    public static bool PHPisOnline = true;

    [SerializeField] private string Link;
    [SerializeField] private Sprite ConnectionError;
    static public int Nickname;
    private static string link;
    [SerializeField] private string secretKey = "your_secret_key";

    [ContextMenu("Forced set static data by local data")]
    public void ForcedLinked()
    {
        link = Link;
        Nickname = 333;
    }
    public void Awake()
    {
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
        Request_Auth(Nickname);
    }
    private static void ErrorProcessor(string error)
    {
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
    public static string Request_Template(string Json, string TargetLink)
    {
        WWWForm form = new WWWForm();
        string LinkSend;
        if (TargetLink.Contains(link))
        {
            LinkSend = TargetLink;
        }
        else if (TargetLink[0]=='/')
        {
            LinkSend = link + TargetLink;
        }
        else
        {
            LinkSend = link + "/" + TargetLink;
        }
        form.AddField("hash", CalculateSHA256(Json));
        form.AddField("data_to_send", Json);
        using (UnityWebRequest www = UnityWebRequest.Post(LinkSend, form))
        {
            www.certificateHandler = new AcceptAllCertificates();
            // «апрос выполн€етс€ дожида€сь его завершени€
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
                Debug.Log("Server response: " + www.downloadHandler.text);
                return www.downloadHandler.text;
            }
        }
    }
    public static string Request_Auth(int external_Nickname)
    {
        WWWForm form = new WWWForm();
        Nickname = external_Nickname;
        form.AddField("Nickname", external_Nickname);
        using (UnityWebRequest www = UnityWebRequest.Post(link + "/Auth.php", form))
        {
            www.certificateHandler = new AcceptAllCertificates();
            // «апрос выполн€етс€ дожида€сь его завершени€
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
                Debug.Log("Server response: " + www.downloadHandler.text);
                return www.downloadHandler.text;
            }
        }
    }
}
