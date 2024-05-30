using UnityEngine;

[CreateAssetMenu(fileName = "Climate Card", menuName = "C!YDyW/Card", order = 0)]
public class ClimateCardData : ScriptableObject {
	public Resource[] cost;
	public int duration;
	public int maxDuration => duration;
	public int damage;
}
