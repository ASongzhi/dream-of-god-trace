/*
 * FileName             : ImageExtensions.cs
 * Author               : yqs
 * Creat Date           : 2018.3.24
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// UnityEngine.UI.Image的拓展方法
    /// </summary>
    public static class ImageExtensions
    {
        public static void SetColorA(this Image image, float a)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
        }
    }
}