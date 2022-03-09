using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    // Eventhandler�� gameobject�� �����Ű�� ���Ͽ� �ִ� �ֵ� �� �ϳ��� �޾Ƽ� �����ش�.

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = eventData.position;

        // ���� ��û�� �ֵ鿡�� ����
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }

}
