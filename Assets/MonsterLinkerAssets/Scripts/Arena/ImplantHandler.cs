using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplantHandler : MonoBehaviour
{
    [Header("For Rising Rage")]
    [Tooltip("Save of the RP count at the start of each attack round")]
    public float PlayerRPatAttackStart;
    [Tooltip("% more BaseDmg for one RP")]
    public float OneRP_Multiplier;

    [Header("For Unleashed Mode")]
    public bool UMactivated;
    public int UMrounds;
    public float UM_oneTimeHeal_Value;
    public float UM_DMGDealt_Multiplier;
    public float UM_DMGTaken_Reduction;

    [Header("For Super Feral Art")]
    public bool SFAused;

    [Header("For Temporary Input Slot")]
    public bool QTEmissed;

    //check which implant is currently on
    //check if SFA or UM have been used
    //check the rounds UM has been active
    //check modifiers of the implants
    //one dmg modifier in baeffects only, it is set by the current implant?    

    public void ResetCounters()
    {
        PlayerRPatAttackStart = 0f;
        UMactivated = false;
        UMrounds = 0;
        SFAused = false;
        QTEmissed = false;
    }

    public float RPMultiplier()
    {
        float value = (OneRP_Multiplier * PlayerRPatAttackStart);
        return value;
    }



}
