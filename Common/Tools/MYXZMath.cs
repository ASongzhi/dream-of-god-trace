/*
 * FileName             : MYXZMath.cs
 * Author               : yqs
 * Creat Date           : 2018.9.27
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using UnityEngine;

namespace MYXZ
{
    public class MYXZMath
    {
        /// <summary>
        /// 判断一个物体是否在user的点作用范围内
        /// </summary>
        /// <param name="userPosition">发起人坐标</param>
        /// <param name="userFaceTo">发起人正对的方向</param>
        /// <param name="target">目标</param>
        /// <param name="minDistance">点作用最小距离</param>
        /// <param name="maxDistance">点作用最大距离</param>
        /// <param name="viewAngle">点作用的角度</param>
        /// <returns>是否在范围内</returns>
        public static bool IsInPointArea(Vector3 userPosition, Vector3 userFaceTo, Vector3 target, float minDistance, float maxDistance, float viewAngle)
        {
            if (Vector3.Angle(userFaceTo, target - userPosition) > (viewAngle / 2)) 
            {
                return false;
            }
            float distance = Vector3.Distance(userPosition, target);
            if (distance < minDistance || distance > maxDistance)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断一个物体是否在发起人的矩形作用区域内
        /// </summary>
        /// <param name="userPosition">发起人的坐标</param>
        /// <param name="userFaceTo">发起人的正面朝向</param>
        /// <param name="target">目标物体</param>
        /// <param name="width">矩形作用区域的宽度</param>
        /// <param name="length">矩形作用区域的长度</param>
        /// <returns>是否在发起人的矩形作用区域内</returns>
        public static bool IsInRectArea(Vector3 userPosition, Vector3 userFaceTo, Vector3 target, float width, float length)
        {
            float angle = Vector2.Angle(new Vector2(userFaceTo.x, userFaceTo.z),
                new Vector2(target.x - userPosition.x, target.z - userPosition.z));
            if (angle > 90)     //如果目标物体和发起人的朝向夹角大于90度
            {
                return false;
            }
            Vector2 userLengthPos = new Vector2(userPosition.x, userPosition.z).normalized * length;    //以userPosition为出发点，构建一条length长度的向量

            //如果这个向量和target生成的平行四边形的面积大于这个矩形判断区域的一半，就说明这个点在矩形外
            if (Vector2.Dot(userLengthPos, new Vector2(target.x - userPosition.x, target.z - userPosition.z)) > (width * length / 2)) 
            {
                return false;
            }
            return true;
        }
    }
}