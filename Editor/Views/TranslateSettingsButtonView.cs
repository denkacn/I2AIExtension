using I2.Loc;
using I2AIExtension.Editor.Managers;
using I2AIExtension.Editor.Tools;
using UnityEditor;
using UnityEngine;

namespace I2AIExtension.Editor.Views
{
    public class TranslateSettingsButtonView : BaseExtensionView
    {
        private readonly I2LocAiTranslateExtensionManager _translateExtensionManager;
        private readonly LanguageSourceAsset _languageSourceAsset;

        public TranslateSettingsButtonView(EditorWindow owner, I2LocAiTranslateExtensionManager translateExtensionManager, LanguageSourceAsset languageSourceAsset) : base(owner)
        {
            _translateExtensionManager = translateExtensionManager;
            _languageSourceAsset = languageSourceAsset;
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal();
            
            var optionsArray = _translateExtensionManager.GetAvailableLanguages().ToArray();

            if (optionsArray.Length > 0)
            {
                var selectedSourceLanguageIndex = UiTools.GetIndexForValue(_translateExtensionManager.SourceLanguage, optionsArray);
                selectedSourceLanguageIndex = EditorGUILayout.Popup("Source", selectedSourceLanguageIndex, optionsArray);
                _translateExtensionManager.SourceLanguage = optionsArray[selectedSourceLanguageIndex];
            
                var selectedDestinationLanguageIndex = UiTools.GetIndexForValue(_translateExtensionManager.DestinationLanguage, optionsArray);
                selectedDestinationLanguageIndex = EditorGUILayout.Popup("Destination", selectedDestinationLanguageIndex, optionsArray);
                _translateExtensionManager.DestinationLanguage = optionsArray[selectedDestinationLanguageIndex];
            
                if (GUILayout.Button("Display Terms"))
                {
                    _translateExtensionManager.GetTranslationTerms();
                }

                if (_translateExtensionManager.IsTranslateProviderAndTranslateSettingSetup)
                {
                    if (_translateExtensionManager.IsTranslateAllStarted)
                    {
                        if (GUILayout.Button("Stop Translate Process"))
                        {
                            _translateExtensionManager.StopTranslateProcess();
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Translate All"))
                        {
                            var result = UiTools.DisplayTranslateAllDialog();

                            if (result) _translateExtensionManager.TranslateAll(Repaint);
                        }
                    }
                    
                    if (GUILayout.Button("Apply Changes"))
                    {
                        var result = UiTools.DisplayApplyChangesDialog();

                        if (result)
                        {
                            if (_translateExtensionManager.ApplyChanges())
                            {
                                EditorUtility.SetDirty(_languageSourceAsset);
                                AssetDatabase.SaveAssets();
                            }
                        }
                    }
                }
            }
            
            GUILayout.EndHorizontal();
        }
    }
}