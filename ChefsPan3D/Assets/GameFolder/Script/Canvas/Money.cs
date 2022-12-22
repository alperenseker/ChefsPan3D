using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    TextMeshProUGUI _moneyTMP;

    private void Start() => _moneyTMP = GetComponentInChildren<TextMeshProUGUI>();
    private void OnEnable() => GameManager.on_money_changed += UpdateMoney;
    private void OnDisable() => GameManager.on_money_changed -= UpdateMoney;
    public void UpdateMoney(int MoneyValue)
    {
        if (_moneyTMP == null)
            _moneyTMP = GetComponentInChildren<TextMeshProUGUI>();

        if (_moneyTMP != null)
        {
            _moneyTMP.text = ((int)MoneyValue).ToString();
            if (MoneyValue > 1000000000) _moneyTMP.text = (MoneyValue / 1000000000).ToString("F") + "B";
            else if (MoneyValue > 1000000) _moneyTMP.text = (MoneyValue / 1000000).ToString("F") + "M";
            else if (MoneyValue > 1000) _moneyTMP.text = (MoneyValue / 1000).ToString("F") + "K";
        }
    }
}
