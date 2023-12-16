using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Test_DELETE : MonoBehaviour
{
    IDisposable timeListener;

    private void OnEnable()
    {
        timeListener = MessageBroker.Default.Receive<GameManager_TimeMessage>().Subscribe(((x) => { Bind(x); })).AddTo(this);
    }
    private void OnDisable()
    {
        if (timeListener != null) timeListener.Dispose();
    }
   
    void Bind(GameManager_TimeMessage message)
    {
        Debug.LogError(message.TimePassed);
    }

    
}
