using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour 
{
    [SerializeField]
    private string loadSceneRank;
    [SerializeField]
    private Button startGameButton;
    [SerializeField]
    private Button rankButton;

    private bool isRank=false;

    private void Awake() 
	{
        rankButton = GetComponent<Button>();

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
        SceneManager.LoadScene(loadSceneRank);
    }

    private IEnumerator EnlargeButton()
    {
        //rankButton.GetComponent<RectTransform>().localScale = new Vector3(2f, 2f, 2f);
        yield return new WaitForSeconds(10f);
        print("!!!!!");
    }
}
