using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public float currentEventModifier = 1f;

    void Awake()
    {
        Instance = this;
    }

    public void RollEvent()
    {
        int roll = Random.Range(0, 100);

        if (roll < 20)
        {
            currentEventModifier = 2f;
            UIManager.Instance.AddLog("Boom gospodarczy! (+100%)");
        }
        else if (roll < 40)
        {
            currentEventModifier = 0.5f;
            UIManager.Instance.AddLog("Kryzys rynkowy! (-50%)");
        }
        else
        {
            currentEventModifier = 1f;
            UIManager.Instance.AddLog("Rynek stabilny.");
        }
    }
}
