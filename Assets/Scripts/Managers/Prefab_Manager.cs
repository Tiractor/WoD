using UnityEngine;

public class Prefab_Manager : TaskExecutor<Prefab_Manager>
{
    [Header("CheckBox")]
    public GameObject CheckBox_Base;
    public GameObject CheckBox_FourState;
    public GameObject CheckBox_Square;
    public GameObject CheckBox_Clear;

    [Header("Buttons")]
    public GameObject Button_AddCounter;
    public GameObject Button_Character;
    public GameObject Button_CloseCharacter;
    public GameObject Button_SettingsCharacter;
    public GameObject Button_ChooseCharacter;
    public GameObject Button_ChooseGroup;
    public GameObject Button_JoinCharacter;

    [Header("Other")]
    public GameObject InputField;
    public GameObject InputField_Special;
    public GameObject CharacterBase;
    public TMPro.TMP_FontAsset Font;

    [Header("Size Settings")]
    public Vector2 TextBox_SizeSettings;
    public Vector2 TableCellSize_SizeSettings;
    public Vector2 Button;
    public Vector2 SizeCounters;

    [Header("Anchors")]
    public Vector2[] AnchorsText = new Vector2[2];
    public Vector2[] AnchorsIF = new Vector2[2];


    private static float _shift;
    public static float Shift()
    {
        if (_shift == 0) 
            _shift = _executor.CheckBox_Base.GetComponent<RectTransform>().sizeDelta.x / 2;
        return _shift;
    }

    private void Awake()
    {
        Denote();
    }

    [ContextMenu("Forced Denote")]
    private void FD()
    {
        Denote();
    }
}
