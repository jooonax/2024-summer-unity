using UnityEngine;
using UnityEngine.UI;

public class HandCardSlotUI : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    [Header("Buttons")]
    [SerializeField]
    private Button activateButton;
    [SerializeField]
    private Button destructButton;

    private CardController cardController;
    private int id;

    public void Init(CardController cardController, int id) {
        this.cardController = cardController;
        this.id = id;
    }

    public void UpdateUI() {
        Card _card = cardController.HandCards[id];
        if (_card == null) {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);

        nameText.text = _card.CardSO.CardName;
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