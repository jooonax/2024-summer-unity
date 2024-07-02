using UnityEngine;
using UnityEngine.UI;

public class BuildingEventUI : MonoBehaviour {
    [SerializeField]
    private Button buildBuildingButton;

    private BuildingEvent buildingEvent;

    public void Init(BuildingEvent buildingEvent) {
        this.buildingEvent = buildingEvent;
    }

    private void Awake() {
        buildBuildingButton.onClick.AddListener(Build);
    }

    private void Build() {
        buildingEvent.BuildBuilding(buildingEvent.DestroyWithCard);
    }

    private void OnDestroy() {
        buildBuildingButton.onClick.RemoveAllListeners();
    }
}