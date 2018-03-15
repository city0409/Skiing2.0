﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    private Action onGameStart;
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

    private void OnEnable()
    {
        onGameStart = OnGameStart;
        EventService.Instance.GetEvent<GameStartEvent>().Subscribe(onGameStart);
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<GameStartEvent>().UnSubscribe(onGameStart);
    }

    private void OnGameStart()
    {
        //ChangeBackgroundMusic(_backgroundMusic1, _backgroundMusic2);
        ChangeBackgroundMusic(_backgroundMusic1, bgClip2);
    }

    private void OnReStartGame()
    {
        ChangeBackgroundMusic(_backgroundMusic1, bgClip1);
    }

    //推荐用第二种
    //private void ChangeBackgroundMusic(AudioSource oldAudio, AudioSource newAudio)
    //{
    //    if (bgCoroutine != null)
    //        StopCoroutine(bgCoroutine);
    //    bgCoroutine = StartCoroutine(FadeInOut.FadeSound(oldAudio, newAudio, 2f));
    //}

    private void ChangeBackgroundMusic(AudioSource audioSource, AudioClip newAudio)
    {
        if (bgCoroutine != null)
            StopCoroutine(bgCoroutine);
        bgCoroutine = StartCoroutine(FadeInOut.FadeSoundByCilp(audioSource, newAudio, 2f));
    }
}
