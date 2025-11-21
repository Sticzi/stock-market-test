using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public List<MarketEvent> events = new();

    public UnityEvent onRollEvent;
    private MarketEvent currentEvent;


    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        onRollEvent.AddListener(ApplyEventImpact);
        onRollEvent.AddListener(DisplayEventName);
    }

    private void DisplayEventName()
    {
        UIManager.Instance.AddLog($"\nWydarzenie rynkowe:\n{currentEvent.eventName}");
    }

    private void ApplyEventImpact()
    {

        if (currentEvent == null)
            return;
        if (MarketManager.Instance.companies == null)
            return;

        var companies = MarketManager.Instance.companies;

        switch (currentEvent.companySector)
        {
            case Sector.Global:
                foreach (var c in companies)
                    c.ChangeStockPrice(currentEvent.eventStrength);
                UIManager.Instance.AddLog($"Wp³yw na ca³y rynek.\n");
                break;

            case Sector.Individual:
                int randomCompanyIndex = Random.Range(0, companies.Count);
                var company = companies[randomCompanyIndex];
                company.ChangeStockPrice(currentEvent.eventStrength);
                UIManager.Instance.AddLog($"Wp³yw na spó³kê: {company.companyName}\n");
                break;

            default:
                foreach (var c in MarketManager.Instance.companies)
                {
                    if (c.companySector == currentEvent.companySector)
                        c.ChangeStockPrice(currentEvent.eventStrength);
                }
                UIManager.Instance.AddLog($"Wp³yw na sektor: {currentEvent.companySector}\n");
                break;
        }
    }


    public void RollEvent()
    {
        currentEvent = events[Random.Range(0, events.Count)];
        onRollEvent.Invoke();
    }
}
