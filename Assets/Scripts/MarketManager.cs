using UnityEngine;
using System.Collections.Generic;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    public List<Company> companies = new();
    public float baseMarketStrength = 1f;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateMarketTurnBased()
    {
        float eventModifier = EventManager.Instance.currentEventModifier;
        float finalStrength = baseMarketStrength * eventModifier;

        foreach (var c in companies)
            c.ApplyMarketChange(finalStrength);

        UIManager.Instance.UpdateCompanyList();
    }
}
