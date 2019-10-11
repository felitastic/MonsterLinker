using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAEffectsHandler : MonoBehaviour
{
    public float maxPlayerHP;
    public float curPlayerHP;
    public float curPlayerRP;

    public float maxEnemyHP;
    public float curEnemyHP;
    public float curEnemyRP;

    public float curDMG;
    public int RPgained;

    public bool deathCheck = false;

    [Header("At the end of one attack round")]
    public float TotalDmgTaken;
    public float TotalDmgDealt;

    [HideInInspector] public Attack curAttack;

    [HideInInspector] public ArenaUIHandler arenaui;
    [HideInInspector] public StatusBarHandler enemystatusbar;
    [HideInInspector] public StatusBarHandler playerstatusbar;
    [HideInInspector] public ImplantHandler implanthandler;

    [Header("Set by QTE Handler")]
    [Tooltip("Influenced by the QTE result")]
    public float QTEResultModifier;
    [Tooltip("Influenced by the Endurance QTE result")]
    public float EnduranceModifier;
    [Tooltip("Influenced by Implant choice")]
    public float ImplantModifier;
    [Tooltip("Damage modifier of the current enemy")]
    public float EnemyDMGModifier;
    [Tooltip("Set by the implant Unleashed Mode")]
    public float UM_DefenseBuff;
    [Tooltip("How many RP enemy gains for every hit taken")]
    public int EnemyRPgain;

    public void StartHpandRPValues(float playerHP, int playerRP, float enemyHP, int enemyRP, int enemyRPgain, float enemyDMGModifier)
    {
        maxPlayerHP = playerHP;
        curPlayerHP = playerHP;
        curPlayerRP = playerRP;

        maxEnemyHP = enemyHP;
        curEnemyHP = enemyHP;
        curEnemyRP = enemyRP;
        EnemyRPgain = enemyRPgain;
        EnemyDMGModifier = enemyDMGModifier;
    }

    public void SetImplantModifier(float implantModifier)
    {
        ImplantModifier = implantModifier;
        print("ImplantModifier: " + ImplantModifier);
    }

    public void SetEnduranceModifier(int mashCount)
    {
        EnduranceModifier = (float)mashCount / 100.0f;
        print("EnduranceModifier: " + EnduranceModifier);
    }

    public void SetFA_QTEResultModifier(float dmgModifier)
    {
        QTEResultModifier += dmgModifier;
        print("QTEResultModifier: " + QTEResultModifier);
    }

    public void SetQTEResultModifier(float dmgModifier, int RPgain)
    {
        QTEResultModifier = dmgModifier;
        RPgained = curAttack.RPGain + RPgain;
        print("QTEResultModifier: " + QTEResultModifier);
    }

    public void CalculatePlayerBaseDmg()
    {
        float tempBaseDMG1 = curAttack.DMG + (curAttack.DMG * ImplantModifier);               
        float tempBaseDMG2 = tempBaseDMG1 + (tempBaseDMG1 * EnduranceModifier);
        curDMG = tempBaseDMG2 + (tempBaseDMG2 * QTEResultModifier);
        //print("player tempBaseDMG1: " +tempBaseDMG1);
        //print("player tempBaseDMG2: " + tempBaseDMG2);
        print("player curDMG: " + curDMG);
    }

    public void CalculateEnemyBaseDmg()
    {
        float tempBaseDMG = curAttack.DMG + (curAttack.DMG * EnemyDMGModifier);
        float tempBaseDMG2 = tempBaseDMG - (tempBaseDMG * QTEResultModifier);
        curDMG = tempBaseDMG2 - (tempBaseDMG2 * UM_DefenseBuff);
        //print("enemy tempBaseDMG: " + tempBaseDMG);
        print("enemy curDMG: " +tempBaseDMG+  "- (" +tempBaseDMG+" x "+QTEResultModifier+" = " + curDMG);
    }

    public void DealDMG()
    {
        switch (GameStateSwitch.Instance.GameState)
        {
            case eGameState.QTEAttack:
                CalculatePlayerBaseDmg();
                EnemyTakesDmg();
                PlayerPaysRP();

                break;
            case eGameState.QTEBlock:
                CalculateEnemyBaseDmg();
                PlayerTakesDmg();
                EnemyPaysRP();

                break;
        }

        HPandRPClamp();
    }

    public void HealPlayer(float curHeal)
    {
        curPlayerHP += curHeal;
        HPandRPClamp();
    }

    public void PlayerPaysRP()
    {
        print("cur attack " + curAttack.name + " costs " + curAttack.RPCost);
        curPlayerRP -= curAttack.RPCost;
    }

    public void EnemyPaysRP()
    {
        curEnemyRP -= curAttack.RPCost;
        enemystatusbar.RPTick(Mathf.RoundToInt(curEnemyRP));
    }

    public void PlayerTakesDmg()
    {
        //print("dealing dmg to player");
        curPlayerHP -= curDMG;
        curEnemyHP += (maxEnemyHP * (curAttack.HPGain/100));
        curPlayerRP += RPgained;
        curEnemyRP += curAttack.RPGain;
        TotalDmgTaken += curDMG;
    }

    public void EnemyTakesDmg()
    {
        curEnemyHP -= curDMG;
        curPlayerHP += (maxPlayerHP * (curAttack.HPGain/100));
        curEnemyRP += EnemyRPgain;
        curPlayerRP += RPgained;
        TotalDmgDealt += curDMG;
    }

    //Make sure HP and RP cannot go over their max value
    public void HPandRPClamp()
    {
        if (Mathf.Round(curEnemyHP) >= Mathf.Round(maxEnemyHP))
            curEnemyHP = Mathf.Round(maxEnemyHP);

        if (Mathf.Round(curPlayerHP) >= Mathf.Round(maxPlayerHP))
            curPlayerHP = Mathf.Round(maxPlayerHP);

        if (Mathf.RoundToInt(curEnemyRP) >= (int)100)
            curEnemyRP = 100.0f;
        else if (Mathf.RoundToInt(curEnemyRP) < (int)0)
            curEnemyRP = 0.0f;

        if (Mathf.RoundToInt(curPlayerRP) >= (int)100)
            curPlayerRP = 100.0f;
        else if (Mathf.RoundToInt(curPlayerRP) < (int)0)
            curEnemyRP = 0.0f;

        UpdateHPandRPCounter();
        UpdateHPandRPbars();

    }

    public void UpdateHPandRPCounter()
    {
        int curEnemyHP_percent = Mathf.RoundToInt(((curEnemyHP/maxEnemyHP) *100));
        int curPlayerHP_percent = Mathf.RoundToInt(((curPlayerHP/maxPlayerHP) *100));

        arenaui.SetPlayerHPandRP(curPlayerHP_percent, Mathf.RoundToInt(curPlayerRP));
        arenaui.SetEnemyHPandRP(curEnemyHP_percent, Mathf.RoundToInt(curEnemyRP));
    }

    public void UpdateHPandRPbars()
    {
        playerstatusbar.HPTick(Mathf.Round(curPlayerHP));
        playerstatusbar.RPTick(Mathf.RoundToInt(curPlayerRP));
        enemystatusbar.HPTick(Mathf.Round(curEnemyHP));
        enemystatusbar.RPTick(Mathf.RoundToInt(curEnemyRP));

        if (deathCheck)
        {
            CheckForDeath();
        }
    }

    public void ShowTotalDmg(float totaldmg)
    {
        print("total damage this round: " + totaldmg);
    }

    public void ResetDmgCount()
    {
        TotalDmgTaken = 0f;
        TotalDmgDealt = 0f;

        EnduranceModifier = 0f;
        ImplantModifier = 0f;
        QTEResultModifier = 0f;
    }

    public void LerpHP()
    {
        switch (GameStateSwitch.Instance.GameState)
        {
            case eGameState.QTEAttack:
                enemystatusbar.HPLerp();
                break;
            case eGameState.QTEBlock:
                playerstatusbar.HPLerp();
                break;
            default:
                print("no state found, not lerpin any HP");
                break;
        }
    }

    public void CheckForDeath()
    {
        switch (GameStateSwitch.Instance.GameState)
        {
            case eGameState.QTEAttack:                
                if (Mathf.RoundToInt(curEnemyHP) <= (int)0)
                {
                    StartCoroutine(GameStateSwitch.Instance.animationhandler.DeathFlag(true));
                    StartCoroutine(ShowResult(eFightResult.Victory));
                }
                else
                {
                    GameStateSwitch.Instance.FightResult = eFightResult.None;
                }
                break;
            case eGameState.QTEBlock:
                if (Mathf.RoundToInt(curPlayerHP) <= (int)0)
                {
                    StartCoroutine(GameStateSwitch.Instance.animationhandler.DeathFlag(false));
                    StartCoroutine(ShowResult(eFightResult.Defeat));
                }
                else
                {
                    GameStateSwitch.Instance.FightResult = eFightResult.None;
                }
                break;
            default:
                break;
        }
    }

    public IEnumerator ShowResult(eFightResult fightresult)
    {
        float DeathTime = 1.5f;
        GameStateSwitch.Instance.FightResult = fightresult;
        yield return new WaitForSeconds(DeathTime);
        GameStateSwitch.Instance.SwitchState(eGameState.Result);
        print("fight state: " + GameStateSwitch.Instance.FightResult);
    }
}