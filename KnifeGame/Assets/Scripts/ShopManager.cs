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
        if (!SaveSystem.IsOwned(skin))
        {
            if (SaveSystem.Apples < skin.price) return;
            SaveSystem.Apples -= skin.price;
            SaveSystem.SetOwned(skin);
        }

        SaveSystem.Select(skin);
        Debug.Log("Skin selected: " + skin.displayName);
        RefreshAll();
    }

    void RefreshAll()
    {
        applesText.text = SaveSystem.Apples.ToString();
        foreach (ShopItemUI item in items) item.Refresh();
    }
}