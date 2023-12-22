using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyHelper 
{
    public static string ToMoney(float amount)
    {
        if (amount >= 1000000)
        {
            amount /= 1000000;
            return "$" + amount.ToString("0.0")+"M";
        }
        if (amount >= 1000)
        {
            amount /= 1000;
            return "$" + amount.ToString("0.0") + "K";
        }
        else
            return "$" + amount.ToString("0");

       
    }
}
