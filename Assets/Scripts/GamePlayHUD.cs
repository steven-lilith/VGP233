using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayHUD : MonoBehaviour
{
    [SerializeField] private Text MessageText = null;
    [SerializeField] private Text ScoreText = null;
    [SerializeField] private Text HealthText = null;
    public void Initialize()
    {
        MessageText.text = string.Empty;
        ScoreText.text = "0";
    }

    public void SetGamePlayHUDActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void UpdateScore(int currentScore)
    {
        ScoreText.text = currentScore.ToString(); 
    }
    public void UpdateHealth(int currentHealth)
    {
        HealthText.text = "Health: " + currentHealth.ToString();
    }
    public void UpdateMessageText(string message)
    {
        MessageText.text = message; 
    }
}
