using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardView : MonoBehaviour
{
    [SerializeField] private Image _cardArt;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _attack;
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _mana;

    [Header("Settings")]
    [SerializeField] private float _delayanimation = 0.2f;
    [SerializeField] private float _duration;
    
    private enum AnimateEnum
    {
        NotAnimated,
        Animated
    }

    private AnimateEnum _status;

    public void SetArt(Sprite art)
    {
        _cardArt.sprite = art;
    }
    
    public void Move(Vector3 target, Vector3 rotate)
    {
        transform.DOMove(target, _duration);
        transform.DORotate(rotate, _duration);
    }

    public void SetCardParam(int cost, int attack, int health)
    {
        _mana.text = $"{cost}";
        _attack.text = $"{attack}";
        _hp.text = $"{health}";
    }
    
    public void OnChangeHealth(int oldNum, int newNum)
    {
        StartCoroutine(AnimationText(oldNum, newNum, _hp));
    }
    
    public void OnChangeAttack(int oldNum, int newNum)
    {
        StartCoroutine(AnimationText(oldNum, newNum, _attack));
    }
    
    public void OnChangeCost(int oldNum, int newNum)
    {
        StartCoroutine(AnimationText(oldNum, newNum, _mana));
    }

    private IEnumerator AnimationText(int oldNum, int newNum, TMP_Text text)
    {
        _status = AnimateEnum.Animated;
        var nowNum = oldNum;
        while (nowNum != newNum)
        {
            if(nowNum < newNum)
                nowNum++;
            if(nowNum > newNum)
                nowNum--;
            text.text = $"{nowNum}";
            yield return new WaitForSeconds(_delayanimation);
        }
        _status = AnimateEnum.NotAnimated;
    }
    
    public async Task<bool> OnDeathCard()
    {
        while (_status == AnimateEnum.Animated)
        {
            await Task.Delay(1000 / 30);
        }
        gameObject.SetActive(false);
        return true;
    }
}
