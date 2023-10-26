using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScreenRightHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [System.Serializable]
    public class ScreenEvent : UnityEvent<int> { }

    public UnityEvent beginControl;
    public ScreenEvent controlling;
    public UnityEvent endControl;

    private float timeFloat;
    private bool isDrag = false;
    private bool isAttackSide = false;
    private bool isOneDrag = true;
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
            if(timeFloat >= 0.15f) {
                hero.Gathering( );
            }
        }
        else {
            hero.NotGathering( );
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
        //Debug.Log("down");
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //Debug.Log(eventData.position);
        //Debug.Log(mousePosition);
        isAttackSide = true;
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        //Debug.Log("up");
        if(!isDrag) {
            if(timeFloat >= 1.0f) {
                Debug.Log("Gather 4 ");
                this.controlling.Invoke(4);
            }
            if(timeFloat <= 0.15f) {
                this.controlling.Invoke(3);
            }
            timeFloat = 0;
        }
        isAttackSide = false;
    }

    public void OnClick( )
    {
        Debug.Log("click");
    }
}
