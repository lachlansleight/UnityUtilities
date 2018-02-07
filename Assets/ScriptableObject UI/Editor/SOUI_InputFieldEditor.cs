using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Foliar.UU {

	[CustomEditor(typeof(SOUI_InputField))]
	public class SOUI_InputFieldEditor : SOUIEditor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			SOUI_InputField myTarget = (SOUI_InputField)target;

			if(myTarget.TargetObject != null) {
				if(myTarget.TargetField != null) {
					if(myTarget.TargetField.FieldType != typeof(string)) {
						EditorGUILayout.HelpBox("Warning: Invalid type - should be string", MessageType.Error);
					}
				}

			}

			myTarget.SetUnityInputField();			
		}
	}
}