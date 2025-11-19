using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CompanyStockUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI priceText;
    public Button buyBtn;
    public Button sellBtn;
    public TextMeshProUGUI ownedStockCounter;
    public int ownedStocks = 0;
    public Image trendIcon;

    public Button buyMultiplier;
    public Button sellMultiplier;

    [SerializeField] private Sprite upTrendSprite;
    [SerializeField] private Sprite downTrendSprite;
    [SerializeField] private Sprite neutralTrendSprite;

    private Company company;

    public void Setup(Company c)
    {
        company = c;
        nameText.text = c.companyName;

        buyBtn.onClick.AddListener(() => PlayerWallet.Instance.Buy(company));
        sellBtn.onClick.AddListener(() => PlayerWallet.Instance.Sell(company));

        Refresh();
    }

    public void Refresh()
    {
        priceText.text = $"{company.currentPrice:F2}";
        ownedStockCounter.text = "Stocks Owned: " + ownedStocks.ToString();
    }
}
