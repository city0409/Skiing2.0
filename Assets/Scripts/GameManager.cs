using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    public int GameFrameRate = 300;
    public float TimeScale { get { return Time.timeScale; } private set { Time.timeScale = value; } }

    //public PlayerController Player { get; set; }

    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } set { isPaused = value; } }

    private float savedTimeScale = 1f;
    //public AppState appState { get; set; }
    protected void Start()
    {
        Application.targetFrameRate = GameFrameRate;
    }

    public void PauseGame()
    {
        TimeScale = 0f;
    }

    public void Reset()
    {
        TimeScale = 1f;
        isPaused = false;
    }

    public virtual void Pause()
    {
        if (Time.timeScale > 0.0f)
        {
            Instance.SetTimeScale(0.0f);
            isPaused =  true;
        }
        else
        {
            UnPause();
        }
    }

    public virtual void UnPause()
    {
        Instance.ResetTimeScale();
        isPaused = false;
    }
    public void SetTimeScale(float newTimeScale)
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = newTimeScale;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = savedTimeScale;
    }
}