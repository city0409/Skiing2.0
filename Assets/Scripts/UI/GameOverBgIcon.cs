using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverBgIcon : MonoBehaviour 
{
    private Action onGameOver;
    private CanvasGroup gameOverCanvasGroup;

    private void Awake() 
	{
        gameOverCanvasGroup = GetComponent<CanvasGroup>();

    }

    private void OnEnable()
    {
        onGameOver = OnGameOver;
        EventService.Instance.GetEvent<PlayerDeadEvent>().Subscribe(onGameOver);
    }

    private void Update () 
	{
		
	}

    private void OnGameOver()
    {
        gameOverCanvasGroup.alpha = 1;
        gameOverCanvasGroup.interactable = true;
        gameOverCanvasGroup.blocksRaycasts = true;

    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<PlayerDeadEvent>().UnSubscribe(onGameOver);
    }
}
