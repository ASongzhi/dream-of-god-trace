/*
 * FileName             : NpcView.cs
 * Author               : yqs
 * Creat Date           : 2018.2.12
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>     
    /// NPC的View
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class NpcView : View
    {
        public string ID;
        public string Name;
        public bool IsStaticNPC;
        [SerializeField]
        public List<Transform> PatrolPoints;
        public float MoveSpeed;

        public bool IsTalking;
        public Signal BeginTalkSignal = new Signal();
        public bl_HUDText HUDRoot;
        private FSMSystem mFsm;


        protected override void Start()
        {
            HUDRoot = GameObject.Find("HUDText").GetComponent<bl_HUDText>();
            HUDRoot.NewText("- " + Random.Range(50, 100).ToString(), base.transform, Color.red, 8, 20f, -1f, 2.2f, bl_Guidance.Static);
            HUDRoot.NewHealthyPoint(base.transform,200,8);
            HUDRoot.ChangeHPValue(HUDRoot.GetHealthyPoint(transform), 100, bl_HUDText.ValueType.damage);
            base.Start();
            InitFsm();
            
        }

        void FixedUpdate()
        {
            mFsm.UpdateFSM(null);
        }

        private void InitFsm()
        {
            mFsm = new FSMSystem(this.gameObject);

            if (IsStaticNPC)
            {
                NpcIdleState idleState = new NpcIdleState(mFsm, this);
                idleState.AddTransition(Transition.ReadytoChat, StateID.ChatState);

                NpcChatState chatState = new NpcChatState(mFsm, this);
                chatState.AddTransition(Transition.ReadytoIdle, StateID.IdleState);

                mFsm.AddStates(idleState, chatState);
            }
            else
            {
                NpcPatrolState patrolState = new NpcPatrolState(mFsm, this);
                patrolState.AddTransition(Transition.ReadytoChat, StateID.ChatState);

                NpcChatState chatState = new NpcChatState(mFsm, this);
                chatState.AddTransition(Transition.ReadytoPatrol, StateID.PatrolState);

                mFsm.AddStates(patrolState, chatState);
            }

        }
    }
}