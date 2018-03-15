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

    [SerializeField]
    private Transform guidepost;
    [SerializeField]
    private Transform QQMountain;

    private CanvasGroup homeUICanvasGroup;

    private bool isRank=false;
    private Tweener EnlargeButton;

    private void Awake()
    {
        homeUICanvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start() 
	{
        Sequence seq = DOTween.Sequence();
        EnlargeButton = startGameButton.transform.DOScale(2, 1);

        guidepost.DOMoveY(-614.36f, 2f, false);

    }

    private void Update () 
	{
        if (isRank)
        {
            //rankButton.GetComponent<RectTransform>().localScale += new Vector3(0.2f, 0.2f, 0.2f);
        }
    }

    public void RankGame()
    {
        isRank = true;

        //StartCoroutine(EnlargeButton());
        UIManager.Instance.FaderOn(true, 1f);
    }

    public void StartGame()
    {
        homeUICanvasGroup.interactable = false;
        homeUICanvasGroup.blocksRaycasts = false;
        GameManager.Instance.GameStartEvent();
    }

    //private IEnumerator EnlargeButton()
    //{
    //    //rankButton.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 2f);
    //    yield return new WaitForSeconds(10f);
    //    print("!!!!!");
    //}
}
