using UnityEngine;



[System.Serializable]
public class Group_Choose : MonoBehaviour
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
}

[System.Serializable]
public class GroupChooseDataArray
{
    public ChooseData[] characters;
}
[System.Serializable]
public class GroupChooseData
{
    public string CharacterName;
    public string idPlayerCharacter;
}