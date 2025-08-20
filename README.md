# Unity I2 Localization AI Extension
An extension for Unity I2 Localization (https://assetstore.unity.com/packages/tools/localization/i2-localization-14884) that helps translate all your labels using AI.

**Installation**
1. Install and configure the **I2 Localization** plugin in Unity.  
2. Add the languages you need in **I2Languages** and create the necessary **terms**.  
3. Choose a base language (e.g., English).  
4. Translate all terms into the base language.  
5. Install **I2AIExtension** via the Package Manager using: https://github.com/denkacn/I2AIExtension.git
6. Open the plugin window:  **Tools → I2 Localization AI Translate Extension Window**  
7. Select the **LanguageSourceAsset** you want to configure.  
8. Add an **AiTranslateProvider** (both local and external providers are supported).  
9. Add a **prompt**, if needed.  
10. Select **Source** and **Destination** languages.  
11. Click **Display Terms** to show all terms.  
12. Click **Translate All** to translate everything, or **Translate** to translate the selected term.  
13. Click **Apply Changes** to apply translations to the **LanguageSourceAsset**.  
14. You can also edit each term individually: switch to **Translated mode** and press **E**. 
