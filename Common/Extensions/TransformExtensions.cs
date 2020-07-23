/*
 * FileName             : TransformExtensions.cs
 * Author               : yqs
 * Creat Date           : 2017.12.27
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using UnityEngine;

namespace MYXZ
{
    /// <summary>
    /// Transform的扩展方法
    /// </summary>
    public static class TransformExtensions
    {
        public static void SetPositionX(this Transform tran, float x)
        {
            tran.position = new Vector3(x, tran.position.y, tran.position.z);
        }

        public static void SetPositionY(this Transform tran, float y)
        {
            tran.position = new Vector3(tran.position.x, y, tran.position.z);
        }

        /// <summary>
        /// 改变Position的Y值，min &lt;= y &lt;= max,超出会被截断
        /// </summary>
        /// <param name="y">修改position的Y值为y</param>
        /// <param name="min">y的最小值</param>
        /// <param name="max">y的最大值</param>
        public static void SetPositionY(this Transform tran, float y, float min, float max)
        {
            y = y < min ? min : y;
            y = y > max ? max : y;
            tran.position = new Vector3(tran.position.x, y, tran.position.z);
        }

        public static void SetPositionZ(this Transform tran, float z)
        {
            tran.position = new Vector3(tran.position.x, tran.position.y, z);
        }

        public static void SetLocalPositionX(this Transform tran, float x)
        {
            tran.localPosition = new Vector3(x, tran.localPosition.y, tran.localPosition.z);
        }

        public static void SetLocalPositionY(this Transform tran, float y)
        {
            tran.localPosition = new Vector3(tran.localPosition.x, y, tran.localPosition.z);
        }

        /// <summary>
        /// 改变LocalPosition的Y值，min &lt;= y &lt;= max,超出会被截断
        /// </summary>
        public static void SetLocalPositionY(this Transform tran, float y, float min, float max)
        {
            y = y < min ? min : y;
            y = y > max ? max : y;
            tran.localPosition = new Vector3(tran.localPosition.x, y, tran.localPosition.z);
        }

        public static void SetLocalPositionZ(this Transform tran, float z)
        {
            tran.localPosition = new Vector3(tran.localPosition.x, tran.localPosition.y, z);
        }
    }
}