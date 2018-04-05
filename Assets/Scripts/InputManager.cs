using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class InputManager : Singleton<InputManager>
{
    //private PlayerController player;

    [Tooltip("Seconds after each click to wait for a follow-up")]
    public float timeLimit = 0.25f;

    [Tooltip("Which mouse/stylus button to react to")]
    public PointerEventData.InputButton button;

    [System.Serializable]
    public class OnAtLeastOneClick : UnityEvent { };
    public OnAtLeastOneClick onAtLeastOneClick;

    [System.Serializable]
    public class OnSingleClick : UnityEvent { };
    public OnSingleClick onSingleClick;

    [System.Serializable]
    public class OnDoubleClick : UnityEvent { };
    public OnDoubleClick onDoubleClick;

    [System.Serializable]
    public class OnTripleClick : UnityEvent { };
    public OnTripleClick onTripleClick;

    private int clickCount;
    private Coroutine delayedClick;

    void Start()
    {
        //player = LevelDirector.Instance.PlayerOBJ.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            onClick();
        }
    }

    public void onClick()
    {
        clickCount++;

        switch (clickCount)
        {
            case 1:
                delayedClick = StartCoroutine(DelayClick(onSingleClick, timeLimit));
                onAtLeastOneClick.Invoke();
                break;
            case 2:
                StopCoroutine(delayedClick);
                delayedClick = StartCoroutine(DelayClick(onDoubleClick, timeLimit));
                break;
            case 3:
                StopCoroutine(delayedClick);
                delayedClick = null;
                onTripleClick.Invoke();
                clickCount = 0;
                break;
        }
    }

    private IEnumerator DelayClick(UnityEvent clickEvent, float delay)
    {
        yield return new WaitForSeconds(delay);
        clickEvent.Invoke();
        clickCount = 0;
        delayedClick = null;
    }

    //public void TripleClickResurgence()
    //{
    //    player.MyState.IsLie = false;
    //    player.MyState.IsOnGround = true;
    //}
}