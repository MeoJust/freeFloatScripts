using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] ParticleSystem _bombBoom;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _bombBoom.Play();
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
