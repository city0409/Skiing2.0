using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoneBorn : MonoBehaviour 
{
    private Action onResurgence;
    private Vector3 init_Transfrom;

    private void Awake()
    {
        init_Transfrom = transform.position;
    }

    private void OnEnable()
    {
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
    }

    private void OnResurgence()
    {
        transform.position = init_Transfrom;
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LevelDirector.Instance.InitFxFeather(transform.position);
            gameObject.SetActive(false);
        }
    }
}
