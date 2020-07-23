/*
 * FileName             : MYXZInputManager.cs
 * Author               : yqs
 * Creat Date           : 2018.3.27
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
    /// 封装了输入，IsEnable返回当前是否接受输入
    /// </summary>
    public class MYXZInputManager
    {
        #region Singleton

        public static MYXZInputManager Instance
        {
            get { return mInstance = mInstance ?? new MYXZInputManager(); }
        }

        private static MYXZInputManager mInstance;

        private MYXZInputManager()
        {
        }

        #endregion

        /// <summary>
        /// 是否接收输入
        /// </summary>
        public bool IsEnable
        {
            get { return mIsEnable; }
            set { mIsEnable = value; }
        }

        private bool mIsEnable = true;

        public bool GetKey(KeyCode keyCode)
        {
            return IsEnable && Input.GetKey(keyCode);
        }

        public bool GetKeyDown(KeyCode keyCode)
        {
            return IsEnable && Input.GetKeyDown(keyCode);
        }

        public bool GetKeyUp(KeyCode keyCode)
        {
            return IsEnable && Input.GetKeyUp(keyCode);
        }

        public bool GetMouseButton(int button)
        {
            return IsEnable && Input.GetMouseButton(button);
        }

        public bool GetMouseButtonDown(int button)
        {
            return IsEnable && Input.GetMouseButtonDown(button);
        }

        public bool GetMouseButtonUp(int button)
        {
            return IsEnable && Input.GetMouseButtonUp(button);
        }
    }
}