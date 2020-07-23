/*
 * FileName             : Entity.cs
 * Author               : zsz
 * Creat Date           : 2018.10.6
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
    public class Entity
    {
        /// <summary>
        /// 所在格子的key值
        /// </summary>
        public string GridKey = "";

        /// <summary>
        /// 实体的x坐标
        /// </summary>
        public int X;

        /// <summary>
        /// 实体的z坐标
        /// </summary>
        public int Z;

        /// <summary>
        /// 实体的transform
        /// </summary>
        public Transform Transform;

        /// <summary>
        /// 该实体曾经感兴趣的格子
        /// </summary>
        public HashSet<AOIGrid> LastInterestGridList = new HashSet<AOIGrid>();

        /// <summary>
        /// 该实体感兴趣的格子
        /// </summary>
        public HashSet<AOIGrid> CurrentInterestGridList = new HashSet<AOIGrid>();

        /// <summary>
        /// 该实体感兴趣的实体
        /// </summary>
        public HashSet<Entity> CurrentInterestEntityList = new HashSet<Entity>();

        /// <summary>
        /// 是否是主角 is player of control
        /// </summary>
        public bool IsPlayer;

        /// <summary>
        /// 当感兴趣的格子有玩家进入 when some one enter interest grid
        /// </summary>
        /// <param name="entity"></param>
        public void OnOtherEnterGrid(Entity entity)
        {
            if (entity != this)
            {
                CurrentInterestEntityList.Add(entity);
            }
        }

        /// <summary>
        /// 当感兴趣的格子有玩家离开 when some one out interest grid
        /// </summary>
        /// <param name="entity"></param>
        public void OnOtherOutGrid(Entity entity)
        {
            if (entity != this)
            {
                CurrentInterestEntityList.Remove(entity);
            }
        }

        /// <summary>
        /// 关注某格子 when interest some gird
        /// </summary>
        /// <param name="grid"></param>
        public void OnInterestGrid(AOIGrid grid)
        {
            CurrentInterestGridList.Add(grid);
            foreach (var item in grid.ExistEntityList)
            {
                CurrentInterestEntityList.Add(item);
            }
        }

        /// <summary>
        /// 不再关注某格子 when no interest some grid
        /// </summary>
        /// <param name="grid"></param>
        public void OnNonInterestGrid(AOIGrid grid)
        {
            CurrentInterestGridList.Remove(grid);
            CurrentInterestEntityList.ExceptWith(grid.ExistEntityList);
        }
    }
}