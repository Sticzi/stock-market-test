using UnityEngine;

public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance;

    public int currentTurn = 1;
    public int maxTurns = 20;

    void Awake()
    {
        Instance = this;
    }

    public void EndTurn()
    {
        if (currentTurn > maxTurns)
        {
            UIManager.Instance.AddLog("Koniec gry — osi¹gniêto limit tur.");
            return;
        }

        currentTurn++;

        // 1. Zdarzenie rynkowe (losowe)
        EventManager.Instance.RollEvent();

        // 2. Aktualizacja cen firm
        MarketManager.Instance.UpdateMarketTurnBased();

        // UI
        UIManager.Instance.UpdateTurnUI(currentTurn, maxTurns);

        
    }
}

