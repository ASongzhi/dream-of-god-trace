/*
 * FileName             : Character.cs
 * Author               : yqs
 * Creat Date           : 2018.3.14
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProtoBuf;

namespace MYXZ
{
    /// <summary>
    /// 玩家控制的角色的信息
    /// </summary>
    [Serializable]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class BaseCharacter
    {
        [Serializable]
        [ProtoContract]
        public class Info 
        {
            [ProtoMember(1)]
            public int HP;

            [ProtoMember(2)]
            public int Level;

            [ProtoMember(3)]
            public int PhysicalAttack;

            [ProtoMember(4)]
            public int PhysicalDefense;

            [ProtoMember(5)]
            public int MagicAttack;

            [ProtoMember(6)]
            public int MagicDefense;

            public Equipment Weapon;
            public Equipment Hat;
            public Equipment Shoes;
            public Equipment Ornament1;
            public Equipment Clothes;
            public Equipment Ornament2;

            public static BaseCharacter.Info operator +(BaseCharacter.Info info, Equipment equipment)
            {
                if (equipment == null)
                {
                    return info;
                }
                info.HP += equipment.HP;
                info.MagicAttack += equipment.MagicAttack;
                info.MagicDefense += equipment.MagicDefense;
                info.PhysicalAttack += equipment.PhysicalAttack;
                info.PhysicalDefense += equipment.PhysicalDefense;
                return info;
            }

            public static BaseCharacter.Info operator -(BaseCharacter.Info info, Equipment equipment)
            {
                if (equipment == null)
                {
                    return info;
                }
                info.HP -= equipment.HP;
                info.MagicAttack -= equipment.MagicAttack;
                info.MagicDefense -= equipment.MagicDefense;
                info.PhysicalAttack -= equipment.PhysicalAttack;
                info.PhysicalDefense -= equipment.PhysicalDefense;
                return info;
            }
        }
        private GameObject mPlayer;
        private CharacterController mCharacterController;

        public BaseCharacter.Info CharaInfo;

        /// <summary>
        /// 固定运动方向，如重力方向
        /// </summary>
        private Vector3 mFixedDirection;
        /// <summary>
        /// 随角色状态变动的运动方向
        /// </summary>
        [HideInInspector]
        public Vector3 Direction;
        public float Mass;
        public float BaseSpeed;

        [HideInInspector]
        public float Rate = 1.0f;

        public float WalkSpeed
        {
            get { return BaseSpeed * Rate; }
        }

        public float RunSpeed
        {
            get { return BaseSpeed * 2 * Rate; }
        }

        public float JumpSpeed;
        [Tooltip("距离小于此时可以交谈")]
        public float TalkDistance;

        public bool IsTalking;

        public StateID CurrentState
        {
            get { return this.mFsm.CurrentStateID; }
        }

        public bool IsAttacked;

        public SkillBase CurrentSkill
        {
            get { return mCurrentSkill; }
        }
        private SkillBase mCurrentSkill;
        public Action UseSkill;
        private FSMSystem mFsm;
        public Animator Animator;

        /// <summary>
        /// 处于IsTalking状态时与玩家对话的NPC
        /// </summary>
        public NpcView TalkingNpc { get; set; }

        public void Init(GameObject player)
        {
            mFixedDirection = Physics.gravity;
            mPlayer = player;
            this.Animator = this.mPlayer.GetComponent<Animator>();
            this.mCharacterController = this.mPlayer.GetComponent<CharacterController>();
            InitFsm();
            this.mCurrentSkill = null;
        }

        private void InitFsm()
        {
            this.mFsm = new FSMSystem(mPlayer);
            IdleState idleState = new IdleState(mFsm, this);
            idleState.AddTransition(Transition.ReadytoWalk, StateID.WalkState);
            idleState.AddTransition(Transition.ReadytoChat, StateID.ChatState);
            idleState.AddTransition(Transition.ReadytoJump, StateID.JumpState);
            idleState.AddTransition(Transition.ReadytoRun, StateID.RunState);

            WalkState walkState = new WalkState(mFsm, this);
            walkState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);
            walkState.AddTransition(Transition.ReadytoChat, StateID.ChatState);
            walkState.AddTransition(Transition.ReadytoJump, StateID.JumpState);
            walkState.AddTransition(Transition.ReadytoRun, StateID.RunState);

            ChatState chatState = new ChatState(mFsm, this);
            chatState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);

            JumpState jumpState = new JumpState(mFsm, this);
            jumpState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);

            RunState runState = new RunState(mFsm, this);
            runState.AddTransition(Transition.ReadytoWalk, StateID.WalkState);
            runState.AddTransition(Transition.ReadytoJump, StateID.JumpState);


            mFsm.AddStates(idleState, walkState, chatState, jumpState, runState);
        }

        public void Update()
        {
            TalkToNPC();
        }

        public void FixedUpdate()
        {
            this.mFsm.UpdateFSM(null);
            this.mCharacterController.Move((Direction + mFixedDirection) * Time.fixedDeltaTime);
        }

        public void SetCurrentSkill(SkillBase skill)
        {
            mCurrentSkill = skill;
            if (skill != null)
            {
                UseSkill();
            }
        }

        /// <summary>
        /// 与NPC交谈
        /// </summary>
        private void TalkToNPC()
        {
            if (this.CurrentState == StateID.IdleState || this.CurrentState == StateID.WalkState)
            {
                if (MYXZInputManager.Instance.GetMouseButtonDown(0))
                {
                    if (!EventSystem.current.IsPointerOverGameObject()) //如果此时点击的不是UI
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(ray, out hitInfo, this.TalkDistance)) //从鼠标向前发射一条长度为TalkDistance的射线
                        {
                            if (hitInfo.transform.CompareTag("NPC")) //如果碰到了NPC
                            {
                                this.TalkingNpc = hitInfo.transform.GetComponent<NpcView>();
                                this.TalkingNpc.BeginTalkSignal.Dispatch(); //与此NPC进行交流
                                this.IsTalking = true; //Player进入交谈状态
                            }
                        }
                    }
                }
            }
        }
    }
}