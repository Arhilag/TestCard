using System;

public class CardModel
{
    private int _health;
    private int _attack;
    private int _cost;

    public Action<int,int,int> OnSetCard;
    public Action<int,int> OnChangeHealth;
    public Action<int,int> OnChangeAttack;
    public Action<int,int> OnChangeCost;
    public Action OnDeathCard;

    public CardModel(CardConfig config)
    {
        _cost = config.Cost;
        _attack = config.Attack;
        _health = config.Health;
    }

    public void Start()
    {
        OnSetCard?.Invoke(_cost,_attack,_health);
    }

    public void ChangeAttack(int newAttack)
    {
        OnChangeAttack?.Invoke(_attack,newAttack);
        _attack = newAttack;
    }

    public void ChangeHealth(int newHealth)
    {
        OnChangeHealth?.Invoke(_health,newHealth);
        _health = newHealth;
        if (_health <= 0)
        {
            OnDeathCard?.Invoke();
        }
    }

    public void ChangeCost(int newCost)
    {
        OnChangeCost?.Invoke(_cost,newCost);
        _cost = newCost;
    }
}
