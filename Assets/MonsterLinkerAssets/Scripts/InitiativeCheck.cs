using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeCheck : MonoBehaviour
{
    public List<BaseAttack> curPlayerInput;
    public List<BaseAttack> curEnemyInput;

    public int PlayerSpeed;
    public int EnemySpeed;

    public ArenaUIHandler arenaui;
    public TurnChanger turnchanger;

    [SerializeField] float ShowIniCheckSecs;

    public void GetSpeedValues()
    {
        foreach (BaseAttack attack in curPlayerInput)
        {
            PlayerSpeed += attack.Speed;
        }

        foreach (BaseAttack attack in curEnemyInput)
        {
            EnemySpeed += attack.Speed;
        }

        if (GameStateSwitch.Instance.implanthandler.Unleashed == eUnleashedMode.active)
        {
            PlayerSpeed = 99;
        }

        print("playerspeed: " + PlayerSpeed + "\n enemyspeed: " + EnemySpeed);
    }

    //Ini Arrow got turned around due to player misunderstandings
    //e = player turn; p = enemy turn
    public IEnumerator UMIni()
    {
        arenaui.SetSpeedValues(EnemySpeed, PlayerSpeed);
        arenaui.UM_Text.SetActive(true);
        arenaui.SetIniArrow("e");
        yield return new WaitForSeconds(ShowIniCheckSecs);
        arenaui.UM_Text.SetActive(false);
        StartCoroutine(turnchanger.SwitchTurn(eTurn.PlayerFirst));
        GameStateSwitch.Instance.animationhandler.MoveToMiddle();
    }

    public IEnumerator CompareSpeed()
    {
        print("comparing speeds");
        if (PlayerSpeed > EnemySpeed)
        {
            print("players turn");
            arenaui.SetSpeedValues(EnemySpeed, PlayerSpeed);
            arenaui.IniBG.SetActive(true);
            arenaui.SetIniArrow("e");
            yield return new WaitForSeconds(ShowIniCheckSecs);
            StartCoroutine(turnchanger.SwitchTurn(eTurn.PlayerFirst));
        }
        else if (EnemySpeed > PlayerSpeed)
        {
            print("enemys turn");
            arenaui.SetSpeedValues(EnemySpeed, PlayerSpeed);
            arenaui.IniBG.SetActive(true);
            arenaui.SetIniArrow("p");
            yield return new WaitForSeconds(ShowIniCheckSecs);
            StartCoroutine(turnchanger.SwitchTurn(eTurn.EnemyFirst));
        }
        else
        {
            print("players turn");
            arenaui.SetSpeedValues(EnemySpeed - 1, PlayerSpeed);
            arenaui.IniBG.SetActive(true);
            arenaui.SetIniArrow("e");
            yield return new WaitForSeconds(ShowIniCheckSecs);
            StartCoroutine(turnchanger.SwitchTurn(eTurn.PlayerFirst));
        }

        arenaui.IniBG.SetActive(false);        
        GameStateSwitch.Instance.animationhandler.MoveToMiddle();
    }

    public void ResetSpeedValues()
    {
        PlayerSpeed = 0;
        EnemySpeed = 0;
    }
}
