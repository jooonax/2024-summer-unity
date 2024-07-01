using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundController : MonoBehaviour {
    public int RoundNumber { get; private set; }

    [SerializeField]
    private CardController cardController;

    public void NextRound() {
        RoundNumber++;
        cardController.Permanent();
        cardController.DrawCards(CardController.HAND_CARDS_AMOUNT - cardController.HandCards.Where(c => c != null).ToArray().Length);
    }
} 