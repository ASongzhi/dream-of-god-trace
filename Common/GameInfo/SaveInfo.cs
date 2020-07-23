/*
 * FileName             : SaveInfo.cs
 * Author               : hy
 * Creat Date           : 2018.4.21
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
    /// 存储存档信息的类
    /// </summary>
    [ProtoContract]
    public class SaveInfo
    {
        [ProtoMember(1)]
        public float PositionX;
        [ProtoMember(2)]
        public float PositionY;
        [ProtoMember(3)]
        public float PositionZ;
        [ProtoMember(4)]
        public float RotationX;
        [ProtoMember(5)]
        public float RotationY;
        [ProtoMember(6)]
        public float RotationZ;
        [ProtoMember(7)]
        public PlayerInfoModel PlayerInfoModel;

        public struct Transform
        {
            public Vector3 Position;
            public Vector3 Rotation;
        }

        public SaveInfo(Vector3 position, Vector3 rotation)
        {
            PositionX = position.x;
            PositionY = position.y;
            PositionZ = position.z;
            RotationX = rotation.x;
            RotationY = rotation.y;
            RotationZ = rotation.z;

        }

        public SaveInfo()
        {

        }

        public Transform GetplayerTransform()
        {
            Transform transform;
            transform.Position = new Vector3(PositionX, PositionY, PositionZ);
            transform.Rotation = new Vector3(RotationX, RotationY, RotationZ);
            return transform;
        }


    }
}
