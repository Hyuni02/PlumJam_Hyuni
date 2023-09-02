using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class Card_Detail : MonoBehaviour {
    public TMP_Text txt_name;
    public TMP_Text txt_description;

    public Image img_thumbnail;

    public TMP_Text txt_HP;
    public TMP_Text txt_Ability;

    public void Set(Card card) {
        txt_name.SetText(card.Name);
        txt_description.SetText(card.Description);
        img_thumbnail.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite(card.image);
        txt_HP.SetText(card.current_HP.ToString());
        txt_Ability.SetText(card.Ability.ToString());
    }

    public void Start() {
        
    }
}