using TMPro;
using UnityEngine;
using GlobalEnums;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;

public class UIManager : MonoBehaviour {
	private static UIManager current; // Declares it as a Singleton since we have a static function.
	public GameState gameState;
	public GameObject menu;

	[Header("Tooltip Management")]
	public GameObject tooltipObject;
	public TextMeshProUGUI tooltipHeader;
	public TextMeshProUGUI tooltipBody;
	public LayoutElement tooltipLayout;
	private RectTransform rectTransform;
	public int tooltipCharacterWrapLimit = 50;
	[SerializedDictionary("Name", "Tooltip")]
	public SerializedDictionary<string, Tooltip> tooltipManager;

	[Header("Game State UI")]
	public Slider doomMeter;
	public Slider turnMeter;
	public TextMeshProUGUI energy;
	public TextMeshProUGUI food;
	public TextMeshProUGUI minerals;
	public TextMeshProUGUI science;


	public void Awake() {
		current = this;
		rectTransform = tooltipObject.GetComponent<RectTransform>();
	}
   
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
		turnMeter.value = gameState.turnCounter;
	}

	public void SetTooltipText(string header="", string body="") {
		if(string.IsNullOrEmpty(header)) {
			tooltipHeader.gameObject.SetActive(false);
		} else {
			tooltipHeader.gameObject.SetActive(true);
			tooltipHeader.text = header;
		}
		tooltipBody.text = body;
		tooltipLayout.enabled = tooltipHeader.text.Length > tooltipCharacterWrapLimit || tooltipBody.text.Length > tooltipCharacterWrapLimit;
	}

	public static void ShowTooltip(TooltipTrigger trigger) {
		// Set the position.
		Vector2 position = trigger.gameObject.transform.position;
		current.tooltipObject.transform.position = position;

		// Anchor the tooltip in such a way that it is not offscreen.
		float pivotX = position.x / Screen.width;
		float pivotY = position.y / Screen.width;
		current.rectTransform.pivot = new Vector2(pivotX,pivotY);

		string header = current.tooltipManager[trigger.tooltipName].header;
		string body = current.tooltipManager[trigger.tooltipName].body;

		// Set Text.
		current.SetTooltipText(header, body);
		current.tooltipObject.SetActive(true);
	}

	public static void HideTooltip() {
		current.tooltipObject.SetActive(false);
	}

	public static void ShowMenu() {
		current.menu.SetActive(true);
	}

	public static void HideMenu() {
		current.menu.SetActive(false);
	}
}
