using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrowStateMachine : StateMachineBehaviour
{
    //OnStateMove is called before OnStateMove is called on any state inside this state machine
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanTankController>();

        if (player != null)
        {
            // Call the DuringThrow function from the AyanTankController class.
            player.DuringThrow();
        }
    }
}
