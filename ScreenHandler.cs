using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScreenHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [System.Serializable]
    public class ScreenEvent : UnityEvent<int> { }

    public UnityEvent beginControl;
    public ScreenEvent controlling;
    public UnityEvent endControl;

    private float timeFloat;
    private bool isDrag = false;
    private bool isAttackSide = false;
    private HeroController hero
    {
        get {
            return FindObjectOfType(typeof(HeroController)) as HeroController;
        }
    }

    void Update( )
    {
        if(Input.GetButton("Fire1") && !isDrag && isAttackSide) {
            timeFloat += Time.deltaTime;
            //if(timeFloat >= 0.5f) {
            //    hero.Gather( );
            //}
        }
        else {
            //hero.DontGather( );
        }
    }
    
    public void OnBeginDrag( PointerEventData eventData )
    {
        this.beginControl.Invoke( );
    }

    public void OnDrag( PointerEventData eventData )
    {
        float deltaX = eventData.delta.x;
        float deltaY = eventData.delta.y;
        if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY)) {
            isDrag = true;
            if(deltaX >= 0) {
                this.controlling.Invoke(0);
            }
            else {
                this.controlling.Invoke(1);
            }
        }
    }

    public void OnEndDrag( PointerEventData eventData )
    {
        isDrag = false;
        this.endControl.Invoke( );
    }

    public void OnPointerDown( PointerEventData eventData )
    {

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Debug.Log(eventData.position);
        Debug.Log(mousePosition);
        if(eventData.position.x >= 412 && eventData.position.x <= 823) {
            isAttackSide = true;
        }
        else if(eventData.position.x >= 0 && eventData.position.x <= 411){
            isAttackSide = false;
        }
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        if(!isDrag) {
            if(eventData.position.x >= 410 && eventData.position.x <= 820) {
                if(timeFloat >= 0.15f) {
                    Debug.Log("Gather 4 ");
                    this.controlling.Invoke(4);
                }
                else {
                    Debug.Log("attack 3 ");
                    this.controlling.Invoke(3);
                }
                timeFloat = 0;
            }
            else {
                Debug.Log("Jump 2 ");
                this.controlling.Invoke(2);
            }
        }
    }
}