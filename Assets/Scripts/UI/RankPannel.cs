using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPannel : MonoBehaviour 
{
    private CanvasGroup rankGroup;

    private void Awake() 
	{
        rankGroup = GetComponent<CanvasGroup>();
    }
	
	private void Update () 
	{
        if (UIManager.Instance.IsRank)
        {
            rankGroup.alpha = 1;
            rankGroup.interactable = true;
            rankGroup.blocksRaycasts = true;
        }
        else
        {
            rankGroup.alpha = 0;
            rankGroup.interactable = false;
            rankGroup.blocksRaycasts = false;
        }
	}
}
