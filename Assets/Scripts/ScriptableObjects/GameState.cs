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
	public int doomMeter = 0;

	public void Reset() {
		currentLevel = 1;
		turnCounter = 1;
		doomMeter = 0;
	}
}