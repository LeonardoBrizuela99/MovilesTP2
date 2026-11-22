using UnityEngine;

public static class SkinSave
{
    const string ApplesKey = "apples";        
    const string SelectedKey = "selectedSkin";
    const string OwnedPrefix = "owned_";

    static KnifeSkin[] allSkins;
    static KnifeSkin cachedSelected;

    public static KnifeSkin[] AllSkins
    {
        get
        {
            if (allSkins == null)
            {
                allSkins = Resources.LoadAll<KnifeSkin>("Skins");
                System.Array.Sort(allSkins, (a, b) => a.price.CompareTo(b.price));
            }
            return allSkins;
        }
    }

    public static int Apples
    {
        get => PlayerPrefs.GetInt(ApplesKey, 0);
        set { PlayerPrefs.SetInt(ApplesKey, value); PlayerPrefs.Save(); }
    }

    public static bool IsOwned(KnifeSkin skin) =>
        skin.price == 0 || PlayerPrefs.GetInt(OwnedPrefix + skin.id, 0) == 1;

    public static void SetOwned(KnifeSkin skin)
    {
        PlayerPrefs.SetInt(OwnedPrefix + skin.id, 1);
        PlayerPrefs.Save();
    }

    public static KnifeSkin GetSelected()
    {
        if (cachedSelected != null) return cachedSelected;

        string id = PlayerPrefs.GetString(SelectedKey, "");
        foreach (KnifeSkin s in AllSkins)
            if (s.id == id) { cachedSelected = s; return s; }

        cachedSelected = AllSkins.Length > 0 ? AllSkins[0] : null;
        return cachedSelected;
    }

    public static void Select(KnifeSkin skin)
    {
        PlayerPrefs.SetString(SelectedKey, skin.id);
        PlayerPrefs.Save();
        cachedSelected = skin;
    }
}