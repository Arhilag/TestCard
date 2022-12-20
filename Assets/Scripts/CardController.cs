using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private Transform _canvasContainer;
    [SerializeField] private CardView _cardViewPrefab;
    [SerializeField] private HandPool _handPool;
    [Header("Config")]
    [SerializeField] private CardsPoolConfig _cardsPoolConfig;
    [Header("Number of cards")]
    [SerializeField] private int _minCountCard = 4;
    [SerializeField] private int _maxCountCard = 6;
    
    private CardQueue _cardQueue;
    private CardPresenter _balancePresenter;
    private CardModel _balanceModel;

    private void Start()
    {
        _cardQueue = new CardQueue(new List<CardPresenter>());
        _cardQueue.SetHandPool(_handPool);

        var countCard = Random.Range(_minCountCard, _maxCountCard+1);
        
        for (int i = 0; i < countCard; i++)
        {
            var view = Instantiate(_cardViewPrefab, _canvasContainer);
            _balanceModel = 
                new CardModel(_cardsPoolConfig.CardConfigs[Random.Range(0,_cardsPoolConfig.CardConfigs.Length)]);
            _balancePresenter = new CardPresenter(view, _balanceModel);
            _cardQueue.AddQueue(_balancePresenter);
        }
    }

    private void OnDestroy()
    {
        _balancePresenter.OnDisable();
    }
}