using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Player _player;
    CoinsCounter _coinsCounter;

    [Header("go")]
    [SerializeField] GameObject _coinBombSpawner;
    [SerializeField] GameObject _endGameCNV;
    [SerializeField] GameObject _menuCNV;
    [SerializeField] GameObject _background;

    [Header("vfx")]
    [SerializeField] ParticleSystem _txtSolutte;

    [Header("buttons")]
    [SerializeField] Button _startBTN;
    [SerializeField] Button _restartBTN;
    [SerializeField] Button _leftBTN;
    [SerializeField] Button _rightBTN;

    [SerializeField] Sprite[] _backSprites;

    public Action StartDatGame;
    public Action SwitchToPreviousMAT;
    public Action SwitchToNextMAT;
    public Action BuyCurrentMAT;

    void Start()
    {
        DependencySetup();
        BTNsSetup();
        BackgroundSetup();
    }

    void BackgroundSetup()
    {
        _background.GetComponent<SpriteRenderer>().sprite = _backSprites[UnityEngine.Random.Range(0, _backSprites.Length)];
    }

    void DependencySetup()
    {
        _player = FindObjectOfType<Player>();
        _player.OhNo += GameStop;

        _coinsCounter = FindObjectOfType<CoinsCounter>();
        _coinsCounter.AddLevel += AddLevel;
        _coinsCounter.AdWasShown += RestartGame;
    }

    void BTNsSetup()
    {
        _startBTN.onClick.AddListener(GameStart);
        _leftBTN.onClick.AddListener(LeftBTNClick);
        _rightBTN.onClick.AddListener(RightBTNClick);
    }

    void AddLevel()
    {
        _txtSolutte.Play();

        if(FindObjectsOfType<CoinBombSpawner>().Length <= 9)
        {
            Instantiate(_coinBombSpawner);
        }
    }

    void GameStart()
    {
        StartDatGame.Invoke();
        Instantiate(_coinBombSpawner);
        Destroy(_menuCNV.gameObject);
    }

    void GameStop()
    {
        EndGameInvoke();

        foreach (CoinBombSpawner spawner in FindObjectsOfType<CoinBombSpawner>())
        {
            Destroy(spawner.gameObject);
        }

        foreach (Coin coin in FindObjectsOfType<Coin>())
        {
            coin.GetComponent<Mover>().SetSpeed(0);
        }

        foreach (Bomb bomb in FindObjectsOfType<Bomb>())
        {
            bomb.GetComponent<Mover>().SetSpeed(0);
        }
    }

    void EndGameInvoke()
    {
        _endGameCNV.SetActive(true);
        _restartBTN.onClick.AddListener(RestartGame);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void LeftBTNClick()
    {
        SwitchToPreviousMAT.Invoke();
    }

    void RightBTNClick()
    {
        SwitchToNextMAT.Invoke();
    }
}
