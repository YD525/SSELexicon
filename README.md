# SSE Lexicon

**SSELex** is a fully open-source and free tool created to assist with Skyrim mod translation. It supports multiple file formats, including PEX, ESM, ESP, and MCM, offering enhanced convenience and flexibility for translators.  
By utilizing advanced string processing and optional online translation integration, SSELex helps streamline translation tasks and improve overall efficiency.

If you want to give feedback, report issues, or discuss SSELex, please feel free to visit any of the following sites:  

- [Nexus Mods (for international users)](https://www.nexusmods.com/skyrimspecialedition/mods/143056)  

Your support and feedback are greatly appreciated!

---

## 📄 License

This project is licensed under the **GNU General Public License version 3.0 (GPL-3.0-only)**.  
See the LICENSE file for details.

---

## 🧩 Third-party Libraries

This project makes use of the following key open-source libraries/frameworks:

- [Mutagen.Bethesda](https://github.com/Mutagen-Modding/Mutagen) – developed by [Mutagen], for reading and writing Bethesda plugin files (.esp and .esm).  
- [Champollion](https://github.com/Orvid/Champollion) – developed by [Orvid], for decompiling Papyrus compiled scripts.  
- [Papyrus-compiler](https://github.com/russo-2025/papyrus-compiler) – used to compile .pas script files.

Other dependencies (such as **AvalonEdit**, various helper libraries, etc.) are also used.  
Please refer to their respective LICENSE files for further information.

---

### 🙏 Special Thanks

I would like to give special thanks to the developers of  
- [Mutagen.Bethesda](https://github.com/Mutagen-Modding/Mutagen)  

[Cutleast](https://github.com/Cutleast),[SkyHorizon3](https://github.com/SkyHorizon3)  for helping me resolve the issue with generating specific JSON fields in the DSD file.

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

By the way, this entire tool is currently developed solely by me — **YD525**.  
While it’s built on top of excellent open-source frameworks like Mutagen and Champollion,  
the tool itself (logic, UI, workflow, translators, etc.) is written entirely by me.  
It’s a lot of work, and yes... I’m tired. 😩  
Your encouragement really means a lot!

---

## 🖼️ UI Icon

The icon used in the UI interface (**"Note"**) is sourced from **Iconfont**.

---

## 💬 Community & Contribution

Join our Discord community: [https://discord.gg/GRu7WtgqsB](https://discord.gg/GRu7WtgqsB)  

Feel free to drop by and chat — always happy to talk code (or just vent boredom).

## ⚠️ Notice on Original Context Generation Technology

The context generation mechanism, including fine‑grained term segmentation and constraint‑based translation workflows,
implemented in many modern AI‑assisted translation tools has been independently developed by various projects.

It has come to our attention that certain third‑party projects have incorporated or referenced similar context generation
and terminology consistency techniques without proper attribution to the original source.

If you are using or adapting solutions inspired by this repository or other foundational work in this domain,
please ensure appropriate credit is given and that usage complies with the respective project licenses.

For clarity: “context generation” in this context refers to mechanisms for extracting, structuring,
and feeding term‑level or phrase‑level data into AI translation workflows to achieve consistent translation outputs.

---
