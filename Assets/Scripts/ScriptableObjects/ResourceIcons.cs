using GlobalEnums;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "ResourceIcons", menuName = "C!YDyW/Resource Icons", order = 0)]
public class ResourceIcons : ScriptableObject {
	[SerializedDictionary("Resource", "Prefab")]
	public SerializedDictionary<ResourceType, GameObject> icon;
}
