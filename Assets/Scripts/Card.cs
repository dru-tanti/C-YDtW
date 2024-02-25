using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using AYellowpaper.SerializedCollections;
using GlobalEnums;

public class Card : DraggableObject {
	public CardData cardData;
	public Image cardImage;
	public TextMeshProUGUI description;
	// public TextMeshProUGUI power;
	// public TextMeshProUGUI cost;

	public GameObject costSlot;
	public GameObject productionSlot;

	[SerializedDictionary("Resource", "Prefab")]
	public SerializedDictionary<ResourceType, GameObject> resourcePrefab;

	[SerializedDictionary("Resource", "Value")]
	public SerializedDictionary<ResourceType, int> cost;

	[SerializedDictionary("Resource", "Value")]
	public SerializedDictionary<ResourceType, int> production;

	public void InitCardData(CardData data) {
		cardData = data;

		// Retrieving the Child Objects.
		GameObject imageObject = transform.GetChild(0).gameObject;
		TextMeshProUGUI[] uiText = transform.GetComponentsInChildren<TextMeshProUGUI>();

		// Setting the reference to the UI Components on the card.
		cardImage	= imageObject.GetComponent<Image>();
		description = uiText[0];

		// Initialising the Card Cost.
		foreach(Resource resource in cardData.cost) {
			cost.Add(resource.type, resource.value);
			GameObject resourceIcon = Instantiate(resourcePrefab[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			resourceIcon.transform.SetParent(costSlot.transform);
		}

		foreach(Resource resource in cardData.production) {
			production.Add(resource.type, resource.value);
			GameObject resourceIcon = Instantiate(resourcePrefab[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			resourceIcon.transform.SetParent(productionSlot.transform);
		}

		// power		= uiText[1];
		// cost		= uiText[2];

		// Setting the Card Data.
		cardImage.sprite = cardData.image;
		description.SetText(cardData.description);
		// cost.SetText(cardData.cost[0].value.ToString());
		// power.SetText(cardData.production[0].value.ToString());
	}

	public override void OnBeginDrag(PointerEventData eventData) {
		base.OnBeginDrag(eventData);
		// cost.raycastTarget = false;
		// power.raycastTarget = false;
		description.raycastTarget = false;
	}

	public override void OnEndDrag(PointerEventData eventData) {
		base.OnEndDrag(eventData);
		// cost.raycastTarget = true;
		// power.raycastTarget = true;
		description.raycastTarget = true;
	}
}
