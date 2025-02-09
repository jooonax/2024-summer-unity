using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [field:Header("UI")]
    [field:SerializeField]
    public CardControllerUI CardControllerUI { get; private set; }

    private void Awake() {
        HandCards = new Card[HAND_CARDS_AMOUNT];
        ActiveCards = new Card[ACTIVE_CARDS_AMOUNT];
        CardControllerUI.Init(this);
        RoundController.OnNextRound += Permanent;
    
    }

    public void ActivateCard(int handIndex) {
        int activeIndex = Array.IndexOf(ActiveCards, null);
        if (activeIndex == -1) { // takes 0 when nothing free (temporary)
            activeIndex = 0;
        }

        if (HandCards[handIndex]) {

            bool successfull = HandCards[handIndex].CardSO.CardEvents.Activate(HandCards[handIndex]);
            if (!successfull) return;

            if (ActiveCards[activeIndex]) { 
                Card _activeCard = ActiveCards[activeIndex];
                _activeCard.Active = false;
                ActiveCards[activeIndex] = null;
                DiscardPile.Add(_activeCard);
            }

            ActiveCards[activeIndex] = HandCards[handIndex];
            HandCards[handIndex] = null;

            RoundController.NextRound();
            CardControllerUI.UpdateUI();
        }
    }

    public void DeactivateCard(int activeIndex) {
        Card _activeCard = ActiveCards[activeIndex];
        _activeCard.Active = false;
        ActiveCards[activeIndex] = null;
        DiscardPile.Add(_activeCard);
        CardControllerUI.UpdateUI();
    }

    public void DestructCard(int handIndex) {
        HandCards[handIndex].CardSO.CardEvents.Destruct(HandCards[handIndex]);
        DiscardPile.Add(HandCards[handIndex]);
        HandCards[handIndex] = null;
        CardControllerUI.UpdateUI();
    }

    public void NextRound() {
        RoundController.NextRound();
        CardControllerUI.UpdateUI();
    }

    public void UseAbility(int activeIndex) {
        if (!ActiveCards[activeIndex].UsedAbility) {
            bool successfull = ActiveCards[activeIndex].CardSO.CardEvents.UseAbility(ActiveCards[activeIndex]);
            ActiveCards[activeIndex].UsedAbility = successfull;
        }
        CardControllerUI.UpdateUI();
    }

    public void Permanent() {
        foreach(Card _card in ActiveCards) {
            if (_card) {
                _card.CardSO.CardEvents.Permanent(_card);
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
        CardControllerUI.UpdateUI();
    }

    public void NextDeckPart() {
        if (ActiveDeckPartNumber < Deck.Count-1) {
            HandCards = new Card[HAND_CARDS_AMOUNT];
            ActiveDeckPartNumber++;
            DrawPile = new(Deck[ActiveDeckPartNumber]);
        }
        CardControllerUI.UpdateUI();
    }

    public void StartGame(List<List<Card>> deck) {
        Deck = deck;
        ActiveDeckPartNumber = 0;
        DrawPile = new(Deck[ActiveDeckPartNumber]);
        HandCards = new Card[HAND_CARDS_AMOUNT];
        ActiveCards = new Card[ACTIVE_CARDS_AMOUNT];
        DrawCards(HAND_CARDS_AMOUNT);
    }

    private void OnDestroy() {
        RoundController.OnNextRound -= Permanent;
    }
}
