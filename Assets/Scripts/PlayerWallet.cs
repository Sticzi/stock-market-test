using UnityEngine;
using System.Collections.Generic;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;

    public float money = 1000f;
    private Dictionary<CompanyStockUI, int> ownedStockUI = new();

    void Awake()
    {
        Instance = this;
    }

    public void Buy(CompanyStockUI companyUI)
    {
        if (GameTurnManager.Instance.IsGameOver) 
            return;

        float totalPrice = companyUI.company.currentPrice * companyUI.buyMultiplier;

        if (money < totalPrice)
        {
            UIManager.Instance.AddLog("Za ma³o pieniêdzy.");
            return;
        }

        money -= totalPrice;
        companyUI.company.playerInfluence += companyUI.buyMultiplier;

        if (!ownedStockUI.ContainsKey(companyUI))
            ownedStockUI[companyUI] = 0;

        ownedStockUI[companyUI] += companyUI.buyMultiplier;

        UIManager.Instance.AddLog($"Kupiono {companyUI.buyMultiplier} akcji: {companyUI.company.companyName}");
        UIManager.Instance.UpdateWallet(ownedStockUI, money);
    }

    public void Sell(CompanyStockUI companyUI)
    {
        if (GameTurnManager.Instance.IsGameOver)
            return;

        if (!ownedStockUI.ContainsKey(companyUI) || ownedStockUI[companyUI] == 0)
        {
            UIManager.Instance.AddLog("Nie masz akcji tej firmy.");
            return;
        }

        companyUI.company.playerInfluence -= companyUI.sellMultiplier;
        ownedStockUI[companyUI] -= companyUI.sellMultiplier;
        money += companyUI.company.currentPrice * companyUI.sellMultiplier;

        UIManager.Instance.AddLog($"Sprzedano {companyUI.sellMultiplier} akcji: {companyUI.company.companyName}");
        UIManager.Instance.UpdateWallet(ownedStockUI, money);
    }
}
