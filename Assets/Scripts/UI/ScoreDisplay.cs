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

    private Action onBedBoySpawn;

    private void OnEnable()
    {
        onBedBoySpawn = OnBedBoySpawn;
        EventService.Instance.GetEvent<BedBoyBornEvent>().Subscribe(onBedBoySpawn);

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

    private void OnDisable()
    {
        EventService.Instance.GetEvent<BedBoyBornEvent>().UnSubscribe(onBedBoySpawn);

    }
}
