using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerFiller
{
    public List<Sprite> FillSpinnerSuccess(List<GambleOptionSO> optionSO, Sprite selected)
    {
        List<Sprite> spriteList = new List<Sprite>();

        while (spriteList.Count < 5)
        {
            int randomInt = Random.Range(0, 6);
            if (optionSO[randomInt].Icon != selected)
                spriteList.Add(optionSO[randomInt].Icon);
        }
        spriteList.Add(selected);

        return spriteList;
    }

    public List<Sprite> FillSpinnerFail(List<GambleOptionSO> optionSO, SpinResult result, int index)
    {
        List<Sprite> spriteList = new List<Sprite>();

        while (spriteList.Count < 5)
        {
            int randomInt = Random.Range(0, 6);
            if (index == 1 && optionSO[randomInt].Icon != result.Slot1
                || index == 2 && optionSO[randomInt].Icon != result.Slot2
                || index == 3 && optionSO[randomInt].Icon != result.Slot3)
                spriteList.Add(optionSO[randomInt].Icon);
        }
        if (index == 1)
            spriteList.Add(result.Slot1);
        else if (index == 2)
            spriteList.Add(result.Slot2);
        else if (index == 3)
            spriteList.Add(result.Slot3);


        return spriteList;
    }
}
