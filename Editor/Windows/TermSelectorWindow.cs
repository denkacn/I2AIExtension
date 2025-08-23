using System;
using System.Collections.Generic;
using I2.Loc;
using UnityEditor;
using UnityEngine;

namespace I2AIExtension.Editor.Windows
{
    public class TermSelectorWindow : EditorWindow
    {
        private const float CAT_BUTTON_WIDTH = 100f;
        private const float SPACING = 5f;
        private const float TERMS_BUTTON_WIDTH = 200;
        
        private Action<string> _onSelectedCallback;
        private List<string> _categories;
        private readonly Dictionary<string, List<string>> _terms = new Dictionary<string, List<string>>();
        private readonly Dictionary<string, Vector2> _scrollPositions = new Dictionary<string, Vector2>();
        
        private string _selectedCategory;
        
        //private Vector2 _scrollPosition = Vector2.zero;
        
        public static void ShowWindow(System.Action<string> callback)
        {
            var window = GetWindow<TermSelectorWindow>("Term Selector Window");
            window._onSelectedCallback = callback;
            window.Preparations();
            window.Show();
        }
        
        private void OnGUI()
        {
            if (focusedWindow.GetType() != typeof(TermSelectorWindow)) return;
            
            DrawCategories();
            DrawTerms();
        }

        private void Preparations()
        {
            _categories = LocalizationManager.GetCategories();
            
            _terms.Clear();
            _scrollPositions.Clear();
            
            foreach (var category in _categories)
            {
                _terms.Add(category, new List<string>(LocalizationManager.GetTermsList(category)));
                _scrollPositions.Add(category, Vector2.zero);
            }
        }
        
        private void DrawCategories()
        {
            EditorGUILayout.LabelField("Categories", EditorStyles.boldLabel);
            
            var buttonsPerRow = Mathf.Max(1, Mathf.FloorToInt(focusedWindow.position.width / (CAT_BUTTON_WIDTH + SPACING)));
            
            for (var i = 0; i < _categories.Count; i++)
            {
                if (i % buttonsPerRow == 0)
                {
                    GUILayout.BeginHorizontal();
                }
            
                if (GUILayout.Button(_categories[i], GUILayout.Width(CAT_BUTTON_WIDTH)))
                {
                    _selectedCategory = _selectedCategory != _categories[i] ? _categories[i] : null;
                }
            
                if ((i + 1) % buttonsPerRow == 0)
                {
                    GUILayout.EndHorizontal();
                }
            }
            
            if (20 % buttonsPerRow != 0)
            {
                GUILayout.EndHorizontal();
            }
        }

        private void DrawTerms()
        {
            GUILayout.BeginVertical();
            EditorGUILayout.LabelField("Terms", EditorStyles.boldLabel);

            foreach (var pair in _terms)
            {
                if (!string.IsNullOrEmpty(_selectedCategory))
                {
                    if(pair.Key != _selectedCategory) continue;
                }
                
                EditorGUILayout.LabelField(pair.Key, EditorStyles.boldLabel);
                
                _scrollPositions[pair.Key] = GUILayout.BeginScrollView(_scrollPositions[pair.Key]);
        
                var buttonsPerRow = Mathf.Max(1, Mathf.FloorToInt(focusedWindow.position.width / (TERMS_BUTTON_WIDTH + SPACING)));
                
                for (var i = 0; i < pair.Value.Count; i++)
                {
                    if (i % buttonsPerRow == 0)
                    {
                        GUILayout.BeginHorizontal();
                    }
            
                    if (GUILayout.Button(pair.Value[i], GUILayout.Width(TERMS_BUTTON_WIDTH), GUILayout.Height(30)))
                    {
                        Debug.Log($"Pressed button {pair.Value[i]}");
                        
                        _onSelectedCallback?.Invoke(pair.Value[i]);
                    }
            
                    if ((i + 1) % buttonsPerRow == 0)
                    {
                        GUILayout.EndHorizontal();
                    }
                }
            
                if (20 % buttonsPerRow != 0)
                {
                    GUILayout.EndHorizontal();
                }
        
                GUILayout.EndScrollView();
            }
            
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }
    }
}