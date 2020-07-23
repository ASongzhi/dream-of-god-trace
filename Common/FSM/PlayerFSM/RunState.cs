/*
 * FileName             : RunState.cs
 * Author               : hy
 * Creat Date           : 2018.9.10
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
    /// Player行走的State，StateID为RunState
    /// </summary>
    public class RunState : FSMState
    {
        /// <summary>
        /// 移动方向
        /// </summary>
        private Vector3 mDirection;

        private bool mIsRun;

        /// <summary>
        /// 用于获取Player当前的信息
        /// </summary>
        private BaseCharacter mCharacter;

        private int mLR = 0;    //左-1右1
        private int mFB = 0;    //前-1后1

        public RunState(FSMSystem fsm, BaseCharacter character) : base(fsm)
        {
            StateID = StateID.RunState;
            this.mCharacter = character;
        }

        public override void DoBeforeEntering()
        {
            mCharacter.Animator.SetBool("run", true);
        }

        public override void StateAction(GameObject[] gameObjects)
        {
            mIsRun = false;
            mLR = mFB = 0;
            mDirection = Vector3.zero;
            if (MYXZInputManager.Instance.GetKey(KeyCode.LeftShift))
            {
                if (MYXZInputManager.Instance.GetKey(KeyCode.W))
                {
                    mDirection += this.Fsm.Owner.transform.forward;
                    mIsRun = true;
                    mFB--;
                }
                if (MYXZInputManager.Instance.GetKey(KeyCode.A))
                {
                    mDirection -= this.Fsm.Owner.transform.right;
                    mIsRun = true;
                    mLR--;
                }
                if (MYXZInputManager.Instance.GetKey(KeyCode.S))
                {
                    mDirection -= this.Fsm.Owner.transform.forward;
                    mIsRun = true;
                    mFB++;
                }
                if (MYXZInputManager.Instance.GetKey(KeyCode.D))
                {
                    mDirection += this.Fsm.Owner.transform.right;
                    mIsRun = true;
                    mLR++;
                }
            }
         
            mCharacter.Animator.SetBool("run", true);
            this.mCharacter.Direction = this.mDirection * mCharacter.RunSpeed;
           
            mCharacter.Animator.SetInteger("LR", mLR);
            mCharacter.Animator.SetInteger("FB", mFB);
        }


        public override void Reason(GameObject[] gameObjects)
        {
            if (!mIsRun)   //如果当前没有在移动
            {
                Fsm.PerformTransition(Transition.ReadytoWalk); //进入IdleState
            }
            if (MYXZInputManager.Instance.GetKeyDown(KeyCode.Space))
            {
                Fsm.PerformTransition(Transition.ReadytoJump); //进入JumpState
            }
        }

        public override void DoAfterLeaving()
        {
            this.mCharacter.Animator.SetBool("run", false);
        }
    }
}
