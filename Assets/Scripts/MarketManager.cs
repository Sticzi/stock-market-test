using UnityEngine;
using System.Collections.Generic;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    public List<Company> companies = new();
    public float baseMarketVolatility = 1f;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateMarketTurnBased()
    {
        foreach (var c in companies)
            c.ChangeStockPrice(Random.Range(-baseMarketVolatility, baseMarketVolatility));

        UIManager.Instance.UpdateCompanyList();
    }
}
