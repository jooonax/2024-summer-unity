using UnityEngine;

[CreateAssetMenu(menuName = "SO/Card")]
[System.Serializable]
public class CardSO : ScriptableObject
{
    [field:SerializeField]
    public Card CardPrefab { get; private set;}
    
    [field:Header("Overview")]
    [field:SerializeField]
    public string CardName { get; private set;}
    [field:SerializeField]
    [field:TextArea(3,10)]
    public string CardDescribtion { get; private set;}
    [field:SerializeField]
    public Sprite CardSprite { get; private set;}


    [field:Header("Events")]
    [field:SerializeField]
    public CardEventsSO CardEvents { get; private set;}
}