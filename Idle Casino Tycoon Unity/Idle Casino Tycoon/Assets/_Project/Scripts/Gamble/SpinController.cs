using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UniRx;
using System;

public class SpinController : MonoBehaviour
{
    [SerializeField] RectTransform lineRectContainer;
    [SerializeField] float spacing=120;
    [SerializeField] float size=100;
    [SerializeField] float startSpeed;
    [SerializeField] int spinCount;
    [SerializeField] bool resultSpinner;
    float y;
    public bool ResultSpinner => resultSpinner;

    Tween myTween;
    Vector2 firstPosition;

    Subject<bool> spinFinished = new Subject<bool>();
    public IObservable<bool> SpinFinished => spinFinished;

    private void Awake()
    {
        firstPosition = lineRectContainer.anchoredPosition;
    }

    public void Init(List<Sprite> spriteList)
    {
        for (int i = 0; i < lineRectContainer.childCount; i++)
        {
            lineRectContainer.GetChild(i).GetComponent<Image>().sprite = spriteList[i];
        }
    }

    public void Spin()
    {
        myTween.Kill();
        lineRectContainer.anchoredPosition = firstPosition;

        int spintCounter = 0;
        y = lineRectContainer.anchoredPosition.y;
        //6 Child count of 4
        myTween = lineRectContainer.DOAnchorPosY(lineRectContainer.anchoredPosition.y + ((spacing + size) * (4)), startSpeed).SetEase(Ease.Linear).SetLoops(spinCount).SetSpeedBased(true)  
    .OnStepComplete(() =>  
    {
        spintCounter++;
        if(spintCounter == 2) DOTween.To(() => myTween.timeScale, x => myTween.timeScale = x, 0.25f, 1.5f).SetEase(Ease.InQuart);

        if (spintCounter == spinCount && resultSpinner) spinFinished.OnNext(true);

        myTween.timeScale *= 0.75f;
    });

        
       

    }
}
