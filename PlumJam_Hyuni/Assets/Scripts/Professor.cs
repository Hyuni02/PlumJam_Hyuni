using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Professor : Player
{
    [SerializeField]
    public List<Card> Assignments = new List<Card>();
}
