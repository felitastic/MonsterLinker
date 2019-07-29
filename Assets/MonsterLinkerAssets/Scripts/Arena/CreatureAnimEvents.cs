using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimEvents : MonoBehaviour
{
    public AnimationHandler animationhandler;
    public AttackRoundHandler attackroundhandler;
    public QTEHandler qtehandler;

    public int qteResult;

    //QTEStateSwitch(eQTEState QTEState)

    public void AttackAnimStart()
    {
        print("animation starts");
        if (qtehandler.curQTEType != eQTEType.FA)
        {
            StartCoroutine(qtehandler.WaitForStart());
        }
    }

    public void FAQTEcall(int FAnumber)
    {
        //qtehandler.AnimString = ("FA" + FAnumber);
        qtehandler.CallFAQTE("FA"+FAnumber);
        //qtehandler.WaitingTime = 0.0f;
        //StartCoroutine(qtehandler.WaitForStart());
    }

    public void FADmg()
    {
        //switch (GameStateSwitch.Instance.GameState)
        //{
        //    case eGameState.QTEAttack:
        //        GameStateSwitch.Instance.baeffectshandler.CalculatePlayerBaseDmg();

        //        break;
        //    case eGameState.QTEBlock:
        //        GameStateSwitch.Instance.baeffectshandler.CalculateEnemyBaseDmg();

        //        break;                
        //}
        GameStateSwitch.Instance.baeffectshandler.DealDMG();
    }

    public void BADmg()
    {
        GameStateSwitch.Instance.baeffectshandler.DealDMG();
    }

    public void VFXImpact()
    {

    }

    public void EnduranceStart()
    {
        print("start endurance button mash now");
        StartCoroutine(qtehandler.ButtonMash());
    }

    public void HitImpact()
    {
        //print("impact, calling animation");
        animationhandler.HurtCheck();
    }

    public void ScreenShake()
    {
        float duration = Random.Range(0.15f, 0.25f);
        float magnitude = Random.Range(2.0f, 3.0f);
        StartCoroutine(GameStateSwitch.Instance.camshake.Shake(duration, magnitude));
    }

    public void QTEScreenShake()
    {
        float duration = 0f;
        float magnitude = 0f;

        switch (qteResult)
        {
            //weakest
            case 1:
                duration = Random.Range(0.15f, 0.25f);
                magnitude = Random.Range(2.0f, 3.0f);
                break;
            case 2:
                 duration = Random.Range(0.17f, 0.27f);
                 magnitude = Random.Range(2.25f, 3.25f);
                break;
            //strongest
            case 3:
                 duration = Random.Range(0.25f, 0.3f);
                 magnitude = Random.Range(2.5f, 3.5f);
                break;
            default:
                break;
        }
        StartCoroutine(GameStateSwitch.Instance.camshake.Shake(duration, magnitude));
    }

    public void SFXImpact(int ImpactNo)
    {
        switch (ImpactNo)
        {
            case 1:
            SoundController.Instance.StartSFX(SoundController.SFX.impact_light);
                break;
            case 2:
            SoundController.Instance.StartSFX(SoundController.SFX.impact_normal);
                break;
            case 3:
            SoundController.Instance.StartSFX(SoundController.SFX.impact_heavy);
                break;
            default:
                break;
        }
    }

    //TODO placeholder for testing, delete later
    public void SFXNormal()
    { }
    public void SFXLight()
    { }
    public void SFXHeavy()
    { }

    public void SFXEndurance()
    {
        //SoundController.Instance.StartLoopingSFX(SoundController.SFX.powerCharge1, 1.0f);
    }

    public void EndEndurance()
    {
        //SoundController.Instance.StopLoopingSound()
    }

    public void AttackAnimEnd()
    {
        print("animation end");
        attackroundhandler.NextAttack();
        //animationhandler.ResetToIdle();
    }
}