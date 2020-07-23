/*
 * FileName             : FSMState.cs
 * Author               : yqs
 * Creat Date           : 2017.12.12
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
    /// FSM状态
    /// </summary>
    public abstract class FSMState
    {
        /// <summary>
        /// 此状态的ID
        /// </summary>
        protected StateID StateID;

        /// <summary>
        /// 此状态的ID
        /// </summary>
        public StateID ID
        {
            get { return StateID; }
        }

        /// <summary>
        /// 此State中的Transition条件下向StateID的转换结果
        /// </summary>
        protected Dictionary<Transition, StateID> Map = new Dictionary<Transition, StateID>();

        /// <summary>
        /// 此State所属于的FSM
        /// </summary>
        protected FSMSystem Fsm;

        public FSMState(FSMSystem fsm)
        {
            this.Fsm = fsm;
        }

        /// <summary>
        /// 向该State添加转换映射，即添加当前状态在tran条件下向id转换
        /// </summary>
        /// <param name="tran">转换条件</param>
        /// <param name="id">转换目标StateID</param>
        public void AddTransition(Transition tran, StateID id)
        {
            if (tran == Transition.NullTransition)
            {
                Debug.LogError("Transition.NullTransition");
                return;
            }
            if (id == StateID.NullStateID)
            {
                Debug.LogError("StateID.NullStateID");
                return;
            }
            if (Map.ContainsKey(tran))
            {
                Debug.LogError(tran + "has been existed");
                return;
            }
            Map.Add(tran, id);
        }

        /// <summary>
        /// 删除tran转换条件
        /// </summary>
        /// <param name="tran">转换条件</param>
        public void DeleteTransition(Transition tran)
        {
            if (tran == Transition.NullTransition)
            {
                Debug.LogError("Transition.NullTransition");
                return;
            }
            if (!Map.ContainsKey(tran))
            {
                Debug.LogError("Can not find " + tran);
                return;
            }
            Map.Remove(tran);
        }

        /// <summary>
        /// 比对Map中是否存在tran的映射，如果有返回tran对应的转化结果ID，如果不存在，返回StateID.NullStateID
        /// </summary>
        /// <param name="tran">转换条件</param>
        /// <returns>此State在tran条件下的转换结果，如果没有转换目标，返回StateID.NullStateID</returns>
        public StateID GetOutputState(Transition tran)
        {
            if (Map.ContainsKey(tran))
            {
                return Map[tran];
            }
            return StateID.NullStateID;
        }

        /// <summary>
        /// 进入此状态前需要做的事
        /// </summary>
        public virtual void DoBeforeEntering()
        {

        }

        /// <summary>
        /// 离开此状态时需要做的事
        /// </summary>
        public virtual void DoAfterLeaving()
        {

        }

        /// <summary>
        /// 当前状态正在做的事，在FSMSystem的UpdateFSM中调用
        /// </summary>
        /// <param name="gameObjects"></param>
        public abstract void StateAction(GameObject[] gameObjects);

        /// <summary>
        /// 状态转换判定，在FSMSystem的UpdateFSM中调用
        /// </summary>
        /// <param name="gameObjects"></param>
        public abstract void Reason(GameObject[] gameObjects);
    }
}