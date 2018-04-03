using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ScoreDisplay : MonoBehaviour 
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreCoinText;
    [SerializeField] private RectTransform scoreRectTransfrom;
    [SerializeField] private RectTransform scoreCoinRectTransfrom;
    private Vector3 init_scoreRectTransfrom;
    private Vector3 init_scoreCoinRectTransfrom;

    private Action onBedBoySpawn;
    private Action onResurgence;

    private void Awake()
    {
        init_scoreRectTransfrom = scoreRectTransfrom.position;
        init_scoreCoinRectTransfrom = scoreCoinRectTransfrom.position;
    }

    private void OnEnable()
    {
        onBedBoySpawn = OnBedBoySpawn;
        EventService.Instance.GetEvent<BedBoyBornEvent>().Subscribe(onBedBoySpawn);
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
    }

    private void Update () 
	{
        scoreCoinText.text = LevelDirector.Instance.ScoreAward.ToString();
        scoreText.text = LevelDirector.Instance.Score.ToString();
    }

    private void OnBedBoySpawn()
    {
        scoreCoinRectTransfrom.DOLocalMoveY(-540f, 0.5f, false);
        scoreRectTransfrom.DOLocalMoveY(0f, 0.5f, false);
    }

    private void OnResurgence()
    {
        scoreRectTransfrom.position = init_scoreRectTransfrom;
        scoreCoinRectTransfrom.position = init_scoreCoinRectTransfrom;
        LevelDirector.Instance.AddHistoryScore();
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<BedBoyBornEvent>().UnSubscribe(onBedBoySpawn);
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }
}
