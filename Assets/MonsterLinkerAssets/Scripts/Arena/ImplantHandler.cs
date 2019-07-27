using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplantHandler : MonoBehaviour
{
    [Header("For Rising Rage")]
    public float PlayerRPatAttackStart;
    [Tooltip("% more BaseDmg for one RP")]
    public float OneRP_Multiplier;

    [Header("For Unleashed Mode")]
    public bool UMactivated;

    [Header("For Super Feral Art")]
    public bool SFAused;

    [Header("For Temporary Input Slot")]
    public bool QTEmissed;

    //check which implant is currently on
    //check if SFA or UM have been used
    //check the rounds UM has been active
    //check modifiers of the implants
    //one dmg modifier in baeffects only, it is set by the current implant?



}
