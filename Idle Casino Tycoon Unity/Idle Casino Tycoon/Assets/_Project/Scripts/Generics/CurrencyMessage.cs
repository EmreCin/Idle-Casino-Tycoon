using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CurrencyMessage
{
    public CurrencyType CurrencyType;
    public float Amount;

    public CurrencyMessage(CurrencyType currencyType, float amount)
    {
        CurrencyType = currencyType;
        Amount = amount;
    }
}