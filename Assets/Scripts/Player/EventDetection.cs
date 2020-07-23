using System;
using UnityEngine;

public class EventDetection : MonoBehaviour
{
    private PlayerController _pc;

    private void Start()
    {
        _pc = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var e = collision.GetComponent<EventTrigger>();
        if (e != null)
            _pc.EnterEventTrigger(e);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EventTrigger>() != null)
            _pc.ExitEventTrigger();
    }
}
