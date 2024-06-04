using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClimateCard : MonoBehaviour
{
    public ClimateCardData cardData;
	public ResourceIcons resourceIcons;

	[Header("UI Elements")]
	public Image cardImage;
	public TextMeshProUGUI description;
	public GameObject costSlot;
	public GameObject effectTimer;

	public void InitCardData(ClimateCardData data) {
		cardData = data;

		// Initialising the Card Cost.
		foreach(Resource resource in cardData.cost) {
			GameObject resourceIcon = Instantiate(resourceIcons.icon[resource.type]);
			resourceIcon.GetComponentInChildren<TextMeshProUGUI>().SetText(resource.value.ToString());
			// resourceIcon.transform.SetParent(costSlot.transform);
		}

		cardImage.sprite = cardData.image;
		description.SetText(cardData.description);
	}
}
