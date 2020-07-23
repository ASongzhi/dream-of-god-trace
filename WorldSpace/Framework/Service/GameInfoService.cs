/*
 * FileName             : GameInfoService.cs
 * Author               : yqs
 * Creat Date           : 2018.2.24
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// 游戏信息的Service类
    /// </summary>
    public class GameInfoService
    {
        /// <summary>
        /// 请勿通过GameInfoService直接访问PlayerInfoModel
        /// </summary>
        [Inject]
        public PlayerInfoModel PlayerInfoModel { get; set; }

        /// <summary>
        /// 玩家获得了ID为id的Item（包括装备）
        /// </summary>
        /// <param name="id">Item的ID</param>
        public void PlayerGetNewItem(string id)
        {
            if (MYXZGameDataManager.Instance.GetItemOrEquipmentById(id) != null)   //如果存在此ID的Item
            {
                if (id.Substring(0, 2).Equals("04"))                    //如果是装备
                {
                    PlayerInfoModel.CurrentHasEquipments.Add(id);
                }
                else if (PlayerInfoModel.CurrentHasItems.ContainsKey(id))
                {
                    PlayerInfoModel.CurrentHasItems[id]++;
                }
                else
                {
                    PlayerInfoModel.CurrentHasItems.Add(id, 1);
                }
            }
        }

        /// <summary>
        /// 玩家获得了经验
        /// </summary>
        /// <param name="Experience">获得的经验</param>
        public void PlayerGetExperience(int Experience)
        {
            PlayerInfoModel.Experience += Experience;
            while (PlayerInfoModel.Info.Level * 100 < PlayerInfoModel.Experience)
            {
                PlayerInfoModel.Experience = PlayerInfoModel.Experience - PlayerInfoModel.Info.Level * 100;
                PlayerInfoModel.Info.Level++;
            }
        }

        /// <summary>
        /// 玩家使用了ID为id的道具（不包括装备）
        /// </summary>
        /// <param name="id">道具ID</param>
        /// <returns>是否使用成功</returns>
        public bool PlayerUseItem(string id)
        {
            if (PlayerInfoModel.CurrentHasItems.ContainsKey(id))
            {
                PlayerInfoModel.CurrentHasItems[id]--;
                if (PlayerInfoModel.CurrentHasItems[id] == 0)
                {
                    PlayerInfoModel.CurrentHasItems.Remove(id);
                }
                //TODO 请实现使用了物品之后的效果
                return true;
            }
            Debug.LogError("玩家不持有ID为" + id + "的道具");
            return false;
        }

        /// <summary>
        /// 玩家装备了ID为id的装备
        /// </summary>
        /// <param name="id">装备ID</param>
        /// <returns>是否装备成功</returns>
        public bool PlayerEquipEquipment(string id)
        {
            if (PlayerInfoModel.CurrentHasEquipments.Contains(id))
            {
                Equipment currentEquipment = MYXZGameDataManager.Instance.GetItemOrEquipmentById(id) as Equipment;
                switch (currentEquipment.EquipmentType)
                {
                    case Equipment.Type.Weapon:
                        PlayerInfoModel.Info -= PlayerInfoModel.Info.Weapon;
                        PlayerInfoModel.Info.Weapon = currentEquipment;
                        break;
                    case Equipment.Type.Hat:
                        PlayerInfoModel.Info -= PlayerInfoModel.Info.Hat;
                        PlayerInfoModel.Info.Hat = currentEquipment;
                        break;
                    case Equipment.Type.Clothes:
                        PlayerInfoModel.Info -= PlayerInfoModel.Info.Clothes;
                        PlayerInfoModel.Info.Clothes = currentEquipment;
                        break;
                    case Equipment.Type.Shoes:
                        PlayerInfoModel.Info -= PlayerInfoModel.Info.Shoes;
                        PlayerInfoModel.Info.Shoes = currentEquipment;
                        break;
                    case Equipment.Type.Ornament:
                        PlayerInfoModel.Info -= PlayerInfoModel.Info.Ornament1;
                        PlayerInfoModel.Info.Ornament1 = currentEquipment;
                        break;
                }
                PlayerInfoModel.Info += currentEquipment;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得当前角色属性
        /// </summary>
        /// <returns></returns>
        public BaseCharacter.Info GetCharaInfo()
        {
            return this.PlayerInfoModel.Info;
        }

        /// <summary>
        /// 获得当前拥有的所有Item（不包括装备）
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetPlayerAllItems()
        {
            return PlayerInfoModel.CurrentHasItems;
        }

        /// <summary>
        /// 获取当前拥有的所有装备
        /// </summary>
        /// <returns></returns>
        public List<string> GetPlayerAllEquipments()
        {
            return PlayerInfoModel.CurrentHasEquipments;
        }

        public void PlayerGetMoney(int gold, int silver, int copper)
        {
            PlayerInfoModel.Copper += copper;
            PlayerInfoModel.Silver += silver + PlayerInfoModel.Copper / 1000;
            PlayerInfoModel.Gold += gold + PlayerInfoModel.Silver / 1000;
            PlayerInfoModel.Silver %= 1000;
            PlayerInfoModel.Copper %= 1000;
        }
    }
}