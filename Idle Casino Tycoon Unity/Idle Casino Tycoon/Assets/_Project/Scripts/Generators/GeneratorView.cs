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

    private CompositeDisposable disposables = new CompositeDisposable();
    string id;

    public void Init(GeneratorModel model)
    {
        id = model.Id;
        generatorName.text = model.Name;
        generatorLevel.text = "Level " + model.Level;

        MessageBroker.Default.Receive<Generator_GenerateMessage>().Where(s=> s.Id == id).Subscribe(((x) => { Generate(x); })).AddTo(disposables);
    }

    private void OnDisable()
    {
        
        disposables.Clear();
    }

    void Generate(Generator_GenerateMessage message)
    {
        fillImageGenerating.DOFillAmount(message.FillAmount, message.TimePassed);
        fillImageCapacity.DOFillAmount(message.CapacityFillAmount, message.TimePassed);

        generatedAmount.text = message.GeneratedAmount.ToString();
    }
}
