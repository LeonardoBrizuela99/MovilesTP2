using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] KnifeSkin skin;
    [SerializeField] Image preview;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text priceText;
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;

    public void Init(ShopManager shop)
    {
        nameText.text = skin.displayName;
        priceText.text = skin.price == 0 ? "Free" : $"{skin.price} apples";
        preview.color = skin.color;
        if (skin.sprite != null) preview.sprite = skin.sprite;

        button.onClick.AddListener(() => shop.OnItemClicked(skin));
    }

    public void Refresh()
    {
        if (SkinSave.GetSelected() == skin)
        {
            buttonText.text = "Using";
            button.interactable = false;
        }
        else if (SkinSave.IsOwned(skin))
        {
            buttonText.text = "use";
            button.interactable = true;
        }
        else
        {
            buttonText.text = "Buy";
            button.interactable = SkinSave.Apples >= skin.price;
        }
    }
}