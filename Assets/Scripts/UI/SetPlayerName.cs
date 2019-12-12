using UnityEngine.UI;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
{
    private InputField _nameField;

    private void Awake()
    {
        _nameField = GetComponent<InputField>();
    }


    public void SetName()
    {
        GameManager.instance.GetLocalPlayerReference().CmdSetName(_nameField.textComponent.text);
    }
}
