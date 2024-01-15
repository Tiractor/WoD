using UnityEngine;

public class Error_Display : MonoBehaviour
{
    [SerializeField]  TMPro.TextMeshProUGUI Code;
    [SerializeField]  TMPro.TextMeshProUGUI Reason;
    public void SetData(ServerError Data)
    {
        Code.text = Data.errorcode;
        Reason.text = Data.reason;
    }
}
