using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ScriptableObject
{
    [Header("For the Blacklist menu")]
    [Tooltip("Sprite used in Blacklist")]
    public Sprite MonsterPic;
    [Tooltip("Sprite used in Blacklist")]
    public Sprite LinkerPic;
    [Tooltip("Name of the enemy monster")]
    public string MonsterName;

    [Header("In-fight values")]
    [Tooltip("Enemys max HP")]
    public int MaxHitPoints;
    [Tooltip("Enemys max Input Slots")]
    public int MaxInputSlots = 5;
    [Tooltip("Modifier for the dmg; 0.5 => 50% more dmg ")]
    public int DmgModifier;
    [Tooltip("How many RP enemy gains for every hit it takes ")]
    public int RPgainperHit;

    [Header("FAs enemy can use")]
    [Tooltip("FAs the enemy can use")]
    public List<FeralArt> FAs;
}
