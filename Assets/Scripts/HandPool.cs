using System.Collections.Generic;
using UnityEngine;

public class HandPool : MonoBehaviour
{
    [Header("Settings")]
    [Header("Vertical")]
    [SerializeField] private float _xSpacing;
    [Header("Horizontal")]
    [SerializeField] private float _arc;
    [SerializeField] private float _yOffset;
    [Header("Rotate")]
    [SerializeField] private int _step;
    private List<CardPresenter> _cardsPool = new List<CardPresenter>();

    public void AddHand(CardPresenter card)
    {
        _cardsPool.Add(card);
        ChangeCardsPosition();
    }

    private void ChangeCardsPosition()
    {
        int count = _cardsPool.Count;
        float halfCount = (float)(count-1) / 2;
        for (int i = 0; i < count; i++)
        {
            float powX = Mathf.Pow(i - halfCount, 2);
            _cardsPool[i].Move(new Vector3(i*(2+_xSpacing)-count,
                    -_arc*powX+_yOffset,0), 
                new Vector3(0,0,_step*(count-2)-_step*2*i+_step));
        }
    }
    
    public void RemoveHand(CardPresenter card)
    {
        _cardsPool.Remove(card);
        ChangeCardsPosition();
    }

    private void Awake()
    {
        CardPresenter.OnRemoveCard += RemoveHand;
    }

    private void OnDestroy()
    {
        CardPresenter.OnRemoveCard -= RemoveHand;
    }

    public List<CardPresenter> GetCardsPool()
    {
        return _cardsPool;
    }
}
