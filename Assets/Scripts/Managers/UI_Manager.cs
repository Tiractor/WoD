using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject SaveChar;
    [SerializeField] private Character_Manager CharManager;

    [SerializeField] private GameObject Group;
    [SerializeField] private GameObject Save;
    [SerializeField] private GameObject ServerLoad;
    private void Awake()
    {
        if(Connector._executor == null)
        {
            Group.SetActive(false);
            Save.SetActive(false);
        }
    }
    public void TryOpen_SaveChar()
    {
        if (CharManager.CharIsOpen()) SaveChar.SetActive(true);
    }
}
