using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    private static readonly Dictionary<int, int> _EnemiesByLevel = new Dictionary<int, int>()
    {
        {1,3},
        {2,5}
    };
    private int _numPickups = 0;
    private int _currentScore = 0;
    public int CurrentScore { get { return _currentScore; } }

    private int _currentLevel = 0;
    public int CurrentLevl { get { return _currentLevel; } }
    public int CurrentHealth;
    private UIManager uIManager = null;

    public GameManager Initialize(int StartLevel)
    {
        return this;
    }
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        ServiceLocator.Register<UIManager>(uiManager);
        ServiceLocator.Register<GameManager>(this);
    }
    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
        ServiceLocator.Get<UIManager>().UpdateHealthDisplay(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            ServiceLocator.Get<UIManager>().DisplayMessage("you loose");
            Time.timeScale = 0;
        }

    }

    public void increaseScore(int score)
    {
        _currentScore += score;
        ServiceLocator.Get<UIManager>().UpdateScoreDisplay(CurrentScore);
        if (CurrentScore >= 30)
        {
            ServiceLocator.Get<UIManager>().DisplayMessage("you Win");
            Time.timeScale = 0;
        }
    }
}
