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
        qtehandler.CallFAQTE("FA" + FAnumber);
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

    public void VFX_Swoosh(GameObject position)
    {

    }

    public void VFXImpact(int ImpactNo)
    {
        int Position = 0;

        if (GameStateSwitch.Instance.GameState == eGameState.QTEAttack)
        {
            //enemy spawn pos
            Position = (int)VFXController.Position.TestMiddle;
        }
        else
        {
            //player spawn pos
            Position = (int)VFXController.Position.TestMiddle;
        }

        //different vfx for different impacts
        switch (ImpactNo)
        {
            case 1:
                VFXController.Instance.SpawnEffectViaInt(VFXController.VFX.TestVFX, Position);
                break;
            case 2:
                VFXController.Instance.SpawnEffectViaInt(VFXController.VFX.TestVFX, Position);
                break;
            case 3:
                VFXController.Instance.SpawnEffectViaInt(VFXController.VFX.TestVFX, Position);
                break;
            default:
                print("impact vfx no not found: " + ImpactNo);
                VFXController.Instance.SpawnEffectViaInt(VFXController.VFX.TestVFX, Position);
                break;
        }
    }

    //Unleashed mode intro animation
    public void VFX_UMStart()
    {
        VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
    }

    //calls a light swoosh
    public void VFX_SwooshLight(int position)
    {
        VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
    }

    //calls a normal swoosh
    public void VFX_SwooshNormal(int position)
    {
        VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
    }

    //calls a heavy swoosh
    public void VFX_SwooshHeavy(int position)
    {
        VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
    }

    //calls a light swoosh
    public void SFX_SwooshLight()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.woosh_light);
    }

    //calls a normal swoosh
    public void SFX_SwooshNormal()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.woosh_normal);
    }

    //calls a heavy swoosh
    public void SVFX_SwooshHeavy()
    {
        SoundController.Instance.StartSFX(SoundController.SFX.woosh_heavy);
    }
   

    //calls hurt for the creature being hit
    public void HitImpact()
    {
        //print("impact, calling animation");
        animationhandler.HurtCheck();
    }

    //General screenshake
    public void ScreenShake(int ShakeNo)
    {
        float duration = Random.Range(0.15f, 0.25f);
        float magnitude = Random.Range(2.0f, 3.0f);

        switch (ShakeNo)
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
                duration = Random.Range(0.20f, 0.3f);
                magnitude = Random.Range(2.5f, 3.5f);
                break;
            default:
                print("shake #" + ShakeNo+" not found, using default");
                break;
        }
        StartCoroutine(GameStateSwitch.Instance.camshake.Shake(duration, magnitude));
    }

    //Screenshake depending on qte result
    public void QTEScreenShake()
    {
        float duration = Random.Range(0.15f, 0.25f);
        float magnitude = Random.Range(2.0f, 3.0f);

        switch (QTEAnimEvents.QTEZone)
        {
            case eQTEZone.None:                           
                break;
            case eQTEZone.Fail:
                duration = Random.Range(0.15f, 0.25f);
                magnitude = Random.Range(2.0f, 3.0f);
                break;
            case eQTEZone.Good:
                duration = Random.Range(0.17f, 0.27f);
                magnitude = Random.Range(2.25f, 3.25f);
                break;
            case eQTEZone.Perfect:
                duration = Random.Range(0.25f, 0.3f);
                magnitude = Random.Range(2.5f, 3.5f);
                break;
        }
        print("qte result:" + QTEAnimEvents.QTEZone);
        StartCoroutine(GameStateSwitch.Instance.camshake.Shake(duration, magnitude));
    }

    //Impact VFX depending on qte result
    public void QTE_ImpactVFX()
    {
        switch (QTEAnimEvents.QTEZone)
        {
            case eQTEZone.None:
                VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
                break;
            case eQTEZone.Fail:
                VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
                break;
            case eQTEZone.Good:
                VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
                break;
            case eQTEZone.Perfect:
                VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
                break;
        }
        print("qte result:" + QTEAnimEvents.QTEZone);
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
                print("sfx #" + ImpactNo + " not found, using normal");
                SoundController.Instance.StartSFX(SoundController.SFX.impact_normal);
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

    public void EnduranceStart()
    {
        StartCoroutine(qtehandler.ButtonMash());
    }


    public void SFXEndurance()
    {
        //SoundController.Instance.StartLoopingSFX(SoundController.SFX.powerCharge1, 1.0f);
    }


    public void EnduranceEnd()
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