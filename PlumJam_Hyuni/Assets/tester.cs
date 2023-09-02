using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    [SerializeField]
    public List<Card> cards = new List<Card>();

    private void Start() {
        Card baek = new Card("백수진");
        cards.Add(baek);
        Card hyunji = new Card("김현지");
        cards.Add(hyunji);
    }
}
