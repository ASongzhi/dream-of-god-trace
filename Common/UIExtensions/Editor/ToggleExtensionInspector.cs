/*
 * FileName             : ToggleExtensionInspector.cs
 * Author               : yqs
 * Creat Date           : 2018.4.7
 * Revision History     : 
 *          R1: 
 *              修改作者：
 *              修改日期：
 *              修改内容：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MYXZ
{
    /// <summary>
    /// ToggleExtension的Inspector面板显示
    /// </summary>
    [CustomEditor(typeof(ToggleExtension))]
    public class ToggleExtensionInspector : Editor
    {
        private ToggleExtension mToggleExtension;

        void OnEnable()
        {
            mToggleExtension = (ToggleExtension)target;
        }

        public override void OnInspectorGUI()
        {
            mToggleExtension.ExtensionType = (ToggleExtension.Type)EditorGUILayout.EnumPopup("Type", mToggleExtension.ExtensionType);
            mToggleExtension.HasChild = EditorGUILayout.Toggle("Has Child", mToggleExtension.HasChild);

            switch (mToggleExtension.ExtensionType)
            {
                case ToggleExtension.Type.Sprite:
                    mToggleExtension.PressedSprite = (Sprite)EditorGUILayout.ObjectField("Pressed Sprite", mToggleExtension.PressedSprite, typeof(Sprite), true);
                    break;
                case ToggleExtension.Type.Image:
                    mToggleExtension.PressedImage = (Image)EditorGUILayout.ObjectField("Pressed Image",
                        mToggleExtension.PressedImage, typeof(Image), true);
                    break;
                default:
                    break;
            }
            if (mToggleExtension.HasChild)
            {
                mToggleExtension.Child = (GameObject)EditorGUILayout.ObjectField("Child", mToggleExtension.Child, typeof(GameObject), true);
            }
        }
    }
}