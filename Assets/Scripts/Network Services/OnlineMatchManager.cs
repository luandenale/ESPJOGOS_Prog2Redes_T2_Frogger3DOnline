﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;

public class OnlineMatchManager : MonoBehaviour, IMatchManager
{
    [SerializeField]
    private InputField _matchName;
    [SerializeField]
    private GameObject _matchesList;
    [SerializeField]
    private GameObject _matchPrefab;
    [SerializeField]
    private GameObject _searchMatchButton;
    [SerializeField]
    private MenuUIManager _menuUIManager;

    [SerializeField]
    private GameObject _InGameMenu;

    private readonly float _searchUpdateInterval = 1f;
    private float _currentUpdateInterval;

    private bool _hasConnected;
    public bool searching = false;

    private List<GameObject> _instantiatedGameObjects = new List<GameObject>();


    private void Start()
    {
        _menuUIManager = transform.root.GetComponent<MenuUIManager>();
    }

    public void SearchForMatches()
    {
        _searchMatchButton.SetActive(false);
        searching = true;
    }

    public void CreateMatch()
    {
        NetworkManagerSingleton.Match.CreateMatch(_matchName.text, 2, true, "", "", "", 0, 0, 
            NetworkManagerSingleton.singleton.OnMatchCreate);
        NetworkManagerSingleton.Discovery.broadcastData = _matchName.text;

        NetworkManagerSingleton.onServerConnect += OnServerConnect;
        NetworkManagerSingleton.onClientConnect += OnClientConnect;

        _hasConnected = true;
        searching = false;

        _menuUIManager.WaitingOponent();
    }

    private void OnDisable()
    {
        NetworkManagerSingleton.onServerConnect -= OnServerConnect;
        NetworkManagerSingleton.onClientConnect -= OnClientConnect;
    }

    private void OnClientConnect(NetworkConnection obj)
    {
        _menuUIManager.PlayerSelect();
        _InGameMenu.SetActive(true);
        _hasConnected = true;
    }

    private void OnServerConnect(NetworkConnection conn)
    {
        _menuUIManager.PlayerSelect();
        _InGameMenu.SetActive(true);
    }

    private void Update()
    {
        if (!_hasConnected && searching)
        {
            _currentUpdateInterval -= Time.deltaTime;
            if (_currentUpdateInterval < 0)
            {
                RefreshMatches();
                UpdateButtons();

                _currentUpdateInterval = _searchUpdateInterval;
            }
        }
    }

    public void RefreshMatches()
    {
        NetworkManagerSingleton.Match.ListMatches(0, 10, "", true, 0, 0, 
            NetworkManagerSingleton.singleton.OnMatchList);
    }

    public void ClearMatches()
    {
        foreach (GameObject __instance in _instantiatedGameObjects)
        {
            Destroy(__instance);
        }
    }

    private void UpdateButtons()
    {
        List<MatchInfoSnapshot> __matches = NetworkManagerSingleton.singleton.matches;
        if(__matches != null)
        {
            ClearMatches();

            foreach(MatchInfoSnapshot __match in __matches)
            {
                GameObject __matchInstance = Instantiate(_matchPrefab, _matchPrefab.transform.position, _matchPrefab.transform.rotation, _matchesList.transform);
                __matchInstance.GetComponentsInChildren<Text>()[0].text = __match.name;
                __matchInstance.GetComponentInChildren<Button>().onClick.AddListener((delegate 
                {
                    OnMatchConnectClick(__match);
                }));

                _instantiatedGameObjects.Add(__matchInstance);
            }
            NetworkManagerSingleton.singleton.matches.Clear();
        }
    }

    private void OnMatchConnectClick(MatchInfoSnapshot p_match)
    {
        // Conecta com a partida, passando o callback do NetworkManager.
        NetworkManagerSingleton.Match.JoinMatch(p_match.networkId, "", "", "", 0, 0, 
            NetworkManagerSingleton.singleton.OnMatchJoined);

        _hasConnected = true;

        _menuUIManager.PlayerSelect();
        _InGameMenu.SetActive(true);
    }

}