using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[SerializeField]
public class Card_Simple : Card
{
    public GameObject img_thumbnail;

    public TMP_Text txt_HP;
    public TMP_Text txt_Ability;

    public void Set() {
        img_thumbnail.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite(image);
        txt_HP.SetText(current_HP.ToString());
        txt_Ability.SetText(Ability.ToString());
    }

    public Card_Simple(string n) : base(n) {
        Set();
    }

}
