using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowStateMachine : StateMachineBehaviour
{
    ////OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    var player = animator.gameObject.transform.parent.GetComponent<AyanTankController>();

    //    if (player != null)
    //    {
    //        player.controller.enabled = false;
    //    }
    //}

    ////OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    var player = animator.gameObject.transform.parent.GetComponent<AyanTankController>();

    //    if (player != null)
    //    {
    //        player.controller.enabled = true;
    //    }
    //}

    //OnStateMove is called before OnStateMove is called on any state inside this state machine
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanTankController>();

        if (player != null)
        {
            player.DuringThrow();
        }
    }
}
