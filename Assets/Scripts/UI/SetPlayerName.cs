using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
{
    // Start is called before the first frame update
    private InputField nameFiled;


    private void Start()
    {
        nameFiled = GetComponent<InputField>();
    }


    public void SetName() {
        GameManager.instance.GetLocalPlayerReference().CmdSetName(nameFiled.textComponent.text);
    }
}
