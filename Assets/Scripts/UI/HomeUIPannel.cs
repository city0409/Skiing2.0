using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class HomeUIPannel : MonoBehaviour 
{
    //[SerializeField] private Button startGameButton;
    //[SerializeField] private Button rankButton;

    [SerializeField] private Transform down_Items;
    [SerializeField] private Transform up_Items;
    [SerializeField] private Image fader;
    
    private CanvasGroup homeUICanvasGroup;

    private Vector3 init_down_Items;
    private Vector3 init_up_Items;
    private Action onResurgence;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        homeUICanvasGroup = GetComponent<CanvasGroup>();
        init_down_Items = down_Items.position;
        init_up_Items = up_Items.position;
        
    }

    private void OnEnable()
    {
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
    }

    private void OnResurgence()
    {
        down_Items.position = init_down_Items;
        up_Items.position = init_up_Items;
        homeUICanvasGroup.alpha = 1;
        homeUICanvasGroup.interactable = true;
        homeUICanvasGroup.blocksRaycasts = true;
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }

    private void Start() 
	{
        Sequence seq = DOTween.Sequence();
    }

    public void RankGame()
    {
        UIManager.Instance.FaderOn(false, 1f);
        //fader.gameObject.SetActive(true);
        //fadeCoroutine = StartCoroutine(FadeInOut.FadeImage(fader, 0.5f, new Color(0, 0, 0, 0f)));
        UIManager.Instance.IsRank = true;
    }

    public void StartGame()
    {
        GameManager.Instance.GameStartEvent();
        GameManager.Instance.BeAboutToDieEvent();

        homeUICanvasGroup.interactable = false;
        homeUICanvasGroup.blocksRaycasts = false;

        down_Items.DOMoveY(-500f, 2f, true);
        up_Items.DOMoveY(1500f, 1f, true);

        
        StartCoroutine(EnFadeButton());
    }
    private IEnumerator EnFadeButton()
    {
        yield return new WaitForSeconds(2f);
        homeUICanvasGroup.alpha = 0;
    }

    public void BackHomeRank()
    {
        //GameManager.Instance.Reset();
        UIManager.Instance.FaderOn(false, 1f);
        UIManager.Instance.IsRank = false;
    }

    public void RestartGame()
    {
        GameManager.Instance.PlayerResurgenceEvent();
        UIManager.Instance.FaderOn(false, 1f);
        UIManager.Instance.IsRank = false;
        StartGame();
    }
}
