using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour 
{
    public delegate void OnDead();
    public event OnDead OnDeadEvent;


    private void Update () 
	{
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision .CompareTag ("Player"))
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        if (OnDeadEvent!=null)
        {
            OnDeadEvent();
        }
        Destroy(gameObject);
    }
}
