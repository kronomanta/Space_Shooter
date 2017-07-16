using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private int? _pointerId = null;

    public void OnPointerDown(PointerEventData e)
    {
        //set out start point
        if (_pointerId == null)
        {
            _pointerId = e.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData e)
    {
        //reset everything
        if (_pointerId == e.pointerId)
        {
            _pointerId = null;
        }
    }

    public bool CanFire()
    {
        return _pointerId.HasValue || Input.GetButton("Fire1");
    }

}
