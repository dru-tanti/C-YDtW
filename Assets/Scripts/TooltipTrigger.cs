using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	public string tooltipName;

	public void OnPointerEnter(PointerEventData pointer) {
		UIManager.ShowTooltip(this);
	}

	public void OnPointerExit(PointerEventData pointer) {
		UIManager.HideTooltip();
	}
}
