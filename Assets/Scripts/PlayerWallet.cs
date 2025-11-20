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

    public void Buy(Company c, int multiplier)
    {
        float totalPrice = c.currentPrice * multiplier;

        if (money < totalPrice)
        {
            UIManager.Instance.AddLog("Za ma³o pieniêdzy.");
            return;
        }

        money -= totalPrice;

        if (!owned.ContainsKey(c))
            owned[c] = 0;

        owned[c] += multiplier;

        UIManager.Instance.AddLog($"Kupiono {multiplier} akcji: {c.companyName}");
        UIManager.Instance.UpdateWallet(owned, money);
    }

    public void Sell(Company c, int multiplier)
    {
        if (!owned.ContainsKey(c) || owned[c] == 0)
        {
            UIManager.Instance.AddLog("Nie masz akcji tej firmy.");
            return;
        }

        owned[c] -= multiplier;
        money += c.currentPrice * multiplier;

        UIManager.Instance.AddLog($"Sprzedano {multiplier} akcji: {c.companyName}");
        UIManager.Instance.UpdateWallet(owned, money);
    }
}
