/*
 * FileName             : GameObjectPoolModel.cs
 * Author               : yqs
 * Creat Date           : 2018.2.13
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 资源池
    /// </summary>
    [Serializable]
    public class GameObjectPoolModel
    {
        public GameObjectPoolType PoolType;

        /// <summary>
        /// 这个资源池对应的Assetbundle的位置
        /// </summary>
        [SerializeField] private string m_TargetABPath;

        /// <summary>
        /// 资源池的容量
        /// </summary>
        [SerializeField] private int m_MaxCount;

        private AssetBundle mAssetBundle;

        /// <summary>
        /// 存储创建出来的物体
        /// </summary>
        private LinkedList<GameObject> mGameObjectList = new LinkedList<GameObject>();

        /// <summary>
        /// 从本资源池中创建一个实例
        /// </summary>
        /// <param name="name">要创建的物体的名字</param>
        /// <returns>创建的物体</returns>
        public GameObject GetInstance(string name)
        {
            if (mAssetBundle == null)
            {
                mAssetBundle = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(m_TargetABPath);
            }
            for (LinkedListNode<GameObject> go = mGameObjectList.First; go.Next != null; go = go.Next) //在已创建的物体中寻找
            {
                if (!go.Value.activeInHierarchy && go.Value.name.Equals(name)) //如果符合条件
                {
                    go.Value.SetActive(true);
                    return go.Value;
                }
            }
            if (mGameObjectList.Count >= m_MaxCount) //如果没有在资源池中找到符合条件的并且资源池已满
            {
                GameObject.Destroy(mGameObjectList.First.Value); //删除第一个物体
                mGameObjectList.RemoveFirst();
            }
            GameObject targetInstance = GameObject.Instantiate(mAssetBundle.LoadAsset<GameObject>(name));
            mGameObjectList.AddLast(targetInstance);
            return targetInstance;
        }

        public void UnLoadPool()
        {
            for (LinkedListNode<GameObject> go = mGameObjectList.First; go.Next != null; go = go.Next) //在已创建的物体中寻找
            {
                GameObject.Destroy(go.Value);
            }
            mAssetBundle.Unload(true);
        }
    }
}