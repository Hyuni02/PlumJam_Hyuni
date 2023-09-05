using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    [SerializeField]
    public List<Card> cards = new List<Card>();

    public GameObject canvas;
    public GameObject CardDetail;
    public GameObject CardSimple;

    private void Start() {
        //Card hyunji = new Card("±èÇöÁö");
        //cards.Add(hyunji);

        //foreach(var card in cards) {
        //    GameObject newCard = Instantiate(CardDetail, canvas.transform);
        //    newCard.GetComponent<Card_Detail>().Set(card);
        //}
        //foreach(var card in cards) {
        //    GameObject newCard = Instantiate(CardSimple, canvas.transform);
        //    newCard.GetComponent<Card_Simple>().Set(card);
        //}
    }

    
}
