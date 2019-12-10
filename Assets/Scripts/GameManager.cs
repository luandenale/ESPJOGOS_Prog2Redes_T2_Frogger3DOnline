using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<PlayerCharacter> _players = new List<PlayerCharacter>();
    public bool startGame = false;
    public bool gameEnded = false;
    public bool bothPlayersConnected = false;
    public bool localPlayerReady = false;
    public bool opponentReady = false;
    public bool bothPlayersStarted = false;
    public MenuUIManager uiManager;
    public GameObject disconnectScreen;
    [SerializeField] Animator _endGameAnimator;
    [SerializeField] AudioSource _musicAudioSource;

    private AudioSource _endGameAudioSource;

    public delegate void OnGameStart();
    public OnGameStart onGameStarts;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        _endGameAudioSource = GetComponent<AudioSource>();
    }

    // Roda apenas no servidor
    public void RegisterPlayer(PlayerCharacter player)
    {
        _players.Add(player);
        if (_players.Count == 2) {
            bothPlayersConnected = true;
        }
    }

    private void Update()
    {
        if(bothPlayersConnected && bothPlayersStarted)
            startGame = true;
    }

    public PlayerCharacter GetLocalPlayerReference()
    {
        foreach (PlayerCharacter player in _players)
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer)
                return player;
        }
        return null;
    }

    public void MatchLost()
    {
        _musicAudioSource.Stop();
        _endGameAudioSource.PlayOneShot(AudioClipReference.instance.youLost);
        _endGameAnimator.SetTrigger("Lost");
        DisableMovement();
    }

    public void MatchWon()
    {
        _musicAudioSource.Stop();
        _endGameAudioSource.PlayOneShot(AudioClipReference.instance.youWon);
        _endGameAnimator.SetTrigger("Won");
        DisableMovement();
    }

    public bool GameEnded() {
        foreach (PlayerCharacter player in _players) {
            if (player.GetComponent<PlayerMovement>().alive)
                return false;
        }
        return true;
    }

    private void DisableMovement()
    {
        foreach (PlayerCharacter player in _players)
            player.GetComponent<PlayerMovement>().enabled = false;
    }

    public void ReloadAllGame()
    {
        StartCoroutine(RealoadGame());
    }

    private IEnumerator RealoadGame()
    {
        yield return new WaitForSeconds(5f);
        uiManager.FadeRestart();
        yield return new WaitForSeconds(1f);
        NetworkManagerSingleton.singleton.StopHost();
        if (onGameStarts.GetInvocationList().Length > 0) {

            Delegate[] calledDelegates = onGameStarts.GetInvocationList();
            foreach (Delegate clientDel in calledDelegates) {
                onGameStarts -= (clientDel as OnGameStart);
            }

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RealoadGameMenu() {
        NetworkManagerSingleton.singleton.StopHost();
        if (onGameStarts.GetInvocationList().Length > 0) {

            Delegate[] calledDelegates = onGameStarts.GetInvocationList();
            foreach (Delegate clientDel in calledDelegates) {
                onGameStarts -= (clientDel as OnGameStart);
            }

        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        foreach (PlayerCharacter player in _players)
        {
            player.ToMenu();
        }

        _players.Clear();
    }
}
