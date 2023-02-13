using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameManager _gm;
    CoinsCounter _coinsCounter;

    [SerializeField] float _flyForce;
    [SerializeField] float _bufferForce;

    [SerializeField] MeshRenderer _baloonMesh;
    [SerializeField] Material[] _baloonMATs;

    [SerializeField] TextMeshProUGUI _baloonCostTXT;
    [SerializeField] Image _baloonCostIMG;

    Rigidbody _rb;

    bool _isGameOn;
    int _matId;
    int _matToStartID;
    int _currentMatId;
    int _getTotalCoins;

    public Action AddCoin;
    public Action OhNo;

    void Start()
    {
        DependencySetup();

        _rb = GetComponent<Rigidbody>();

        _getTotalCoins = _coinsCounter.GetTotalCoins();

        BaloonMatSetup();
    }

    void BaloonMatSetup()
    {
        _matId = PlayerPrefs.GetInt("matID");
        _matToStartID = PlayerPrefs.GetInt("matToStartID");
        _baloonMesh.material = _baloonMATs[_matId];

        if (_getTotalCoins < _matId * 100)
        {
            ShowBaloonCost(true);
            _baloonCostTXT.text = "x " +  (_matId * 100).ToString();
        }
        else
        {
            ShowBaloonCost(false);
        }
    }

    void DependencySetup()
    {
        _gm = FindObjectOfType<GameManager>();
        _gm.StartDatGame += GameStart;
        _gm.SwitchToPreviousMAT += SwitchMATLeft;
        _gm.SwitchToNextMAT += SwitchMATRight;

        _coinsCounter = FindObjectOfType<CoinsCounter>();
    }

    void FixedUpdate()
    {
        IBelieveICanFly();
        BordersSetup();
    }

    void BordersSetup()
    {
        if (transform.position.y > 10)
        {
            _rb.AddForce(Vector3.up * -_bufferForce, ForceMode.Impulse);
        }

        if (transform.position.y < -8)
        {
            _rb.AddForce(Vector3.up * _bufferForce, ForceMode.Impulse);
        }
    }

    void IBelieveICanFly()
    {
        if (_isGameOn)
        {
            if (Input.GetMouseButton(0) && (transform.position.y <= 10 || transform.position.y >= -8))
            {
                _rb.AddForce(Vector3.up * _flyForce);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Coin>())
        {
            AddCoin.Invoke();
        }

        if (other.GetComponent<Bomb>())
        {
            OhNo.Invoke();
            Destroy(gameObject);
        }
    }

    void GameStart()
    {
        _isGameOn = true;
        GetComponent<Rigidbody>().isKinematic = false;
        _baloonMesh.material = _baloonMATs[_matToStartID];
        PlayerPrefs.SetInt("matToStartID", _matToStartID);
    }

    void SwitchMATLeft()
    {
        _currentMatId = _matId;
        if(_currentMatId > 0)
        {
            _currentMatId--;
            ChangeMAT();
        }
        else
        {
            _currentMatId = _baloonMATs.Length - 1;
            ChangeMAT();
        }
    }

    void SwitchMATRight()
    {
        _currentMatId = _matId;
        if(_currentMatId < _baloonMATs.Length - 1)
        {
            _currentMatId++;
            ChangeMAT();
        }
        else
        {
            _currentMatId = 0;
            ChangeMAT();
        }
    }

    void ChangeMAT()
    {
        _baloonMesh.material = _baloonMATs[_currentMatId];
        if(_getTotalCoins >= _currentMatId * 100)
        {
            ShowBaloonCost(false);
            _matToStartID = _currentMatId;
        }
        else
        {
            ShowBaloonCost(true);
            _baloonCostTXT.text = "x " +  (_currentMatId * 100).ToString();
        }
        _matId = _currentMatId;
        PlayerPrefs.SetInt("matID", _matId);
    }

    void ShowBaloonCost(bool value)
    {
        _baloonCostIMG.enabled = value;
        _baloonCostTXT.enabled = value;
    }
}
