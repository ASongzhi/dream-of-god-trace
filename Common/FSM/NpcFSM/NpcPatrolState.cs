/*
 * FileName             : NpcPatrolState.cs
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
    /// NPC巡逻的State，StateID为PatrolState
    /// </summary>
    public class NpcPatrolState : FSMState
    {
        private NpcView mNpcView;

        /// <summary>
        /// 目标巡逻的点的索引
        /// </summary>
        private int mIndex;

        public NpcPatrolState(FSMSystem fsm, NpcView npcView) : base(fsm)
        {
            StateID = StateID.PatrolState;
            this.mNpcView = npcView;
        }

        public override void StateAction(GameObject[] gameObjects)
        {
            this.Fsm.Owner.transform.LookAt(mNpcView.PatrolPoints[mIndex].position);   //转向目标巡逻点
            this.Fsm.Owner.transform.Translate(Vector3.forward * Time.deltaTime * mNpcView.MoveSpeed); //移动
            if (Vector3.Distance(this.Fsm.Owner.transform.position, mNpcView.PatrolPoints[mIndex].position) < 1)
            {
                mIndex++;
                mIndex %= mNpcView.PatrolPoints.Count;
            }
        }

        public override void Reason(GameObject[] gameObjects)
        {
            if (mNpcView.IsTalking) //如果NPC正在谈话
            {
                Fsm.PerformTransition(Transition.ReadytoChat); //进入NpcChatState
            }
        }
    }
}