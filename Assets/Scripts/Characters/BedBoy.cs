using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BedBoy : MonoBehaviour 
{
    [SerializeField] private Transform posBornStone;
    [SerializeField] private GameObject zzzFx;
    [SerializeField] private float transRayDown = 1.2f;
    [SerializeField] private LayerMask layerMaskGround;

    private Action onBedBoyBorn;
    private Action onResurgence;

    private float speed = 30f;
    private Quaternion relativeRotation;
    private bool isBedBoyGo = false;
    private bool isPlayerStayed = true;
    private Vector3 init_Transfrom;

    private void Awake()
    {
        init_Transfrom = transform.position;
    }

    private void Start()
    {
        relativeRotation = Quaternion.Inverse(Quaternion.LookRotation(Vector3.right) * transform.rotation);
    }

    private void OnEnable()
    {
        onBedBoyBorn = OnBedBoyBorn;
        EventService.Instance.GetEvent<BedBoyBornEvent>().Subscribe(onBedBoyBorn);
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
    }

    private void OnResurgence()
    {
        transform.position = init_Transfrom;
        gameObject.SetActive(true);
        isBedBoyGo = false;
        zzzFx.SetActive(true);

    }

    private void Update()
    {
        if (isBedBoyGo)
        {
            MoveBedBoy();
            zzzFx.SetActive(false);
            if (isPlayerStayed && transform.position.x >= posBornStone.position.x )
            {
                LevelDirector.Instance.IsFollowSkiBoy = true;
                gameObject.SetActive(false);
                LevelDirector.Instance.InitPlayer();
                GameManager.Instance.OnPlayerSpawnEvent();
                isPlayerStayed = false;
            }
        }
    }
    private void OnBedBoyBorn()
    {
        isBedBoyGo = true;
    }

    public void MoveBedBoy()
    {
        Vector2 nextFramePos = transform.position;
        nextFramePos.x += speed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(nextFramePos, Vector3.down, transRayDown, layerMaskGround);
        if (hit.collider != null)
        {
            Vector2 v2 = Vector2.up * 0.77f + hit.point;
            transform.position = v2;
            Vector3 normal = hit.normal;
            Quaternion direction = Quaternion.LookRotation(Vector3.Cross(normal, Vector3.forward));
            direction *= relativeRotation;
            transform.rotation = direction;
        }

    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<BedBoyBornEvent>().UnSubscribe(onBedBoyBorn);
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);

    }


}
