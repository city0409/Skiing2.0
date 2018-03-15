using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Test : MonoBehaviour 
{

	private void Awake() 
	{
		
	}
	
	private void Update () 
	{
        transform.DOShakePosition(3f, 2, 10, 90, false, true);

        //transform.DOShakeRotation(1f, 90, 10, 90, true);
    }
}
