using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField NameInput;
    public TextMeshProUGUI placeholder, nameInput;
    private string playerName;

    public void InsertPlayerName()
    {
        placeholder.text = " ";
        playerName = NameInput.text;
    }
}
