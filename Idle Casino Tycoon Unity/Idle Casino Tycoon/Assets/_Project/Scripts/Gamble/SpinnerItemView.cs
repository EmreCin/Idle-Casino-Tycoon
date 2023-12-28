using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerItemView : MonoBehaviour
{
    [SerializeField] Image resultTransform;
    [SerializeField] TMPro.TMP_Text resultText;

    private const string failText = "Try Again!";

    public void PublishResult(bool isWin, string winMessage)
    {

        if (isWin) PublishWin(winMessage);
        else PublishLose();

        resultTransform.gameObject.SetActive(true);
    }

    void PublishWin(string message)
    {
        resultTransform.color = Color.green;
        resultText.text = message;
    }
    void PublishLose()
    {
        resultText.text = failText;
        resultTransform.color = Color.red;
    }

    public void Reset()
    {
        resultTransform.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Reset();
    }
}
