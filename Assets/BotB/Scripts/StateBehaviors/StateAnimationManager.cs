using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimationManager : StateMachineBehaviour
{

    // This class allows us to take a data-driven approach to affecting animation state machine parameter changes through
    // gameplay state machine states.


    [System.Serializable]
    public struct BotBAnimationStateTriggers {
        public bool Dash_A;
        public bool Punch_A;
        public bool Punch_B;
        public bool Punch_C;
        public bool Punch_D;
        public bool Kick_A;
        public bool HitReact_A;
        public bool HitReact_B;
        public bool HitReact_GuardBlock;

    }

    private Animator animStateMachine;
    public BotBAnimationStateTriggers animTriggers;
    public bool locomotionIsAllowed;
    private PlayerController playerController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get a reference to the animation state machine
        playerController = animator.gameObject.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        animStateMachine = playerController.getStateMachine(true);
        
        if(animTriggers.Dash_A)
            animStateMachine.SetTrigger("Dash_A");
        if (animTriggers.Punch_A)
            animStateMachine.SetTrigger("Punch_A");
        if (animTriggers.Punch_B)
            animStateMachine.SetTrigger("Punch_B");
        if (animTriggers.Punch_C)
            animStateMachine.SetTrigger("Punch_C");
        if (animTriggers.Punch_D)
            animStateMachine.SetTrigger("Punch_D");

        if (animTriggers.Kick_A)
            animStateMachine.SetTrigger("Kick_A");
        if (animTriggers.HitReact_A)
            animStateMachine.SetTrigger("HitReact_A");
        if (animTriggers.HitReact_B)
            animStateMachine.SetTrigger("HitReact_B");
        if (animTriggers.HitReact_GuardBlock)
            animStateMachine.SetTrigger("HitReact_GuardBlock");
        if (animTriggers.HitReact_GuardBlock)
            animStateMachine.SetTrigger("HitReact_GuardBlock");

        if (locomotionIsAllowed)
        {
            updateLocomotion();
        }
        else {
            animStateMachine.SetFloat("Movement_Forward", 0f);
            animStateMachine.SetFloat("Movement_Right", 0f);
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (locomotionIsAllowed)
        {
            updateLocomotion();
        }
    }

    private void updateLocomotion() {
        animStateMachine.SetFloat("Movement_Forward", playerController.getMovementForward());
        animStateMachine.SetFloat("Movement_Right", playerController.getMovementRight());

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
