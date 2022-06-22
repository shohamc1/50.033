using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomCastEvent : UnityEvent<KeyCode>
{

}
public class CastEventListener : MonoBehaviour
{
    public CastEvent Event;
    public CustomCastEvent Response;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(KeyCode p)
    {
        Response.Invoke(p);
    }
}