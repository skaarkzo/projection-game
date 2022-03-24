using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class RollTest : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanMainController>();

        if (player != null)
        {
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
            player.RollingComplete();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = animator.gameObject.transform.parent.GetComponent<AyanMainController>();

        if (player != null)
        {
            player.controller.height = 2f;
            player.controller.center = new Vector3(0, 1, 0);
            MainController.lookDirectionLock = false;
        }
    }


}
