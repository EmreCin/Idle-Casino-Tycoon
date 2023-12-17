using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency
{
    public CurrencyType Id;
    public float Amount;

    public Currency(CurrencyType id, float amount)
    {
        Id = id;
        Amount = amount;
    }
}
