using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] ParticleSystem _coinSolutte;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _coinSolutte.Play();
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;

            Invoke(nameof(DieMfkrDie), .3f);
        }
    }

    void DieMfkrDie()
    {
        Destroy(gameObject);
    }
}
