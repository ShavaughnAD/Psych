using UnityEngine;

public class HealthDelegate
{
    public delegate void EventParam(float amount);
    EventParam thisEvent;

    public void BindToEvent(EventParam function)
    {
        thisEvent += function;
    }
    public void UnbindToEvent(EventParam function)
    {
        thisEvent -= function;
    }
    public void CallEvent(float param)
    {
        if (null == thisEvent) return;
        thisEvent.Invoke(param);
    }
}
