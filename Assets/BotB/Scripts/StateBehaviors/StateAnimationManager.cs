using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimationManager : StateMachineBehaviour
{
    [System.Serializable]
    public struct BotBAnimationStateTriggers {
        public bool Dash_A;
        public bool Punch_A;
        public bool Kick_A;
        public bool HitReact_A;
        public bool HitReact_B;
        public bool HitReact_GuardBlock;
    }

    public bool canDoLocomotion;

    public BotBAnimationStateTriggers animTriggers;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //get a reference to the animation state machine
        //animator = 
        /*
        if(animTriggers.Dash_A)
            animator.SetTrigger("Dash_A");
        if (animTriggers.Punch_A)
            animator.SetTrigger("Punch_A");
        if (animTriggers.Kick_A)
            animator.SetTrigger("Kick_A");
        if (animTriggers.HitReact_A)
            animator.SetTrigger("HitReact_A");
        if (animTriggers.HitReact_B)
            animator.SetTrigger("HitReact_B");
        if (animTriggers.HitReact_GuardBlock)
            animator.SetTrigger("HitReact_GuardBlock");
        
        //get locomotion info from higher up
       
        if (isInLocomotion) {
            animator.setFloat("Movement_Forward", movementForward);
            animator.setFloat("Movement_Right", movementRight);
        }
        */
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
