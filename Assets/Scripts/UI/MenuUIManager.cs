using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField]
    private Text _popUpText;
    private Animator _menuAnimator;

    [SerializeField] LANMatchManager _lanMatchManager;
    // [SerializeField] OnlineMatchManager _onlineMatchManager;
    [SerializeField] GameObject _searchMatchButton;

    // Start is called before the first frame update
    void Start()
    {
        _menuAnimator = GetComponent<Animator>();
    }

    public void LanSelect()
    {
        _menuAnimator.SetTrigger("Show Lobby");
    }

    public void CreateMatch()
    {
        WaitingOponent();
    }

    public void PlayerSelect()
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

    // public void InternetSelect()
    // {
    //     GameMode.mode = Mode.ONLINE;
    //     _menuAnimator.SetTrigger("Pop Up Lobby");
    // }

    // public void CloseLobby()
    // {
    //     _searchMatchButton.SetActive(true);
    //     if(GameMode.mode == Mode.LAN)
    //     {
    //         NetworkManagerSingleton.Discovery.StopBroadcast();
    //         _lanMatchManager.ClearMatches();
    //         _lanMatchManager.searching = false;
    //     }
    //     else
    //     {
    //         NetworkManagerSingleton.singleton.matches.Clear();
    //         _onlineMatchManager.ClearMatches();
    //         _onlineMatchManager.searching = false;       
    //     }
    //     _menuAnimator.SetTrigger("Pop Down Lobby");
    // }

    /*
    private void Update()
    {
        if(GameManager.instance.opponentReady && GameManager.instance.localPlayerReady)
            OpponentReady();
    }
    */
}
