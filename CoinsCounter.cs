using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class CoinsCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coinsTXT;
    [SerializeField] TextMeshProUGUI _totalCoinsTXT;

    [SerializeField] GameObject _coinIMG;

    public Action AddLevel;

    int _coins = 0;
    int _totalCoins;
    int _adBonus;

    Player _player;
    GameManager _gm;

    string ppTotalCoins = "totalCoins";

    public Action AdWasShown;

    void Start()
    {
        CoinsSetupOnStart();
        DependencySetup();

        _adBonus = 3;
    }

    void CoinsSetupOnStart()
    {
        _totalCoins = PlayerPrefs.GetInt(ppTotalCoins);
        _totalCoinsTXT.text = _totalCoins.ToString();

        _coinsTXT.enabled = false;
    }

    void DependencySetup()
    {
        _player = FindObjectOfType<Player>();
        _player.AddCoin += AddCoin;
        _player.OhNo += OnGameEnd;

        _gm = FindObjectOfType<GameManager>();
        _gm.StartDatGame += OnGameStart;
    }

    void OnGameStart()
    {
        _totalCoinsTXT.enabled = false;
        _coinsTXT.enabled = true;
    }

    void OnGameEnd()
    {
        AddToTotalCoins();
    }

    void AddToTotalCoins()
    {
        PlayerPrefs.SetInt(ppTotalCoins, _totalCoins + _coins);
    }

    void AddCoin()
    {
        _coins++;
        _coinsTXT.text = _coins.ToString();

        CoinIMGDance();
        LevelUP();
    }

    void LevelUP()
    {
        if (_coins % 10 == 0)
        {
            AddLevel.Invoke();
        }
    }

    void CoinIMGDance()
    {
        float imgSqcTime = UnityEngine.Random.Range(.1f, .5f);
        float txtSqcTime = UnityEngine.Random.Range(.1f, .5f);

        Sequence imgSQC = DOTween.Sequence();

        imgSQC.Append(_coinIMG.transform.DOScale(new Vector3(1.5f, 1.5f, 0), imgSqcTime));
        imgSQC.Append(_coinIMG.transform.DOScale(new Vector3(1, 1, 0), imgSqcTime));

        Sequence txtSQC = DOTween.Sequence();

        txtSQC.Append(_coinsTXT.transform.DOScale(new Vector3(2f, 2f, 0), txtSqcTime));
        txtSQC.Append(_coinsTXT.transform.DOScale(new Vector3(1, 1, 0), txtSqcTime));
    }

    void AdShown(int value)
    {
        value = _adBonus;
        _coins *= value;
        AddToTotalCoins();
        AdWasShown.Invoke();
    }

    public int GetTotalCoins() 
    {
        return _totalCoins;
    }

    ////temp
    //void AddCoinsTEMP()
    //{
    //    if (Input.GetKeyDown(KeyCode.G))
    //    {
    //        PlayerPrefs.SetInt(ppTotalCoins, PlayerPrefs.GetInt(ppTotalCoins) + 100);
    //        _totalCoins = PlayerPrefs.GetInt(ppTotalCoins);
    //    }
    //}

    ////temp
    //void ResetCoinsTEMP()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        PlayerPrefs.SetInt(ppTotalCoins, 0);
    //        _totalCoins = PlayerPrefs.GetInt(ppTotalCoins);
    //    }
    //}
}
