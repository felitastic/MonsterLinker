using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All important vars
/// </summary>
[CreateAssetMenu(fileName = "new Profile", menuName = "Other/SaveProfile")]
public class Save : ScriptableObject
{
    [Tooltip("Is this a fresh save profile?")]
    public bool Empty;
    [Header("Player Information")]
    public string LinkerName;
    public string MonsterName;
    [Tooltip("Which arena has the player reached?")]
    public int Arena = 1;
    public eTutorial Tutorial = eTutorial.notstarted;

    [Header("Important ingame, dont touch")]
    public int maxBaseAttackInputSlots = 5;
    public List<FeralArt> FALoadout;
    public List<FeralArt> StartFAs;
    public Implant curImplant;
    public Implant defaultImplant;
    public int lowestFAcost;
    public int MaxHitPoints = 5000;
    [Tooltip("Should be 0 normally")]
    [SerializeField] public int curRP;

    public void SetCheapestFAcost()
    {
        Debug.Log("setting lowest FA cost in profile");
        lowestFAcost = 100;

        for (int i = 0; i < FALoadout.Count; i++)
        {
            if (lowestFAcost > FALoadout[i].RPCost)
            {
                lowestFAcost = FALoadout[i].RPCost;
            }
            else
            {
                continue;
            }
        }
    }

    public void ResetSave()
    {
        Empty = true;
        LinkerName = "";
        MonsterName = "Netherclaw";
        Arena = 1;
        Tutorial = eTutorial.notstarted;
        FALoadout = StartFAs;
        curImplant = defaultImplant;
        lowestFAcost = 0;
        MaxHitPoints = 5000;
    }
}
