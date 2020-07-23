/*
 * FileName             : SkillRootNode.cs
 * Author               : yqs
 * Creat Date           : 2018.9.30
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
    /// 技能树的根节点，不存储具体技能信息，此节点的ID即为技能树的ID
    /// </summary>
    public class SkillRootNode : SkillNode
    {
        public bool Loop;
        public string Name;
        public SkillRootNode(string id) : base(id)
        {
            this.NextUse = 0;
        }

        public override SkillNodeState Use(Entity skillUser)
        {
            SkillNodeState currentNodeState = SkillNodeState.FailToRun;
            for (int i = 0; i < ChildSkillNodes.Count; i++)
            {
                SkillNodeState childTreeState = ChildSkillNodes[NextUse].Use(skillUser);
                if (childTreeState == SkillNodeState.FailToRun)         //如果本次执行的子技能节点无法释放
                {
                    NextUse = (NextUse + 1) % ChildSkillNodes.Count;    //下一次进入时执行下一个子技能节点
                }

                if (childTreeState == SkillNodeState.Running)           //如果本次执行的子技能节点正在释放中
                {
                    currentNodeState = SkillNodeState.Running;          //当前节点的子节点还在执行
                    break;                                              //跳出循环
                }

                if (childTreeState == SkillNodeState.Finish)            //如果本次执行的子技能节点全部释放完毕，也就是说这个节点下没有可以执行的技能了
                {
                    NextUse = (NextUse + 1) % ChildSkillNodes.Count;    //下一次进入时执行下一个子技能节点
                    currentNodeState = SkillNodeState.Running;          //当前节点是正在运行的
                    break;                                              //跳出循环
                }
            }
            return currentNodeState;
        }
    }
}