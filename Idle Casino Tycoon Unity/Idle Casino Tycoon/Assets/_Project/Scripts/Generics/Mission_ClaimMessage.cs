using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Mission_ClaimMessage 
{
    public CurrencyType CurrencyType;
    public float Amount;
    public Mission_ClaimMessage(CurrencyType currencyType,float amount)
    {
        CurrencyType = currencyType;
        Amount = amount;
    }
}
