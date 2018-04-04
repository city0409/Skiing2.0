using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CalculateDistance : MonoBehaviour 
{
    private float lastX;

    private float score;
    private Action onResurgence;
    private Action onPlayerSpawn;

    private bool isCalculate =true;

    private void Start()
    {
        lastX = transform.position.x;
    }

    private void OnEnable()
    {
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
        onPlayerSpawn = OnPlayerSpawn;
        EventService.Instance.GetEvent<OnPlayerSpawnEvent>().Subscribe(onPlayerSpawn);
    }


    private void OnResurgence()
    {
        isCalculate = false;
        LevelDirector.Instance.Score = 0;
    }

    private void OnPlayerSpawn()
    {
        isCalculate = true;
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<OnPlayerSpawnEvent>().UnSubscribe(onPlayerSpawn);
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }

    private void Update()
    {
        if(isCalculate && transform.position.x > lastX + 5)//调加分数快慢
        {
            lastX = transform.position.x;
            score += 10;
            LevelDirector.Instance.Score = (int)score;
        }

    }
}
