using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    // Eventhandler를 gameobject에 적용시키면 산하에 있는 애들 중 하나가 받아서 물어준다.

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = eventData.position;

        // 구독 신청한 애들에게 전파
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }

}
