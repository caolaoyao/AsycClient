using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public delegate void UIDelegate(GameObject go);
public delegate void UIVectorDelegate(GameObject go, Vector2 delta);
public class EventTriggrListener : EventTrigger
{
    public UIDelegate OnClickDelegate;

    public UIDelegate OnPointDownDelegate;

    public UIDelegate OnPoinitEnterDelegate;

    public UIDelegate OnUpdateSelectDelegate;

    public UIDelegate OnDragEndDelegate;

    public UIVectorDelegate OnDragDelegate;

    public static EventTriggrListener GetEventTriggerListener(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError("Event Trigger listener is NULL");
            return null;
        }
        else
        {
            EventTriggrListener nswellEventTrigger = go.GetComponent<EventTriggrListener>();

            if (nswellEventTrigger == null)
            {
                nswellEventTrigger = go.AddComponent<EventTriggrListener>();
            }
            return nswellEventTrigger;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickDelegate != null)
        {
            OnClickDelegate(gameObject);
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (OnDragDelegate != null) OnDragDelegate(gameObject, eventData.delta);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (OnDragEndDelegate != null) OnDragEndDelegate(gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointDownDelegate != null) OnPointDownDelegate(gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPoinitEnterDelegate != null) OnPoinitEnterDelegate(gameObject);
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (OnUpdateSelectDelegate != null) OnUpdateSelectDelegate(gameObject);
    }
}
