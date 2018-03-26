using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour 
{
    private Camera camera;
    private Action onGameStart;
    private bool isEnlargeCamera = false;

    [SerializeField] private Transform mainPlayer;
    [SerializeField] private Transform bedBoy;
    private Transform currentPlayer;

    [SerializeField] private Transform bgText;

    private Vector3 tempPos;
    [SerializeField] private LayerMask mask;

    private bool isFollowSkiBoy = false;

    private Vector2 change;

    private void Awake () 
	{
        camera = GetComponent<Camera>();
        currentPlayer = bedBoy;
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
            camera.transform.SetLocalXY(camera.transform.position.x + 0.1f, camera.transform.position.y - 0.2f); //扩展方法SetLocalY
        }
    }

    private void LateUpdate () 
	{
        if (Input.GetKeyDown(KeyCode.A))
        {
            isFollowSkiBoy = true;
            
        }
        if (isEnlargeCamera)
        {
            bgText.transform.position = GetComponent<Transform>().position + new Vector3(0f, 0f, 20f);
            if (isFollowSkiBoy)
            {
                currentPlayer = mainPlayer;
                change = new Vector2(13, -2);
            }
            RaycastHit2D hit = Physics2D.Raycast(currentPlayer.position, Vector2.down, 30f, mask);
            if (hit == true)
            {
                tempPos = Vector2.Lerp(tempPos, hit.point + change, Time.deltaTime * 5);
                tempPos.z = -10;
                transform.position = tempPos;
            }
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

    private void OnDrawGizmosSelected()
    {
        if (currentPlayer != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(currentPlayer.position, currentPlayer.position + new Vector3(0, -30, 0));
        }
    } 
}
