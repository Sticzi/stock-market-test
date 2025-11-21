using UnityEngine;
using UnityEngine.Events;

public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance;

    [SerializeField] private UnityEvent onTurnEnded = new UnityEvent();

    private int currentTurn = 0;
    [SerializeField] private int maxTurns = 50;
    public bool IsGameOver => currentTurn >= maxTurns;


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.SetupGame();
        //UIManager.Instance.UpdateTurnUI(1, maxTurns);

        onTurnEnded.AddListener(MarketManager.Instance.UpdateMarketTurnBased);
        onTurnEnded.AddListener(EventManager.Instance.RollEvent);
    }

    public void EndTurn()
    {
        if (currentTurn > maxTurns)
        {
            UIManager.Instance.AddLog("Koniec gry — osi¹gniêto limit tur.");
            return;
        }
        currentTurn++;

        onTurnEnded.Invoke();

        UIManager.Instance.UpdateTurnUI(currentTurn, maxTurns);
        UIManager.Instance.UpdateCompanyList();


    }
}

