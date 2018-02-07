using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Foliar.UU {

	[CustomEditor(typeof(SOUI_Toggle))]
	public class SOUI_ToggleEditor : SOUIEditor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			SOUI_Toggle myTarget = (SOUI_Toggle)target;

			if(myTarget.TargetObject != null) {
				
				if(myTarget.TargetField.FieldType == typeof(bool)) {
					myTarget.isBoolType = true;
				} else {
					myTarget.isBoolType = false;
					myTarget.OnValue = (ScriptableObject)EditorGUILayout.ObjectField("On Value", myTarget.OnValue, myTarget.TargetField.FieldType, false);
					if(myTarget.OnValue != null) {
						if(myTarget.OnValue.GetType() != myTarget.TargetField.FieldType) {
							EditorGUILayout.HelpBox("Warning: Invalid type - should be " + myTarget.TargetField.FieldType, MessageType.Error);
						}
					}
					myTarget.OffValue = (ScriptableObject)EditorGUILayout.ObjectField("Off Value", myTarget.OffValue, myTarget.TargetField.FieldType, false);
					if(myTarget.OffValue != null) {
						if(myTarget.OffValue.GetType() != myTarget.TargetField.FieldType) {
							EditorGUILayout.HelpBox("Warning: Invalid type - should be " + myTarget.TargetField.FieldType, MessageType.Error);
						}
					}
				}

				myTarget.SetUnityToggle();
			}

			
		}
	}
}