using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KnifeSkinApplier : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    private void Reset()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        KnifeSkin skin = SkinSave.GetSelected();
        if (skin == null) return;

        sr.color = skin.color;
        if (skin.sprite != null) sr.sprite = skin.sprite;
    }
}