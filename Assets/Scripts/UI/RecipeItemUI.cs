using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeItemUI : MonoBehaviour
{
    //[SerializeField] private GameObject line;
    [SerializeField] private Image itemImage;
    [SerializeField] private TMPro.TMP_Text itemAmount;

    private RecipeManager.RecipeItemInfo info;

    public void Initialize(RecipeManager.RecipeItemInfo info)
    {
        this.info = info;
        itemImage.sprite = info.item.so.AliveSprite;
        itemAmount.text = "X" + info.AmountNeeded;
    }

    private void Update()
    {
        if (info == null)
            return;

        if(!info.IsDone)
        {
            itemAmount.text = "X" + info.AmountNeeded;
        }
        else
        {
            itemAmount.text = "OK";
        }
    }
}
