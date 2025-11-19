using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Company List")]
    public Transform companyListParent;
    public GameObject companyRowPrefab;
    private Dictionary<Company, CompanyStockUI> companyUI = new();

    [Header("Player UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI logText;

    //[Header("Turn UI")]
    public TextMeshProUGUI turnText;
    //public Button endTurnButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {        
        GenerateRows();
        UpdateCompanyList();
        UpdateWallet(null, PlayerWallet.Instance.money);
        UpdateTurnUI(1, GameTurnManager.Instance.maxTurns);
        
        foreach (var company in companyUI.Values)
        {
            company.buyBtn.onClick.AddListener(() => GameTurnManager.Instance.EndTurn());
            company.sellBtn.onClick.AddListener(() => GameTurnManager.Instance.EndTurn());
        }
    }

    void GenerateRows()
    {
        int rowCounter = 0;
        foreach (var company in MarketManager.Instance.companies)
        {
            Vector2 position = companyListParent.transform.position + new Vector3(0, -rowCounter * 2);
            var rowObj = Instantiate(companyRowPrefab, position, Quaternion.identity, companyListParent);
            var row = rowObj.GetComponent<CompanyStockUI>();
            row.Setup(company);
            companyUI.Add(company, row);
            rowCounter++;
        }
    }

    public void UpdateCompanyList()
    {
        foreach (var row in companyUI.Values)
            row.Refresh();
    }

    public void UpdateWallet(Dictionary<Company, int> owned, float money)
    {
        moneyText.text = $" {money:F2}";

        if (owned == null) return;

        foreach (var c in owned.Keys)
        {
            if (companyUI.TryGetValue(c, out var ui))
            {
                ui.ownedStocks = owned[c];
            }
        }
    }

    public void UpdateTurnUI(int turn, int maxTurn)
    {
        turnText.text = $"Tura {turn}/{maxTurn}";
        logText.text = "\n" + $"Tura {turn}/{maxTurn}" + "\n" + logText.text;
    }

    public void AddLog(string t)
    {
        logText.text = t + "\n" + logText.text;
    }
}
