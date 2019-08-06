using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator EnemyAnim;
    public Animator PlayerAnim;
    
    
    public IEnumerator IdleOffset()
    {
        EnemyAnim.speed = 0f;
        yield return new WaitForSeconds(0.5f);
        EnemyAnim.speed = 1f;
    }
    
    //TODO set to um animation when its there
    public void PlayerUMActivation()
    {
        //PlayerAnim.SetTrigger("um");
    }

    public void EnduranceAnimStart()
    {
        PlayerAnim.SetBool("Endurance", true);
    }

    //TODO set effect
    public void EnduranceAnimEnd()
    {
        PlayerAnim.SetBool("Endurance", false);
        VFXController.Instance.SpawnEffect(VFXController.VFX.TestVFX, VFXController.Position.TestMiddle);
    }

    public void MoveToMiddle()
    {
        PlayerAnim.SetTrigger("walk");
        EnemyAnim.SetTrigger("walk");
    }

    public void JumpBack()
    {
        PlayerAnim.SetTrigger("jump");
        EnemyAnim.SetTrigger("jump");
    }

    public void PlayerAttack(string animString)
    {
        print("starting player attack: " + animString);
        PlayerAnim.SetTrigger(animString);
    }
    public void EnemyAttack(string animString)
    {
        print("starting enemy attack: " + animString);
        EnemyAnim.SetTrigger(animString);
    }

    public void HurtCheck()
    {
        switch(GameStateSwitch.Instance.GameState)
        {
            case eGameState.QTEAttack: 
                //print("enemy hurt");
                EnemyAnim.SetTrigger("hurt");
                break;
            case eGameState.QTEBlock:
                //print("player hurt");
                PlayerAnim.SetTrigger("hurt");
                break;
        }
    }

    public IEnumerator DeathFlag(bool playerwins)
    {
        if (playerwins)
        {
            EnemyAnim.SetBool("death", true);
            PlayerAnim.SetFloat("speed", 0.01f);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.windeath);
            GameStateSwitch.Instance.cameramovement.StartLerp(0.5f);
            yield return new WaitForSeconds(0.2f);
            EnemyAnim.SetFloat("speed", 0.5f);
            yield return new WaitForSeconds(0.7f);
            EnemyAnim.SetFloat("speed", 1f);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.resultwin);
            GameStateSwitch.Instance.cameramovement.StartLerp(0.4f);
            yield return new WaitForSeconds(0.5f);
            PlayerAnim.SetTrigger("victory");
        }
        else
        {
            PlayerAnim.SetBool("death", true);
            PlayerAnim.SetFloat("speed", 0.01f);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.lossdeath);
            GameStateSwitch.Instance.cameramovement.StartLerp(0.5f);
            yield return new WaitForSeconds(0.2f);
            PlayerAnim.SetFloat("speed", 0.5f);
            yield return new WaitForSeconds(0.7f);
            PlayerAnim.SetFloat("speed", 1f);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.resultloss);
            GameStateSwitch.Instance.cameramovement.StartLerp(0.4f);
            yield return new WaitForSeconds(0.5f);
            EnemyAnim.SetTrigger("victory");
        }        
    }

    public void ResetToIdle()
    {
        EnemyAnim.SetBool("block", false);
        PlayerAnim.SetBool("block", false);
    }
}
