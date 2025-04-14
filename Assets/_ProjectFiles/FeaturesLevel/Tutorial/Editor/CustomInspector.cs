using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(ConditionChecker), true)]
public class CustomInspector : Editor
{
	private SerializedProperty selectedObjectProp;
	private SerializedProperty selectedClassProp;
	private SerializedProperty selectedFieldProp;
	private SerializedProperty selectedPropertyProp;
	private SerializedProperty selectedOperatorProp;
	private SerializedProperty comparisonValueProp;

	private Type selectedType;
	private List<string> fieldNames = new List<string>();
	private int selectedFieldIndex;
	private int selectedPropertyIndex;
	private int selectedOperatorIndex;
	private string[] comparisonOperators;

	private List<string> propertyNames = new List<string>();

	private void OnEnable()
	{
		comparisonOperators = new string[]{ "<", ">", "==", "!=" };
		selectedObjectProp = serializedObject.FindProperty("selectedObject");
		selectedClassProp = serializedObject.FindProperty("selectedClass");
		selectedFieldProp = serializedObject.FindProperty("selectedField");
		selectedPropertyProp = serializedObject.FindProperty("selectedProperty");
		selectedOperatorProp = serializedObject.FindProperty("selectedOperator");
		comparisonValueProp = serializedObject.FindProperty("comparisonValue");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.LabelField("Actual values.");

		EditorGUILayout.PropertyField(selectedObjectProp);
		EditorGUILayout.PropertyField(selectedClassProp);
		EditorGUILayout.PropertyField(selectedFieldProp);
		EditorGUILayout.PropertyField(selectedPropertyProp);
		EditorGUILayout.PropertyField(selectedOperatorProp);
		EditorGUILayout.PropertyField(comparisonValueProp);

		EditorGUILayout.LabelField("Change Condition Checker values using fields below.");

		if (selectedObjectProp.objectReferenceValue != null)
		{
			var gameObject = (GameObject)selectedObjectProp.objectReferenceValue;
			MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();
			List<Type> types = new List<Type>();

			foreach (var component in components)
			{
				if (component != null) types.Add(component.GetType());
			}

			ConditionChecker checker = (ConditionChecker)target;
			if (types.Count > 0)
			{
				string[] typeNames = types.ConvertAll(type => type.Name).ToArray();
				int selectedTypeIndex = EditorGUILayout.Popup("Select Class", selectedType != null ? Array.IndexOf(typeNames, selectedType.Name) : -1, typeNames);

				if (selectedTypeIndex >= 0)
				{
					selectedType = types[selectedTypeIndex];
					checker.selectedClassIndex = selectedTypeIndex;
					selectedClassProp.stringValue = selectedType.Name;

					FieldInfo[] fields = selectedType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
					fieldNames.Clear();

					foreach (var field in fields)
					{
						fieldNames.Add(field.Name);
					}

					if (fieldNames.Count > 0)
					{
						int newSelectedFieldIndex = EditorGUILayout.Popup("Select Field", checker.selectedFieldIndex, fieldNames.ToArray());
						if (newSelectedFieldIndex != checker.selectedFieldIndex)
						{
							checker.selectedFieldIndex = newSelectedFieldIndex;
							selectedFieldProp.stringValue = checker.selectedFieldIndex >= 0 ? fieldNames[checker.selectedFieldIndex] : "";
						}

						if (checker.selectedFieldIndex >= 0)
						{
							var selectedField = fields[checker.selectedFieldIndex];
							propertyNames.Clear();

							propertyNames.Add("Null");
							if (typeof(IEnumerable).IsAssignableFrom(selectedField.FieldType))
							{
								propertyNames.Add("Count");
							}

							int newSelectedPropertyIndex = EditorGUILayout.Popup("Select Property", checker.selectedPropertyIndex, propertyNames.ToArray());
							if (newSelectedPropertyIndex != checker.selectedPropertyIndex)
							{
								checker.selectedPropertyIndex = newSelectedPropertyIndex;
								selectedPropertyProp.stringValue = checker.selectedPropertyIndex >= 0 ? propertyNames[checker.selectedPropertyIndex] : "";
							}

							int newSelectedOperatorIndex = EditorGUILayout.Popup("Select Operator", checker.selectedOperatorIndex, comparisonOperators);
							if (newSelectedOperatorIndex != checker.selectedOperatorIndex)
							{
								checker.selectedOperatorIndex = newSelectedOperatorIndex;
								selectedOperatorProp.stringValue = checker.selectedOperatorIndex >= 0 ? comparisonOperators[checker.selectedOperatorIndex] : "";
							}

							comparisonValueProp.stringValue = EditorGUILayout.TextField("Comparison Value", comparisonValueProp.stringValue);
						}
					}
					else
					{
						checker.selectedFieldIndex = -1;
						checker.selectedPropertyIndex = -1;
						checker.selectedOperatorIndex = -1;
					}
				}
			}
			else
			{
				EditorGUILayout.LabelField("No components found on the selected GameObject.");
			}
		}

		serializedObject.ApplyModifiedProperties();
	}
}