/*
 * FileName             : FSMSystem.cs
 * Author               : yqs
 * Creat Date           : 2017.12.12
 * Revision History     : 
 *          R1: 
 *              修改作者：yqs
 *              修改日期：2017.12.14
 *              修改内容：添加public函数AddStates(params FSMState[] states)，将原函数AddState(FSMState state)改为private
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// FSM管理系统
    /// </summary>
    public class FSMSystem
    {
        /// <summary>
        /// 此FSM的拥有者
        /// </summary>
        public GameObject Owner { get; private set; }

        /// <summary>
        /// 状态ID到状态的字典
        /// </summary>
        private Dictionary<StateID, FSMState> mIDToState = new Dictionary<StateID, FSMState>();

        /// <summary>
        /// 当前状态ID
        /// </summary>
        public StateID CurrentStateID
        {
            get { return mCurrentState.ID; }
        }

        /// <summary>
        /// 当前状态
        /// </summary>
        private FSMState mCurrentState;

        public FSMSystem(GameObject owner)
        {
            this.Owner = owner;
        }

        /// <summary>
        /// 更新FSM，此方法需要在FixedUpdate中调用
        /// </summary>
        /// <param name="gameObjects"></param>
        public void UpdateFSM(GameObject[] gameObjects)
        {
            mCurrentState.StateAction(gameObjects);
            mCurrentState.Reason(gameObjects);
        }

        /// <summary>
        /// 向此FSMSystem中添加状态
        /// </summary>
        /// <param name="states"></param>
        public void AddStates(params FSMState[] states)
        {
            foreach (FSMState state in states)
            {
                AddState(state);
            }
        }

        /// <summary>
        /// 添加一个状态
        /// </summary>
        /// <param name="state"></param>
        private void AddState(FSMState state)
        {
            if (state == null)
            {
                Debug.LogError("无法添加一个空的状态");
                return;
            }
            if (mCurrentState == null) //如果当前状态为空，把添加的第一个映射作为初始状态
            {
                mCurrentState = state;
            }
            if (mIDToState.ContainsKey(state.ID))
            {
                Debug.LogError("已存在" + state.ID + "的映射");
                return;
            }
            mIDToState.Add(state.ID, state);
        }

        /// <summary>
        /// 通过状态ID来删除mState中的一个映射
        /// </summary>
        /// <param name="id"></param>
        public void DeleteState(StateID id)
        {
            if (id == StateID.NullStateID)
            {
                Debug.LogError("空状态" + id + "不可被删除");
            }
            if (!mIDToState.ContainsKey(id))
            {
                Debug.LogError(id + "不存在");
                return;
            }
            mIDToState.Remove(id);
        }

        /// <summary>
        /// 执行转换条件tran
        /// </summary>
        /// <param name="tran"></param>
        public void PerformTransition(Transition tran)
        {
            if (tran == Transition.NullTransition)
            {
                Debug.LogError("无法执行空的转换条件" + tran);
                return;
            }
            StateID id = mCurrentState.GetOutputState(tran); //获得由当前状态在tran条件下的转换结果ID
            if (id == StateID.NullStateID)
            {
                Debug.LogError("当前状态 " + CurrentStateID + " 无法发生由" + tran + "发生转换");
                return;
            }
            if (!mIDToState.ContainsKey(id))
            {
                Debug.LogError("当前状态机里没有状态" + id);
                return;
            }
            //更新当前状态
            FSMState state = mIDToState[id];
            mCurrentState.DoAfterLeaving();
            mCurrentState = state;
            mCurrentState.DoBeforeEntering();
        }
    }
}