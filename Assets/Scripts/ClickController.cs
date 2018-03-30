using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickController : MonoBehaviour
{
    // Configure how we listen for clicks (how fast, which button).

    [Tooltip("Seconds after each click to wait for a follow-up")]
    public float timeLimit = 0.25f;

    [Tooltip("Which mouse/stylus button to react to")]
    public PointerEventData.InputButton button;

    // Expose events we can wire-up in the inspector to our desired handlers.

    // I added this so we can use it to show visible feedback immediately
    // on the first click, to minimize perceived latency.
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

    // Internal state for keeping track of clicks.

    private int clickCount;
    private Coroutine delayedClick;

    // Auto-configure on Start.
    // I added this to reduce fiddly inspector setup - we'll use an existing
    // EventTrigger component if it's there, or add one & wire it up if not.
    void Start()
    {
        //EventTrigger trigger = GetComponent<EventTrigger>();
        //if (trigger == null)
        //    trigger = gameObject.AddComponent<EventTrigger>();

        //var entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerClick;
        //entry.callback.AddListener(onClick);

        //trigger.triggers.Add(entry);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            onClick();
        }
    }

    // Main click handler - this is where the magic happens.
    public void onClick()
    {
        //PointerEventData pointerData = data as PointerEventData;

        //// Ignore clicks on buttons we're not watching.
        //if (this.button != pointerData.button)
        //    return;

        // Count up the clicks.
        clickCount++;

        // React accordingly.
        switch (clickCount)
        {
            // First click: fire OnAtLeastOneClick and wait to see if a second comes in.
            case 1:
                delayedClick = StartCoroutine(DelayClick(onSingleClick, timeLimit));
                onAtLeastOneClick.Invoke();
                break;
            // Second click: cancel single-click and wait to see if a third comes in.
            case 2:
                StopCoroutine(delayedClick);
                delayedClick = StartCoroutine(DelayClick(onDoubleClick, timeLimit));
                break;
            // Third click: cancel double-click fire OnTripleClick immediately.
            case 3:
                StopCoroutine(delayedClick);
                delayedClick = null;
                onTripleClick.Invoke();
                clickCount = 0;
                break;
        }
    }

    // This handles firing off the click after a delay.
    // We cancel it if a new click comes in sooner.
    private IEnumerator DelayClick(UnityEvent clickEvent, float delay)
    {
        yield return new WaitForSeconds(delay);
        // This coroutine didn't get stopped, so no new click came in.
        // Fire off the corresponding click event and reset.
        clickEvent.Invoke();
        clickCount = 0;
        delayedClick = null;
    }
}