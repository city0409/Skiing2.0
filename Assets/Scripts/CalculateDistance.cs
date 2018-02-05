using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDistance : MonoBehaviour 
{
    private float lastX;

    private float score;

    private void Start()
    {
        lastX = transform.position.x;
    }

    private void Update()
    {
        if(transform.position.x > lastX + 5)//调加分数快慢
        {
            lastX = transform.position.x;
            score += 10;
            LevelDirector.Instance.Score = (int)score;
        }

       
    }
}
