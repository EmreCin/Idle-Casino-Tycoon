using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeViewController : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] DecorativeItemView decorativeItemPrefab;

    

    public void Init(DecorativeDataContainerSO dataContainer)
    {
        DecorativeFactory decorativeFactory = new DecorativeFactory();

        foreach (var decorative in dataContainer.DecorativeList)
        {
            var itemView = decorativeFactory.Create(decorative, decorativeItemPrefab, container);
           

            //controller.Collected.Subscribe(x => Collect(x)).AddTo(disposables);
            //controller.Leveluped.Subscribe(x => LevelUpGenerator(x)).AddTo(disposables);
            //controller.Unlocked.Subscribe(x => UnlockGenerator(x)).AddTo(disposables);
        }
    }

    public void OnOff(bool isOn)
    {
        if (isOn)
            On();
        else
            Off();
    }

    void On()
    {
        Vector3 originalPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(originalPos.x, originalPos.y - 700, originalPos.z);
        gameObject.transform.DOMove(originalPos, 0.5f).SetEase(Ease.InOutQuad);

        gameObject.SetActive(true);
    }
    void Off()
    {
        Vector3 originalPos = gameObject.transform.position;
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 700, gameObject.transform.position.z), 0.5f).SetEase(Ease.InBack)
            .OnComplete(() => {
                gameObject.SetActive(false);
                gameObject.transform.position = originalPos;
            });
    }
}
