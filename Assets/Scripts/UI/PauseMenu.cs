using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot paused, unpaused;

    [SerializeField]
    private CanvasGroup pauseGroup;
    
    private List<CanvasGroup> canvasGroupList = new List<CanvasGroup>();

    private void Start()
    {
        canvasGroupList.Add(pauseGroup);
        DisplayMenu();
    }

    private void Update()
    {

    }

    public void PauseGame()
    {
        GameManager.Instance.Pause();
        DisplayMenu();
    }

    public void ContinueGame()
    {
        GameManager.Instance.Reset();
        DisplayMenu();
    }

    public void RestartGame()
    {
        GameManager.Instance.Reset();
        DisplayMenu();
        //UIManager.Instance.FaderOn(true, 0.1f);
        //LevelDirector.Instance.PlayerController.GetComponentInChildren<TrailRenderer>().isVisible = false;

        //LevelDirector.Instance.InitPlayer();
    }
    public void BackHome()
    {
        GameManager.Instance.Reset();
        DisplayMenu();
        UIManager.Instance.FaderOn(true, 1f);
        LoadSceneManager.LoadScene(1);
    }

    public void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(0.01f);
        }
        else
        {
            unpaused.TransitionTo(0.01f);
        }
    }

    //    public void Exit()
    //    {
    //#if UNITY_EDITOR
    //        EditorApplication.isPlaying = false;
    //#else
    //            Application.Quit();
    //#endif
    //    }

    private void DisplayMenu()
    {
        foreach (var item in canvasGroupList)
        {
            item.alpha = 0;
            item.interactable = false;
            item.blocksRaycasts = false;
        }
        if (GameManager.Instance.IsPaused == true)
        {
            CanvasGroup cg = pauseGroup;
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }
        Lowpass();
    }
}