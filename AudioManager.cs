using UnityEngine;

public class AudioManager : MonoBehaviour
{
    Player _player;

    [SerializeField] AudioClip _boomSFX;
    [SerializeField] AudioClip _coinSFX;
    [SerializeField] AudioClip _levelUpSFX;

    [SerializeField] AudioClip[] _music;

    AudioSource _audioSource;
    CoinsCounter _coinsCounter;

    void Start()
    {
        DependencySetup();

        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _music[Random.Range(0, _music.Length)];
        _audioSource.Play();
    }

    void DependencySetup()
    {
        _player = FindObjectOfType<Player>();
        _player.AddCoin += PlayCoinSFX;
        _player.OhNo += PlayBoomSFX;

        _coinsCounter = FindObjectOfType<CoinsCounter>();
        _coinsCounter.AddLevel += PlayLevelUpSFX;
    }

    void PlayBoomSFX()
    {
        _audioSource.PlayOneShot(_boomSFX);
    }

    void PlayCoinSFX()
    {
        _audioSource.PlayOneShot(_coinSFX);
    }

    void PlayLevelUpSFX()
    {
        _audioSource.PlayOneShot(_levelUpSFX);
    }
}
