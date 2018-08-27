using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool pressed;

    void Start()
    {
        pressed = false;
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        pressed = true;
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        pressed = false;
    }

    void Update()
    {
        pressed = false;
    }

    public bool getPressed()
    {
        return pressed;
    }
}
