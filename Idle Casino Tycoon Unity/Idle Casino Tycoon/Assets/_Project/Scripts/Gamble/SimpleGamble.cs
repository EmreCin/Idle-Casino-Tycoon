using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleGamble
{
    public SpinResult Spin(List<GambleOptionSO> optionSO)
    {
        Debug.Log("*** Spin STARTED ***");

        SpinResult result = new SpinResult();
        result.IsSuccess = true;
        //slot1
        float randomNumber = Random.Range(100, 1001);
        Debug.Log("randomNumber--> " + randomNumber);
        result.Selected = optionSO.Where(s => s.StartWeight <= randomNumber).OrderByDescending(o => o.StartWeight).FirstOrDefault();
        Debug.Log("selected--> " + result.Selected.GambleOption.ToString());
        result.Slot1 = result.Selected.Icon;

        //slot2
        randomNumber = Random.Range(100, 1001);
        Debug.Log("randomNumber--> " + randomNumber);
        if (result.Selected.StartWeight > randomNumber)
        {
            result.IsSuccess = false;
            result.Slot2 = optionSO.Where(s => s.StartWeight <= randomNumber).OrderByDescending(o => o.StartWeight).FirstOrDefault().Icon;
        }
        else result.Slot2 = result.Selected.Icon;
        //slot3
        randomNumber = Random.Range(100, 1001);
        Debug.Log("randomNumber--> " + randomNumber);
        if (result.Selected.EndWeight > randomNumber)
        {
            result.IsSuccess = false;
            result.Slot3 = optionSO.Where(s => s.StartWeight <= randomNumber && result.Selected != s).OrderByDescending(o => o.StartWeight).FirstOrDefault().Icon;
        }
        else result.Slot3 = result.Selected.Icon;

        Debug.Log("*** Spin FINISHED *** " + result.IsSuccess);

        return result;
    }
}