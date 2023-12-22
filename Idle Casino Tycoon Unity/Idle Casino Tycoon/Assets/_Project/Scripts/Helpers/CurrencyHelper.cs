using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyHelper 
{
    public static string ToMoney(float amount, bool isSignActive = true)
    {
        if (amount >= 1000000)
        {
            amount /= 1000000;
            return (isSignActive ? "$":"") + amount.ToString("0.0")+"M";
        }
        if (amount >= 1000)
        {
            amount /= 1000;
            return (isSignActive ? "$" : "") + amount.ToString("0.0") + "K";
        }
        else
            return (isSignActive ? "$" : "") + amount.ToString("0");

       
    }
}
