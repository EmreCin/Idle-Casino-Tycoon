using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class DecorativeViewController : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] DecorativeItemView decorativeItemPrefab;

    private CompositeDisposable disposables = new CompositeDisposable();

    List<DecorativeItemView> decorativeItemList = new List<DecorativeItemView>();
    Vector3 originalPos = Vector3.one * -1;
    private void Awake()
    {
        
        MessageBroker.Default.Receive<Decorative_BuyMessage>().Subscribe(((x) => { Buyed(x.Model); })).AddTo(disposables);
    }

    public void Init(DecorativeDataContainerSO dataContainer)
    {
        DecorativeFactory decorativeFactory = new DecorativeFactory();

        foreach (var decorative in dataContainer.DecorativeList)
        {
            var itemView = decorativeFactory.Create(decorative, decorativeItemPrefab, container);


            decorativeItemList.Add(itemView);
        }
       
    }

    void Buyed(DecorativeModel model)
    {
        var buyed = decorativeItemList.FirstOrDefault(s => s.Id == model.Id);
        if (buyed != null) buyed.SetUnlocked();

       var visual = Instantiate(model.VisualPrefab, model.VisualPosition, Quaternion.identity);
        visual.GetComponent<SpriteRenderer>().sprite = model.SpriteList[model.SelectedVisual];
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
        if(originalPos == Vector3.one * -1) originalPos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(originalPos.x, originalPos.y - 700, originalPos.z);
        gameObject.transform.DOMove(originalPos, 0.5f).SetEase(Ease.InOutQuad);

        gameObject.SetActive(true);
    }
    void Off()
    {
        if (originalPos == Vector3.one * -1) originalPos = gameObject.transform.position;
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 700, gameObject.transform.position.z), 0.5f).SetEase(Ease.InBack)
            .OnComplete(() => {
                gameObject.SetActive(false);
                gameObject.transform.position = originalPos;
            });
    }
}
