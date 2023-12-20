using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorView : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text generatorName;
    [SerializeField] TMPro.TMP_Text generatorLevel;
    [SerializeField] TMPro.TMP_Text generatedAmount;
    [SerializeField] Image fillImageGenerating;
    [SerializeField] Image fillImageCapacity;
    [SerializeField] Button levelUpButton;
    [SerializeField] Button collectButton;
    [SerializeField] Button selectButton;
    public Button LevelUpButton => levelUpButton;
    public Button CollectButton => collectButton;
    public Button SelectButton => selectButton;

    private CompositeDisposable disposables = new CompositeDisposable();
    string id;

    public void Init(GeneratorModel model, GeneratorController controller)
    {
        id = model.Id;
        generatorName.text = model.Name;
        generatorLevel.text = "Level " + model.Level;

        

        controller.Collected.Subscribe(x => Collect(x)).AddTo(disposables);
        controller.Leveluped.Subscribe(x => LevelUp(x)).AddTo(disposables);
        controller.LevelReady.Subscribe(x => LevelupReady(x)).AddTo(disposables);
        controller.CollectReady.Subscribe(x => CollectReady(x)).AddTo(disposables);
        MessageBroker.Default.Receive<Generator_GenerateMessage>().Where(s=> s.Id == id).Subscribe(((x) => { Generate(x); })).AddTo(disposables);
    }

    private void OnDisable()
    {
        
        disposables.Clear();
    }

    void LevelUp(GeneratorModel model)
    {
        generatorLevel.text = "Level " + (model.Level+1);
    }

    void Collect(CurrencyMessage message)
    {
        generatedAmount.text = "0";
    }

    void LevelupReady(bool isReady)
    {
        levelUpButton.gameObject.SetActive(isReady);
    }
    void CollectReady(bool isReady)
    {
        collectButton.gameObject.SetActive(isReady);
    }


    void Generate(Generator_GenerateMessage message)
    {
        fillImageGenerating.DOFillAmount(message.FillAmount, message.TimePassed);
        fillImageCapacity.DOFillAmount(message.CapacityFillAmount, message.TimePassed);

        generatedAmount.text = message.GeneratedAmount.ToString();
    }
}
