using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector>
{
    [SerializeField]
    private GameObject GroundTestPos1;
    [SerializeField]
    private GameObject GroundTestPos2;
    [SerializeField]
    private PlayerController playerController;
    public PlayerController PlayerController { get { return playerController; } set { playerController = value; } }
    [SerializeField]
    private PlayerMotor playerMotor;
    public PlayerMotor PlayerMotor { get { return playerMotor; } set { playerMotor = value; } }

    [SerializeField]
    private Vector3 initPlayerPos;
    [SerializeField]
    private Vector3 initGround2Pos;

    private int score;
    public int Score{get { return score; }set{score = value;}}
    private int scoreAward;
    public int ScoreAward { get { return scoreAward; } set { scoreAward = value; } }

    private PlayerData data;


    private void Start () 
	{
        playerController.MyState.IsSkiing = true;
        InitPlayer();
    }
	
	public void InitPlayer () 
	{
        playerController.transform.position = initPlayerPos;
        playerController.GetComponentInChildren<TrailRenderer>().time = 0.1f;
        playerMotor.Cur_velocity = Vector2.zero;
        //playerMotor.Visual1.SetActive(true);
        //playerMotor.Visual2.SetActive(false);//这两句主角会不见
        //playerMotor.Visual3.SetActive(false);
        //playerMotor.Reset = true;
        GroundTestPos1.transform.position = initGround2Pos;
        GroundTestPos2.transform.position = initGround2Pos;
    }

    private void AddHistoryScore()
    {
        if (score < 0) return;
        if (data.LeaderboardDatas.Count >= 10)
        {
            for (int i = 0; i < data.LeaderboardDatas.Count; i++)
            {
                if (score > data.LeaderboardDatas[i].score)
                {
                    LeaderboardData leaderboardData = new LeaderboardData();
                    leaderboardData.score = score;
                    leaderboardData.date = System.DateTime.Now.ToString("yy-MM-dd,h:mm:ss tt");
                    data.LeaderboardDatas.Add(leaderboardData);
                    break;
                }
            }
            if (data.LeaderboardDatas.Count > 10)
            {
                data.LeaderboardDatas.RemoveAt(data.LeaderboardDatas.Count - 2);
            }
        }
        else
        {
            LeaderboardData leaderboardData = new LeaderboardData();
            leaderboardData.score = score;
            leaderboardData.date = System.DateTime.Now.ToString("yy-MM-dd,h:mm:ss tt");
            data.LeaderboardDatas.Add(leaderboardData);
        }
    }
}
