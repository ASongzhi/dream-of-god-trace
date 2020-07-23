/*
 * FileName             : MYXZAssetBundleManager.cs
 * Author               : yqs
 * Creat Date           : 2018.9.18
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 *  思路（极简GC）：当加载AssetBundle ab时，获取它的所有依赖包集合list，并与ab一并加入mAllAssetBundles中，然后对于list中的每个AssetBundle的使用
 * 记录+1，当卸载一个AssetBundl ab时，获取它的所有依赖包集合list，将用户指定要卸载的ab直接卸载，list的使用记录全部-1，
 * 如果这是某个包的使用记录降为0了，那么就将这个包卸载
 */
namespace MYXZ
{
    /// <summary>
    /// 管理所有AssetBundle
    /// </summary>
    public class MYXZAssetBundleManager
    {
        #region Singleton
        public static MYXZAssetBundleManager Instance
        {
            get { return mInstance = mInstance ?? new MYXZAssetBundleManager(); }
        }

        private static MYXZAssetBundleManager mInstance;

        private MYXZAssetBundleManager()
        {
            AssetBundle streamingAsset = AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/StreamingAssets");
            mStreamingAssetManifest = streamingAsset.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            mAllAssetBundles.Add("StreamingAssets", streamingAsset);
        }
        #endregion

        /// <summary>
        /// 记录所有AssetBundle的依赖和路径
        /// </summary>
        private AssetBundleManifest mStreamingAssetManifest;
        private Dictionary<string, AssetBundle> mAllAssetBundles = new Dictionary<string, AssetBundle>();
        /// <summary>
        /// 记录被依赖的AssetBundle引用次数
        /// </summary>
        private Dictionary<string, int> mDependenciesUse = new Dictionary<string, int>();

        /// <summary>
        /// 加载路径为path的AssetBundle，如果已加载，直接返回
        /// </summary>
        /// <param name="path">AssetBundle的路径</param>
        /// <returns>目标AssetBundle</returns>
        public AssetBundle LoadOrGetAssetBundle(string path)
        {
            if (!mAllAssetBundles.ContainsKey(path))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(System.IO.Path.Combine(Application.streamingAssetsPath, path));
                mAllAssetBundles.Add(path, ab);
                string[] allDependencies = mStreamingAssetManifest.GetAllDependencies(path);     //获取所有依赖包名
                foreach (string dependency in allDependencies)                                  //加载所有依赖包 
                {
                    if (!mAllAssetBundles.ContainsKey(dependency))
                    {
                        mAllAssetBundles.Add(dependency, AssetBundle.LoadFromFile(System.IO.Path.Combine(Application.streamingAssetsPath, dependency)));
                    }
                    if (!mDependenciesUse.ContainsKey(dependency))  //如果此前没有AssetBundle引用了这个依赖，添加它，否则，引用次数加1
                    {
                        mDependenciesUse.Add(dependency, 1);
                    }
                    else
                    {
                        mDependenciesUse[dependency]++;
                    }
                }
                return ab;
            }
            return mAllAssetBundles[path];
        }

        /// <summary>
        /// 卸载路径为path的AssetBundle
        /// </summary>
        /// <param name="path">要卸载的AssetBundle的路径</param>
        /// <param name="unloadAllLoadedObjects">是否卸载已经加载的物体</param>
        public void Unload(string path, bool unloadAllLoadedObjects = true)
        {
            if (mAllAssetBundles.ContainsKey(path))
            {
                mAllAssetBundles[path].Unload(unloadAllLoadedObjects);      //卸载目标AssetBundle
                mAllAssetBundles.Remove(path);
                string[] dependencies = mStreamingAssetManifest.GetAllDependencies(path);    //获取卸载目标的依赖
                foreach (string dependency in dependencies)
                {
                    if (mDependenciesUse.ContainsKey(dependency))
                    {
                        mDependenciesUse[dependency]--;         //此依赖的引用次数减1
                        if (mDependenciesUse[dependency] == 0)  //如果此依赖没有被引用了，卸载它
                        {
                            mAllAssetBundles[dependency].Unload(true);
                            mAllAssetBundles.Remove(dependency);
                        }
                    }
                }
            }
        }
    }
}