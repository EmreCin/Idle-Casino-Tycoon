using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DecorativeItemView : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text nameText;
    [SerializeField] TMPro.TMP_Text unlockedCostText;
    [SerializeField] Button unlockButton;
    DecorativeModel model;


    public void Init(DecorativeModel model)
    {
        this.model = model;

        nameText.text = model.Name;
        unlockedCostText.text = CurrencyHelper.ToMoney(model.UnlockCost);

        unlockButton.onClick.AddListener(() => SelectToUnlock());
    }

    void SelectToUnlock()
    {
        MessageBroker.Default.Publish(new Decorative_SelectMessage(model));
    }
}
