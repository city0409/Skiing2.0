﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour 
{
    private Camera camera;
    private Action onGameStart;
    private Action onResurgence;
    private Vector3 init_Transfrom;

    private bool isEnlargeCamera = false;

    [SerializeField] private GameObject bedBoy;
    private GameObject currentPlayer;

    [SerializeField] private Transform bgText;

    private Vector3 tempPos;
    [SerializeField] private LayerMask mask;

    private Vector2 change;

    private void Awake () 
	{
        camera = GetComponent<Camera>();
        currentPlayer = bedBoy;
        init_Transfrom = new Vector3(0f,1f,-10f);
    }

    private void OnEnable()
    {
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
        onGameStart = OnGameStart;
        EventService.Instance.GetEvent<GameStartEvent>().Subscribe(onGameStart);
    }

    private void OnResurgence()
    {
        camera.GetComponent<Transform>().position = new Vector3(0f,1f,-10f);
        camera.orthographicSize = 10f;
        currentPlayer = bedBoy;
        isEnlargeCamera = false;
        LevelDirector.Instance.IsFollowSkiBoy = false;
        tempPos = Vector3.zero;
        change = Vector3.zero;
    }

    private void Update()
    {
        if (isEnlargeCamera && camera.orthographicSize >= 9.5f)
        {
            camera.orthographicSize -= 0.05f;
            camera.transform.SetLocalXY(camera.transform.position.x + 0.1f, camera.transform.position.y - 0.2f); //扩展方法SetLocalY
        }
    }

    private void LateUpdate () 
	{
        if (LevelDirector.Instance.IsFollowSkiBoy)
        {
            currentPlayer = LevelDirector.Instance .PlayerOBJ;
            change = new Vector2(13f, -1.5f);
        }
        if (isEnlargeCamera)
        {
            bgText.transform.position = GetComponent<Transform>().position + new Vector3(0f, 0f, 40f);
            
            RaycastHit2D hit = Physics2D.Raycast(currentPlayer.transform.position, Vector2.down, 1000f, mask);
            if (hit == true)
            {
                tempPos = Vector2.Lerp(tempPos, hit.point + change, Time.deltaTime*5);
                tempPos.z = -10;
                transform.position = tempPos;
                transform.position = new Vector3(transform.position.x,
                                     Mathf.Clamp(transform.position.y, currentPlayer.transform.position.y - 7f, currentPlayer.transform.position.y + 3f),
                                     transform.position.z);
            }
        }
        
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<GameStartEvent>().UnSubscribe(onGameStart);
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }

    private void OnGameStart()
    {
        isEnlargeCamera = true;
        StartCoroutine(FollowBedBoy());
    }

    IEnumerator FollowBedBoy()
    {
        yield return new WaitForSeconds(0.5f);
        change = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), new Vector2(13, -2), 2f);

    }

    private void OnDrawGizmosSelected()
    {
        if (currentPlayer != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentPlayer.transform.position, currentPlayer.transform.position + new Vector3(0, -30, 0));
        }
    } 
}
