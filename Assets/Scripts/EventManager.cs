using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MarketEvent : UnityEvent<float, string> { }

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public float currentEventModifier = 1f;
    public string currentEventDescription = "Rynek stabilny.";

    // Event: sends modifier + text
    public MarketEvent OnMarketEvent;

    void Awake()
    {
        Instance = this;
    }

    public void RollEvent()
    {
        int roll = Random.Range(0, 100);

        if (roll < 20)
        {
            currentEventModifier = 5f;
            currentEventDescription = "Rynek is bOOOMINN! (++++)";
        }
        else if (roll < 70)
        {
            currentEventModifier = -5f;
            currentEventDescription = "Rynek is not doing good :(! (----)";
        }
        else
        {
            currentEventModifier = 0f;
            currentEventDescription = "Rynek w stagnacji.";
        }

        // Fire the event
        OnMarketEvent?.Invoke(currentEventModifier, currentEventDescription);
    }
}
