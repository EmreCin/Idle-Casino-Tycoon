using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

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

    Subject<bool> levelReady = new Subject<bool>();
    public IObservable<bool> LevelReady => levelReady;

    Subject<bool> collectReady = new Subject<bool>();
    public IObservable<bool> CollectReady => collectReady;

    private CompositeDisposable disposables = new CompositeDisposable();
    

    public void Init(GeneratorModel model)
    {
        this.model = model;

        behavior = new GeneratorBehavior(this.model);

        this.model.UpgradeCurrency.ObserveEveryValueChanged(s => s.Amount).Subscribe(x => { CheckForUpgrade(); }).AddTo(disposables);
        MessageBroker.Default.Receive<GameManager_TimeMessage>().Subscribe(((x) => { Generate(x); })).AddTo(disposables);
    }

    public void AddButtonEvents(Button collect, Button upgrade, Button select)
    {
        collect.onClick.AddListener(() => Collect());
        upgrade.onClick.AddListener(() => LevelUp());
        select.onClick.AddListener(() => SelectGenerator());
        
    }

    public void UpdateModel(GeneratorModel model)
    {
        this.model = model;
        behavior.UpdateModel(this.model);

        MessageBroker.Default.Publish(new GeneratorController_SelectedMessage(model));
    }
   

    private void OnDisable()
    {
        disposables.Clear();
    }

    void SelectGenerator()
    {
        MessageBroker.Default.Publish(new GeneratorController_SelectedMessage(model));
        behavior.Generate(0);
    }


    void Generate(GameManager_TimeMessage message)
    {
        behavior.Generate(message.TimePassed);

        collectReady.OnNext(behavior.CurrentAmount > 0);
    }

    void CheckForUpgrade()
    {
       levelReady.OnNext(behavior.CheckForLevelUp());
       //Debug.LogError("*** Level Up Ready----------------->" + model.Id + " - " + model.Level);
    }

    
    void Collect()
    {
        collected.OnNext(new CurrencyMessage(model.GenerationCurrency.Id, behavior.Collect()));

        //MessageBroker.Default.Publish(new GeneratorController_SelectedMessage(model));
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






