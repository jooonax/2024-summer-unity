using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveCardSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;
    [SerializeField]
    private TMPro.TextMeshProUGUI describtionText;
    [SerializeField]
    private Button deactivateButton;
    [SerializeField]
    private GameObject slotObject;
    [SerializeField]
    private GameObject emptySlotObject;
    [SerializeField]
    private GameObject activeSlotObject;

    [Header("Ability")]
    [SerializeField]
    private Button abilityButton;
    [SerializeField]
    private TMPro.TextMeshProUGUI abilityTitleText;
    [SerializeField]
    private TMPro.TextMeshProUGUI abilityCostDescritpionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI abilityEffectDescritpionText;


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

    private CardController cardController;
    private int id;
    private bool isActiveSlot;

    public void Init(CardController cardController, int id) {
        this.cardController = cardController;
        this.id = id;
    }

    public void UpdateUI() {
        Card _card = cardController.ActiveCards[id];
        if (_card == null) {
            emptySlotObject.SetActive(true);
            slotObject.SetActive(false);
            return;
        }
        slotObject.SetActive(true);
        emptySlotObject.SetActive(false);

        nameText.text = _card.CardSO.CardName;
        describtionText.text = _card.CardSO.CardDescribtion;

        abilityTitleText.text = _card.CardSO.CardEvents.AbilityName;
        abilityCostDescritpionText.text = _card.CardSO.CardEvents.AbilityCost.Description;
        abilityEffectDescritpionText.text = _card.CardSO.CardEvents.AbilityEffect.Description;

        permanentTitleText.text = _card.CardSO.CardEvents.PermanentName;
        permanentCostDescritpionText.text =  _card.CardSO.CardEvents.PermanentCost.Description;
        permanentEffectDescritpionText.text = _card.CardSO.CardEvents.PermanentEffect.Description;

        specialTitleText.text = _card.CardSO.CardEvents.SpecialName;
        specialTriggerDescritpionText.text = _card.CardSO.CardEvents.SpecialEvent.EventTriggerDescription;
        specialDescritpionText.text = _card.CardSO.CardEvents.SpecialEvent.Event.Description;

        abilityButton.gameObject.SetActive(!_card.UsedAbility);
    }

    private void Awake() {
        abilityButton.onClick.AddListener(this.OnAbility);
        deactivateButton.onClick.AddListener(this.OnDeactivate);
    }

    private void OnAbility() {
        cardController.UseAbility(id);
    }
    private void OnDeactivate() {
        if (isActiveSlot) {
            SetActiveSlot(false);
        }
        cardController.DeactivateCard(id);
    }

    private void OnDestroy() {
        abilityButton.onClick.RemoveAllListeners();
        deactivateButton.onClick.RemoveAllListeners();
    }

    private bool cursorOverSlot = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorOverSlot = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        cursorOverSlot = false;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Card _card = cardController.ActiveCards[id];
            if (cursorOverSlot) {
                if (_card != null) {
                    SetActiveSlot(true);
                }
            } else if (isActiveSlot && _card != null) {
                SetActiveSlot(false);
            }
        }
    }

    public void SetActiveSlot(bool a) {
        activeSlotObject.SetActive(a);
        cardController.ActiveCards[id].relatedBuildings.ForEach(b => {
            b.Tile.Marked = a;
        });
        isActiveSlot = a;
    }
}