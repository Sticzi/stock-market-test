using UnityEngine;

[System.Serializable]
public class Company
{
    public Sector companySector;
    public string companyName;
    public float currentPrice = 100f;
    [SerializeField] private float volatility = 2f;
    [HideInInspector]public int playerInfluence = 0;
    [HideInInspector]public float change;

    public void ChangeStockPrice(float strength)
    {   
        change = 0f;
        float finalStrength = strength + playerInfluence;
        change = Random.Range(0, volatility) * finalStrength;
        currentPrice = Mathf.Max(1f, currentPrice + change);

        if (playerInfluence > 0)
            playerInfluence--;
        else if (playerInfluence < 0)
            playerInfluence++;
    }
}
