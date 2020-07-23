/*
 * FileName             : AOIInfo.cs
 * Author               : ZSZ
 * Creat Date           : 2018.10.7
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
    /// AOI的信息
    /// </summary>
    public class AOIInfoModel
    {
        /// <summary> 
        /// 所有格子 all grids
        /// </summary> 
        public Dictionary<string, AOIGrid> AllGrids;

        /// <summary> 
        /// 所有的实体玩家
        /// </summary> 
        public List<Entity> AllEntity;

        /// <summary> 
        /// 控制的玩家 the player of control
        /// </summary> 
        public Entity PlayerEntity;

        /// <summary> 
        /// 视野范围 单位是unity里的单位 player field (real unit in unity)
        /// </summary> 
        public static float FIELD = 30;

        public AOIInfoModel()
        {
            AllGrids = new Dictionary<string, AOIGrid>(100);
            AllEntity = new List<Entity>();
        }
    }
}
