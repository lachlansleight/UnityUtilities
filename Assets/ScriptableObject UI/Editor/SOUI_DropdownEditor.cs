using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Foliar.UU {

	[CustomEditor(typeof(SOUI_Dropdown))]
	public class SOUI_DropdownEditor : SOUIEditor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			SOUI_Dropdown myTarget = (SOUI_Dropdown)target;

			if(myTarget.TargetObject != null) {
				if(myTarget.Options == null) myTarget.Options = new ScriptableObject[0];

				EditorGUILayout.LabelField("Options:");
				for(int i = 0; i < myTarget.Options.Length; i++) {
					GUILayout.BeginHorizontal();
					myTarget.Options[i] = (ScriptableObject)EditorGUILayout.ObjectField(myTarget.Options[i], myTarget.TargetField.FieldType, false);
					if(i > 0) {
						if(GUILayout.Button("▲")) {
							myTarget.MoveUp(i);
						}
					}
					if(i < myTarget.Options.Length - 1 && myTarget.Options.Length > 1) {
						if(GUILayout.Button("▼")) {
							myTarget.MoveDown(i);
						}
					}
					if(GUILayout.Button("X")) {
						myTarget.RemoveOption(i);
					}
					GUILayout.EndHorizontal();
					if(myTarget.Options[i] != null) {
						if(myTarget.Options[i].GetType() != myTarget.TargetField.FieldType) {
							EditorGUILayout.HelpBox("Warning: Invalid type - should be " + myTarget.TargetField.FieldType, MessageType.Error);
						}
					}
				}
				if(GUILayout.Button("Add Option")) {
					myTarget.AddOption();
				}

				myTarget.UpdateUnityOptions();
			}

			
		}
	}
}