using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject SaveChar;
    [SerializeField] private Character_Manager CharManager;
    public void TryOpen_SaveChar()
    {
        if (CharManager.CharIsOpen()) SaveChar.SetActive(true);
    }
}
