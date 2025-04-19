using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class HittableMinigame : MonoBehaviour
{
    public Action<int, int> OnScoreModified;
    public float MinigameTimer => _minigameTimer;
    
    [SerializeField] private Hittable _hittablePrefab;
    [SerializeField] private GameObject _startMinigameButton;
    [SerializeField] private float _minigameTime;
    [SerializeField] private float _hittableFrequency;
    [SerializeField] private float _hittableFrequencyAcceleration;
    [SerializeField, Range(0,1)] private float _screenWidthMargin;
    [SerializeField] private int _scoreAdded;

    private float _minigameTimer;
    private float _hittableSpawnTimer;
    private float _currentFrequency;
    private bool _isPlaying;
    private Camera _camera;
    private int _totalScore;
    private int _highScore;


    private void Start()
    {
        _camera = Camera.main;
    }

    public void StartMinigame()
    {
        _minigameTimer = _minigameTime;
        _hittableSpawnTimer = 0;
        _totalScore = 0;
        _currentFrequency = _hittableFrequency;
        OnScoreModified?.Invoke(_totalScore, _highScore);
        _isPlaying = true;
        _startMinigameButton.SetActive(false);
    }

    private void Update()
    {
        if (_isPlaying == false) return;
        
        CheckSpawnHittables();

        CheckEnd();
    }

    private void CheckSpawnHittables()
    {
        _hittableSpawnTimer += Time.deltaTime;

        if (_hittableSpawnTimer > _currentFrequency)
        {
            _currentFrequency -= _hittableFrequencyAcceleration;
            _hittableSpawnTimer = 0;
            var hittable = Instantiate(_hittablePrefab, _camera.ScreenToWorldPoint(new Vector3(Random.Range(_screenWidthMargin * Screen.width, Screen.width - _screenWidthMargin * Screen.width), Screen.height, 10f)), Quaternion.identity);
            hittable.OnWin += AddScore;
        }
    }

    private void AddScore()
    {
        _totalScore += _scoreAdded;
        if (_totalScore > _highScore) _highScore = _totalScore;
        OnScoreModified?.Invoke(_totalScore, _highScore);
    }

    private void CheckEnd()
    {
        _minigameTimer -= Time.deltaTime;

        if (_minigameTimer <= 0)
        {
            _isPlaying = false;
            _startMinigameButton.SetActive(true);
        }
    }
}