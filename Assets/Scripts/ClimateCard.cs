using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClimateCard : MonoBehaviour
{
    public ClimateCardData cardData;
	public ResourceIcons resourceIcons;

	[Header("UI Elements")]
	public Image cardImage;
	public TextMeshProUGUI description;
	public TextMeshProUGUI effectTimer;
	public GameObject costSlot;
	public int timer;

	public void InitCardData(ClimateCardData data) {
		cardData = data;
		timer = cardData.maxDuration;

		// Initialising Resource Icons.
		foreach(Resource resource in cardData.cost) {
			GameObject resourceIcon = Instantiate(resourceIcons.icon[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			resourceIcon.transform.SetParent(costSlot.transform);
		}
		
		// Setting UI Elements
		cardImage.sprite = cardData.image;
		description.SetText(cardData.description);
		effectTimer.SetText(timer.ToString());
	}

	public void DecreaseTimer() {
		timer--;
		effectTimer.SetText(timer.ToString());
	}
}
