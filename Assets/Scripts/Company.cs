using UnityEngine;

[System.Serializable]
public class Company
{
    public string companyName;
    public float basePrice = 100f;
    public float currentPrice = 100f;
    public float volatility = 2f;

    public void ApplyMarketChange(float strength)
    {
        float change = Random.Range(-volatility, volatility) * strength;
        currentPrice = Mathf.Max(1f, currentPrice + change);
    }
}
