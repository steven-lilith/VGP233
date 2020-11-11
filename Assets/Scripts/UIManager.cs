using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text pointsText;


    public void UpdateScoreDisplay(int currentScore)
    {
        pointsText.text = "Score: " + currentScore.ToString();
    }
}
