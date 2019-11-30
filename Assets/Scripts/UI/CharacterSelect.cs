using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    private Color choosedColor, waitingColor;
    public Character characterType;
    public Image panelImageChad, panelImageVirgin;
    public PlayerMenu localPlayer;

    private void Start() {
        choosedColor = new Color(69f,180,69,100);
        waitingColor = new Color(255f, 255f, 255f, 100f);
    }

    private void OnMouseDown() {
        if (characterType == Character.Chad) {
            panelImageVirgin.color = waitingColor;
            localPlayer.SetCharacter(Character.Chad);
            panelImageChad.color = choosedColor;
        }
        else if(characterType == Character.Virgin) {
            panelImageVirgin.color = waitingColor;
            localPlayer.SetCharacter(Character.Virgin);
            panelImageChad.color = choosedColor;
        }
    }

}
