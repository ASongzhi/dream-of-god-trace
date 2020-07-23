/*
 * FileName             : LoadArchiveCommand.cs
 * Author               : hy
 * Creat Date           : 2018.4.21
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using UnityEngine;
using ProtoBuf;
using System.IO;
using strange.extensions.command.impl;

namespace MYXZ
{
    /// <summary>
    /// 读取存档来开始游戏的Command， Bind From LoadArchiveSignal
    /// </summary>
    public class RequestLoadArchiveCommand : Command
    {
        [Inject]
        public PlayerInfoModel PlayerInfoModel { get; set; }

        public override void Execute()
        {
            SaveInfo save;
            if (File.Exists(Application.dataPath + "/../Save/SaveInfo.bin"))
            {
                using (var fs = File.OpenRead(Application.dataPath + "/../Save/SaveInfo.bin"))//反序列化读取存档
                    save = Serializer.Deserialize<SaveInfo>(fs);
                MYXZGameDataManager.Instance.CurrentSaveInfo = save;
                PlayerInfoModel.Init(MYXZGameDataManager.Instance.CurrentSaveInfo);
            }
        }
    }
}
