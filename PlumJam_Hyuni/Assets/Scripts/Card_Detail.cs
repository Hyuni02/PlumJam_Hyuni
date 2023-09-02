using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
[SerializeField]
public class Card_Detail : Card {
    public TMP_Text txt_name;
    public TMP_Text txt_description;

    public GameObject img_thumbnail;

    public TMP_Text txt_HP;
    public TMP_Text txt_Ability;

    public void Set() {
        txt_name.SetText(Name);
        txt_description.SetText(Description);
        img_thumbnail.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite(image);
        txt_HP.SetText(current_HP.ToString());
        txt_Ability.SetText(Ability.ToString());
    }

    public Card_Detail(string n) : base(n) {
        Set();
    }

}