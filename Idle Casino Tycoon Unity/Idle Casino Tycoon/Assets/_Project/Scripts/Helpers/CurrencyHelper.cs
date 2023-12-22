using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyHelper 
{
    public static string ToMoney(float amount)
    {
        if (amount > 1000000)
            amount /= 1000000;
        if (amount > 1000)
            amount /= 1000;

        return "$" + amount.ToString("0.0");
    }
}
