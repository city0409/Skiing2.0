using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDirector : Singleton<LevelDirector>
{
    [SerializeField]
    private PlayerController player;
    public PlayerController Player { get { return player; } set { player = value; } }
    

    private int score;
    public int Score{get { return score; }set{score = value;}}
    private int scoreAward;
    public int ScoreAward { get { return scoreAward; } set { scoreAward = value; } }

    private void Start () 
	{
        //player = GetComponent<PlayerController>();
        player.MyState.IsSkiing = true;
    }
	
	private void Update () 
	{
		
	}
}
