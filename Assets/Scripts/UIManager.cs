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
    private readonly List<CompanyStockUI> companyUI = new();

    [Header("Player UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI logText;
    public TextMeshProUGUI turnText;

    void Awake()
    {
        Instance = this;
    }    

    public void SetupGame()
    {
        GenerateRows();
        UpdateCompanyList();
        UpdateWallet(null, PlayerWallet.Instance.money);
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
            companyUI.Add(row);
            rowCounter++;
        }
    }

    public void UpdateCompanyList()
    {
        foreach (var row in companyUI)
            row.Refresh();
    }

    public void UpdateWallet(Dictionary<CompanyStockUI, int> owned, float money)
    {
        moneyText.text = $" {money:F2}";

        if (owned == null) return;

        foreach (var c in owned.Keys)
        {
            c.ownedStocks = owned[c];
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
