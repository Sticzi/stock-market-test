using UnityEngine;

public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance;

    private int currentTurn = 1;
    [SerializeField] private int maxTurns = 50;
    public bool IsGameOver => currentTurn >= maxTurns;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.SetupGame();
        UIManager.Instance.UpdateTurnUI(1, maxTurns);
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

        // 3. update UI
        UIManager.Instance.UpdateTurnUI(currentTurn, maxTurns);

        
    }
}

