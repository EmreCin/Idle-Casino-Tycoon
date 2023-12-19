using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    public string Id { get { return model.Id; } }

    GeneratorModel model;
    GeneratorBehavior behavior;

    //Events
    Subject<CurrencyMessage> collected = new Subject<CurrencyMessage>();
    public IObservable<CurrencyMessage> Collected => collected;

    //Events
    Subject<GeneratorModel> leveluped = new Subject<GeneratorModel>();
    public IObservable<GeneratorModel> Leveluped => leveluped;


    private CompositeDisposable disposables = new CompositeDisposable();
    

    public void Init(GeneratorModel model)
    {
        this.model = model;

        behavior = new GeneratorBehavior(this.model);

        this.model.UpgradeCurrency.ObserveEveryValueChanged(s => s.Amount).Subscribe(x => { CheckForUpgrade(); }).AddTo(disposables);
        MessageBroker.Default.Receive<GameManager_TimeMessage>().Subscribe(((x) => { Generate(x); })).AddTo(disposables);
    }

    public void UpdateModel(GeneratorModel model)
    {
        this.model = model;
        behavior.UpdateModel(this.model);
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

    
    void Collect()
    {
        collected.OnNext(new CurrencyMessage(model.GenerationCurrency.Id, behavior.Collect()));
    }

    void LevelUp()
    {
        if (behavior.CheckForLevelUp())
            leveluped.OnNext(model);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Collect();
        if (Input.GetKeyDown(KeyCode.L))
            LevelUp();
    }
}






