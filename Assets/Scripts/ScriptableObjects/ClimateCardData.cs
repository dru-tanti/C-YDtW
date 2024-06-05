using GlobalEnums;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "Climate Card", menuName = "C!YDyW/Card/Climate Card", order = 0)]
public class ClimateCardData : ScriptableObject {
    public Sprite image;
    public string description;
	public Resource[] cost;
	public int maxDuration;
	public int damage;
}
