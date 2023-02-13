using UnityEngine;

public class CoinBombSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _staff;

    void Start()
    {
        InvokeRepeating("SpawnSomeStaff", 2, Random.Range(.5f, 2));
    }

    void SpawnSomeStaff()
    {
        Instantiate(_staff[Random.Range(0, _staff.Length)], new Vector3(Random.Range(25, 30), Random.Range(9, -8), 0), Quaternion.identity);
    }
}
