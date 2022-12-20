using UnityEngine;

[CreateAssetMenu(fileName = "CardsPoolConfig", menuName = "ScriptableObjects/CardsPoolConfig", order = 1)]
public class CardsPoolConfig : ScriptableObject
{
    public CardConfig[] CardConfigs;
}
