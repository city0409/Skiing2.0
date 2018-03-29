using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameOverBgMenu : MonoBehaviour 
{
    private Action onPlayerDead;
    private CanvasGroup gameOverCanvasGroup;
    //[SerializeField]
    //private Button restartGameButton;
    //[SerializeField]
    //private Button endButton;

    private void Awake() 
	{
        gameOverCanvasGroup = GetComponent<CanvasGroup>();

    }

    private void OnEnable()
    {
        onPlayerDead = OnPlayerDead;
        EventService.Instance.GetEvent<PlayerDeadEvent>().Subscribe(onPlayerDead);
    }

    private void Update () 
	{
		
	}

    private void OnPlayerDead()
    {
        gameOverCanvasGroup.alpha = 1;
        gameOverCanvasGroup.interactable = true;
        gameOverCanvasGroup.blocksRaycasts = true;
    }

    public void OnGameEnd()
    {
        gameOverCanvasGroup.alpha = 0;
        gameOverCanvasGroup.interactable = false;
        gameOverCanvasGroup.blocksRaycasts = false;
        GameManager.Instance.GameEndEvent();
        StartCoroutine(EndGameRankDisplay());
    }

    private IEnumerator EndGameRankDisplay()
    {
        yield return new WaitForSeconds(4f);
        UIManager.Instance.FaderOn(true, 1f);
        UIManager.Instance.IsRank = true;
    }

    public void OnRestartGame()
    {


    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<PlayerDeadEvent>().UnSubscribe(onPlayerDead);
    }
}
