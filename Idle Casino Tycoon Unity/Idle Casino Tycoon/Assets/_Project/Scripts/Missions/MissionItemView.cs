using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MissionItemView : MonoBehaviour
{
    [SerializeField] Transform animationLayer;
    [SerializeField] TMPro.TMP_Text missionDesc;
    [SerializeField] TMPro.TMP_Text rewardText;
    [SerializeField] Image progressBar;
    [SerializeField] Button claimButton;

    private MissionModel model;
    public int ItemViewId { get { return model.Id; } }

    public void Init(MissionModel model)
    {
        this.model = model;
        missionDesc.text = model.Desc;
        rewardText.text = CurrencyHelper.ToCurrency(model.Reward, model.RewardCurrencyType);
        StartCoroutine(Begin());
    }

    private IEnumerator Begin()
    {
        yield return new WaitForSeconds(2);
        

        var originalPos = animationLayer.position.x;
        animationLayer.position = new Vector2(originalPos + 1080, animationLayer.position.y);
        animationLayer.DOMoveX(originalPos, 0.5f).SetEase(Ease.InOutQuad);

        animationLayer.gameObject.SetActive(true);
    }

    public void ActivateClaimButton()
    {
        claimButton.gameObject.SetActive(true);
        claimButton.onClick.RemoveAllListeners();
        claimButton.onClick.AddListener(() => model.MissionBehavior.CompleteMission());
    }

    public void CompleteMission()
    {
        var originalPos = animationLayer.position.x;
        
        animationLayer.DOMoveX((originalPos - 1080), 0.5f).SetEase(Ease.InOutQuad).OnComplete(()=> {
            claimButton.gameObject.SetActive(false);
            animationLayer.gameObject.SetActive(false);
            animationLayer.position = new Vector3(originalPos, animationLayer.position.y, animationLayer.position.z);
        });

       
    }

    
   
}
