using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[Tooltip("The index of the Tooltip that this will show in the Tooltip Manager")]
	public string tooltipName;
	private static LTDescr delay;

	public void OnPointerEnter(PointerEventData pointer) {
		// Sets a half second delay before the tooltip appears.
		delay = LeanTween.delayedCall(0.5f, () => {
			UIManager.ShowTooltip(this);
		});
	}

	public void OnPointerExit(PointerEventData pointer) {
		LeanTween.cancel(delay.uniqueId);
		UIManager.HideTooltip();
	}
}
 