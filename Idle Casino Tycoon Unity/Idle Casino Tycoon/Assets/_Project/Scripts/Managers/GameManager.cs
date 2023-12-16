using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelDataSO levelData;
    [SerializeField] GeneratorController generatorController;
    [SerializeField] Transform generatorContainer;

    float timer;
    bool isWorldActive;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        GeneratorFactory factory = new GeneratorFactory();
        foreach (var generator in levelData.LevelGenerator.GeneratorList)
        {
            factory.Create(1 ,generatorController, generator, generatorContainer);
        }

        isWorldActive = true;
    }


    private void Update()
    {
            timer += Time.deltaTime;

        if(isWorldActive && timer >= 1)
        {
            MessageBroker.Default.Publish(new GameManager_TimeMessage(timer));
            timer = 0;
        }

    }

}
