using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPannel : MonoBehaviour 
{
    private CanvasGroup rankCanvasGroup;
    private void Awake() 
	{
        rankCanvasGroup = GetComponent<CanvasGroup>();

    }
	
	private void Update () 
	{
        if (UIManager.Instance.IsRank)
        {
            rankCanvasGroup.alpha = 1;
            rankCanvasGroup.interactable = true;
            rankCanvasGroup.blocksRaycasts = true;
        }
	}
}
