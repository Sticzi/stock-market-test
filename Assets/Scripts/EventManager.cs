using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public float currentEventModifier = 1f;
    public string currentEventDescription = "Rynek stabilny.";

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
            currentEventDescription = "Rynek niestabilny! (du¿e zmiany)";
        }
        else if (roll < 70)
        {
            currentEventModifier = 1f;
            currentEventDescription = "Rynek stabilny! (drobne zmiany)";
        }
        else
        {
            currentEventModifier = 0f;
            currentEventDescription = "Rynek w stagnacji.";
        }
        UIManager.Instance.AddLog($"Zdarzenie rynkowe: {currentEventDescription}");
    }
}
