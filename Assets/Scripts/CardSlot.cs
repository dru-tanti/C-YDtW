using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler {
	public event Action<Card> OnCardDropped;
	public bool ClimateSlot { get; set; } = false;
	public void OnDrop(PointerEventData eventData) {
		Card card = eventData.pointerDrag.GetComponent<Card>();
		// Only set this as a parent if there isn't a card already attached to the slot, and if the card is set as playable.
		if(card.IsPlayable && this.transform.childCount < 1) {
			card.parentAfterDrag = this.transform;
			OnCardDropped?.Invoke(card);
		}
	}
}
