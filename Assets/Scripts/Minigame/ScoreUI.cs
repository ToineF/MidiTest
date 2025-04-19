using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private HittableMinigame _minigame;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;

    private void Start()
    {
        _minigame.OnScoreModified += UpdateUI;
    }

    private void UpdateUI(int totalScore, int highscore)
    {
        _scoreText.text = totalScore.ToString();
        _highScoreText.text = highscore.ToString();
    }
}