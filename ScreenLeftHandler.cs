using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScreenLeftHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [System.Serializable]
    public class ScreenEvent : UnityEvent<int> { }

    public UnityEvent beginControl;
    public ScreenEvent controlling;
    public UnityEvent endControl;

    private float timeFloat;
    private bool isDrag = false;
    private bool isOneDrag = true;

    public void OnBeginDrag( PointerEventData eventData )
    {
        this.beginControl.Invoke( );
    }

    public void OnDrag( PointerEventData eventData )
    {
        float deltaX = eventData.delta.x;
        float deltaY = eventData.delta.y;
        if(isOneDrag) {
            if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY) && Mathf.Abs(deltaX + deltaY) > 5) {
                isDrag = true;

                if(deltaX >= 0) {
                    this.controlling.Invoke(0);
                }
                else {
                    this.controlling.Invoke(1);
                }


            }
            else {
                isDrag = false;
                if(deltaY < 0) {
                    this.controlling.Invoke(5);
                }
            }
            isOneDrag = false;
        }
    }

    public void OnEndDrag( PointerEventData eventData )
    {
        isDrag = false;
        isOneDrag = true;
        this.endControl.Invoke( );
    }

    public void OnPointerDown( PointerEventData eventData )
    {

    }

    public void OnPointerUp( PointerEventData eventData )
    {
        if(!isDrag) {
            this.controlling.Invoke(2);
        }
    }
}
//if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) {
//    if(deltaX >= 0) {
//        this.controlling.Invoke(0);
//    }
//    else {
//        this.controlling.Invoke(1);
//    }
//}
//else {
//    if(deltaY < 0) {
//        this.controlling.Invoke(5);
//    }
//}