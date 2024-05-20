using GlobalEnums;
using UnityEngine;
using AYellowpaper.SerializedCollections;
[CreateAssetMenu(fileName = "LevelSettings", menuName = "C!YDyW/Level Settings", order = 0)]
public class LevelSettings : ScriptableObject {
	[Tooltip("How many card slots will be available in game.")]
	public int maxCardSlots = 12;
	[Tooltip("How many card slots will be available for climate effects in game.")]
	public int maxClimateEffects = 6;
	[Tooltip("How many cards will the player start with.")]
	public int startingHand;
	[Header("Resources")]
	// public Resource[] resources;
	[SerializedDictionary("Resource Type", "Value")]
	public SerializedDictionary<ResourceType, int> startingResources;
	[SerializedDictionary("Resource Type", "Value")]
	public SerializedDictionary<ResourceType, int> targetResources;
	[Header("Turn Rules")]
	[Tooltip("Value the Doom Meter needs to exceed until the First Point of No Return is triggered")]
	public int firstPNR;
	[Tooltip("Value the Doom Meter needs to exceed until the Second Point of No Return is triggered")]
	public int secondPNR;
	[Tooltip("Value the Doom Meter needs to exceed until the Third Point of No Return is triggered")]
	public int thirdPNR;
	[Tooltip("Value the Doom Meter needs to exceed to trigger a Game Over")]
	public int armageddon;
}
