# Lexicon AI Translator

**Lexicon AI Translator** is a free, open-source tool for Skyrim mod localization.
It supports multiple mod-related file formats, including PEX, ESM, ESP, and MCM.
The tool focuses on structured string preprocessing, rule-based protection, and customizable translation workflows to reduce errors during batch translation.
With optional integration of local or online translation engines and user-defined dictionaries, Lexicon AI Translator helps translators work more efficiently while maintaining control over the final output.

If you want to give feedback, report issues, or discuss Lex Translator, please feel free to visit any of the following sites:  

- [Nexus Mods (for international users)](https://www.nexusmods.com/skyrimspecialedition/mods/143056)  

You can download it directly from Nexus Mods or build it yourself here. Both versions are kept up to date.

Your support and feedback are greatly appreciated!

---

## 📦 Features

- ✅ Support for `.pex`, `.esm`, `.esp`, and `.mcm` formats  
- 🔁 Batch processing and translation history tracking  
- 🌐 Integration with OpenAI, DeepL, and other translation APIs  
- 🧠 Heuristic filtering to avoid code-related terms being mistranslated  
- 🔧 Designed for extendability and customization

---

### Steps:

1. Clone the repository:  
   [https://github.com/YD525/PhoenixEngine](https://github.com/YD525/PhoenixEngine)

2. Open the solution in Visual Studio and build the project.

3. After building, make sure to **reference the generated DLLs** (e.g., `PhoenixEngine.dll`) in the **Lex Translator** project.  
   You can do this either by adding project references or linking the compiled DLLs directly.

This step is **mandatory** — the LexTranslator project will not build correctly without it.

---

## 🧩 Third-party Components

This project uses the following key open-source libraries/frameworks:

- [AvalonEdit](https://github.com/icsharpcode/AvalonEdit) – WPF text editor component used for code/text display.  

---


## 📜 Translation & Localization Exception

I’m happy to allow translators to localize the Lexicon AI Translator and share their work with others.  
**Therefore, I am granting a specific exception that allows translators to create and distribute localized versions of this project, strictly limited to localization purposes only.**  

Please note that this exception **does not extend to modification, redistribution, or derivative works beyond translation**.  
Executable files (`.exe`) included with the project remain the original work of the author and are governed by the **CC BY-NC-ND 4.0 license**.

---

### 🙏 Special Thanks

I would like to give special thanks to the developers of 

[Cutleast](https://github.com/Cutleast) It helped me solve some problems with reading ESP files.

[Mutagen.Bethesda](https://github.com/Mutagen-Modding/Mutagen) This framework was a huge help!

[Champollion](https://github.com/Orvid/Champollion)  This framework was a huge help!

[Noggog](https://github.com/Noggog) for helping me understand how StringsFile reads and writes.

[Cutleast](https://github.com/Cutleast), [SkyHorizon3](https://github.com/SkyHorizon3) for helping me resolve the issue with generating specific JSON fields in the DSD file.

Their excellent libraries provide Lex Translator with a stable and solid foundation, allowing us to focus more on developing the translation features.

Acknowledgements: Nexus Mods,9DM,2Game.info,and 泰姆瑞尔MOD组, for their support and encouragement that inspire my creative work.

---

# ❤️ Personal Note from the Developer

Lex Translator and SSEAT may collaborate in the future, complementing each other’s strengths and addressing their respective weaknesses.

If you find this project helpful,  
consider giving it a ⭐ star —  
your support is the driving force behind ongoing development! ❤️

Also, if you're thinking about adapting **Lex Translator** to support other games, you're totally welcome to do so!  
The `TransItem` class is designed to be generic — you can construct your own instances and plug in custom readers for other game formats.  

Just fork the project and make your own modifications — it's easy to extend.  
And of course, if you contribute something awesome, your name will be added to the list of contributors. 😊

If you have any questions or need help, feel free to drop by our Discord — I'm always happy to help:  
[https://discord.gg/GRu7WtgqsB](https://discord.gg/GRu7WtgqsB)


---

## 🖼️ UI Icon

The icon used in the UI interface (**"Note"**) is sourced from **Iconfont**.

---