using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GeneratorController : MonoBehaviour//, IGeneratorController
{
    GeneratorModel model;
    GeneratorBehavior behavior;

    IDisposable timeListener;
    

    public void Init(GeneratorModel model)
    {
        this.model = model;
        behavior = new GeneratorBehavior(this.model);

        timeListener = MessageBroker.Default.Receive<GameManager_TimeMessage>().Subscribe(((x) => { Generate(x); })).AddTo(this);
    }

    private void OnDisable()
    {
        if (timeListener != null) timeListener.Dispose();
    }

    void Generate(GameManager_TimeMessage message)
    {
        behavior.Generate(message.TimePassed);
    }

    
}




