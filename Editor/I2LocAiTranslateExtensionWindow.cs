using System;
using I2.Loc;
using I2AIExtension.Editor.Context;
using I2AIExtension.Editor.Managers;
using I2AIExtension.Editor.PromtFactories;
using I2AIExtension.Editor.Views;
using UnityEditor;
using UnityEngine;

namespace I2AIExtension.Editor
{
    [Serializable]
    public class I2LocAiTranslateExtensionWindow : EditorWindow
    {
        private LanguageSourceAsset _languageSourceAsset;
        private I2LocAiTranslateExtensionManager _translateExtensionManager;
        private Vector2 _scrollPosition;
        
        private TranslateSettingsButtonView _translateSettingsButtonView;
        private TranslationTermsView _translationTermsView;
        private AiTranslateProviderView _aiTranslateProviderView;
        private PromtView _promtView;

        private bool _isShowSettingsMenu = true;
        
        [MenuItem("Tools/I2 Localization Ai Translate Extension Window")]
        public static void ShowWindow()
        {
            GetWindow<I2LocAiTranslateExtensionWindow>("I2 Ai Translate Extension");
        }
        
        private void OnGUI()
        {
            GUILayout.Space(10);
            
            GUILayout.BeginHorizontal();
            
            _languageSourceAsset = (LanguageSourceAsset)EditorGUILayout.ObjectField(
                "Language Source Asset",
                _languageSourceAsset,
                typeof(LanguageSourceAsset),
                true);
            
            GUILayout.EndHorizontal();
            
            if (_languageSourceAsset == null) return;
            
            GUILayout.Space(10);
            
            //DrawSetupUI();
            
            if (_translateExtensionManager != null && _translateExtensionManager.IsContextSetup)
            {
                if (_isShowSettingsMenu)
                {
                    _aiTranslateProviderView?.Draw();
                    _promtView?.Draw();
                }
                
                GUILayout.Space(10);
                
                DrawTranslateUI();
            }
            else
            {
                DrawSetupUI();
            }
        }

        private void DrawSetupUI()
        {
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Setup"))
            {
                Setup();
            }
            
            GUILayout.EndHorizontal();
        }

        private void DrawTranslateUI()
        {
            if (GUILayout.Button(_isShowSettingsMenu? "Close Settings" : "Open Settings"))
            {
                _isShowSettingsMenu = !_isShowSettingsMenu;
            }
            
            GUILayout.Label(new GUIContent(_translateExtensionManager.GetInfo()));
            
            GUILayout.Space(10);
            
            _translateSettingsButtonView?.Draw();
            
            _translationTermsView?.Draw();
        }

        private void Setup()
        {
            var context = new I2LocAiExtensionContext();
            context.BaseLanguage = "English";
            context.PromtFactory = new PromtFactorySimple();
            
            _translateExtensionManager = new I2LocAiTranslateExtensionManager(_languageSourceAsset, context);
            
            _translateSettingsButtonView = new TranslateSettingsButtonView(this, _translateExtensionManager, _languageSourceAsset);
            _translationTermsView = new TranslationTermsView(this, _translateExtensionManager, _languageSourceAsset);
            _aiTranslateProviderView = new AiTranslateProviderView(this, _translateExtensionManager);
            _promtView = new PromtView(this, _translateExtensionManager);
        }
    }
}
