/*
 * FileName             : IdleState.cs
 * Author               : hy
 * Creat Date           : 2018.2.20
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using UnityEngine;
using System.Collections;

namespace MYXZ
{
    /// <summary>
    /// Player静止的State，StateID为IdleState
    /// </summary>
    public class IdleState : FSMState
    {
        private BaseCharacter mCharacter;

        public IdleState(FSMSystem fsm, BaseCharacter character) : base(fsm)
        {
            StateID = StateID.IdleState;
            this.mCharacter = character;
        }

        public override void StateAction(GameObject[] gameObjects)
        {
        }

        public override void Reason(GameObject[] gameObjects)
        {
            if (MYXZInputManager.Instance.GetKey(KeyCode.A) || MYXZInputManager.Instance.GetKey(KeyCode.W)
                || MYXZInputManager.Instance.GetKey(KeyCode.S) || MYXZInputManager.Instance.GetKey(KeyCode.D)) //如果玩家按下了移动键
            {
                this.Fsm.PerformTransition(Transition.ReadytoWalk); //进入WalkState
            }
            if (mCharacter.IsTalking)       //如果玩家正在谈话
            {
                this.Fsm.PerformTransition(Transition.ReadytoChat); //进入ChatState
            }
            if (MYXZInputManager.Instance.GetKeyDown(KeyCode.Space))
            {
                Fsm.PerformTransition(Transition.ReadytoJump); //进入JumpState
            }
           
        }
    }
}
