/*
 * FileName             : ChatState.cs
 * Author               : hy
 * Creat Date           : 2018.2.20
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// Player交谈的State，StateID为ChatState
    /// </summary>
    public class ChatState : FSMState
    {
        private BaseCharacter mCharacter;

        public ChatState(FSMSystem fsm, BaseCharacter character) : base(fsm)
        {
            StateID = StateID.ChatState;
            this.mCharacter = character;
        }

        public override void DoBeforeEntering()
        {
            this.mCharacter.Animator.SetBool("talk", true);
        }

        public override void StateAction(GameObject[] gameObjects)
        {

        }

        public override void Reason(GameObject[] gameObjects)
        {
            if (!mCharacter.IsTalking) //如果玩家不在交谈状态，即谈话结束
            {
                this.Fsm.PerformTransition(Transition.ReadytoIdle); //进入IdleState
            }
        }

        public override void DoAfterLeaving()
        {
            this.mCharacter.Animator.SetBool("talk", false);
        }
    }
}