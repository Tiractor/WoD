using UnityEngine;

public class Prefab_Manager : TaskExecutor<Prefab_Manager>
{

    public GameObject CheckBox_Base;
    public GameObject CheckBox_Clear;
    public GameObject Button_Character;
    public GameObject Button_CloseCharacter;
    public GameObject Button_SettingsCharacter;
    public GameObject InputField;
    public Vector2 TextBox_SizeSettings;
    private static float _shift;
    public Vector2 Button;
    public TMPro.TMP_FontAsset Font;
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
