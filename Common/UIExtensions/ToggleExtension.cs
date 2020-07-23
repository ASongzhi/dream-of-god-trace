/*
 * FileName             : ToggleExtension.cs
 * Author               : yqs
 * Creat Date           : 2018.2.3
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
    /// 对Toggle的扩展，isOn时会将当前的图片切换为PressedSprite，Child处于active
    /// </summary>
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public class ToggleExtension : MonoBehaviour
    {
        public enum Type
        {
            Sprite,
            Image
        }

        public Type ExtensionType;
        public bool HasChild = true;

        /// <summary>
        /// 当此Toggle处于isOn状态时显示的Sprite
        /// </summary>
        public Sprite PressedSprite;

        public Image PressedImage;
        [Tooltip("当此Toggle处于isOn时Child才会显示")]
        public GameObject Child;

        private Toggle mToggle;
        private Sprite mDefaultSprite;
        private Image mDefaultImage;

        void Start()
        {
            mToggle = GetComponent<Toggle>();
            mDefaultImage = GetComponent<Image>();
            mDefaultSprite = mDefaultImage.sprite;
        }

        void Update()
        {
            if (ExtensionType == Type.Sprite)
            {
                mDefaultImage.sprite = mToggle.isOn ? PressedSprite : mDefaultSprite;
            }
            if (ExtensionType == Type.Image)
            {
                mDefaultImage.enabled = !mToggle.isOn;
                PressedImage.gameObject.SetActive(mToggle.isOn);
            }
            if (HasChild)
            {
                Child.SetActive(mToggle.isOn);
            }
        }
    }
}