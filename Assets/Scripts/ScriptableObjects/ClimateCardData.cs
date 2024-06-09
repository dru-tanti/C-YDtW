using GlobalEnums;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "Climate Card", menuName = "C!YDyW/Card/Climate Card", order = 0)]
public class ClimateCardData : ScriptableObject {
    public Sprite image;
    public string description;
	public Resource[] costOnPlay;
	public Resource[] costPerTurn;
	[SerializedDictionary("Resource Type", "Below"), Tooltip("Cards that produce an amount below this value produce nothing.")]
	public SerializedDictionary<ResourceType, int> preventProduction;
	public int maxDuration;
	public int damage;
}
