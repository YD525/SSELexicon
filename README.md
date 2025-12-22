# SSE Lexicon - AI Translator

**SSE Lexicon** is a fully open-source and free tool created to assist with Skyrim mod translation. It supports multiple file formats, including PEX, ESM, ESP, and MCM, offering enhanced convenience and flexibility for translators.  
By utilizing advanced string processing and optional online translation integration, SSELex helps streamline translation tasks and improve overall efficiency.

If you want to give feedback, report issues, or discuss SSELex, please feel free to visit any of the following sites:  

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

3. After building, make sure to **reference the generated DLLs** (e.g., `PhoenixEngine.dll`) in the **SSELex** project.  
   You can do this either by adding project references or linking the compiled DLLs directly.

This step is **mandatory** — the SSELex project will not build correctly without it.

---

## 🧩 Third-party Libraries

This project makes use of the following key open-source libraries/frameworks:

- [Champollion](https://github.com/Orvid/Champollion) – developed by [Orvid], for decompiling Papyrus compiled scripts.  
- [Papyrus-compiler](https://github.com/russo-2025/papyrus-compiler/tree/master/bin/Original%20Compiler) – used to compile .pas script files.Downloading the official components is fine too. They're the same,You just need to download them and put them in the Tool\Original Compiler directory of the program. (You can also get them from CK.)

Other dependencies (such as **AvalonEdit**, various helper libraries, etc.) are also used.  
Please refer to their respective LICENSE files for further information.

---

### 🙏 Special Thanks

I would like to give special thanks to the developers of  
- [Mutagen.Bethesda](https://github.com/Mutagen-Modding/Mutagen)  

[Noggog](https://github.com/Noggog) for helping me understand how StringsFile reads and writes.

[Cutleast](https://github.com/Cutleast), [SkyHorizon3](https://github.com/SkyHorizon3) for helping me resolve the issue with generating specific JSON fields in the DSD file.

Their excellent libraries provide SSE Lexicon with a stable and solid foundation, allowing us to focus more on developing the translation features.

Acknowledgements: Nexus Mods,9DM,2Game.info,and 泰姆瑞尔MOD组, for their support and encouragement that inspire my creative work.

---

# ❤️ Personal Note from the Developer

SSELex and SSEAT may collaborate in the future, complementing each other’s strengths and addressing their respective weaknesses.

If you find this project helpful,  
consider giving it a ⭐ star —  
your support is the driving force behind ongoing development! ❤️

Also, if you're thinking about adapting **SSELex** to support other games, you're totally welcome to do so!  
The `TransItem` class is designed to be generic — you can construct your own instances and plug in custom readers for other game formats.  

Just fork the project and make your own modifications — it's easy to extend.  
And of course, if you contribute something awesome, your name will be added to the list of contributors. 😊

If you have any questions or need help, feel free to drop by our Discord — I'm always happy to help:  
[https://discord.gg/GRu7WtgqsB](https://discord.gg/GRu7WtgqsB)


---

## 🖼️ UI Icon

The icon used in the UI interface (**"Note"**) is sourced from **Iconfont**.

---