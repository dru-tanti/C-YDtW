using UnityEngine;

[CreateAssetMenu(fileName = "Climate Card", menuName = "C!YDyW/Card", order = 0)]
public class ClimateEffect : ScriptableObject {
	public Resource[] cost;
	public int duration;
	public int turnsLeft => duration;
	public int damage;
}
