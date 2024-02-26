using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler {
	public event Action<Card> OnCardPlayed;
	public void OnDrop(PointerEventData eventData) {
		Card card = eventData.pointerDrag.GetComponent<Card>();
		// Only set this as a parent if there isn't a card already attached to the slot.
		if(this.transform.childCount < 1) {
			card.parentAfterDrag = this.transform;
			OnCardPlayed?.Invoke(card);
		}
	}
}
