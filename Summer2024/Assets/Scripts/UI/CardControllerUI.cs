using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardControllerUI : MonoBehaviour {
    [SerializeField]
    private Button nextRoundButton;
    [SerializeField]
    private TMPro.TextMeshProUGUI roundText;
    [SerializeField]
    private TMPro.TextMeshProUGUI deckPartText;


    [SerializeField]
    private RectTransform handCardParent;
    [SerializeField]
    private GameObject handCardSlot;
    private HandCardSlotUI[] handCardSlots;

    [SerializeField]
    private RectTransform activeCardParent;
    [SerializeField]
    private GameObject activeCardSlot;
    private ActiveCardSlotUI[] activeCardSlots;

    private CardController cardController;

    public void Init(CardController cardController) {
        this.cardController = cardController;
        
        handCardSlots = new HandCardSlotUI[CardController.HAND_CARDS_AMOUNT];
        for (int i = 0; i < CardController.HAND_CARDS_AMOUNT; i++) {
            HandCardSlotUI _slot = Instantiate(handCardSlot, handCardParent).GetComponent<HandCardSlotUI>();
            _slot.Init(cardController, i);
            handCardSlots[i] = _slot;
        }

        activeCardSlots = new ActiveCardSlotUI[CardController.ACTIVE_CARDS_AMOUNT];
        for (int i = 0; i < CardController.ACTIVE_CARDS_AMOUNT; i++) {
            ActiveCardSlotUI _slot = Instantiate(activeCardSlot, activeCardParent).GetComponent<ActiveCardSlotUI>();
            _slot.Init(cardController, i);
            activeCardSlots[i] = _slot;
        }

        nextRoundButton.onClick.AddListener(cardController.NextRound);
    }

    public void UpdateUI() {
        roundText.text = "Round " + cardController.RoundController.RoundNumber;
        deckPartText.text = "Deck Part " + (cardController.ActiveDeckPartNumber+1);
        
        foreach (HandCardSlotUI handCardSlotUI in handCardSlots) {
            handCardSlotUI.UpdateUI();
        }
        foreach (ActiveCardSlotUI activeCardSlotUI in activeCardSlots) {
            activeCardSlotUI.UpdateUI();
        }
    }

    private void OnDestroy() {
        nextRoundButton.onClick.RemoveAllListeners();
    }
}