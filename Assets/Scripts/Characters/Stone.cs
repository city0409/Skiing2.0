using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stone : MonoBehaviour 
{
    

    private void Update () 
	{
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelDirector.Instance.InitFxFeather(transform.position);
            Destroy(gameObject, 0.05f);
        }
    }
}
