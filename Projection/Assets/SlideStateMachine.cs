using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideStateMachine : StateMachineBehaviour
{
    //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanTankController>();

        if (player != null)
        {
            player.controller.height = 0.5f;
            player.controller.radius = 0.8f;
            player.controller.center = new Vector3(0, 0.82f, 0);
            MainController.lookDirectionLock = true;
        }
    }

    //OnStateMove is called before OnStateMove is called on any state inside this state machine
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanTankController>();

        if (player != null)
        {
            player.DuringSlide();
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanTankController>();

        if (player != null)
        {
            player.controller.height = 2f;
            player.controller.radius = 0.3f;
            player.controller.center = new Vector3(0, 1, 0);
            MainController.lookDirectionLock = false;
        }
    }
}
