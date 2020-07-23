/*
 * FileName             : RectTargetPicker.cs
 * Author               : 
 * Creat Date           : 
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
    public class RectTargetPicker : TargetPicker
    {
        private float mWidth;
        private float mLength;

        public RectTargetPicker(float width, float length)
        {
            this.mWidth = width;
            this.mLength = length;

        }

        public override List<Transform> Pick(Transform player, List<Transform> targets)
        {
            if (targets == null)
                return null;
            List<Transform> results = new List<Transform>();
            foreach (Transform target in targets)
            {
                if (MYXZMath.IsInRectArea(player.position, player.forward, target.position, this.mWidth, this.mLength))
                {
                    results.Add(target);
                }
            }
            return results;
        }
    }
}