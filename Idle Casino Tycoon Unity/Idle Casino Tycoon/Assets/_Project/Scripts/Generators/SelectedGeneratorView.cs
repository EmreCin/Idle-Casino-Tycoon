using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.UI;

public class SelectedGeneratorView : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text generatorName;
    [SerializeField] TMPro.TMP_Text generatorLevel;
    [SerializeField] TMPro.TMP_Text capacity;
    [SerializeField] TMPro.TMP_Text upgradeCost;
    [SerializeField] TMPro.TMP_Text generation;
    [SerializeField] Image capacityFill;
    [SerializeField] Image levelFill;


    string currentId = "";
    GeneratorModel model;
    private CompositeDisposable disposables = new CompositeDisposable();

    public void Init(GeneratorModel generatorModel)
    {
        model = generatorModel;
        

        generatorName.text = model.Name;
        generatorLevel.text = "Level "+ model.Level.ToString()+" / "+ model.MaxLevel;
        levelFill.fillAmount = (float)model.Level / model.MaxLevel;
        upgradeCost.text = "$"+model.UpgradeCost.ToString();
        generation.text = "$" + model.GeneratingAmount.ToString() + " / " + model.Interval.ToString() + " sec.";

        if (generatorModel.Id != currentId)
        {
            MessageBroker.Default.Receive<Generator_GenerateMessage>().Where(s => s.Id == currentId).Subscribe(((x) => { UpdateInfo(x); })).AddTo(disposables);
            currentId = model.Id;
            Begin();
        }
        

    }

    void Begin()
    {
        Vector3 originalPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(originalPos.x, originalPos.y - 700, originalPos.z);
        gameObject.transform.DOMove(originalPos, 0.5f).SetEase(Ease.InOutQuad);

        gameObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(DelayedClose());
    }

    private void UpdateInfo(Generator_GenerateMessage message)
    {
        capacity.text = "$"+message.GeneratedAmount+" / $"+ model.IdleCapacity.ToString();
        capacityFill.fillAmount = message.GeneratedAmount / model.IdleCapacity;

        string amountMultiplierText = "";
        if(message.AmountMultiplier != 1)
        {
            amountMultiplierText = "<color=green>" +" +"+ ((model.GeneratingAmount * message.AmountMultiplier) - model.GeneratingAmount).ToString() + "</color>";
        }

        string speedMultiplierText = "";
        if (message.SpeedMultiplier != 1)
        {
            speedMultiplierText = "<color=green> " +((model.Interval * message.SpeedMultiplier) - model.Interval).ToString() + "</color>";
        }
        generation.text = "$" + model.GeneratingAmount.ToString() +amountMultiplierText+ " / " + model.Interval.ToString() + speedMultiplierText+ " sec.";
    }



    private void OnDisable()
    {
        StopAllCoroutines();
        disposables.Clear();
    }

    IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(15);

        Close();
    }

   public void Close()
    {
        Vector3 originalPos = gameObject.transform.position;
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 700, gameObject.transform.position.z), 0.5f).SetEase(Ease.InBack)
            .OnComplete(()=> {
                gameObject.SetActive(false);
                gameObject.transform.position = originalPos;
                currentId = "";
            });
    }

}
