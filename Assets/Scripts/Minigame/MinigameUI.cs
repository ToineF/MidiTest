using TMPro;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    [SerializeField] private HittableMinigame _minigame;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        _minigame.OnScoreModified += UpdateUI;
    }

    private void UpdateUI(int totalScore, int highscore)
    {
        _scoreText.text = totalScore.ToString();
        _highScoreText.text = highscore.ToString();
    }

    private void Update()
    {
        if (_minigame.MinigameTimer > 0)
            _timerText.text = ((int)_minigame.MinigameTimer).ToString();
        else 
            _timerText.text = "";
    }
}