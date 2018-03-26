using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SlideIcon : MonoBehaviour 
{
    private Action onSlideCome;
    private RectTransform rectTransfrom;
    

    private void OnEnable()
    {
        onSlideCome = OnSlideCome;
        EventService.Instance.GetEvent<BeAboutToDieEvent>().Subscribe(onSlideCome);
    }

    private void Awake()
    {
        rectTransfrom = GetComponent<RectTransform>();
    }

    IEnumerator SlideIconAccess()
    {
        rectTransfrom.DOLocalMoveX(116f, 1f, true);
        yield return new WaitForSeconds(1f);
        transform.DOShakePosition(8f, 3f, 80, 180, false, false);
        yield return new WaitForSeconds(8f);
        rectTransfrom.DOLocalMoveX(-116f, 1.5f, true);
    }

    private void OnSlideCome()
    {
        StartCoroutine(SlideIconAccess());
    }

    private void OnDisable()
    {
        EventService.Instance.GetEvent<BeAboutToDieEvent>().UnSubscribe(onSlideCome);
    }
}
