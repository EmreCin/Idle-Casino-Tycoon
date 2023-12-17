using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelDataSO levelData;
    [SerializeField] GeneratorController generatorController;
    [SerializeField] Transform generatorContainer;

    GeneratorFactory factory;
    Wallet wallet;


    float timer;
    bool isWorldActive;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Currency test = new Currency(CurrencyType.Money, 100);
        List<Currency> testList = new List<Currency>();
        testList.Add(test);

        wallet = new Wallet(testList);

        factory = new GeneratorFactory();
        foreach (var generator in levelData.LevelGenerator.GeneratorList)
        {
            factory.Create(1 ,generatorController, generator, wallet, generatorContainer);
        }

        isWorldActive = true;
    }


    private void Update()
    {
        //    timer += Time.deltaTime;

        //if(isWorldActive && timer >= 1)
        //{
        //    MessageBroker.Default.Publish(new GameManager_TimeMessage(timer));
        //    timer = 0;
        //}
        MessageBroker.Default.Publish(new GameManager_TimeMessage(Time.deltaTime));
    }

}
