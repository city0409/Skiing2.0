using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector>
{
    [SerializeField] private GameObject Ground001;
    [SerializeField] private GameObject Ground002;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject Fxfeather;
    private GameObject fxFeatherOBJ;
    private GameObject playerOBJ;
    public GameObject PlayerOBJ { get { return playerOBJ; } private set { playerOBJ = value; } }
    private bool isFollowSkiBoy = false;
    public bool IsFollowSkiBoy { get { return isFollowSkiBoy; } set { isFollowSkiBoy = value; } }
    private Action onResurgence;

    [SerializeField] private Vector3 initPlayerPos;
    [SerializeField] private Vector3 initSlidePos;
    public Vector3 InitSlidePos { get { return initSlidePos; } set { initSlidePos = value; } }

    [SerializeField] private Vector3 initGround2Pos;


    private int score;
    public int Score{get { return score; }set{score = value;}}
    private int scoreAward;
    public int ScoreAward { get { return scoreAward; } set { scoreAward = value; } }
    [SerializeField]
    private PlayerData data;
    public PlayerData Data { get { return data; } set { data = value; } }

    [SerializeField]
    private GameObject bedBoyOBJ;
    public GameObject BedBoyOBJ { get { return bedBoyOBJ; } private set { bedBoyOBJ = value; } }
    [SerializeField]
    private GameObject slideOBJ;
    public GameObject SlideOBJ { get { return slideOBJ; } private set { slideOBJ = value; } }

    private void Start () 
	{
        InitBG();

    }

    private void OnEnable()
    {
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
    }
    private void OnResurgence()
    {
        Destroy(playerOBJ);
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }

    public void InitBG()
    {
        Ground001.transform.position = initGround2Pos;
        Ground002.transform.position = initGround2Pos;
    }

    public void InitFxFeather(Vector3 curPos)
    {
        fxFeatherOBJ = Instantiate(Fxfeather, curPos, Quaternion.identity);
    }

    public void InitPlayer () 
	{
        playerOBJ = Instantiate(playerPrefab, initPlayerPos, Quaternion.identity);
    }

    public void TripleClickResurgence()//三连击应该是InputManager管的事情
    {
        if (playerOBJ == null) return;
        playerOBJ.GetComponent<PlayerMotor>().Reset = true;
        playerOBJ.GetComponent<PlayerController>().MyState.IsLie = false;
        playerOBJ.GetComponent<PlayerController>().MyState.IsOnGround = true;
        playerOBJ.GetComponent<PlayerController>().MyState.IsSkiing = true;
        playerOBJ.GetComponent<PlayerController>().MyState.IsRollling = false;
        //Vector3 pos = playerOBJ.transform.position;
        //Destroy(playerOBJ);
        //Instantiate(playerPrefab, pos, Quaternion.identity);
        playerOBJ.GetComponent<PlayerMotor>().Rig.bodyType = RigidbodyType2D.Dynamic;

    }

    public void AddHistoryScore()
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
