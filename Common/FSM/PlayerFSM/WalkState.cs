/*
 * FileName             : WalkState.cs
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
    /// Player行走的State，StateID为WalkState
    /// </summary>
    public class WalkState : FSMState
    {
        /// <summary>
        /// 移动方向
        /// </summary>
        private Vector3 mDirection;

        private bool mIsWalk;

        /// <summary>
        /// 用于获取Player当前的信息
        /// </summary>
        private BaseCharacter mCharacter;

        private int mLR = 0;    //左-1右1
        private int mFB = 0;    //前-1后1

        public WalkState(FSMSystem fsm, BaseCharacter character) : base(fsm)
        {
            StateID = StateID.WalkState;
            this.mCharacter = character;
        }

        public override void DoBeforeEntering()
        {
            mCharacter.Animator.SetBool("walk", true);
        }

        public override void StateAction(GameObject[] gameObjects)
        {
            mIsWalk = false;
            mLR = mFB = 0;
            mDirection = Vector3.zero;
            if (MYXZInputManager.Instance.GetKey(KeyCode.W))
            {
                mDirection += this.Fsm.Owner.transform.forward;
                mIsWalk = true;
                mFB--;
            }
            if (MYXZInputManager.Instance.GetKey(KeyCode.A))
            {
                mDirection -= this.Fsm.Owner.transform.right;
                mIsWalk = true;
                mLR--;
            }
            if (MYXZInputManager.Instance.GetKey(KeyCode.S))
            {
                mDirection -= this.Fsm.Owner.transform.forward;
                mIsWalk = true;
                mFB++;
            }
            if (MYXZInputManager.Instance.GetKey(KeyCode.D))
            {
                mDirection += this.Fsm.Owner.transform.right;
                mIsWalk = true;
                mLR++;
            }
            mCharacter.Animator.SetBool("walk", true);
            mCharacter.Animator.SetBool("run", false);
            this.mCharacter.Direction = this.mDirection * mCharacter.WalkSpeed;
            //mPlayerController.Move(mDirection * mCharacter.WalkSpeed * Time.fixedDeltaTime); //行走移动
            
            mCharacter.Animator.SetInteger("LR", mLR);
            mCharacter.Animator.SetInteger("FB", mFB);
        }


        public override void Reason(GameObject[] gameObjects)
        {
            if (!mIsWalk)   //如果当前没有在移动
            {
                Fsm.PerformTransition(Transition.ReadytoIdle); //进入IdleState
            }
            if (mCharacter.IsTalking) //如果Player当前正在交谈
            {
                Fsm.PerformTransition(Transition.ReadytoChat); //进入ChatState
            }
            if (MYXZInputManager.Instance.GetKeyDown(KeyCode.Space))
            {
                Fsm.PerformTransition(Transition.ReadytoJump); //进入JumpState
            }
            if (MYXZInputManager.Instance.GetKey(KeyCode.LeftShift))
            {
                Fsm.PerformTransition(Transition.ReadytoRun);//进入RunState
            }
        }

        public override void DoAfterLeaving()
        {
            this.mCharacter.Animator.SetBool("walk", false);
            this.mCharacter.Animator.SetBool("run", false);
            //this.mCharacter.Direction -= this.mDirection * mCharacter.WalkSpeed;
        }
    }
}
