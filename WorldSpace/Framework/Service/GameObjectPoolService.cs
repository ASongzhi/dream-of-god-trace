/*
 * FileName             : GameObjectPoolService.cs
 * Author               : yqs
 * Creat Date           : 2018.2.13
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
    /// 资源池的Service
    /// </summary>
    public class GameObjectPoolService
    {
        /// <summary>
        /// asset文件名
        /// </summary>
        public static string PoolConfigName = "PoolConfiguration";

        /// <summary>
        /// asset文件位于的Assetbundle
        /// </summary>
        private string POOL_CONFIGURATION_AB_PATH = "configuration/poolconfiguration.tables";

        private Dictionary<GameObjectPoolType, GameObjectPoolModel> mTypeToPoolDic;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            AssetBundle configAssetBundle = AssetBundle.LoadFromFile(POOL_CONFIGURATION_AB_PATH);
            GameObjectPoolList
                poolList = configAssetBundle.LoadAsset<GameObjectPoolList>(PoolConfigName); //将资源池的信息反序列化出来
            mTypeToPoolDic = new Dictionary<GameObjectPoolType, GameObjectPoolModel>();
            foreach (GameObjectPoolModel pool in poolList.PoolList) //按照配置文件创建各个资源池
            {
                mTypeToPoolDic.Add(pool.PoolType, pool);
            }
            configAssetBundle.Unload(true);
        }

        /// <summary>
        /// 从对应type的资源池中创建一个实例
        /// </summary>
        /// <param name="type">资源池的类型</param>
        /// <param name="name">要创建的物体的名字</param>
        /// <returns>创建的物体</returns>
        public GameObject GetInstance(GameObjectPoolType type, string name)
        {
            GameObjectPoolModel pool;
            mTypeToPoolDic.TryGetValue(type, out pool);
            if (pool != null)
            {
                return pool.GetInstance(name);
            }
            else
            {
                Debug.LogError("不存在" + type + "类型的GameObejctPool");
                return null;
            }
        }

        /// <summary>
        /// 卸载所有的pool
        /// </summary>
        public void UnLoadAllPools()
        {
            foreach (GameObjectPoolModel poolModel in mTypeToPoolDic.Values)
            {
                poolModel.UnLoadPool();
            }
            mTypeToPoolDic = null;
        }
    }
}