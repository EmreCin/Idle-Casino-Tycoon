using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorativeOptionItemView : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Transform selectedIcon;
    [SerializeField] Button selectButton;

    

    public void InitOption(Sprite sprite, Action<int> select, int index)
    {
        icon.sprite = sprite;
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => { select?.Invoke(index); } );
    }

    public void OptionSelected(bool isSelected)
    {
        selectedIcon.gameObject.SetActive(isSelected);
    }

    
}
