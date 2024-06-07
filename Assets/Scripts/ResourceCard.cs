using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceCard : DraggableObject {
	public ResourceCardData cardData;
	public ResourceIcons resourceIcons;
	public bool IsPlayable { get; set; } // Set by the Game Manager on card pickup.

	[Header("UI Elements")]
	public Image cardImage;
	public TextMeshProUGUI description;
	public GameObject costSlot;
	public GameObject productionSlot;
	public event Action<ResourceCard> OnCardPickup;

	public void InitCardData(ResourceCardData data) {
		cardData = data;

		// Initialising the Resource Icons for the Cards Cost.
		foreach(Resource resource in cardData.cost) {
			GameObject resourceIcon = Instantiate(resourceIcons.icon[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			resourceIcon.transform.SetParent(costSlot.transform);
		}

		// Initialising the Resource Icons for the Cards Production per turn.
		foreach(Resource resource in cardData.production) {
			GameObject resourceIcon = Instantiate(resourceIcons.icon[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			resourceIcon.transform.SetParent(productionSlot.transform);
		}

		// Setting UI Elements
		cardImage.sprite = cardData.image;
		description.SetText(cardData.description);
	}

	public override void OnBeginDrag(PointerEventData eventData) {
		base.OnBeginDrag(eventData);
		// Inform the GameManager that a card has been picked up by the player.
		OnCardPickup.Invoke(this);
		description.raycastTarget = false;
	}

	public override void OnEndDrag(PointerEventData eventData) {
		base.OnEndDrag(eventData);
		description.raycastTarget = true;
	}
}
