using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    [SerializeField]
    public List<Card> cards = new List<Card>();

    private void Start() {
        Card baek = new Card("�����");
        cards.Add(baek);
        Card hyunji = new Card("������");
        cards.Add(hyunji);
    }
}
