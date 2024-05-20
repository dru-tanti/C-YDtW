using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[Tooltip("The index of the Tooltip that this will show in the Tooltip Manager")]
	public string tooltipName;

	public void OnPointerEnter(PointerEventData pointer) {
		UIManager.ShowTooltip(this);
	}

	public void OnPointerExit(PointerEventData pointer) {
		UIManager.HideTooltip();
	}
}
