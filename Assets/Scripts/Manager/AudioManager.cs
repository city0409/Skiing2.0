using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    private Action onGameStart;
    private Action onGameEnd;
    private Action onResurgence;

    [Range(0, 1)]
    public float MusicVolume = 0.3f;
    [SerializeField]
    private AudioSource _backgroundMusic1 = null;
    //[SerializeField]
    //private AudioSource _backgroundMusic2 = null;

    private AudioSource currentBGM;
    private Coroutine bgCoroutine;
    [SerializeField]
    private AudioClip bgClip1;
    [SerializeField]
    private AudioClip bgClip2;
    [SerializeField]
    private AudioClip gameoverClip3;

    private void Awake()
    {
        _backgroundMusic1 = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        onGameStart = OnGameStart;
        EventService.Instance.GetEvent<GameStartEvent>().Subscribe(onGameStart);
        onGameEnd = OnGameEnd;
        EventService.Instance.GetEvent<GameEndEvent>().Subscribe(onGameEnd);
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
    }

    private void OnResurgence()
    {
        OnReStartGame();
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<GameStartEvent>().UnSubscribe(onGameStart);
        EventService.Instance.GetEvent<GameEndEvent>().UnSubscribe(onGameEnd);
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }

    private void OnGameStart()
    {
        //ChangeBackgroundMusic(_backgroundMusic1, _backgroundMusic2);
        ChangeBackgroundMusic(_backgroundMusic1, bgClip2, true);
    }

    private void OnReStartGame()
    {
        ChangeBackgroundMusic(_backgroundMusic1, bgClip1, true);
    }

    private void OnGameEnd()
    {
        ChangeBackgroundMusic(_backgroundMusic1, gameoverClip3, false);
    }

    //推荐用第二种
    //private void ChangeBackgroundMusic(AudioSource oldAudio, AudioSource newAudio)
    //{
    //    if (bgCoroutine != null)
    //        StopCoroutine(bgCoroutine);
    //    bgCoroutine = StartCoroutine(FadeInOut.FadeSound(oldAudio, newAudio, 2f));
    //}

    private void ChangeBackgroundMusic(AudioSource audioSource, AudioClip newAudio,bool isLoop)
    {
        if (bgCoroutine != null)
            StopCoroutine(bgCoroutine);
       
        audioSource.loop = isLoop;
        bgCoroutine = StartCoroutine(FadeInOut.FadeSoundByCilp(audioSource, newAudio, 2f));
    }
}
