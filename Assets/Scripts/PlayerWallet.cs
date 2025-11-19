using UnityEngine;
using System.Collections.Generic;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;

    public float money = 1000f;
    private Dictionary<Company, int> owned = new();

    void Awake()
    {
        Instance = this;
    }

    public void Buy(Company c)
    {
        if (money < c.currentPrice)
        {
            UIManager.Instance.AddLog("Za ma³o pieniêdzy.");
            return;
        }

        money -= c.currentPrice;

        if (!owned.ContainsKey(c))
            owned[c] = 0;

        owned[c]++;

        UIManager.Instance.AddLog($"Kupiono 1 akcjê: {c.companyName}");
        UIManager.Instance.UpdatePortfolio(owned, money);
    }

    public void Sell(Company c)
    {
        if (!owned.ContainsKey(c) || owned[c] == 0)
        {
            UIManager.Instance.AddLog("Nie masz akcji tej firmy.");
            return;
        }

        owned[c]--;
        money += c.currentPrice;

        UIManager.Instance.AddLog($"Sprzedano 1 akcjê: {c.companyName}");
        UIManager.Instance.UpdatePortfolio(owned, money);
    }
}
