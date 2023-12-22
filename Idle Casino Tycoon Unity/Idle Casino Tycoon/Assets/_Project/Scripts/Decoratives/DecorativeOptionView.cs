using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DecorativeOptionView : MonoBehaviour
{
    [SerializeField] List<DecorativeOptionItemView> optionItemList;
    [SerializeField] Transform panel;

    private CompositeDisposable disposables = new CompositeDisposable();

    private void Awake()
    {
        MessageBroker.Default.Receive<Decorative_SelectMessage>().Subscribe(((x) => { FillOptionIcons(x.Model.SpriteList); })).AddTo(disposables);
    }

    private void OnDisable()
    {
        disposables.Clear();
    }

    public void FillOptionIcons(List<Sprite> spriteList)
    {
        ResetOptions();

        for (int i = 0; i < optionItemList.Count; i++)
        {
            if(i < spriteList.Count)
            {
                optionItemList[i].gameObject.SetActive(true);
                optionItemList[i].InitOption(spriteList[i], SelectOption, i);
                if (i == 0) optionItemList[i].OptionSelected(true);
            }
        }
        panel.gameObject.SetActive(true);
    }

    void SelectOption(int index)
    {
        for (int i = 0; i < optionItemList.Count; i++)
        {
            optionItemList[i].OptionSelected(i == index);
        }
    }

    private void ResetOptions()
    {
        foreach (var item in optionItemList)
        {
            item.gameObject.SetActive(false);
            item.OptionSelected(false);
        }
    }

  
}
