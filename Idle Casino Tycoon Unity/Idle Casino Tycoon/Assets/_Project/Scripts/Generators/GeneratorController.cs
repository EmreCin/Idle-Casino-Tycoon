using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    GeneratorModel model;
    GeneratorBehavior behavior;

    //IDisposable timeListener;
    //currency.ObserveEveryValueChanged(s=> s.Amount).Subscribe(x => { Test(x); }).AddTo(this); //.AddTo(disposables);
    //if (timeListener != null) timeListener.Dispose();
    private CompositeDisposable disposables = new CompositeDisposable();
    

    public void Init(GeneratorModel model)
    {
        this.model = model;

        behavior = new GeneratorBehavior(this.model);

        this.model.UpgradeCurrency.ObserveEveryValueChanged(s => s.Amount).Subscribe(x => { CheckForUpgrade(); }).AddTo(disposables);
        MessageBroker.Default.Receive<GameManager_TimeMessage>().Subscribe(((x) => { Generate(x); })).AddTo(disposables);
    }
   

    private void OnDisable()
    {
        disposables.Clear();
    }

    void Generate(GameManager_TimeMessage message)
    {
        behavior.Generate(message.TimePassed);
    }

    void CheckForUpgrade()
    {
        if (behavior.CheckForLevelUp())
            Debug.LogError("*** Level Up Ready----------------->" + model.Id + " - " + model.Level);
    }

    


}




