using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainCamera : MonoBehaviour 
{
    private Camera camera ;
    private Action onGameStart;
    private bool isEnlargeCamera=false;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        onGameStart = OnGameStart;
        EventService.Instance.GetEvent<GameStartEvent>().Subscribe(onGameStart);
    }

    private void Update()
    {
        if (isEnlargeCamera && camera.orthographicSize >= 7f)
        {
            camera.orthographicSize -= 0.1f;
        }
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<GameStartEvent>().UnSubscribe(onGameStart);
    }

    private void OnGameStart()
    {
        isEnlargeCamera = true;
    }
}
