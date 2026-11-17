using UnityEngine;

[CreateAssetMenu(menuName = "KnifeGame/Level", fileName = "Level_")]
public class LevelData : ScriptableObject
{
    [Min(1)] public int knivesToThrow = 6;

   
    public RotationStep[] rotation = { new RotationStep { speed = 120f, duration = 3f } };

    [Range(0, 8)] public int preStuckKnives = 0;
    [Range(0, 6)] public int apples = 1;

    public bool isBoss = false;
    public Sprite logSprite;
}

[System.Serializable]
public struct RotationStep
{
    public float speed;
    public float duration;
}