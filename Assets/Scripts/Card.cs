using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;
using GlobalEnums;

public class Card : DraggableObject {
	public CardData cardData;

	[SerializedDictionary("Resource", "Prefab")]
	public SerializedDictionary<ResourceType, GameObject> resourcePrefab;
	public bool IsPlayable { get; set; } // Set by the Game Manager on card pickup.

	[Header("UI Elements")]
	public Image cardImage;
	public TextMeshProUGUI description;
	public GameObject costSlot;
	public GameObject productionSlot;
	public event Action<Card> OnCardPickup;

	public void InitCardData(CardData data) {
		cardData = data;

		// Initialising the Card Cost.
		foreach(Resource resource in cardData.cost) {
			GameObject resourceIcon = Instantiate(resourcePrefab[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			resourceIcon.transform.SetParent(costSlot.transform);
		}

		// Initialising the Card Production per turn.
		foreach(Resource resource in cardData.production) {
			GameObject resourceIcon = Instantiate(resourcePrefab[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			resourceIcon.transform.SetParent(productionSlot.transform);
		}

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
