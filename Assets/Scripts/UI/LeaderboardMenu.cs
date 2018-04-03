﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject LayoutGroupName;

    private PlayerData data;
    private List<Text> scoreTexts = new List<Text>();
    private List<Text> nameTexts = new List<Text>();

    private void Awake()
    {
        data = LevelDirector.Instance.Data;
        //data = Resources.Load<PlayerData>("PlayerData");
        foreach (Transform item in LayoutGroupName.transform)
        {
            nameTexts.Add(item.GetComponent<Text>());
            scoreTexts.Add(item.GetChild(0).GetComponent<Text>());
        }
    }

    private void Start()
    {
        data.LeaderboardDatas.Sort();
        data.LeaderboardDatas.Reverse();
        for (int i = 0; (i < scoreTexts.Count) && (i < data.LeaderboardDatas.Count); i++)
        {
            scoreTexts[i].text = data.LeaderboardDatas[i].score.ToString();
            nameTexts[i].text = data.LeaderboardDatas[i].name;
        }

    }
}
