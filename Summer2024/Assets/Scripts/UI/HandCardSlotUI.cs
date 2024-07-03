using UnityEngine;
using UnityEngine.UI;

public class HandCardSlotUI : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI describtionText;
    [SerializeField]
    private GameObject slotObject;
    [SerializeField]
    private GameObject emptySlotObject;

    [Header("Active")]
    [SerializeField]
    private Button activateButton;
    [SerializeField]
    private TMPro.TextMeshProUGUI activateTitleText;
    [SerializeField]
    private TMPro.TextMeshProUGUI activateCostDescritpionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI activateEffectDescritpionText;

    [Header("Permanent")]
    [SerializeField]
    private TMPro.TextMeshProUGUI permanentTitleText;
    [SerializeField]
    private TMPro.TextMeshProUGUI permanentCostDescritpionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI permanentEffectDescritpionText;

    [Header("Special")]
    [SerializeField]
    private TMPro.TextMeshProUGUI specialTitleText;
    [SerializeField]
    private TMPro.TextMeshProUGUI specialTriggerDescritpionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI specialDescritpionText;

    [Header("Destruct")]
    [SerializeField]
    private Button destructButton;
    [SerializeField]
    private TMPro.TextMeshProUGUI destructTitleText;
    [SerializeField]
    private TMPro.TextMeshProUGUI destructDescritpionText;

    private CardController cardController;
    private int id;

    public void Init(CardController cardController, int id) {
        this.cardController = cardController;
        this.id = id;
    }

    public void UpdateUI() {
        Card _card = cardController.HandCards[id];
        if (_card == null) {
            emptySlotObject.SetActive(true);
            slotObject.SetActive(false);
            return;
        }
        slotObject.SetActive(true);
        emptySlotObject.SetActive(false);

        nameText.text = _card.CardSO.CardName;
        describtionText.text = _card.CardSO.CardDescribtion;

        activateTitleText.text = _card.CardSO.CardEvents.ActivationName;
        activateCostDescritpionText.text = _card.CardSO.CardEvents.ActivationCost.Description;
        activateEffectDescritpionText.text = _card.CardSO.CardEvents.ActivationEffect.Description;

        permanentTitleText.text = _card.CardSO.CardEvents.PermanentName;
        permanentCostDescritpionText.text = _card.CardSO.CardEvents.PermanentCost.Description;
        permanentEffectDescritpionText.text = _card.CardSO.CardEvents.PermanentEffect.Description;

        specialTitleText.text = _card.CardSO.CardEvents.SpecialName;
        specialTriggerDescritpionText.text = _card.CardSO.CardEvents.SpecialEvent.EventTriggerDescription;
        specialDescritpionText.text = _card.CardSO.CardEvents.SpecialEvent.Event.Description;

        destructTitleText.text = _card.CardSO.CardEvents.DestructionName;
        destructDescritpionText.text = _card.CardSO.CardEvents.DestructionEffect.Description;

    }

    private void Awake() {
        activateButton.onClick.AddListener(this.OnActivate);
        destructButton.onClick.AddListener(this.OnDestruct);
    }

    private void OnActivate() {
        cardController.ActivateCard(id);
    }

    private void OnDestruct() {
        cardController.DestructCard(id);
    }

    private void OnDestroy() {
        activateButton.onClick.RemoveAllListeners();
        destructButton.onClick.RemoveAllListeners();
    }
}