/*
 * FileName             : Grid.cs
 * Author               : zsz
 * Creat Date           : 2018.10.7
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// Grid类，管理格子信息
    /// </summary>
    public class AOIGrid
    {
        /// <summary>
        ///格子所属的总的列表
        /// </summary>
        private Dictionary<string, AOIGrid> mAllGrids;

        /// <summary>
        ///格子的key值
        /// </summary>
        private readonly string mGridKey;

        /// <summary>
        ///格子中拥有的实体的表
        /// </summary>
        public HashSet<Entity> ExistEntityList = new HashSet<Entity>();

        /// <summary>
        ///对该格子感兴趣的实体的表
        /// </summary>
        public HashSet<Entity> InterestMeList = new HashSet<Entity>();

        public AOIGrid(string key, Dictionary<string, AOIGrid> allGrids)
        {
            this.mGridKey = key;
            this.mAllGrids = allGrids;
        }

        /// <summary>
        /// 进入格子 enter some grid
        /// </summary>
        /// <param name="entity"></param>
        public void EnterGrid(Entity entity)
        {
            //如果添加格子里的玩家列表成功
            if (ExistEntityList.Add(entity))
            {
                //通知对这个格子感兴趣的人
                foreach (var item in InterestMeList)
                {
                    item.OnOtherEnterGrid(entity);
                }

                int x = 0;
                int z = 0;
                float space = AOIInfoModel.FIELD / 2;
                if (entity.Transform.transform.position.x >= 0)
                {
                    x = (int)((entity.Transform.transform.position.x + space) / AOIInfoModel.FIELD);
                }
                else
                {
                    x = (int)((entity.Transform.transform.position.x - space) / AOIInfoModel.FIELD);
                }
                if (entity.Transform.transform.position.z >= 0)
                {
                    z = (int)((entity.Transform.transform.position.z + space) / AOIInfoModel.FIELD);
                }
                else
                {
                    z = (int)((entity.Transform.transform.position.z - space) / AOIInfoModel.FIELD);
                }

                string key = string.Format("{0},{1}", x, z);

                //更新玩家格子信息 refresh entity grid info
                entity.GridKey = key;
                entity.X = x;
                entity.Z = z;

                //增加对周围9个格子的兴趣 add 9 grid around the sentity
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = z - 1; j <= z + 1; j++)
                    {
                        key = string.Format("{0},{1}", i, j);
                        if (!mAllGrids.ContainsKey(key))
                        {
                            mAllGrids.Add(key, new AOIGrid(key, mAllGrids));
                        }
                        mAllGrids[key].AddInterester(entity);
                    }
                }
                //检查不在感兴趣的格子 check no interest grid
                entity.LastInterestGridList.ExceptWith(entity.CurrentInterestGridList);
                foreach (var item in entity.LastInterestGridList)
                {
                    entity.OnNonInterestGrid(item);
                }

            }
        }

        /// <summary>
        /// 离开格子 out some grid
        /// </summary>
        /// <param name="entity"></param>
        public void OutGrid(Entity entity)
        {
            //如果成功从格子列表里删除 if success remove from exist entity list
            if (ExistEntityList.Remove(entity))
            {
                entity.GridKey = "";
                //清空上一次记录的感兴趣的人 clear last record interest entity
                entity.LastInterestGridList.Clear();

                foreach (var item in entity.CurrentInterestGridList)
                {
                    entity.LastInterestGridList.Add(item);
                    item.RemoveInterester(entity);
                }
                entity.CurrentInterestGridList.Clear();

                //通知感兴趣的人有人离开格子了 notify all interest player someone out grid
                foreach (var item in InterestMeList)
                {
                    item.OnOtherOutGrid(entity);
                }
            }
        }

        /// <summary>
        /// 增加对这个格子感兴趣的人 add someone interest this grid
        /// </summary>
        /// <param name="entity"></param>
        public void AddInterester(Entity entity)
        {
            //如果对这个格子感兴趣成功
            if (InterestMeList.Add(entity))
            {
                entity.OnInterestGrid(this);
            }
        }

        /// <summary>
        /// 移除对这个格子感兴趣的人 remove someone interest this grid
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveInterester(Entity entity)
        {
            if (InterestMeList.Remove(entity))
            {
                //发送该地图所有的玩家信息给这个感兴趣的人 notify all interest player the players in this grid is remove 
                foreach (var item in ExistEntityList)
                {
                    entity.OnOtherOutGrid(item);
                }
            }
            //如果没人感兴趣 则从格子列表中删除 if no one interest this grid, remove from all grids
            if (InterestMeList.Count == 0)
            {
                mAllGrids.Remove(mGridKey);
            }
        }
    }
}
