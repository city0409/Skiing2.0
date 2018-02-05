using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour 
{
    private  LevelDirector director;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text scoreAwardText;

    private void Start () 
	{
        director = LevelDirector.Instance;

    }

    private void Update () 
	{
        scoreText.text = director.ScoreAward.ToString();
        scoreAwardText.text =director .Score.ToString();
    }
}
