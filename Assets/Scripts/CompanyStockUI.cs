using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CompanyStockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    public Button buyBtn;
    public Button sellBtn;
    [SerializeField] private TextMeshProUGUI ownedStockCounter;
    public int ownedStocks = 0;


    private readonly int[] multiplierCycle = { 1, 2, 3, 4, 5, 10, 20, 50, 100 };
    [SerializeField] private Button buyMultiplierButton;
    public int buyMultiplier = 1;
    [SerializeField] private Button sellMultiplierButton;
    public int sellMultiplier = 1;

    [SerializeField] private Image trendIcon;
    [SerializeField] private Sprite upTrendSprite;
    [SerializeField] private Sprite downTrendSprite;
    [SerializeField] private Sprite neutralTrendSprite;

    public Company company;

    public void Setup(Company c)
    {
        company = c;
        nameText.text = c.companyName;

        buyBtn.onClick.AddListener(() => PlayerWallet.Instance.Buy(this));
        sellBtn.onClick.AddListener(() => PlayerWallet.Instance.Sell(this));
        buyBtn.onClick.AddListener(() => GameTurnManager.Instance.EndTurn());
        sellBtn.onClick.AddListener(() => GameTurnManager.Instance.EndTurn());

        buyMultiplierButton.onClick.AddListener(OnClickMultiplierBuy);
        sellMultiplierButton.onClick.AddListener(OnClickMultiplierSell);

        Refresh();
    }
    private int CycleMultiplier(int current)
    {
        int index = System.Array.IndexOf(multiplierCycle, current);

        index = (index + 1) % multiplierCycle.Length;

        return multiplierCycle[index];
    }

    private void OnClickMultiplierBuy()
    {
        buyMultiplier = CycleMultiplier(buyMultiplier);
        buyMultiplierButton.GetComponentInChildren<TextMeshProUGUI>().text = "x" + buyMultiplier.ToString();
    }
    
    private void OnClickMultiplierSell()
    {
        sellMultiplier = CycleMultiplier(sellMultiplier);
        sellMultiplierButton.GetComponentInChildren<TextMeshProUGUI>().text = "x" + sellMultiplier.ToString();
    }

    public void Refresh()
    {
        priceText.text = $"{company.currentPrice:F2}";
        ownedStockCounter.text = "Stocks Owned: " + ownedStocks.ToString();
        if (company.change > 0)
            trendIcon.sprite = upTrendSprite;
        else if (company.change < 0)
            trendIcon.sprite = downTrendSprite;
        else
            trendIcon.sprite = neutralTrendSprite;
    }
}
