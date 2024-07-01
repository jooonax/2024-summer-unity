using UnityEngine;

[CreateAssetMenu(menuName = "SO/Card")]
[System.Serializable]
public class CardSO : ScriptableObject
{
    [SerializeField]
    private GameObject _cardPrefab;
    public Card CardPrefab { 
        get {
            Card _card = Instantiate(_cardPrefab).GetComponent<Card>();
            _card.CardSO = this;
            return _card;
        }
    }
    
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