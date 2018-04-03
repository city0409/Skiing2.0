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

    public void AppSuspendEvent()
    {
        EventService.Instance.GetEvent<AppSuspendEvent>().Publish();
    }

    public void BackToMainMenuEvent()
    {
        EventService.Instance.GetEvent<BackToMainMenuEvent>().Publish();
    }

    public void GameStartEvent()
    {
        EventService.Instance.GetEvent<GameStartEvent>().Publish();
        EventService.Instance.GetEvent<SlideBornEvent>().Publish();
    }

    public void PlayerResurgenceEvent()
    {
        TimeScale = 1f;

        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Publish();
    }

    public void GameEndEvent()
    {
        EventService.Instance.GetEvent<GameEndEvent>().Publish();
    }

    public void BedBoyBornEvent()
    {
        EventService.Instance.GetEvent<BedBoyBornEvent>().Publish();
    }

    public void OnPlayerSpawnEvent()
    {
        EventService.Instance.GetEvent<OnPlayerSpawnEvent>().Publish();
    }

    public void BeAboutToDieEvent()
    {
        EventService.Instance.GetEvent<BeAboutToDieEvent>().Publish();
    }

    public void PlayerDeadEvent()
    {
        TimeScale = 0f;
        EventService.Instance.GetEvent<PlayerDeadEvent>().Publish();
    }

    public void ResourceInitedEvent()
    {
        EventService.Instance.GetEvent<ResourceInitedEvent>().Publish();
    }

    public void UnPausedEvent()
    {
        EventService.Instance.GetEvent<UnPausedEvent>().Publish();
    }
}