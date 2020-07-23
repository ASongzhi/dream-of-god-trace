/*
 * FileName             : PointTargetPicker.cs
 * Author               : yqs
 * Creat Date           : 2018.9.27
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
    /// 攻击区域
    /// </summary>
    public class PointTargetPicker : TargetPicker
    {
        private float mMinDistance;
        private float mMaxDistance;
        private float mViewAngle;

        public PointTargetPicker(float minDistance, float maxDistance, float viewAngle)
        {
            this.mMinDistance = minDistance;
            this.mMaxDistance = maxDistance;
            this.mViewAngle = viewAngle;
        }
        
        public override List<Transform> Pick(Transform player, List<Transform> targets)
        {
            if (targets == null)
                return null;
            List<Transform> results = new List<Transform>();
            foreach (Transform target in targets)
            {
                if (MYXZMath.IsInPointArea(player.position, player.forward, target.position, mMinDistance, mMaxDistance,
                    mViewAngle))
                {
                    results.Add(target);
                }
            }
            return results;
        }
    }
}