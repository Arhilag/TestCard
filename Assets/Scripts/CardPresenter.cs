using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class CardPresenter
{
    private CardView _cardView;
    private CardModel _cardModel;
    private string _imageUrl = "https://picsum.photos/90/80";
    private Texture2D _texture = null;

    public static Action<CardPresenter> OnRemoveCard;

    public CardPresenter(CardView view, CardModel model)
    {
        _cardView = view;
        _cardModel = model;
        OnEnable();
    }

    private async void OnEnable()
    {
        _cardModel.OnSetCard += _cardView.SetCardParam;
        _cardModel.OnChangeAttack += _cardView.OnChangeAttack;
        _cardModel.OnChangeCost += _cardView.OnChangeCost;
        _cardModel.OnChangeHealth += _cardView.OnChangeHealth;
        _cardModel.OnDeathCard += OnDeathCard;
        
        _cardView.gameObject.SetActive(false);
        _cardModel.Start();
        _texture = await GetRemoteTexture(_imageUrl);
        _cardView.SetArt(Sprite.Create(_texture, new Rect(0.0f, 0.0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100.0f));
        _cardView.gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        _cardModel.OnSetCard -= _cardView.SetCardParam;
        _cardModel.OnChangeAttack -= _cardView.OnChangeAttack;
        _cardModel.OnChangeCost -= _cardView.OnChangeCost;
        _cardModel.OnChangeHealth -= _cardView.OnChangeHealth;
        _cardModel.OnDeathCard -= OnDeathCard;
    }

    private static async Task<Texture2D> GetRemoteTexture ( string url )
    {
        using UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        var asyncOp = www.SendWebRequest();

        while( asyncOp.isDone==false )
            await Task.Delay(1000/30);
        
        if( www.result!=UnityWebRequest.Result.Success )
        {
            return null;
        }

        return DownloadHandlerTexture.GetContent(www);
    }

    public void Move(Vector3 target, Vector3 rotate)
    {
        _cardView.Move(target, rotate);
    }

    public Texture2D GetTexture()
    {
        return _texture;
    }

    private async void OnDeathCard()
    {
        await _cardView.OnDeathCard();
        OnRemoveCard?.Invoke(this);
    }

    public void RandomizeValue(int newNum)
    {
        var value = Random.Range(0, 3);
        switch (value)
        {
            case 0:
                _cardModel.ChangeAttack(newNum);
                break;
            case 1:
                _cardModel.ChangeCost(newNum);
                break;
            case 2:
                _cardModel.ChangeHealth(newNum);
                break;
        }
    }
}
