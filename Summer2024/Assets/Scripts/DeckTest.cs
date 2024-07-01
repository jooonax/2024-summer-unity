using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckTest : MonoBehaviour {
    [SerializeField]
    private List<CardSO> earlyDeck;
    [SerializeField]
    private List<CardSO> midDeck;
    [SerializeField]
    private List<CardSO> endDeck;

    [SerializeField]
    private CardController cardController;
    

    void Start () {
        cardController.StartGame(new()
        {
            earlyDeck.Select(c => c.CardPrefab).ToList(),
            midDeck.Select(c => c.CardPrefab).ToList(),
            endDeck.Select(c => c.CardPrefab).ToList()
        });
    }
}