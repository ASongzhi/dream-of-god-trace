/*
 * FileName             : SaveArchiveCommand.cs
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
    /// 保存游戏存档时的Command， Bind From SaveArchiveSignal
    /// </summary>
    public class RequestSaveArchiveCommand : Command
    {
        /// <summary>
        /// 玩家人物的坐标
        /// </summary>
        [Inject]
        public Transform Transform { get; set; }

        [Inject]
        public PlayerInfoModel PlayerInfoModel { get; set; }

        public override void Execute()
        {
            SaveInfo save = new SaveInfo(Transform.position, Transform.rotation.eulerAngles);
            save.PlayerInfoModel = PlayerInfoModel;
            if(!Directory.Exists(Application.dataPath + "/../Save"))
            {
                Directory.CreateDirectory(Application.dataPath + "/../Save");
            }
            using (var fs = File.Create(Application.dataPath + "/../Save/SaveInfo.bin"))//序列化存档
            {
                Serializer.Serialize<SaveInfo>(fs, save);
            }
        }

    }
}
