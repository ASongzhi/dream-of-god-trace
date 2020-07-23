/*
 * FileName             : MYXZXmlReader.cs
 * Author               : ZSZ
 * Creat Date           : 2018.2.12
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace MYXZ
{
    /// <summary>     
    /// XML文件读取  
    /// </summary>
    public class MYXZXmlReader
    {
        /// <summary>
        /// 获取目标id的配置文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetConfigAssetBundlePath(string id)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(new StreamReader(
#if UNITY_EDITOR
                System.IO.Path.Combine(Application.dataPath, "ABResources/Config/TotalConfig.xml")
#else
                System.IO.Path.Combine(Application.dataPath, "TotalConfig.xml")
#endif
                ).ReadToEnd());
            XmlNode rootNode = doc.FirstChild;
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Attributes["Id"].Value.Equals(id))
                {
                    return node.Attributes["Path"].Value;
                }
            }
            Debug.Log("未找到Id为" + id + "配置文件");
            return "";
        }
        /// <summary>
        /// 从xmlFile中读取此NPC的信息
        /// </summary>
        /// <param name="npcInfo">读取得到的信息out写入npcInfo</param>
        /// <param name="xmlText">这个XML文件的string</param>
        /// <returns>是否读取成功</returns>
        public static bool ReadNpcInfo(out NpcInfo npcInfo, string npcId)
        {
            npcInfo = new NpcInfo();

            AssetBundle npcInfoAB = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("NPC"));
            TextAsset taskText = npcInfoAB.LoadAsset<TextAsset>(npcId);
            if (taskText == null)
            {
                Debug.LogError("无法找到id为" + npcId + "的NPC的信息");
                MYXZAssetBundleManager.Instance.Unload(npcInfoAB.name);
                return false;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(taskText.text); //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return false;
            }

            XmlNode npcNode = doc.FirstChild;                   //取得此NPC节点

            npcInfo.Name = npcNode.Attributes["Name"].Value;    //此NPC的名字
            npcInfo.Id = npcNode.Attributes["Id"].Value;        //此NPC的ID
            npcInfo.Sprite = MYXZGameDataManager.Instance.GetHeadSpriteById(npcNode.Attributes["Sprite"].Value);

            foreach (XmlNode node in npcNode.ChildNodes)
            {
                if (node.Name.Equals("Task"))
                {
                    npcInfo.TaskIds = node.Attributes["Id"].Value.Split(',');   //获取此NPC的任务
                }
                else if (node.Name.Equals("Talk"))
                {
                    npcInfo.Talks = GetTalks(node);                                     //获取此NPC的交谈
                }
            }
            MYXZAssetBundleManager.Instance.Unload(npcInfoAB.name);

            return true;
        }

        /// <summary>
        /// 从xmlFile中读取此Task的信息
        /// </summary>
        /// <param name="task">读取得到的信息out写入task中</param>
        /// <param name="xmlText"></param>
        /// <returns>是否读取成功</returns>
        public static bool ReadTask(out Task task, string taskId)
        {
            task = new Task();

            AssetBundle taskInfoAb = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("Task"));

            TextAsset taskText = taskInfoAb.LoadAsset<TextAsset>(taskId);
            if (taskText == null)
            {
                Debug.LogError("无法找到id为" + taskId + "的Task的信息");
                MYXZAssetBundleManager.Instance.Unload(taskInfoAb.name);
                return false;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(taskText.text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return false;
            }

            XmlNode taskNode = doc.FirstChild;

            task.Name = taskNode.Attributes["Name"].Value;              //任务名字
            task.Id = taskNode.Attributes["Id"].Value;                  //任务ID
            task.Publisher = taskNode.Attributes["Publisher"].Value;    //任务发布者ID
            task.Deliverer = taskNode.Attributes["Deliverer"].Value;    //任务交付者ID

            foreach (XmlNode node in taskNode.ChildNodes)
            {
                if (node.Name.Equals("Require"))
                {
                    if (!node.Attributes["Predecessors"].Value.Equals(""))    //如果有前置任务
                    {
                        task.Predecessors = node.Attributes["Predecessors"].Value.Split(','); //设置前置任务
                    }
                    task.LevelRequirement = Int32.Parse(node.Attributes["Level"].Value);      //等级需求
                    if (!node.Attributes["Item"].Value.Equals(""))            //如果有道具需求
                    {
                        task.RequireItems = new Dictionary<string, int>();
                        string[] items = node.Attributes["Item"].Value.Split(',');
                        for (int i = 0; i < items.Length; i++)
                        {
                            string[] item = items[i].Split('/');
                            task.RequireItems.Add(item[0], Int32.Parse(item[1])); //接取任务所需要的物品，item[0]是ID，item[1]是数量
                        }
                    }
                }
                else if (node.Name.Equals("Description"))
                {
                    task.Description = node.InnerText;            //对任务的描述
                }
                else if (node.Name.Equals("Target"))
                {
                    task.Targets = new Task.Target[node.ChildNodes.Count];
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        task.Targets[i] = new Task.Target();
                        task.Targets[i].Type = (TaskTargetType)Enum.Parse(typeof(TaskTargetType), node.ChildNodes[i].Attributes["Type"].Value);//任务类型
                        task.Targets[i].TargetId = node.ChildNodes[i].Attributes["Id"].Value;                                              //任务目标ID
                        task.Targets[i].Count = int.Parse(node.ChildNodes[i].Attributes["Count"].Value);        //对目标操作次数
                    }
                }
                else if (node.Name.Equals("TakeTask"))
                {
                    task.TakeTaskTalk = GetTalks(node);     //接取任务时的对话
                }
                else if (node.Name.Equals("InTask"))
                {
                    task.InTaskTalk = GetTalks(node);       //在任务中的对话
                }
                else if (node.Name.Equals("FininTask"))
                {
                    task.FinishTaskTalk = GetTalks(node);   //任务交付时的对话，只有交付者不是010000（系统）时才有
                }
                else if (node.Name.Equals("TakeTaskReward"))
                {
                    task.TakeTaskExperience = int.Parse(node.Attributes["Experience"].Value);
                    ReadRewardMoney(node.Attributes["Money"].Value, ref task.TakeTaskGold, ref task.TakeTaskSilver, ref task.TakeTaskCopper);
                    task.TakeTaskItem = ReadRewardItem(task.TakeTaskItem, node);
                }
                else if (node.Name.Equals("FinishTaskReward"))
                {
                    task.FinishTaskExperience = int.Parse(node.Attributes["Experience"].Value);
                    ReadRewardMoney(node.Attributes["Money"].Value, ref task.FinishTaskGold, ref task.FinishTaskSilver, ref task.FinishTaskCopper);
                    task.FinishTaskItem = ReadRewardItem(task.FinishTaskItem, node);
                }
            }
            MYXZAssetBundleManager.Instance.Unload(taskInfoAb.name);

            return true;
        }


        /// <summary>
        /// 从xmlFile中读取此ReadRewardMoney的信息
        /// </summary>
        /// <param name="money">xml中被读取的金钱</param>
        /// <param name="gold">奖励的金币</param>
        /// <param name="silver">奖励的银币</param>
        /// <param name="copper">奖励的铜币</param>
        private static void ReadRewardMoney(string money, ref int gold, ref int silver, ref int copper)
        {
            int temp = 0;
            string tmpMoney = null;
            for (int i = 0; i < money.Length; i++)
            {
                if (money[i] != '/')
                {
                    tmpMoney = tmpMoney + money[i];
                }
                else if (money[i] == '/' && temp == 0)
                {
                    gold = int.Parse(tmpMoney);
                    tmpMoney = null;
                    temp++;
                }
                else if (money[i] == '/' && temp == 1)
                {
                    silver = int.Parse(tmpMoney);
                    tmpMoney = null;
                    temp++;
                }
                if (i + 1 == money.Length && temp == 2)
                {
                    copper = int.Parse(tmpMoney);
                    tmpMoney = null;
                    temp++;
                }
            }
        }


        /// <summary>
        /// 从xmlFile中读取此RewardItem的信息
        /// </summary>
        /// <param name="dictionary">RewardItem的dictionary</param>
        /// <param name="node">xml的结点</param>
        private static Dictionary<string, int> ReadRewardItem(Dictionary<string, int> dictionary, XmlNode node)
        {
            string item = null;
            int count = '0';
            string temp = null;
            dictionary = new Dictionary<string, int>();

            for (int i = 0; i < node.InnerText.Length; i++)
            {
                if (i > 0 && node.InnerText[i] == '/')
                {
                    temp = node.InnerText[i + 1].ToString();
                    count = int.Parse(temp);
                }
                else if (node.InnerText[i] == ',' || i + 1 == node.InnerText.Length)
                {
                    dictionary.Add(item, count);
                    count = 0;
                    item = null;
                }
                else if (!(node.InnerText[i] == ',') && !(node.InnerText[i] == '/') && (i + 1 != node.InnerText.Length) && !(node.InnerText[i + 1] == ','))
                {
                    item = item + node.InnerText[i];
                }
            }
            return dictionary;
        }


        /// <summary>
        /// 从xmlFile中读取此SceneInfo的信息
        /// </summary>
        /// <param name="sceneInfo">读取得到的信息out赋值给sceneInfo</param>
        /// <param name="xmlText">xml的string</param>
        /// <returns>是否读取成功</returns>
        public static bool ReadSceneInfo(out SceneInfo sceneInfo, string xmlText)
        {
            sceneInfo = new SceneInfo();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlText); //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return false;
            }

            XmlNode sceneInfoNode = doc.FirstChild; //获取SceneInfo节点
            sceneInfo.Name = sceneInfoNode.Attributes["Name"].Value; //获取当前场景的名称
            sceneInfo.Id = sceneInfoNode.Attributes["Id"].Value; //获取当前场景的Id

            foreach (XmlNode node in sceneInfoNode.ChildNodes)
            {
                if (node.Name.Equals("NPC"))
                {
                    sceneInfo.Npcs = node.Attributes["Id"].Value.Split(','); //获取当前场景所有NPC的ID
                }
            }
            return true;
        }

        /// <summary>
        /// 从这个对话节点中获取这段话的内容
        /// </summary>
        /// <param name="talksNode">对话节点</param>
        /// <returns>对话</returns>
        private static Talk[] GetTalks(XmlNode talksNode)
        {
            Talk[] talks = new Talk[talksNode.ChildNodes.Count];
            for (int i = 0; i < talksNode.ChildNodes.Count; i++)
            {
                talks[i] = new Talk();
                talks[i].Speaker = talksNode.ChildNodes[i].Attributes["Speaker"].Value; //这句话的讲诉者
                talks[i].Words = talksNode.ChildNodes[i].InnerText; //这句话的内容
            }
            return talks;
        }

        /// <summary>
        /// 从xmlText读取Item信息
        /// </summary>
        /// <param name="item">读取得到的信息out写入item</param>
        /// <param name="xmlText">xml文档</param>
        /// <returns>是否读取成功</returns>
        public static bool ReadItemInfo(out Item item, string itemId)
        {
            item = null;

            AssetBundle itemInfoAB = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("Item"));

            TextAsset itemText = itemInfoAB.LoadAsset<TextAsset>(itemId);
            if (itemText == null)
            {
                Debug.LogError("无法找到id为" + itemId + "的Item的信息");
                MYXZAssetBundleManager.Instance.Unload(itemInfoAB.name);
                return false;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(itemText.text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return false;
            }
            XmlNode itemNode = doc.FirstChild;
            string itemType = itemNode.Attributes["ID"].Value.Substring(0, 2);  //获取Item的类型

            string name = itemNode.Attributes["Name"].Value;
            string id = itemNode.Attributes["ID"].Value;
            string spriteId = itemNode.Attributes["Sprite"].Value;
            string itemDescription = "";
            string useDescription = "";
            int buyPrice = 0;
            int salePrice = 0;

            int hp = 0;
            int physicalAttack = 0;
            int physicalDefense = 0;
            int magicAttack = 0;
            int magicDefense = 0;
            Equipment.Type equipmentType = Equipment.Type.Weapon;

            foreach (XmlNode node in itemNode.ChildNodes)
            {
                if (node.Name.Equals("ItemDescription"))
                {
                    itemDescription = node.InnerText;
                }
                else if (node.Name.Equals("UseDescription"))
                {
                    useDescription = node.InnerText;
                }
                else if (node.Name.Equals("SalePrice"))
                {
                    salePrice = Int32.Parse(node.InnerText);
                }
                else if (node.Name.Equals("BuyPrice"))
                {
                    buyPrice = Int32.Parse(node.InnerText);
                }
                else if (node.Name.Equals("HP"))
                {
                    hp = Int32.Parse(node.InnerText);
                }
                else if (node.Name.Equals("PhysicalAttack"))
                {
                    physicalAttack = Int32.Parse(node.InnerText);
                }
                else if (node.Name.Equals("MagicAttack"))
                {
                    magicAttack = Int32.Parse(node.InnerText);
                }
                else if (node.Name.Equals("PhysicalDefense"))
                {
                    physicalDefense = Int32.Parse(node.InnerText);
                }
                else if (node.Name.Equals("MagicDefense"))
                {
                    magicDefense = Int32.Parse(node.InnerText);
                }
                else if (node.Name.Equals("Type"))
                {
                    equipmentType = (Equipment.Type)Enum.Parse(typeof(Equipment.Type), node.InnerText);
                }
            }

            if (itemType.Equals("03"))
            {
                item = new Consumable(name, id, spriteId, itemDescription, useDescription, buyPrice, salePrice,
                    hp);
            }
            else if (itemType.Equals("04"))
            {
                item = new Equipment(name, id, spriteId, itemDescription, useDescription, buyPrice, salePrice,
                    physicalAttack, magicAttack, physicalDefense, magicDefense, hp, equipmentType);
            }
            else if (itemType.Equals("09"))
            {
                item = new TaskItem(name, id, spriteId, itemDescription, useDescription);
            }
            else if (itemType.Equals("10"))
            {
                item = new Material(name, id, spriteId, itemDescription, useDescription, buyPrice, salePrice);
            }
            MYXZAssetBundleManager.Instance.Unload(itemInfoAB.name);

            return true;
        }

        /// <summary>
        /// 根据技能树的ID来获取一个技能树实例
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="rootNode"></param>
        public static void ReadSkillTree(string skillId, out SkillRootNode rootNode)
        {
            AssetBundle skillConfigAB = MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(MYXZXmlReader.GetConfigAssetBundlePath("Skill"));  //包括SkillTree和SkillBase
            TextAsset skillText = skillConfigAB.LoadAsset<TextAsset>(skillId);

            rootNode = new SkillRootNode(skillId);
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(skillText.text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return;
            }

            XmlNode configNode = doc.FirstChild;
            rootNode.Loop = bool.Parse(configNode.Attributes["Loop"].Value);
            rootNode.Name = configNode.Attributes["Name"].Value;
            TraverseToGetSkillTree(rootNode, configNode);
            MYXZAssetBundleManager.Instance.Unload(skillConfigAB.name);
        }

        /// <summary>
        /// 使用递归来遍历这个技能树的所有节点
        /// </summary>
        /// <param name="skillNode"></param>
        /// <param name="xmlNode"></param>
        private static void TraverseToGetSkillTree(SkillNode skillNode, XmlNode xmlNode)
        {
            if (xmlNode.HasChildNodes)
            {
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    if (childNode.Name.Equals("Condition")) //条件节点
                    {
                        SkillCondition skillCondition = new SkillCondition("Condition");
                        skillNode.AddChildNode(skillCondition);
                        TraverseToGetSkillTree(skillCondition, childNode);
                    }

                    if (childNode.Name.Equals("SkillBase")) //具体技能
                    {
                        SkillBase skillBase = GetSkillBase(childNode.Attributes["Id"].Value);
                        skillNode.AddChildNode(skillBase);
                        TraverseToGetSkillTree(skillBase, childNode);
                    }
                }
            }
        }
        /// <summary>
        /// 返回此节点指向的子技能对应的SkillBase
        /// </summary>
        /// <param name="skillBaseNode"></param>
        /// <returns></returns>
        private static SkillBase GetSkillBase(string skillBaseId)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(MYXZAssetBundleManager.Instance.LoadOrGetAssetBundle(
                    MYXZXmlReader.GetConfigAssetBundlePath("Skill")
                    ).LoadAsset<TextAsset>(skillBaseId).text);   //读取此XML字符串至doc
            }
            catch (XmlException e)
            {
                Debug.LogException(e);
                return null;
            }

            TargetPicker attackArea = null;
            string animationName = "";
            float skillTime = 0.0f;
            foreach (XmlNode childNode in doc.FirstChild.ChildNodes)
            {
                if (childNode.Name.Equals("AttackArea"))
                {
                    switch (childNode.Attributes["Mode"].Value)
                    {
                        case "Point":
                            attackArea = new PointTargetPicker(
                                float.Parse(childNode.Attributes["MinDistance"].Value),
                                float.Parse(childNode.Attributes["MaxDistance"].Value),
                                float.Parse(childNode.Attributes["Angle"].Value));
                            break;
                        case "Rect":
                            attackArea = new RectTargetPicker(
                                float.Parse(childNode.Attributes["Width"].Value),
                                float.Parse(childNode.Attributes["Length"].Value));
                            break;
                    }
                }

                if (childNode.Name.Equals("Animation"))
                {
                    animationName = childNode.Attributes["Name"].Value;
                }

                if (childNode.Name.Equals("SkillTime"))
                {
                    skillTime = float.Parse(childNode.InnerText);
                }
            }
            return new SkillBase(skillBaseId, attackArea, animationName, skillTime);
        }
    }
}