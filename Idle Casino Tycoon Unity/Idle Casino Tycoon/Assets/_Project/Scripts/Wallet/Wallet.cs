using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wallet 
{
    private List<Currency> currencyList;

    public Wallet(List<Currency> currencies)
    {
        currencyList = currencies;
    }

    public void Spend(CurrencyType id, float amount)
    {
        currencyList.FirstOrDefault(s => s.Id == id).Amount -= amount;
    }

    public void Gain(CurrencyType id, float amount)
    {
        currencyList.FirstOrDefault(s => s.Id == id).Amount += amount;
    }

    public Currency GetCurrnecyById(CurrencyType id)
    {
        return currencyList.FirstOrDefault(s => s.Id == id);
    }
}
