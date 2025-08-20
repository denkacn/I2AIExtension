# Unity I2 Localization AI Extension
An extension for Unity I2 Localization (https://assetstore.unity.com/packages/tools/localization/i2-localization-14884) that helps translate all your labels using AI.

### Installation
1. Install and configure the **I2 Localization** plugin in Unity.  
2. Add the languages you need in **I2Languages** and create the necessary **terms**.  
3. Choose a base language (e.g., English).  
4. Translate all terms into the base language.  
5. Install **I2AIExtension** via the Package Manager using: https://github.com/denkacn/I2AIExtension.git
6. Open the plugin window:  **Tools → I2 Localization AI Translate Extension Window**  
7. Select the **LanguageSourceAsset** you want to configure.  
8. Add an **AiTranslateProvider** (both local and external providers are supported, Open AI, Ollama, LM Studio) and select it.  
9. Add a **prompt**, if needed and select it.  
10. Select **Source** and **Destination** languages.  
11. Click **Display Terms** to show all terms.  
12. Click **Translate All** to translate everything, or **Translate** to translate the selected term.  
13. Click **Apply Changes** to apply translations to the **LanguageSourceAsset**.  
14. You can also edit each term individually: switch to **Translated mode** and press **E**.

### Providers

You will need to provide some data to configure the provider.

**Example: Local LM Studio**
- **Host**: `http://127.0.0.1:1234`  
- **Endpoint**: `/v1/chat/completions`  
- **Token**: *(if not a local server, you will need to specify it)*  
- **Model**: `qwen/qwen3-14b` *(works quite well for translations into different languages)*  

**Example: Local OpenAI**
- **Host**: `https://api.openai.com`  
- **Endpoint**: `/v1/responses`  
- **Token**: *insert your API key from the admin panel here*  
- **Model**: `gpt-5`  

Save Path -> `Assets/Editor/TranslateProviderSettings.json`

### Promts

A **prompt** allows you to customize the translation — for example, you can set a strict style, a more cheerful tone, or even add references to *Star Wars*.  

The default prompt looks like this:  
`Translate fully the following into {0} and return ONLY the translations in the same format, be case-sensitive, consider line breaks, inside curly braces, with nothing else in the output:\n{1}`

However, you can customize it as you wish.

Save Path -> `Assets/Editor/PromtSettings.json`
