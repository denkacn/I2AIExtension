using I2.Loc;
using I2AIExtension.Editor.Windows;
using I2AIExtension.Runtime.TermSelector;
using UnityEditor;
using UnityEngine;

namespace I2AIExtension.Editor.Inspectors.TermSelector
{
    [CustomEditor(typeof(LocalizeWithTermSelector))]
    [CanEditMultipleObjects]
    public class LocalizeWithTermSelectorInspector : LocalizeInspector
    {
        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Term Selector"))
            {
                TermSelectorWindow.ShowWindow(OnTermSelected);
            }
            
            GUILayout.EndHorizontal();
            
            base.OnInspectorGUI();
        }
        
        private void OnTermSelected(string term)
        {
            var localizeWithTermSelector = (LocalizeWithTermSelector)target;
            
            localizeWithTermSelector.SetTerm(term);
            
            EditorUtility.SetDirty(target);
        }
    }
}