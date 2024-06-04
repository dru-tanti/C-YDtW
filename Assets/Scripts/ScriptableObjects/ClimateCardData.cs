using GlobalEnums;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "Climate Card", menuName = "C!YDyW/Card/Climate Card", order = 0)]
public class ClimateCardData : ScriptableObject {
    public Sprite image;
    public string description;
	public Resource[] cost;
	
	// [Range(0f, 100f)]
	public SerializedDictionary<ResourceType, int> icon;

	public int duration;
	public int maxDuration => duration;
	public int damage;
}
