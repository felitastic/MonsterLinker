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
        float fallspeed = 0.3f;

        float firstlerp = 0.5f;
        float secondlerp = 0.7f;
        float thirdlerp = 1.5f;

        float firstwait = 0.2f;
        float secondwait = 0.8f;
        float thirdwait = 1f;
        float forthwait = 0.6f;

        if (playerwins)
        {
            EnemyAnim.SetBool("death", true);
            PlayerAnim.SetFloat("speed", 0.01f);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.windeath);
            GameStateSwitch.Instance.cameramovement.StartLerp(firstlerp);
            yield return new WaitForSeconds(firstwait);
            EnemyAnim.SetFloat("speed", fallspeed);
            yield return new WaitForSeconds(secondwait);
            EnemyAnim.SetFloat("speed", 1f);
            yield return new WaitForSeconds(thirdwait);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.resultwinturn);
            GameStateSwitch.Instance.cameramovement.StartLerp(secondlerp);
            yield return new WaitForSeconds(forthwait);
            PlayerAnim.SetTrigger("victory");
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.resultwinzoom);
            GameStateSwitch.Instance.cameramovement.StartLerp(thirdlerp);

        }
        else
        {
            PlayerAnim.SetBool("death", true);
            PlayerAnim.SetFloat("speed", 0.01f);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.lossdeath);
            GameStateSwitch.Instance.cameramovement.StartLerp(firstlerp);
            yield return new WaitForSeconds(firstwait);
            PlayerAnim.SetFloat("speed", fallspeed);
            yield return new WaitForSeconds(secondwait);
            PlayerAnim.SetFloat("speed", 1f);
            yield return new WaitForSeconds(thirdwait);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.resultlossturn);
            GameStateSwitch.Instance.cameramovement.StartLerp(secondlerp);
            yield return new WaitForSeconds(forthwait);
            EnemyAnim.SetTrigger("victory");
            yield return new WaitForSeconds(1f);
            GameStateSwitch.Instance.cameramovement.SetPositions(eCamPosition.resultlosszoom);
            GameStateSwitch.Instance.cameramovement.StartLerp(thirdlerp);
        }        
    }

    public void ResetToIdle()
    {
        EnemyAnim.SetBool("block", false);
        PlayerAnim.SetBool("block", false);
    }
}
