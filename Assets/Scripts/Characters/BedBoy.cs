using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BedBoy : MonoBehaviour 
{
    [SerializeField]
    private Transform posBornStone;

    [SerializeField]
    private GameObject zzzFx;
    [SerializeField]
    private float transRayDown = 1.2f;
    [SerializeField]
    private LayerMask layerMaskGround;

    private Action onBedBoyBorn;
    private float speed = 30f;
    private Quaternion relativeRotation;
    private bool isBedBoyGo = false;
    private bool isPlayerStayed = true;


    private void Start()
    {
        relativeRotation = Quaternion.Inverse(Quaternion.LookRotation(Vector3.right) * transform.rotation);
    }

    private void OnEnable()
    {
        onBedBoyBorn = OnBedBoyBorn;
        EventService.Instance.GetEvent<BedBoyBornEvent>().Subscribe(onBedBoyBorn);
    }
    private void Update()
    {
        if (isBedBoyGo)
        {
            MoveBedBoy();
            Destroy(zzzFx);
            if (isPlayerStayed && transform.position.x >= posBornStone.position.x )
            {
                LevelDirector.Instance.IsFollowSkiBoy = true;
                Destroy(gameObject);

                LevelDirector.Instance.InitPlayer();
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
    }

   
}
