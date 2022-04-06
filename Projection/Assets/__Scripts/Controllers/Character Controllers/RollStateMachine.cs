using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RollStateMachine : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanMainController>();

        if (player != null)
        {
            // Change controller height when roll animation starts and lock the direction of player so direction is not changed while rolling.
            player.controller.height = 1f;
            player.controller.center = new Vector3(0, 0.5f, 0);
            MainController.lookDirectionLock = true;
        }
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanMainController>();

        if (player != null)
        {
            // Call the DuringRoll function from the AyanMainController script.
            player.DuringRoll();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanMainController>();

        if (player != null)
        {
            // Reset the controller and direction lock settings.
            player.controller.height = 2f;
            player.controller.center = new Vector3(0, 1, 0);
            MainController.lookDirectionLock = false;
        }
    }
}
