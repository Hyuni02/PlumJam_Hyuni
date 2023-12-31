using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]
public class Card_Simple : MonoBehaviour
{
    public Card c;

    public GameObject img_thumbnail;

    public TMP_Text txt_HP;
    public TMP_Text txt_Ability;
    public Button btn_onclick;
    public void Set(Card card) {
        c = card;
        img_thumbnail.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite(card.image);
        txt_HP.SetText(card.current_HP.ToString());
        txt_Ability.SetText(card.Ability.ToString());
    }

}
