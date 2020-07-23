/*
 * FileName             : JumpState.cs
 * Author               : hy
 * Creat Date           : 2018.9.8
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
    /// Player行走的State，StateID为JumpState
    /// </summary>
    public class JumpState : FSMState
    {
        /// <summary>
        /// 移动方向
        /// </summary>
        private Vector3 mDirection;

        /// <summary>
        /// 通过CharacterController来控制跳跃
        /// </summary>
        private CharacterController mPlayerController;

        /// <summary>
        /// 跳跃阶段（二段跳）
        /// </summary>
        private int mJumpStep;

        /// <summary>
        /// 用于获取Player当前的信息
        /// </summary>
        private BaseCharacter mCharacter;


        public JumpState(FSMSystem fsm, BaseCharacter character) : base(fsm)
        {
            StateID = StateID.JumpState;
            this.mCharacter = character;
            mPlayerController = fsm.Owner.GetComponent<CharacterController>();
        }

        public override void DoBeforeEntering()
        {
            this.mCharacter.Animator.SetBool("jump", true);
            mJumpStep = 1;
        }

        public override void StateAction(GameObject[] gameObjects)
        {
            if (mJumpStep == 1)
            {
                mDirection.y = mCharacter.JumpSpeed;
                mJumpStep++;
            }
            mDirection.y += Physics.gravity.y * Time.fixedDeltaTime;
            mPlayerController.Move(mDirection * Time.fixedDeltaTime); //跳跃
        }

        public override void DoAfterLeaving()
        {
            mJumpStep = 1;
        }
        public override void Reason(GameObject[] gameObjects)
        {
            if (IsGrounded(this.mPlayerController.transform)) //如果当前没有在空中
            {
                this.mCharacter.Direction = Vector3.zero;
                this.mCharacter.Animator.SetBool("jump", false);
                Fsm.PerformTransition(Transition.ReadytoIdle); //进入WalkState
            }
        }
        bool IsGrounded(Transform transform)
        {
            //这里transform.position 一般在物体的中间位置，注意根据需要修改margin的值
            return Physics.Raycast(transform.position, -transform.up, 0.3f);
        }
    }
}
