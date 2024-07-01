using UnityEngine;
using UnityEngine.UI;

public class ActiveCardSlotUI : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    [Header("Buttons")]
    [SerializeField]
    private Button abilityButton;

    private CardController cardController;
    private int id;

    public void Init(CardController cardController, int id) {
        this.cardController = cardController;
        this.id = id;
    }

    public void UpdateUI() {
        Card _card = cardController.ActiveCards[id];
        if (_card == null) {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        nameText.text = _card.CardSO.CardName;
    }

    private void Awake() {
        abilityButton.onClick.AddListener(this.OnAbility);
    }

    private void OnAbility() {
        cardController.UseAbility(id);
    }


    private void OnDestroy() {
        abilityButton.onClick.RemoveAllListeners();
    }
}