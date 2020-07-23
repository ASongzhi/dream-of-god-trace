/*
 * FileName             : MYXZGameDataManager.cs
 * Author               : yqs
 * Creat Date           : 2018.3.15
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 用于获取游戏中的所有Config配置信息及Sprite
    /// </summary>
    public class MYXZGameDataManager
    {
        #region Singleton
        public static MYXZGameDataManager Instance
        {
            get { return mInstance = mInstance ?? new MYXZGameDataManager(); }
        }

        private static MYXZGameDataManager mInstance;

        private MYXZGameDataManager()
        {
        }
        #endregion

        private Dictionary<string, SceneInfo> mIdToSceneInfoDic;
        private Dictionary<string, NpcInfo> mIdToNpcInfoDic;
        private Dictionary<string, Task> mIdToTaskDic;
        private Dictionary<string, Item> mIdToItemDic;
        private Dictionary<string, Sprite> mIdToSpriteDic;

        #region InitMembers

        public SaveInfo CurrentSaveInfo;
        /// <summary>
        /// 对游戏数据做初始化
        /// </summary>
        public void Init()
        {
            mIdToNpcInfoDic = new Dictionary<string, NpcInfo>();
            mIdToTaskDic = new Dictionary<string, Task>();
            mIdToItemDic = new Dictionary<string, Item>();
            mIdToSpriteDic = new Dictionary<string, Sprite>();
        }

        private void InitSceneInfo()
        {
            mIdToSceneInfoDic = new Dictionary<string, SceneInfo>();
            AssetBundle sceneInfoAB = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("Scene"));
            TextAsset[] sceneInfoTexts = sceneInfoAB.LoadAllAssets<TextAsset>();
            foreach (TextAsset text in sceneInfoTexts)
            {
                SceneInfo sceneInfo;
                MYXZXmlReader.ReadSceneInfo(out sceneInfo, text.text);
                mIdToSceneInfoDic.Add(sceneInfo.Id, sceneInfo);
            }
            MYXZAssetBundleManager.Instance.Unload(sceneInfoAB.name);
        }
        #endregion

        /// <summary>
        /// 通过ID获取Item或者装备
        /// </summary>
        /// <param name="id">Item的ID</param>
        /// <returns>对应的Item，没找到会返回null</returns>
        public Item GetItemOrEquipmentById(string itemId)
        {
            Item item;
            if (!this.mIdToItemDic.TryGetValue(itemId, out item))
            {
                if (MYXZXmlReader.ReadItemInfo(out item, itemId))
                {
                    this.mIdToItemDic.Add(itemId, item);
                }
                else
                {
                    Debug.LogError("读取ID为" + itemId + "的Item信息时发生错误");
                }
            }
            return item;
        }

        /// <summary>
        /// 通过ID获取NpcInfo
        /// </summary>
        /// <param name="npcId">npc的ID</param>
        /// <returns>对应的NpcInfo，没找到会返回null</returns>
        public NpcInfo GetNpcInfoById(string npcId)
        {
            NpcInfo info;
            if (!this.mIdToNpcInfoDic.TryGetValue(npcId, out info))
            {
                if (MYXZXmlReader.ReadNpcInfo(out info, npcId))
                {
                    this.mIdToNpcInfoDic.Add(npcId, info);
                }
                else
                {
                    Debug.LogError("读取ID为" + npcId + "的NPC信息时发生错误");
                }
            }
            return info;
        }

        /// <summary>
        /// 通过ID获取Task
        /// </summary>
        /// <param name="taskId">task的ID</param>
        /// <returns>对应的Task，没找到会返回null</returns>
        public Task GetTaskById(string taskId)
        {
            Task task;
            if (!this.mIdToTaskDic.TryGetValue(taskId, out task))
            {
                if (MYXZXmlReader.ReadTask(out task, taskId))
                {
                    this.mIdToTaskDic.Add(taskId, task);
                }
                else
                {
                    Debug.LogError("读取ID为" + taskId + "的任务时发生错误");
                }
            }
            return task;
        }

        public SceneInfo GetSceneInfoById(string sceneId)
        {
            SceneInfo sceneInfo;
            if (this.mIdToSceneInfoDic.TryGetValue(sceneId, out sceneInfo))
            {
                return sceneInfo;
            }
            Debug.LogError("无法找到id为" + sceneId + "的SceneInfo的信息");
            return null;
        }

        /// <summary>
        /// 获得ID为spriteId的Sprite
        /// </summary>
        /// <param name="spriteId"></param>
        /// <returns></returns>
        public Sprite GetItemSpriteById(string spriteId)
        {
            if (!this.mIdToSpriteDic.ContainsKey(spriteId))
            {
                Sprite sprite = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("ItemSprite")).LoadAsset<Sprite>(spriteId);
                this.mIdToSpriteDic.Add(spriteId, sprite);
            }
            return mIdToSpriteDic[spriteId];
        }

        public Sprite GetHeadSpriteById(string spriteId)
        {
            if (!this.mIdToSpriteDic.ContainsKey(spriteId))
            {
                Sprite sprite = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("HeadSprite")).LoadAsset<Sprite>(spriteId);
                this.mIdToSpriteDic.Add(spriteId, sprite);
            }
            return mIdToSpriteDic[spriteId];
        }

        /// <summary>
        /// 获得所有UIPanel的配置表
        /// </summary>
        /// <param name="typeToAssetBundle"></param>
        /// <param name="typeToName"></param>
        public void GetUIConfig(out Dictionary<UIPanelType, AssetBundle> typeToAssetBundle, out Dictionary<UIPanelType, string> typeToName)
        {
            typeToAssetBundle = new Dictionary<UIPanelType, AssetBundle>();
            typeToName = new Dictionary<UIPanelType, string>();
            AssetBundle uiConfigAB = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("UI"));
            TextAsset uiConfig = uiConfigAB.LoadAsset<TextAsset>("UIConfig");
            List<UIPanelInfo> uiPanelInfos = JsonConvert.DeserializeObject<List<UIPanelInfo>>(uiConfig.text);
            foreach (UIPanelInfo uiPanelInfo in uiPanelInfos)
            {
                typeToAssetBundle.Add(uiPanelInfo.Type, MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(uiPanelInfo.AssetBundlePath));
                typeToName.Add(uiPanelInfo.Type, uiPanelInfo.Name);
            }
            MYXZAssetBundleManager.Instance.Unload(uiConfigAB.name);
        }

        /// <summary>
        /// 由技能ID获取对应的技能树
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns></returns>
        public SkillRootNode GetSkillTreeById(string skillId)
        {
            //由于同一类型的角色可能存在多个并且拥有同一个技能，所以不应该使用一个Dictionary来缓存，而应该每次都创建一个新的返回
            SkillRootNode skillRootNode = null;

            MYXZXmlReader.ReadSkillTree(skillId, out skillRootNode);

            return skillRootNode;
        }
    }
}
