using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _speed;

    void Update()
    {
        MoveMfkrMove();
        DieMfkrDie();
    }

    void MoveMfkrMove()
    {
        transform.position += new Vector3(Time.deltaTime * _speed, 0, 0);
    }

    void DieMfkrDie()
    {
        if(transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
