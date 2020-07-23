/*
 * FileName             : NpcChatState.cs
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
    /// NPC与玩家交谈时的State，StateID为ChatState
    /// </summary>
    public class NpcChatState : FSMState
    {
        private NpcView mNpcView;

        public NpcChatState(FSMSystem fsm, NpcView npcView) : base(fsm)
        {
            StateID = StateID.ChatState;
            this.mNpcView = npcView;
        }

        public override void StateAction(GameObject[] gameObjects)
        {

        }

        public override void Reason(GameObject[] gameObjects)
        {
            if (!mNpcView.IsTalking) //如果此NPC不在谈话中
            {
                if (mNpcView.IsStaticNPC) //如果是静止不动的NPC
                {
                    this.Fsm.PerformTransition(Transition.ReadytoIdle); //进入NpcIdleState
                }
                else //如果是巡逻的NPC
                {
                    this.Fsm.PerformTransition(Transition.ReadytoPatrol); //进入NpcPatrolState
                }
            }
        }
    }
}
