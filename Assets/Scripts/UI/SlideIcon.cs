using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SlideIcon : MonoBehaviour 
{
    private Action onSlideCome;
    private Action onResurgence;
    
    private RectTransform rectTransfrom;
    private Vector3 init_rectTransfrom;

    private void Awake()
    {
        rectTransfrom = GetComponent<RectTransform>();
        init_rectTransfrom = rectTransfrom.position;
    }

    private void OnEnable()
    {
        onSlideCome = OnSlideCome;
        EventService.Instance.GetEvent<BeAboutToDieEvent>().Subscribe(onSlideCome);
        onResurgence = OnResurgence;
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().Subscribe(onResurgence);
    }

    IEnumerator SlideIconAccess()
    {
        rectTransfrom.DOLocalMoveX(116f, 1f, true);
        yield return new WaitForSeconds(1f);
        transform.DOShakePosition(3f, 3f, 80, 180, false, false);
        yield return new WaitForSeconds(3f);
        transform.DOScale(0.75f,5f);
        yield return new WaitForSeconds(5f);
        rectTransfrom.DOLocalMoveX(-116f, 1.5f, true);
    }

    private void OnSlideCome()
    {
        StartCoroutine(SlideIconAccess());
    }

    private void OnResurgence()
    {
        rectTransfrom.position = init_rectTransfrom;
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<BeAboutToDieEvent>().UnSubscribe(onSlideCome);
        EventService.Instance.GetEvent<PlayerResurgenceEvent>().UnSubscribe(onResurgence);
    }
}
