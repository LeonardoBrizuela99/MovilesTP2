using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] TMP_Text applesText;
    [SerializeField] ShopItemUI[] items;

    private void Start()
    {
        foreach (ShopItemUI item in items) item.Init(this);
        RefreshAll();
    }

    public void OnItemClicked(KnifeSkin skin)
    {
        if (!SkinSave.IsOwned(skin))
        {
            if (SkinSave.Apples < skin.price) return;
            SkinSave.Apples -= skin.price;
            SkinSave.SetOwned(skin);
        }

        SkinSave.Select(skin);
        RefreshAll();
    }

    void RefreshAll()
    {
        applesText.text = SkinSave.Apples.ToString();
        foreach (ShopItemUI item in items) item.Refresh();
    }
}