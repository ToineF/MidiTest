using UnityEngine;

public class AudioSourceVariationShifter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _minVolume = 0.7f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minPitch = 0.9f;
    [SerializeField] private float _maxPitch = 1.1f;
    private void Start()
    {
        _audioSource.volume = Random.Range(_minVolume, _maxVolume);
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.Play();
    }
    
}