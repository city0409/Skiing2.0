using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class SnowSlide : MonoBehaviour
{
    private const string pauseName = "pause";
    private const string continueName = "continue";
    private const string restartName = "restart";
    [SerializeField] private float transRayDown = 1.2f;
    [SerializeField] private LayerMask layerMaskGround;

    private Action onSlideBorn;
    private float speed = 40f;
    private Quaternion relativeRotation;
    private bool isSlideGo = false;


    private void Start()
    {
        relativeRotation = Quaternion.Inverse(Quaternion.LookRotation(Vector3.right) * transform.rotation);
    }

    private void OnEnable()
    {
        onSlideBorn = OnSlideBorn;
        EventService.Instance.GetEvent<SlideBornEvent>().Subscribe(onSlideBorn);
    }

    private void Update()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        if (obj && (obj.name == pauseName || obj.name == continueName || obj.name == restartName))
        {
            return;
        }
        else if (isSlideGo)
        {
            MoveSlide();
        }
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<SlideBornEvent>().UnSubscribe(onSlideBorn);
    }

    private void OnSlideBorn()
    {
        isSlideGo = true;
        StartCoroutine(threaten());
    }

    private IEnumerator threaten()
    {
        yield return new WaitForSeconds(0.5f);
        isSlideGo = false;
        GameManager.Instance.BedBoyBornEvent();
    }

    public void MoveSlide()
    {
        //if (LevelDirector.Instance.PlayerOBJ.GetComponent<PlayerController>().MyState.IsDie)
        //{
        //    return;
        //}
        //else
        //{
            Vector2 nextFramePos = transform.position;
            nextFramePos.x += speed * Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(nextFramePos, Vector3.down, transRayDown, layerMaskGround);
            if (hit.collider != null)
            {
                Vector2 v2 = Vector2.up * 0.7f + hit.point;
                transform.position = v2;
                Vector3 normal = hit.normal;
                Quaternion direction = Quaternion.LookRotation(Vector3.Cross(normal, Vector3.forward));
                direction *= relativeRotation;
                transform.rotation = direction;
            }
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            LevelDirector.Instance.PlayerOBJ.GetComponent<PlayerController>().MyState.IsLie = true;
            GameManager.Instance.PlayerDeadEvent();
            Time.timeScale = 0f;
        }
    }
}
