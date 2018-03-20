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
        if (isEnlargeCamera && camera.orthographicSize >= 8.8f)
        {
            camera.orthographicSize -= 0.05f;
            //y = camera.transform.position.y - 0.1f;
            //camera.transform.position = new Vector3(camera.transform.position.x, y, camera.transform.position.z);
            camera.transform.SetLocalXY(camera.transform.position.x + 0.1f,camera.transform.position.y - 0.2f); //扩展方法SetLocalY
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
