using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MultiplierManager : MonoBehaviour
{
    private List<MultiplierInstance> multiplierList = new List<MultiplierInstance>();

    private CompositeDisposable disposables = new CompositeDisposable();

    float timeProgress;

   

    private void Start()
    {
        MessageBroker.Default.Receive<GameManager_TimeMessage>().Subscribe(((x) => { GetTime(x); })).AddTo(disposables);
        MessageBroker.Default.Receive<Decorative_BuyMessage>().Subscribe(((x) => { BuyedDecorative(x.Model.MultiplierInstanceList); })).AddTo(disposables);
        //TEST ****************************
        MultiplierInstance test1 = new MultiplierInstance("L1_SlotMachine_Level", MultiplierType.Amount, 2, 3);
        MultiplierInstance test2 = new MultiplierInstance("L1_SlotMachine_Formula", MultiplierType.Speed, 0.1f, 3);

       
        AddToList(test1);
        AddToList(test2);
        //TEST
    }

    private void OnDestroy()
    {
        disposables.Clear();
    }

    private void GetTime(GameManager_TimeMessage message)
    {
        timeProgress = message.TimePassed;
    }

    private void AddToList(MultiplierInstance multiplier)
    {
        multiplierList.Add(multiplier);
        MessageBroker.Default.Publish(new MultiplierMessage(multiplierList));

        
        if(multiplier.Duration.Value > 0)
        {
            this.UpdateAsObservable()
           .Where(_ => multiplier.Duration.Value > 0f)
           .Subscribe(_ =>
           {
               multiplier.Duration.Value -= timeProgress;
               timeProgress = 0;
           });

            multiplier.Duration.Subscribe(value =>
            {
                if (value <= 0f)
                {
                    if (multiplierList.Contains(multiplier))
                    {
                        multiplierList.Remove(multiplier);
                        MessageBroker.Default.Publish(new MultiplierMessage(multiplierList));
                    }
                }
            });
        }
       
    }

    private void BuyedDecorative(List<MultiplierInstance> multiplierList)
    {
        foreach (var item in multiplierList)
        {
            AddToList(item);
        }
    }

}


public class MultiplierInstance
{
    public string Id;
    public MultiplierType MultiplierType;
    public float Multiplier;
    public ReactiveProperty<float> Duration = new ReactiveProperty<float>();

    public MultiplierInstance(string id, MultiplierType multiplierType, float multiplier, float duration)
    {
        Id = id;
        MultiplierType = multiplierType;
        Multiplier = multiplier;
        Duration.Value = duration;
    }
}