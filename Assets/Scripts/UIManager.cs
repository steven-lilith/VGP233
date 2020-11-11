using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject hudPrefab = null;
    public GamePlayHUD _hud = null;

    private void Awake()
    {
        if(hudPrefab==null)
        {
            Debug.Log("no hud assigned");
            return;
        }
    }

    public void Initialize()
    {
        var hudObject = Instantiate(hudPrefab);
        hudObject.transform.SetParent(transform);
        _hud = hudObject.GetComponent<GamePlayHUD>();
        if(_hud==null)

        {
            Debug.LogError("gamehud is null");
            return;
        }
        _hud.Initialize();
    }

    public void UpdateScoreDisplay(int currentScore)
    {
        _hud.UpdateScore(currentScore);
    }

    public void UpdateHealthDisplay(int currentHealth)
    {
        _hud.UpdateHealth(currentHealth);
    }

    public void DisplayMessage(string message)
    {
        _hud.UpdateMessageText(message);
    }
}
