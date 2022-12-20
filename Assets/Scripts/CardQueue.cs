using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CardQueue
{
    private HandPool _handPool;
    private List<CardPresenter> _cardPresenters;

    public CardQueue(List<CardPresenter> cardPresenters)
    {
        _cardPresenters = cardPresenters;
        OnEnable();
    }
    
    private void OnEnable()
    {
        RunQueue();
    }

    public void AddQueue(CardPresenter presenter)
    {
        _cardPresenters.Add(presenter);
    }

    private async void RunQueue()
    {
        while (Application.isPlaying)
        {
            if (_cardPresenters != null && _cardPresenters.Count > 0)
            {
                await Task.Delay(1000);
                var presenter = _cardPresenters[0];
                _cardPresenters.Remove(presenter);
                while (presenter.GetTexture() == null)
                {
                    await Task.Yield();
                }
                _handPool.AddHand(presenter);
            }
            await Task.Yield();
        }
    }

    public void SetHandPool(HandPool handPool)
    {
        _handPool = handPool;
    }
}
