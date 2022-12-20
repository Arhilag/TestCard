using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RandomChanger : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private HandPool _handPool;
    [SerializeField] private CardPresenter[] _cardsPool;
    [Header("Settings")]
    [SerializeField] private int _minRandomNum = -2;
    [SerializeField] private int _maxRandomNum = 9;
    [SerializeField] private float _delayChangeCard = 0.5f;

    private void Awake()
    {
        _button.onClick.AddListener(StartRandomizing);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(StartRandomizing);
    }

    private async void StartRandomizing()
    {
        _button.interactable = false;
        GetCardsPool();
        foreach (var card in _cardsPool)
        {
            var randomNum = Random.Range(_minRandomNum, _maxRandomNum+1);
            card.RandomizeValue(randomNum);
            await Task.Delay((int)(_delayChangeCard*1000));
        }
        if(Application.isPlaying)
            _button.interactable = true;
    }

    private void GetCardsPool()
    {
        var cardList = _handPool.GetCardsPool();
        _cardsPool = new CardPresenter[cardList.Count];
        for (int i = 0; i < cardList.Count; i++)
        {
            _cardsPool[i] = cardList[i];
        }
    }
}
