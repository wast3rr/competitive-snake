using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using PlayerResult = SaveData.GameResult.PlayerResult;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MiniGameManager _miniGameManager;
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private Transform[] _spawnPoints;
    private float _timer;
    [SerializeField]
    private float _gameDuration;
    private bool _gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        _miniGameManager.GameStarted += OnGameStarted;

    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameStarted)
        {
            return;
        }

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            EndGame();
        }
    }

    void OnGameStarted(PlayerData[] playerDataArray)
    {
        Debug.Log(playerDataArray);
        _gameStarted = true;
        _timer = _gameDuration;

        for (int i = 0; i < playerDataArray.Length; i++)
        {
            PlayerController playerInstance = Instantiate(_playerPrefab).GetComponent<PlayerController>();
            playerInstance.Construct(playerDataArray[i]);
            playerInstance.transform.position = _spawnPoints[i].position;
        }
    }

    void EndGame()
    {
        PlayerResult[] results = new PlayerResult[] {
            new PlayerResult(){Player=0, Points=100},
            new PlayerResult(){Player=1, Points=50},
            new PlayerResult(){Player=2, Points=25}
            };
        _miniGameManager.EndGame();
        _gameStarted = false;
    }
}
