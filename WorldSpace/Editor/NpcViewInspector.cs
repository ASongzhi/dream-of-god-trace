/*
 * FileName             : NpcViewInspector.cs
 * Author               : 
 * Creat Date           : 
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

namespace MYXZ
{
    [CustomEditor(typeof(NpcView))]
    public class NpcViewInspector : Editor
    {
        private SerializedObject mSerializedObject;
        private NpcView mNpcView;

        public void OnEnable()
        {
            mNpcView = target as NpcView;
            mSerializedObject = new SerializedObject(mNpcView);
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(mSerializedObject.FindProperty("ID"), true);
            EditorGUILayout.PropertyField(mSerializedObject.FindProperty("Name"), true);
            if (mNpcView.IsTalking)
            {
                EditorGUILayout.LabelField("IsTalking");
            }
            EditorGUILayout.PropertyField(mSerializedObject.FindProperty("IsStaticNPC"), true);
            if (!mNpcView.IsStaticNPC)
            {
                ShowPatrolNpcChoice();
            }
            if (EditorGUI.EndChangeCheck())
            {
                mSerializedObject.ApplyModifiedProperties();
            }
        }

        private void ShowPatrolNpcChoice()
        {
            EditorGUILayout.PropertyField(mSerializedObject.FindProperty("MoveSpeed"), true);
            EditorGUILayout.PropertyField(mSerializedObject.FindProperty("PatrolPoints"), true);
        }
    }
}