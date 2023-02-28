using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler

{
    private RectTransform joystickTransform;
    [SerializeField]
    private int dragMovementDistance = 30;
    [SerializeField]
    private float dragThreshold = 0.6f;
    [SerializeField]
    private int dragOffsetDistance = 100;
    public Vector2 move;
    public event Action<Vector2> OnMove;


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickTransform,
            eventData.position,
            null, out offset);
        //(-1)-1
        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;
        Debug.Log(offset);

        joystickTransform.anchoredPosition = offset * dragMovementDistance;

        Vector2 inputVector = CalculateMovementInput(offset);
        OnMove?.Invoke(inputVector);
        if(offset.x >0.2f)
        {
            PlayerMobileInput.Instance.gameObject.transform.eulerAngles = new Vector2(0, 0);
           
        }
        else if (offset.x < 0.2f)
        {
            PlayerMobileInput.Instance.gameObject.transform.eulerAngles = new Vector2(0, 180);
        }
        Debug.Log("Dragmovement distance" + dragMovementDistance);
        Debug.Log("Dragoffset distance" + dragOffsetDistance);
        Debug.Log("Drag Threshold" + dragThreshold);
    }

    private Vector2 CalculateMovementInput(Vector2 offset)
    {

        float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;
        return new Vector2(x, y).normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.down);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.up);
    }

    // Start is called before the first frame update
    void Awake()
    {
        joystickTransform = (RectTransform)transform;
    }
}
