using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class HomeUIPannel : MonoBehaviour 
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button rankButton;

    [SerializeField] private Transform down_Items;
    [SerializeField] private Transform up_Items;
    

    private CanvasGroup homeUICanvasGroup;
    //[SerializeField] private CanvasGroup rankCanvasGroup;

    
    private Tweener EnlargeButton;

    private void Awake()
    {
        homeUICanvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start() 
	{
        Sequence seq = DOTween.Sequence();
        //EnlargeButton = startGameButton.transform.DOScale(2, 1);


    }

    private void Update () 
	{
        if (UIManager.Instance.IsRank)
        {
            //rankButton.GetComponent<RectTransform>().localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    public void RankGame()
    {
        UIManager.Instance.IsRank = true;

        UIManager.Instance.FaderOn(true, 1f);

    }

    public void StartGame()
    {
        homeUICanvasGroup.interactable = false;
        homeUICanvasGroup.blocksRaycasts = false;

        down_Items.DOMoveY(-500f, 2f, true);
        up_Items.DOMoveY(1500f, 1f, true);
        
        GameManager.Instance.GameStartEvent();
        StartCoroutine(EnFadeButton());
    }
    private IEnumerator EnFadeButton()
    {
        yield return new WaitForSeconds(2f);
        homeUICanvasGroup.alpha = 0;
    }
    
}
