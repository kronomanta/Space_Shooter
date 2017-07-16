using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Smoothing;

    private Vector2 _origin;
    private Vector2 _direction = Vector2.zero;
    private Vector2 _smoothDirection;

    private int? _pointerId = null;

    public void OnDrag(PointerEventData e)
    {
        //compare the difference between out start point and current pointer pos
        if (_pointerId == e.pointerId)
        {
            _direction = (e.position - _origin).normalized;
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        //set out start point
        if (_pointerId == null)
        {
            _pointerId = e.pointerId;
            _origin = e.position;
        }
    }

    public void OnPointerUp(PointerEventData e)
    {
        //reset everything
        if (_pointerId == e.pointerId)
        {
            _direction = Vector2.zero;
            _pointerId = null;
        }
    }

    public Vector2 GetDirection()
    {
        _smoothDirection = Vector2.MoveTowards(_smoothDirection, _direction, Smoothing);
        return _smoothDirection;
    }
}
