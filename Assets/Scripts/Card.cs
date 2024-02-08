using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : DraggableObject {
	public CardData cardData;

	public Image cardImage;

	public TextMeshProUGUI description;

	public TextMeshProUGUI power;

	public TextMeshProUGUI cost;

	public void InitCardData(CardData data) {
		this.cardData = data;

		// Retrieving the Child Objects.
		GameObject imageObject = this.transform.GetChild(0).gameObject;
		TextMeshProUGUI[] uiText = this.transform.GetComponentsInChildren<TextMeshProUGUI>();

		// Setting the reference to the UI Components on the card.
		cardImage	= imageObject.GetComponent<Image>();
		description = uiText[0];
		power		= uiText[1];
		cost		= uiText[2];

		// Setting the Card Data.
		cardImage.sprite = cardData.image;
		description.SetText(cardData.description);
		cost.SetText(cardData.cost[0].value.ToString());
		power.SetText(cardData.production[0].value.ToString());
	}

	public CardData GetCardData() {
		return cardData;
	}

	public void OnCardPlay() {
		
	}
}
