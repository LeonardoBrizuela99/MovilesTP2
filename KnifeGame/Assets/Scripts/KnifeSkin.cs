using UnityEngine;

[CreateAssetMenu(menuName = "KnifeGame/Knife Skin", fileName = "Skin_")]
public class KnifeSkin : ScriptableObject
{
    public string id = "default";       
    public string displayName = "Classic";
    [Min(0)] public int price = 0;      
    public Color color = Color.white;
    public Sprite sprite;              
}