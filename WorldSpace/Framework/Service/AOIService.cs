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

using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// AOIService，管理AOI信息
    /// </summary>
    public class AOIService
    {
        [Inject]
        public AOIInfoModel AOIInfoModel { get; set; }

        /// <summary>
        /// 刷新视野 refresh aoi
        /// </summary>
        /// <param name = "transform" ></param >
        public void RefreshAoi(Transform transform)
        {
            Entity entity = null;
            for (int i = 0; i < AOIInfoModel.AllEntity.Count; i++)
            {
                if (AOIInfoModel.AllEntity[i].Transform.Equals(transform))
                {
                    entity = AOIInfoModel.AllEntity[i];
                    break;
                }
            }

            if (entity == null)
            {
                entity = new Entity { Transform = transform };
                AOIInfoModel.AllEntity.Add(entity);
            }

            string key = GetGridKeyFromTransform(entity.Transform);         //需要更新的物体在AOI的Grid中所在的位置
            if (entity.GridKey != key)                                      //如果这个物体的位置发生了改变
            {
                if (AOIInfoModel.AllGrids.ContainsKey(entity.GridKey))      //如果这个物体已经在AOI的Grid里
                {
                    AOIInfoModel.AllGrids[entity.GridKey].OutGrid(entity);  //将这个物体移出原来的Grid
                }

                if (!AOIInfoModel.AllGrids.ContainsKey(key))                //如果场景中没有当前坐标对应的Grid
                {
                    AOIInfoModel.AllGrids.Add(key, new AOIGrid(key, AOIInfoModel.AllGrids));    //添加之
                }

                AOIInfoModel.AllGrids[key].EnterGrid(entity);               //有个物体进入了坐标为key的Grid
            }
        }

        public List<GameObject> GetInterestGameObjects(Transform transform)
        {
            string key = GetGridKeyFromTransform(transform);
            List<GameObject> interestGameObjects = new List<GameObject>();
            if (AOIInfoModel.AllGrids.ContainsKey(key))
            {
                foreach (Entity interestEntity in AOIInfoModel.AllGrids[key].InterestMeList)
                {
                    interestGameObjects.Add(interestEntity.Transform.gameObject);
                }
            }
            return interestGameObjects;
        }

        public Entity GetEntity(Transform transform)
        {
            string key = GetGridKeyFromTransform(transform);
            foreach (Entity entity in AOIInfoModel.AllGrids[key].ExistEntityList)
            {
                if (entity.Transform.Equals(transform))
                {
                    return entity;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取此物体所在的Grid的Key
        /// </summary>
        /// <param name="transform"></param>
        /// <returns>此物体的GridKey</returns>
        private string GetGridKeyFromTransform(Transform transform)
        {
            int x;
            int z;
            float space = AOIInfoModel.FIELD / 2;
            if (transform.position.x >= 0)
            {
                x = (int)((transform.position.x + space) / AOIInfoModel.FIELD);
            }
            else
            {
                x = (int)((transform.position.x - space) / AOIInfoModel.FIELD);
            }
            if (transform.position.z >= 0)
            {
                z = (int)((transform.position.z + space) / AOIInfoModel.FIELD);
            }
            else
            {
                z = (int)((transform.position.z - space) / AOIInfoModel.FIELD);
            }

            return string.Format("{0},{1}", x, z);                    //需要更新的物体在AOI的Grid中所在的位置
        }
    }
}
