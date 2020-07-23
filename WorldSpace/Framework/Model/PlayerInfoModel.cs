/*
 * FileName             : PlayerInfoModel.cs
 * Author               : yqs
 * Creat Date           : 2018.2.22
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

namespace MYXZ
{
    /// <summary>
    /// Player的信息
    /// </summary>
    [ProtoContract]
    public class PlayerInfoModel
    {
        /// <summary>
        /// Player已经完成的任务
        /// </summary>
        [ProtoMember(1)]
        public List<string> FinishTaskIds = new List<string>();     //TODO 这里是测试用例

        /// <summary>
        /// Player正在执行的任务
        /// </summary>
        [ProtoMember(2)]
        public List<string> CurrentTaskIds = new List<string>();    //TODO 这里是测试用例

        /// <summary>
        /// 当前玩家拥有的Item
        /// </summary>
        [ProtoMember(3)]
        public Dictionary<string, int> CurrentHasItems = new Dictionary<string, int>();//TODO 这里是测试用例

        //[ProtoMember(4)]
        //public List<string> CurrentHasEquipments = new List<string>() { };   //TODO 这里是测试用例

        [ProtoMember(4)]
        public List<string> CurrentHasEquipments = new List<string>() { };

        /// <summary>
        /// 当前人物的属性
        /// </summary>
        [ProtoMember(5)]
        public BaseCharacter.Info Info = new BaseCharacter.Info();  //TODO 这里是测试用例


        /// <summary>
        /// 当前人物的金币
        /// </summary>
        [ProtoMember(6)]
        public int Gold;

        /// <summary>
        /// 当前人物的银币
        /// </summary>
        [ProtoMember(7)]
        public int Silver;

        /// <summary>
        /// 当前人物的铜币
        /// </summary>
        [ProtoMember(8)]
        public int Copper;

        /// <summary>
        /// 当前人物的经验
        /// </summary>
        [ProtoMember(9)]
        public int Experience;

        public PlayerInfoModel()    //TODO 测试用
        {
            this.Info.HP = 200;
            this.Info.Level = 1;
            this.Info.PhysicalAttack = 20;
            this.Info.MagicAttack = 22;
            this.Info.PhysicalDefense = 10;
            this.Info.MagicDefense = 12;
            this.Experience = 0;
        }

        public void Init(SaveInfo save)
        {
            this.CurrentHasEquipments = save.PlayerInfoModel.CurrentHasEquipments;
            this.Info = save.PlayerInfoModel.Info;
            this.FinishTaskIds = save.PlayerInfoModel.FinishTaskIds;
            this.CurrentTaskIds = save.PlayerInfoModel.CurrentTaskIds;
            this.CurrentHasItems = save.PlayerInfoModel.CurrentHasItems;
            this.Gold = save.PlayerInfoModel.Gold;
            this.Silver = save.PlayerInfoModel.Silver;
            this.Copper = save.PlayerInfoModel.Copper;
            this.Experience = save.PlayerInfoModel.Experience;
        }
    }
}