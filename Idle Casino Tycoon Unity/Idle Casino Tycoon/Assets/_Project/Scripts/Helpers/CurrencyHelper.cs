using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyHelper 
{
    public static string ToCurrency(float amount,CurrencyType currencyType, bool isSignActive = true)
    {
        if (currencyType == CurrencyType.Chip)
            return ToChip(amount, isSignActive);
        else
            return ToMoney(amount, isSignActive);
    }

    private static string ToMoney(float amount, bool isSignActive = true)
    {
        if (amount >= 1000000)
        {
            amount /= 1000000;
            return (isSignActive ? "$" : "") + amount.ToString("0.0") + "M";
        }
        if (amount >= 1000)
        {
            amount /= 1000;
            return (isSignActive ? "$" : "") + amount.ToString("0.0") + "K";
        }
        else
            return (isSignActive ? "$" : "") + amount.ToString("0");
    }

    private static string ToChip(float amount, bool isSignActive = true)
    {
       return (isSignActive ? "Chp" : "") + amount.ToString("0");
    }


}
