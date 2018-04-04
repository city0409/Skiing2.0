using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeadlineSlide : MonoBehaviour 
{
    public bool startDeadline = false;
    private float timer = 6f;
    private void Awake() 
	{
		
	}
	
	private void Update () 
	{
        if (startDeadline)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                startDeadline = false;
                print("nice func once.");
                LevelDirector.Instance.InitSlide();
                timer = 4f;
            }
        }
        
    }
}
