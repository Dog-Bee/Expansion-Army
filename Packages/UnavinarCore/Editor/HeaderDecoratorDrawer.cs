using UnityEditor;
using UnityEngine;

	#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(HeaderAttribute))]
	public class HeaderDecoratorDrawer : DecoratorDrawer
	{
		public override float GetHeight()
		{
			return EditorGUIUtility.singleLineHeight * 2f;
		}

		public override void OnGUI(Rect position)
		{
			position.yMin += EditorGUIUtility.singleLineHeight * .5f;

			GUIStyle style = new GUIStyle(EditorStyles.boldLabel);
			style.richText = true;

			GUIContent label =
				new GUIContent($"<color=lightblue><size=14>{(attribute as HeaderAttribute)?.header}</size></color>");
			
			GUI.Label(position, label, style);
		}
	}
	#endif
