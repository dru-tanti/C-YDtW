using TMPro;
using UnityEngine;
using GlobalEnums;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	public GameState gameState;
	public Slider doomMeter;
	public TextMeshProUGUI energy;
	public TextMeshProUGUI food;
	public TextMeshProUGUI minerals;
	public TextMeshProUGUI science;

    // Start is called before the first frame update
    void Start() {
        UpdateUI();
    }

	public void UpdateUI() {
		energy.SetText(gameState.resources[ResourceType.Energy].ToString());
		food.SetText(gameState.resources[ResourceType.Food].ToString());
		minerals.SetText(gameState.resources[ResourceType.Material].ToString());
		science.SetText(gameState.resources[ResourceType.Science].ToString());
		doomMeter.value = gameState.doomMeter;
	}
}
