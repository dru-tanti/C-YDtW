using GlobalEnums;
using UnityEngine;
using AYellowpaper.SerializedCollections;
[CreateAssetMenu(fileName = "GameState", menuName = "C!YDyW/Game State", order = 0)]
public class GameState : ScriptableObject {
	// public Resource[] resources;
	[SerializedDictionary("Resource Type", "Value")]
	public SerializedDictionary<ResourceType, int> resources;
	public bool isPlayerTurn = true;
	public int currentLevel = 1;
	public int turnCounter = 1;
	public int turnLimit = 10;
	public int doomMeter = 0;

	public void AddResources(Resource[] resources) {
		foreach(Resource resource in resources) {
			this.resources[resource.type] += resource.value;
		}
	}

	public void DecreaseResources(Resource[] resources) {
		foreach(Resource resource in resources) {
			this.resources[resource.type] -= resource.value;
		}
	}

	public void ResetResources(SerializedDictionary<ResourceType, int> startingResources) {
		foreach(ResourceType type in startingResources.Keys) {
			resources[type] = startingResources[type];
		}
	}
}
