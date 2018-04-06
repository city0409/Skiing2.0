using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeadlineSlide : MonoBehaviour 
{
    private bool startDeadline = false;
    public bool StartDeadline { get { return startDeadline; } set { startDeadline = value; } }

    private float timer = 6f;
    public float Timer { get { return timer; } set { timer = value; } }

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
                transform.GetComponent<AroundInitOBJ>().InitSlide();
                GameManager.Instance.BeAboutToDieEvent();
                timer = 4f;
            }
        }
        
    }
}
