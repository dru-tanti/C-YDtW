using GlobalEnums;
using UnityEngine;

[System.Serializable]
public struct Resource {
    public int value;
    public ResourceType type;
}

[CreateAssetMenu(fileName = "Card", menuName = "C!YDyW/Card/Resource Card", order = 0)]
public class ResourceCardData : ScriptableObject {
    public Sprite image;
    public string description;
    public Resource[] cost;
    public Resource[] production;
    public int buildTime;
    public int damage;
}
