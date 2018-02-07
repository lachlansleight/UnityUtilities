using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace Foliar.UU {
	/// <summary>
	/// Base class for all SOUI classes.
	/// </summary>
	public class SOUI : MonoBehaviour {

		public ScriptableObject TargetObject;
		public string TargetFieldName;
		public FieldInfo TargetField {
			get {
				if(TargetObject == null) return null;
				return TargetObject.GetType().GetField(TargetFieldName);
			}
		}
	}

}