using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private Text _popUpText;

    private Animator _menuAnimator;

    void Start()
    {
        _menuAnimator = GetComponent<Animator>();
    }

    public void LanSelect()
    {
        _menuAnimator.SetTrigger("Show Lobby");
        GameMode.mode = Mode.LAN;
    }

    public void OnlineSelect()
    {
        _menuAnimator.SetTrigger("Show Lobby");
        GameMode.mode = Mode.Online;
    }

    public void QuitSelect()
    {
        Application.Quit();
    }

    public void CreateMatch()
    {
        WaitingOponent();
    }

    public void PlayerSelect()
    {
        _menuAnimator.SetTrigger("Show Player Selection");
    }

    public void PlayerSelectFromServer()
    {
        _menuAnimator.SetTrigger("Show Player Selection");
    }

    public void WaitingOponent()
    {
        _menuAnimator.SetTrigger("Show Pop Up");
        _popUpText.text = "WAITING OPPONENT...";
    }

    public void OpponentReady()
    {
        _menuAnimator.SetTrigger("Show Pop Up");
        StartCoroutine(TextCountdown());
        
        GameManager.instance.onGameStarts();
    }

    private IEnumerator TextCountdown()
    {
        _popUpText.text = "OPPONENT CONNECTED\nSTARTING IN 3";
        yield return new WaitForSeconds(1f);
        _popUpText.text = "OPPONENT CONNECTED\nSTARTING IN 2";
        yield return new WaitForSeconds(1f);
        _popUpText.text = "OPPONENT CONNECTED\nSTARTING IN 1";
        yield return new WaitForSeconds(1f);
        _popUpText.text = "OPPONENT CONNECTED\nSTARTING NOW...";

        _menuAnimator.SetTrigger("Game Start");
        GameManager.instance.bothPlayersStarted = true;
    }
}
