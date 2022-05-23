using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    private TextMeshProUGUI _textScore;
    private int _score;

    private void Awake()
    {
        _textScore = GetComponent<TextMeshProUGUI>();
        _score = PlayerPrefs.GetInt("Score");
        _textScore.text = _score.ToString();
    }

    public void UpdateScore()
    {
        _score++;
        _textScore.text = _score.ToString();
        PlayerPrefs.SetInt("Score", _score);
    }
}
