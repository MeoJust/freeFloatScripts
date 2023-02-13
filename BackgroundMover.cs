using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    GameManager _gm;
    Player _player;

    [SerializeField] float _moveSpeed;

    BoxCollider _collider;
    Vector3 _startPos;

    bool _isGameOn;

    void Start()
    {
        DependencySetup();

        _collider= GetComponent<BoxCollider>();
        _startPos= transform.position;
    }

    void DependencySetup()
    {
        _gm = FindObjectOfType<GameManager>();
        _gm.StartDatGame += GameStart;

        _player = FindObjectOfType<Player>();
        _player.OhNo += GameStop;
    }

    void Update()
    {
        MoveDatStaff();
    }

    void MoveDatStaff()
    {
        if (_isGameOn)
        {
            transform.position += new Vector3(Time.deltaTime * _moveSpeed, 0, 0);

            if (transform.position.x < _startPos.x - _collider.size.x / 2)
            {
                transform.position = _startPos;
            }
        }
    }

    void GameStart()
    {
        _isGameOn = true;
    }

    void GameStop()
    {
        _isGameOn = false;
    }
}
