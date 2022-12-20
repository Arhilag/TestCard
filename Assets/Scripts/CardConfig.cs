using UnityEngine;

[CreateAssetMenu(fileName = "CardConfig", menuName = "ScriptableObjects/CardConfig", order = 0)]
public class CardConfig : ScriptableObject
{
    public int Health;
    public int Attack;
    public int Cost;
}
