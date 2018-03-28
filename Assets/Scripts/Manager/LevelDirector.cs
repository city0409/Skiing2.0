using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector>
{
    [SerializeField] private GameObject Ground001;
    [SerializeField] private GameObject Ground002;
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerOBJ;
    public GameObject PlayerOBJ { get { return playerOBJ; } private set { playerOBJ = value; } }
    private bool isFollowSkiBoy = false;
    public bool IsFollowSkiBoy { get { return isFollowSkiBoy; } set { isFollowSkiBoy = value; } }

    [SerializeField] private Vector3 initPlayerPos;
    [SerializeField] private Vector3 initGround2Pos;

    private int score;
    public int Score{get { return score; }set{score = value;}}
    private int scoreAward;
    public int ScoreAward { get { return scoreAward; } set { scoreAward = value; } }

    private PlayerData data;
    [SerializeField]
    private GameObject bedBoyOBJ;
    public GameObject BedBoyOBJ { get { return bedBoyOBJ; } private set { bedBoyOBJ = value; } }

    private void Start () 
	{
        InitBG();
    }
	
    public void InitBG()
    {
        Ground001.transform.position = initGround2Pos;
        Ground002.transform.position = initGround2Pos;
    }

    public void InitPlayer () 
	{
        playerOBJ = Instantiate(playerPrefab, initPlayerPos, Quaternion.identity);
        playerOBJ.GetComponent<PlayerController>().MyState.IsSkiing = true;
        playerOBJ.GetComponentInChildren<TrailRenderer>().time = 0.1f;
        playerOBJ.GetComponent<PlayerMotor>().Cur_velocity = Vector2.zero;
    }

    private void AddHistoryScore()
    {
        if (score < 0) return;
        if (data.LeaderboardDatas.Count >= 5)
        {
            for (int i = 0; i < data.LeaderboardDatas.Count; i++)
            {
                if (score > data.LeaderboardDatas[i].score)
                {
                    LeaderboardData leaderboardData = new LeaderboardData();
                    leaderboardData.score = score;
                    leaderboardData.name = data.playerName;
                    data.LeaderboardDatas.Add(leaderboardData);
                    break;
                }
            }
            if (data.LeaderboardDatas.Count > 5)
            {
                data.LeaderboardDatas.RemoveAt(data.LeaderboardDatas.Count - 2);
            }
        }
        else
        {
            LeaderboardData leaderboardData = new LeaderboardData();
            leaderboardData.score = score;
            leaderboardData.name = data.playerName;
            data.LeaderboardDatas.Add(leaderboardData);
        }
    }


}
