using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace Foliar.UU {

	[CustomEditor(typeof(SOUI))]
	public class SOUIEditor : Editor {

		FieldInfo[] Fields;

		public override void OnInspectorGUI() {
			SOUI myTarget = (SOUI)target;

			myTarget.TargetObject = (ScriptableObject)EditorGUILayout.ObjectField("Target object", myTarget.TargetObject, typeof(ScriptableObject), false);

			if(myTarget.TargetObject != null) {
				Fields = myTarget.TargetObject.GetType().GetFields();

				if(Fields.Length > 0) {
					string[] FieldNames = new string[Fields.Length];
					int CurrentFieldIndex = -1;
					for(int i = 0; i < FieldNames.Length; i++) {
						FieldNames[i] = Fields[i].Name;
						if(FieldNames[i] == myTarget.TargetFieldName) CurrentFieldIndex = i;
					}
					if(CurrentFieldIndex == -1) {
						myTarget.TargetFieldName = FieldNames[0];
					}
					myTarget.TargetFieldName = FieldNames[EditorGUILayout.Popup("Target Field", CurrentFieldIndex, FieldNames)];
					EditorGUILayout.LabelField("Field type: " + myTarget.TargetField.FieldType);
				}
			}

			
		}
	}
}