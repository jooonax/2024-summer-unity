using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundController : MonoBehaviour {
    public int RoundNumber { get; private set; }

    [SerializeField]
    private CardController cardController;

    public delegate void NextRoundDelegate();
    public NextRoundDelegate OnNextRound;

    public void NextRound() {
        RoundNumber++;
        OnNextRound?.Invoke();
        if (cardController.DrawPile.Count == 0) {
            NextDeckPart();
        } else {
            cardController.DrawCards(CardController.HAND_CARDS_AMOUNT - cardController.HandCards.Where(c => c != null).ToArray().Length);
        }
    }
    public void NextDeckPart() {
        cardController.NextDeckPart();
        cardController.DrawCards(CardController.HAND_CARDS_AMOUNT);
    }
} 