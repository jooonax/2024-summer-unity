using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public static readonly int HAND_CARDS_AMOUNT = 5;
    public static readonly int ACTIVE_CARDS_AMOUNT = 3;

    public int ActiveDeckPartNumber { get; private set; }
    public List<List<Card>> Deck { get; private set; }
    [field:SerializeField]
    public List<Card> DiscardPile { get; private set; }
    [field:SerializeField]
    public List<Card> DrawPile { get; private set; }
    [field:SerializeField]
    public Card[] HandCards { get; private set; }
    [field:SerializeField]
    public Card[] ActiveCards { get; private set; }

    [field:SerializeField]
    public RoundController RoundController { get; private set; }
    [SerializeField]
    private PlayerData playerData;

    [Header("UI")]
    [SerializeField]
    private CardControllerUI cardControllerUI;

    private void Awake() {
        HandCards = new Card[HAND_CARDS_AMOUNT];
        ActiveCards = new Card[ACTIVE_CARDS_AMOUNT];
        cardControllerUI.Init(this);
    }

    public void ActivateCard(int handIndex) {
        int activeIndex = Array.IndexOf(ActiveCards, null);
        if (activeIndex == -1) {
            activeIndex = ACTIVE_CARDS_AMOUNT-1;
        }

        if (HandCards[handIndex]) {
            if (ActiveCards[activeIndex]) {
                Card _activeCard = ActiveCards[activeIndex];
                _activeCard.Active = false;
                ActiveCards[activeIndex] = null;
                DiscardPile.Add(_activeCard);
            }
            ActiveCards[activeIndex] = HandCards[handIndex];
            ActiveCards[activeIndex].CardSO.CardEvents.Activate();
            HandCards[handIndex] = null;

            RoundController.NextRound();
            cardControllerUI.UpdateUI();
        }
    }

    public void DestructCard(int handIndex) {
        HandCards[handIndex].CardSO.CardEvents.Destruct();
        HandCards[handIndex] = null;
        cardControllerUI.UpdateUI();
    }

    public void UseAbility(int activeIndex) {
        ActiveCards[activeIndex].CardSO.CardEvents.UseAbility();
        cardControllerUI.UpdateUI();
    }

    public void Permanent() {
        foreach(Card _card in ActiveCards) {
            if (_card) {
                _card.CardSO.CardEvents.Permanent();
            }
        }
    }

    public void DrawCards(int amount) {
        int firstEmptyHandIndex = Array.IndexOf(HandCards, null);
        if (firstEmptyHandIndex == -1) return;
        amount = Math.Min(DrawPile.Count, amount);

        if (amount+firstEmptyHandIndex > HAND_CARDS_AMOUNT) {
            amount = HAND_CARDS_AMOUNT-firstEmptyHandIndex;
        }
        for (int i = 0; i < amount; i++) {
            HandCards[firstEmptyHandIndex+i] = DrawPile[0];
            DrawPile.RemoveAt(0);
        }
        cardControllerUI.UpdateUI();
    }

    public void NextDeckPart() {
        if (ActiveDeckPartNumber < Deck.Count-1) {
            HandCards = new Card[HAND_CARDS_AMOUNT];
            ActiveDeckPartNumber++;
            DrawPile = new(Deck[ActiveDeckPartNumber]);
        }
        cardControllerUI.UpdateUI();
    }

    public void StartGame(List<List<Card>> deck) {
        Deck = deck;
        ActiveDeckPartNumber = 0;
        DrawPile = new(Deck[ActiveDeckPartNumber]);
        HandCards = new Card[HAND_CARDS_AMOUNT];
        ActiveCards = new Card[ACTIVE_CARDS_AMOUNT];
        DrawCards(HAND_CARDS_AMOUNT);
    }
}
