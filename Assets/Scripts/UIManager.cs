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
    private List<CompanyStockUI> companyUI = new();

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
        //endTurnButton.onClick.AddListener(() => GameTurnManager.Instance.EndTurn());
        GenerateRows();
        UpdateCompanyList();
        UpdateWallet(null, PlayerWallet.Instance.money);
        UpdateTurnUI(1, GameTurnManager.Instance.maxTurns);
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
        foreach (var r in companyUI)
            r.Refresh();
    }

    public void UpdateWallet(Dictionary<Company, int> owned, float money)
    {
        moneyText.text = $" {money:F2}";
    }

    public void UpdateTurnUI(int turn, int maxTurn)
    {
        turnText.text = $"Tura {turn}/{maxTurn}";
    }

    public void AddLog(string t)
    {
        logText.text = t + "\n" + logText.text;
    }
}
