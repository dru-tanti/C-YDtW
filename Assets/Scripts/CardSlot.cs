using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler {
	// Make sure you select Dynamic Data in the Editor
	public UnityEvent<Card> cardDrop;
	public void OnDrop(PointerEventData eventData) {
		Card card = eventData.pointerDrag.GetComponent<Card>();
		// Only set this as a parent if there isn't a card already attached to the slot.
		if(this.transform.childCount < 1) {
			card.parentAfterDrag = this.transform;
			cardDrop.Invoke(card);
		}
	}
}
