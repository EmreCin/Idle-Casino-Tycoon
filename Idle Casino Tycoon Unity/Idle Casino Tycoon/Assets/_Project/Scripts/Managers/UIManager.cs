using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] SelectedGeneratorView selectedGenerator;
    // Start is called before the first frame update
    private CompositeDisposable disposables = new CompositeDisposable();

    void Start()
    {
        MessageBroker.Default.Receive<GeneratorController_SelectedMessage>().Subscribe(((x) => { SelectGenerator(x.Model); })).AddTo(disposables);
    }

    private void OnDestroy()
    {
        disposables.Clear();
    }

    void SelectGenerator(GeneratorModel model)
    {
        //selectedGenerator.gameObject.SetActive(true);
        selectedGenerator.Init(model);
    }
    
}
