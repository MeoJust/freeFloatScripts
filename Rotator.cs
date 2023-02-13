using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _rotSpeed;

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * _rotSpeed, 0, Space.Self);
    }
}
