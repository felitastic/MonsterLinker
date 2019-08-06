using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hyped_Animator : StateMachineBehaviour
{
    int anim1played = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        anim1played++;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(anim1played == 2)
        {
            animator.SetTrigger("play2");
            anim1played = 0;
        }

    }
}
