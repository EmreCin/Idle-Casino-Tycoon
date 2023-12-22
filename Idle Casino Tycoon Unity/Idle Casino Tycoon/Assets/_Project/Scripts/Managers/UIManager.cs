using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] SelectedGeneratorView selectedGenerator;
    [SerializeField] Transform CurrenyContainer;
    [SerializeField] Button decorativeButton;
    [SerializeField] DecorativeViewController decorativeController;
    // Start is called before the first frame update
    private CompositeDisposable disposables = new CompositeDisposable();


    void Awake()
    {
        MessageBroker.Default.Receive<Wallet_Message>().Subscribe(((x) => { InitCurrencies(x.Wallet); })).AddTo(disposables);
        MessageBroker.Default.Receive<GeneratorController_SelectedMessage>().Subscribe(((x) => { SelectGenerator(x.Model); })).AddTo(disposables);

        decorativeButton.onClick.AddListener(() => OnOffDecorative());
    }

    void InitCurrencies(Wallet wallet)
    {
        for (int i = 0; i < CurrenyContainer.childCount; i++)
        {
            CurrenyContainer.GetChild(i).GetComponent<CurrencyView>().Init(wallet);
        }
    }


    private void OnDestroy()
    {
        disposables.Clear();
    }

    void SelectGenerator(GeneratorModel model)
    {
        decorativeController.OnOff(false);
        selectedGenerator.Init(model);
    }

    void OnOffDecorative()
    {
        decorativeController.OnOff(!decorativeController.gameObject.activeSelf);
        selectedGenerator.Close();
    }
    
}
