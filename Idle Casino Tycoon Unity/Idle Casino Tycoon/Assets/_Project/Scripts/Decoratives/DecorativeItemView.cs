using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DecorativeItemView : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text nameText;
    [SerializeField] TMPro.TMP_Text unlockedCostText;
    [SerializeField] Image icon;
    [SerializeField] Button unlockButton;
    
    DecorativeModel model;

    public string Id { get { return model.Id; } }


    public void Init(DecorativeModel model)
    {
        this.model = model;

        nameText.text = model.Name;
        icon.sprite = model.SpriteList.FirstOrDefault();

        CheckUnlock();
        
    }

    void SelectToUnlock()
    {
        MessageBroker.Default.Publish(new Decorative_SelectMessage(model));
    }

    public void SetUnlocked()
    {
        model.IsUnlocked = true;
        CheckUnlock();
    }

    void CheckUnlock()
    {
        if (model.IsUnlocked)
        {
            unlockButton.onClick.RemoveAllListeners();
            unlockedCostText.text = "";
        }
        else
        {
            unlockButton.onClick.AddListener(() => SelectToUnlock());
            unlockedCostText.text = "Buy\n"+CurrencyHelper.ToCurrency(model.UnlockCost, model.UnlockCurrency);
        }
            
    }
}
