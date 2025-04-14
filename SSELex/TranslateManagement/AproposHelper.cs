using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using SSELex.SkyrimModManager;

namespace SSELex.TranslateCore
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class AproposHelper
    {
        public static void TranslatePath(ProgressBar OneBar, string FilePath, string Suffix = ".txt")
        {
            new Thread(() => {

                int Sucess = 0;
                var GetFiles = DataHelper.GetAllFile(FilePath, new List<string>() { Suffix });

                OneBar.Dispatcher.Invoke(new Action(() => {
                    OneBar.Maximum = GetFiles.Count;
                }));


                foreach (var Get in GetFiles)
                {
                    string GetContent = DataHelper.ReadFileByStr(Get.FilePath, Encoding.UTF8);

                    if (GetContent == "")
                    {
                        GC.Collect();
                    }

                    GetContent = ProcessAproposCode(Get.FileName, GetContent);
                    DataHelper.WriteFile(Get.FilePath, Encoding.UTF8.GetBytes(GetContent));
                    if (GetContent.Contains("(") || GetContent.Contains("（"))
                    {

                    }
                    Sucess++;

                    OneBar.Dispatcher.Invoke(new Action(() => {
                        OneBar.Value = Sucess;
                    }));
                }

            }).Start();
        }

        public static string GetAproposTranslate(string Content)
        {
            ACodeParse OneACodeParse = new ACodeParse(Content);
            List<EngineProcessItem> EngineProcessItems = new List<EngineProcessItem>();
            string GetCN = new WordProcess().ProcessWords(ref EngineProcessItems, OneACodeParse.Content, DeFine.SourceLanguage, DeFine.TargetLanguage);
            return OneACodeParse.UsingCode(GetCN);
        }
        public static string ProcessAproposCode(string FileName, string Content)
        {
            if (FileName == "Synonyms.txt")
            {
                SynonymsItem GetSynonyms = JsonSerializer.Deserialize<SynonymsItem>(Content);

                var Options = new JsonSerializerOptions
                {
                    WriteIndented = true // 格式化输出，使JSON易于阅读
                };

                string GetJson = JsonSerializer.Serialize(GetSynonyms, Options);

                return GetJson;//Test

                for (int i = 0; i < GetSynonyms.ACCEPT.Length; i++)
                {
                    GetSynonyms.ACCEPT[i] = GetAproposTranslate(GetSynonyms.ACCEPT[i]);
                }

                for (int i = 0; i < GetSynonyms.ACCEPTING.Length; i++)
                {
                    GetSynonyms.ACCEPTING[i] = GetAproposTranslate(GetSynonyms.ACCEPTING[i]);
                }

                for (int i = 0; i < GetSynonyms.ACCEPTS.Length; i++)
                {
                    GetSynonyms.ACCEPTS[i] = GetAproposTranslate(GetSynonyms.ACCEPTS[i]);
                }

                for (int i = 0; i < GetSynonyms.ASS.Length; i++)
                {
                    GetSynonyms.ASS[i] = GetAproposTranslate(GetSynonyms.ASS[i]);
                }

                for (int i = 0; i < GetSynonyms.BEAST.Length; i++)
                {
                    GetSynonyms.BEAST[i] = GetAproposTranslate(GetSynonyms.BEAST[i]);
                }

                for (int i = 0; i < GetSynonyms.BEASTCOCK.Length; i++)
                {
                    GetSynonyms.BEASTCOCK[i] = GetAproposTranslate(GetSynonyms.BEASTCOCK[i]);
                }

                for (int i = 0; i < GetSynonyms.BITCH.Length; i++)
                {
                    GetSynonyms.BITCH[i] = GetAproposTranslate(GetSynonyms.BITCH[i]);
                }
                for (int i = 0; i < GetSynonyms.BOOBS.Length; i++)
                {
                    GetSynonyms.BOOBS[i] = GetAproposTranslate(GetSynonyms.BOOBS[i]);
                }

                for (int i = 0; i < GetSynonyms.BREED.Length; i++)
                {
                    GetSynonyms.BREED[i] = GetAproposTranslate(GetSynonyms.BREED[i]);
                }

                for (int i = 0; i < GetSynonyms.BUG.Length; i++)
                {
                    GetSynonyms.BUG[i] = GetAproposTranslate(GetSynonyms.BUG[i]);
                }

                for (int i = 0; i < GetSynonyms.BUGCOCK.Length; i++)
                {
                    GetSynonyms.BUGCOCK[i] = GetAproposTranslate(GetSynonyms.BUGCOCK[i]);
                }

                for (int i = 0; i < GetSynonyms.BUTTOCKS.Length; i++)
                {
                    GetSynonyms.BUTTOCKS[i] = GetAproposTranslate(GetSynonyms.BUTTOCKS[i]);
                }

                for (int i = 0; i < GetSynonyms.COCK.Length; i++)
                {
                    GetSynonyms.COCK[i] = GetAproposTranslate(GetSynonyms.COCK[i]);
                }

                for (int i = 0; i < GetSynonyms.CREAM.Length; i++)
                {
                    GetSynonyms.CREAM[i] = GetAproposTranslate(GetSynonyms.CREAM[i]);
                }

                for (int i = 0; i < GetSynonyms.CUM.Length; i++)
                {
                    GetSynonyms.CUM[i] = GetAproposTranslate(GetSynonyms.CUM[i]);
                }

                for (int i = 0; i < GetSynonyms.CUMMING.Length; i++)
                {
                    GetSynonyms.CUMMING[i] = GetAproposTranslate(GetSynonyms.CUMMING[i]);
                }

                for (int i = 0; i < GetSynonyms.CUMS.Length; i++)
                {
                    GetSynonyms.CUMS[i] = GetAproposTranslate(GetSynonyms.CUMS[i]);
                }

                for (int i = 0; i < GetSynonyms.DEAD.Length; i++)
                {
                    GetSynonyms.DEAD[i] = GetAproposTranslate(GetSynonyms.DEAD[i]);
                }

                for (int i = 0; i < GetSynonyms.EXPLORE.Length; i++)
                {
                    GetSynonyms.EXPLORE[i] = GetAproposTranslate(GetSynonyms.EXPLORE[i]);
                }

                for (int i = 0; i < GetSynonyms.EXPOSE.Length; i++)
                {
                    GetSynonyms.EXPOSE[i] = GetAproposTranslate(GetSynonyms.EXPOSE[i]);
                }

                for (int i = 0; i < GetSynonyms.FEAR.Length; i++)
                {
                    GetSynonyms.FEAR[i] = GetAproposTranslate(GetSynonyms.FEAR[i]);
                }

                for (int i = 0; i < GetSynonyms.FFAMILY.Length; i++)
                {
                    GetSynonyms.FFAMILY[i] = GetAproposTranslate(GetSynonyms.FFAMILY[i]);
                }

                for (int i = 0; i < GetSynonyms.FOREIGN.Length; i++)
                {
                    GetSynonyms.FOREIGN[i] = GetAproposTranslate(GetSynonyms.FOREIGN[i]);
                }

                for (int i = 0; i < GetSynonyms.FUCK.Length; i++)
                {
                    GetSynonyms.FUCK[i] = GetAproposTranslate(GetSynonyms.FUCK[i]);
                }

                for (int i = 0; i < GetSynonyms.FUCKED.Length; i++)
                {
                    GetSynonyms.FUCKED[i] = GetAproposTranslate(GetSynonyms.FUCKED[i]);
                }

                for (int i = 0; i < GetSynonyms.FUCKING.Length; i++)
                {
                    GetSynonyms.FUCKING[i] = GetAproposTranslate(GetSynonyms.FUCKING[i]);
                }

                for (int i = 0; i < GetSynonyms.FUCKS.Length; i++)
                {
                    GetSynonyms.FUCKS[i] = GetAproposTranslate(GetSynonyms.FUCKS[i]);
                }

                for (int i = 0; i < GetSynonyms.GENWT.Length; i++)
                {
                    GetSynonyms.GENWT[i] = GetAproposTranslate(GetSynonyms.GENWT[i]);
                }

                for (int i = 0; i < GetSynonyms.GIRTH.Length; i++)
                {
                    GetSynonyms.GIRTH[i] = GetAproposTranslate(GetSynonyms.GIRTH[i]);
                }

                for (int i = 0; i < GetSynonyms.HEAVING.Length; i++)
                {
                    GetSynonyms.HEAVING[i] = GetAproposTranslate(GetSynonyms.HEAVING[i]);
                }

                for (int i = 0; i < GetSynonyms.HOLE.Length; i++)
                {
                    GetSynonyms.HOLE[i] = GetAproposTranslate(GetSynonyms.HOLE[i]);
                }

                for (int i = 0; i < GetSynonyms.HOLES.Length; i++)
                {
                    GetSynonyms.HOLES[i] = GetAproposTranslate(GetSynonyms.HOLES[i]);
                }

                for (int i = 0; i < GetSynonyms.HORNY.Length; i++)
                {
                    GetSynonyms.HORNY[i] = GetAproposTranslate(GetSynonyms.HORNY[i]);
                }

                for (int i = 0; i < GetSynonyms.HUGE.Length; i++)
                {
                    GetSynonyms.HUGE[i] = GetAproposTranslate(GetSynonyms.HUGE[i]);
                }

                for (int i = 0; i < GetSynonyms.HUGELOAD.Length; i++)
                {
                    GetSynonyms.HUGELOAD[i] = GetAproposTranslate(GetSynonyms.HUGELOAD[i]);
                }

                for (int i = 0; i < GetSynonyms.INSERT.Length; i++)
                {
                    GetSynonyms.INSERT[i] = GetAproposTranslate(GetSynonyms.INSERT[i]);
                }

                for (int i = 0; i < GetSynonyms.INSERTED.Length; i++)
                {
                    GetSynonyms.INSERTED[i] = GetAproposTranslate(GetSynonyms.INSERTED[i]);
                }

                for (int i = 0; i < GetSynonyms.INSERTING.Length; i++)
                {
                    GetSynonyms.INSERTING[i] = GetAproposTranslate(GetSynonyms.INSERTING[i]);
                }

                for (int i = 0; i < GetSynonyms.INSERTS.Length; i++)
                {
                    GetSynonyms.INSERTS[i] = GetAproposTranslate(GetSynonyms.INSERTS[i]);
                }

                for (int i = 0; i < GetSynonyms.JIGGLE.Length; i++)
                {
                    GetSynonyms.JIGGLE[i] = GetAproposTranslate(GetSynonyms.JIGGLE[i]);
                }

                for (int i = 0; i < GetSynonyms.JUICY.Length; i++)
                {
                    GetSynonyms.JUICY[i] = GetAproposTranslate(GetSynonyms.JUICY[i]);
                }

                for (int i = 0; i < GetSynonyms.LARGELOAD.Length; i++)
                {
                    GetSynonyms.LARGELOAD[i] = GetAproposTranslate(GetSynonyms.LARGELOAD[i]);
                }

                for (int i = 0; i < GetSynonyms.LOUDLY.Length; i++)
                {
                    GetSynonyms.LOUDLY[i] = GetAproposTranslate(GetSynonyms.LOUDLY[i]);
                }

                for (int i = 0; i < GetSynonyms.MACHINE.Length; i++)
                {
                    GetSynonyms.MACHINE[i] = GetAproposTranslate(GetSynonyms.MACHINE[i]);
                }

                for (int i = 0; i < GetSynonyms.MACHINESLIME.Length; i++)
                {
                    GetSynonyms.MACHINESLIME[i] = GetAproposTranslate(GetSynonyms.MACHINESLIME[i]);
                }

                for (int i = 0; i < GetSynonyms.MACHINESLIMY.Length; i++)
                {
                    GetSynonyms.MACHINESLIMY[i] = GetAproposTranslate(GetSynonyms.MACHINESLIMY[i]);
                }

                for (int i = 0; i < GetSynonyms.METAL.Length; i++)
                {
                    GetSynonyms.METAL[i] = GetAproposTranslate(GetSynonyms.METAL[i]);
                }

                for (int i = 0; i < GetSynonyms.MFAMILY.Length; i++)
                {
                    GetSynonyms.MFAMILY[i] = GetAproposTranslate(GetSynonyms.MFAMILY[i]);
                }

                for (int i = 0; i < GetSynonyms.MNONFAMILY.Length; i++)
                {
                    GetSynonyms.MNONFAMILY[i] = GetAproposTranslate(GetSynonyms.MNONFAMILY[i]);
                }

                for (int i = 0; i < GetSynonyms.MOAN.Length; i++)
                {
                    GetSynonyms.MOAN[i] = GetAproposTranslate(GetSynonyms.MOAN[i]);
                }

                for (int i = 0; i < GetSynonyms.MOANING.Length; i++)
                {
                    GetSynonyms.MOANING[i] = GetAproposTranslate(GetSynonyms.MOANING[i]);
                }

                for (int i = 0; i < GetSynonyms.MOANS.Length; i++)
                {
                    GetSynonyms.MOANS[i] = GetAproposTranslate(GetSynonyms.MOANS[i]);
                }

                for (int i = 0; i < GetSynonyms.MOUTH.Length; i++)
                {
                    GetSynonyms.MOUTH[i] = GetAproposTranslate(GetSynonyms.MOUTH[i]);
                }

                for (int i = 0; i < GetSynonyms.OPENING.Length; i++)
                {
                    GetSynonyms.OPENING[i] = GetAproposTranslate(GetSynonyms.OPENING[i]);
                }

                for (int i = 0; i < GetSynonyms.PAIN.Length; i++)
                {
                    GetSynonyms.PAIN[i] = GetAproposTranslate(GetSynonyms.PAIN[i]);
                }

                for (int i = 0; i < GetSynonyms.PENIS.Length; i++)
                {
                    GetSynonyms.PENIS[i] = GetAproposTranslate(GetSynonyms.PENIS[i]);
                }

                for (int i = 0; i < GetSynonyms.PROBE.Length; i++)
                {
                    GetSynonyms.PROBE[i] = GetAproposTranslate(GetSynonyms.PROBE[i]);
                }

                for (int i = 0; i < GetSynonyms.PUSSY.Length; i++)
                {
                    GetSynonyms.PUSSY[i] = GetAproposTranslate(GetSynonyms.PUSSY[i]);
                }

                for (int i = 0; i < GetSynonyms.QUIVERING.Length; i++)
                {
                    GetSynonyms.QUIVERING[i] = GetAproposTranslate(GetSynonyms.QUIVERING[i]);
                }

                for (int i = 0; i < GetSynonyms.RAPE.Length; i++)
                {
                    GetSynonyms.RAPE[i] = GetAproposTranslate(GetSynonyms.RAPE[i]);
                }

                for (int i = 0; i < GetSynonyms.RAPED.Length; i++)
                {
                    GetSynonyms.RAPED[i] = GetAproposTranslate(GetSynonyms.RAPED[i]);
                }

                for (int i = 0; i < GetSynonyms.SALTY.Length; i++)
                {
                    GetSynonyms.SALTY[i] = GetAproposTranslate(GetSynonyms.SALTY[i]);
                }

                for (int i = 0; i < GetSynonyms.SCREAM.Length; i++)
                {
                    GetSynonyms.SCREAM[i] = GetAproposTranslate(GetSynonyms.SCREAM[i]);
                }

                for (int i = 0; i < GetSynonyms.SCREAMS.Length; i++)
                {
                    GetSynonyms.SCREAMS[i] = GetAproposTranslate(GetSynonyms.SCREAMS[i]);
                }

                for (int i = 0; i < GetSynonyms.SCUM.Length; i++)
                {
                    GetSynonyms.SCUM[i] = GetAproposTranslate(GetSynonyms.SCUM[i]);
                }

                for (int i = 0; i < GetSynonyms.SLIME.Length; i++)
                {
                    GetSynonyms.SLIME[i] = GetAproposTranslate(GetSynonyms.SLIME[i]);
                }

                for (int i = 0; i < GetSynonyms.SLIMY.Length; i++)
                {
                    GetSynonyms.SLIMY[i] = GetAproposTranslate(GetSynonyms.SLIMY[i]);
                }

                for (int i = 0; i < GetSynonyms.SLOPPY.Length; i++)
                {
                    GetSynonyms.SLOPPY[i] = GetAproposTranslate(GetSynonyms.SLOPPY[i]);
                }

                for (int i = 0; i < GetSynonyms.SLOWLY.Length; i++)
                {
                    GetSynonyms.SLOWLY[i] = GetAproposTranslate(GetSynonyms.SLOWLY[i]);
                }

                for (int i = 0; i < GetSynonyms.SLUTTY.Length; i++)
                {
                    GetSynonyms.SLUTTY[i] = GetAproposTranslate(GetSynonyms.SLUTTY[i]);
                }

                for (int i = 0; i < GetSynonyms.SODOMIZE.Length; i++)
                {
                    GetSynonyms.SODOMIZE[i] = GetAproposTranslate(GetSynonyms.SODOMIZE[i]);
                }

                for (int i = 0; i < GetSynonyms.SODOMIZED.Length; i++)
                {
                    GetSynonyms.SODOMIZED[i] = GetAproposTranslate(GetSynonyms.SODOMIZED[i]);
                }

                for (int i = 0; i < GetSynonyms.SODOMIZES.Length; i++)
                {
                    GetSynonyms.SODOMIZES[i] = GetAproposTranslate(GetSynonyms.SODOMIZES[i]);
                }

                for (int i = 0; i < GetSynonyms.SODOMIZING.Length; i++)
                {
                    GetSynonyms.SODOMIZING[i] = GetAproposTranslate(GetSynonyms.SODOMIZING[i]);
                }

                for (int i = 0; i < GetSynonyms.SODOMY.Length; i++)
                {
                    GetSynonyms.SODOMY[i] = GetAproposTranslate(GetSynonyms.SODOMY[i]);
                }

                for (int i = 0; i < GetSynonyms.SOLID.Length; i++)
                {
                    GetSynonyms.SOLID[i] = GetAproposTranslate(GetSynonyms.SOLID[i]);
                }

                for (int i = 0; i < GetSynonyms.STRAPON.Length; i++)
                {
                    GetSynonyms.STRAPON[i] = GetAproposTranslate(GetSynonyms.STRAPON[i]);
                }

                for (int i = 0; i < GetSynonyms.SUBMISSIVE.Length; i++)
                {
                    GetSynonyms.SUBMISSIVE[i] = GetAproposTranslate(GetSynonyms.SUBMISSIVE[i]);
                }

                for (int i = 0; i < GetSynonyms.SUBMIT.Length; i++)
                {
                    GetSynonyms.SUBMIT[i] = GetAproposTranslate(GetSynonyms.SUBMIT[i]);
                }

                for (int i = 0; i < GetSynonyms.SWEARING.Length; i++)
                {
                    GetSynonyms.SWEARING[i] = GetAproposTranslate(GetSynonyms.SWEARING[i]);
                }

                for (int i = 0; i < GetSynonyms.TASTY.Length; i++)
                {
                    GetSynonyms.TASTY[i] = GetAproposTranslate(GetSynonyms.TASTY[i]);
                }

                for (int i = 0; i < GetSynonyms.THICK.Length; i++)
                {
                    GetSynonyms.THICK[i] = GetAproposTranslate(GetSynonyms.THICK[i]);
                }

                for (int i = 0; i < GetSynonyms.TIGHTNESS.Length; i++)
                {
                    GetSynonyms.TIGHTNESS[i] = GetAproposTranslate(GetSynonyms.TIGHTNESS[i]);
                }

                for (int i = 0; i < GetSynonyms.UNTHINKING.Length; i++)
                {
                    GetSynonyms.UNTHINKING[i] = GetAproposTranslate(GetSynonyms.UNTHINKING[i]);
                }

                for (int i = 0; i < GetSynonyms.VILE.Length; i++)
                {
                    GetSynonyms.VILE[i] = GetAproposTranslate(GetSynonyms.VILE[i]);
                }

                for (int i = 0; i < GetSynonyms.WET.Length; i++)
                {
                    GetSynonyms.WET[i] = GetAproposTranslate(GetSynonyms.WET[i]);
                }

                for (int i = 0; i < GetSynonyms.WHORE.Length; i++)
                {
                    GetSynonyms.WHORE[i] = GetAproposTranslate(GetSynonyms.WHORE[i]);
                }

                //if (GetJson.Replace(" ", "").Replace("\r\n", "") == Content.Replace(" ", "").Replace("\r\n", "").Replace("\t", ""))
                //{
                //    GC.Collect();
                //}
                return GetJson;
            }
            else
            if (FileName == "WearAndTear_Descriptors.txt")
            {
                WearAndTearItem GetWearAndTear = JsonSerializer.Deserialize<WearAndTearItem>(Content);


                string GetJson = JsonSerializer.Serialize(GetWearAndTear, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                //if (GetJson.Replace(" ", "").Replace("\r\n", "") == Content.Replace(" ", "").Replace("\r\n", "").Replace("\t", ""))
                //{
                //    GC.Collect();
                //}
                return GetJson;
            }
            else
            if (FileName == "Arousal_Descriptors.txt")
            {
                ArousalItem GetArousal = JsonSerializer.Deserialize<ArousalItem>(Content);

                //Process

                string GetJson =JsonSerializer.Serialize(GetArousal, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                //if (GetJson.Replace(" ", "").Replace("\r\n", "") == Content.Replace(" ", "").Replace("\r\n", "").Replace("\t", ""))
                //{
                //    GC.Collect();
                //}
                return GetJson;
            }
            else
            {
                if (!Content.Contains("1st Person"))
                {
                    return Content;
                }
            }


            AproposItem GetApropos =JsonSerializer.Deserialize<AproposItem>(Content);

            if (GetApropos._1stPerson != null)
                for (int i = 0; i < GetApropos._1stPerson.Length; i++)
                {
                    string GetLine = GetApropos._1stPerson[i];

                    GetApropos._1stPerson[i] = GetAproposTranslate(GetLine);
                }

            if (GetApropos._2ndPerson != null)
                for (int i = 0; i < GetApropos._2ndPerson.Length; i++)
                {
                    string GetLine = GetApropos._2ndPerson[i];

                    GetApropos._2ndPerson[i] = GetAproposTranslate(GetLine);
                }

            if (GetApropos._3rdPerson != null)
                for (int i = 0; i < GetApropos._3rdPerson.Length; i++)
                {
                    string GetLine = GetApropos._3rdPerson[i];

                    GetApropos._3rdPerson[i] = GetAproposTranslate(GetLine);
                }
            
            
            string GetJsonA = JsonSerializer.Serialize(GetApropos, new JsonSerializerOptions
            {
                WriteIndented = true 
            });

            //if (GetJsonA.Replace(" ", "").Replace("\r\n", "") == Content.Replace(" ", "").Replace("\r\n", "").Replace("\t", ""))
            //{
            //    GC.Collect();
            //}

            return GetJsonA;
        }
    }


    public class AproposItem
    {
        [JsonPropertyName("1st Person")]
        public string[] _1stPerson { get; set; }

        [JsonPropertyName("2nd Person")]
        public string[] _2ndPerson { get; set; }

        [JsonPropertyName("3rd Person")]
        public string[] _3rdPerson { get; set; }
    }



    public class SynonymsItem
    {
        [JsonPropertyName("{ACCEPTS}")]
        public string[] ACCEPTS { get; set; }

        [JsonPropertyName("{ACCEPT}")]
        public string[] ACCEPT { get; set; }

        [JsonPropertyName("{ACCEPTING}")]
        public string[] ACCEPTING { get; set; }

        [JsonPropertyName("{ASS}")]
        public string[] ASS { get; set; }

        [JsonPropertyName("{BEASTCOCK}")]
        public string[] BEASTCOCK { get; set; }

        [JsonPropertyName("{BEAST}")]
        public string[] BEAST { get; set; }

        [JsonPropertyName("{BITCH}")]
        public string[] BITCH { get; set; }

        [JsonPropertyName("{BOOBS}")]
        public string[] BOOBS { get; set; }

        [JsonPropertyName("{BREED}")]
        public string[] BREED { get; set; }

        [JsonPropertyName("{BUGCOCK}")]
        public string[] BUGCOCK { get; set; }

        [JsonPropertyName("{BUG}")]
        public string[] BUG { get; set; }

        [JsonPropertyName("{BUTTOCKS}")]
        public string[] BUTTOCKS { get; set; }

        [JsonPropertyName("{COCK}")]
        public string[] COCK { get; set; }

        [JsonPropertyName("{CREAM}")]
        public string[] CREAM { get; set; }

        [JsonPropertyName("{CUMMING}")]
        public string[] CUMMING { get; set; }

        [JsonPropertyName("{CUMS}")]
        public string[] CUMS { get; set; }

        [JsonPropertyName("{CUM}")]
        public string[] CUM { get; set; }

        [JsonPropertyName("{DEAD}")]
        public string[] DEAD { get; set; }

        [JsonPropertyName("{EXPLORE}")]
        public string[] EXPLORE { get; set; }

        [JsonPropertyName("{EXPOSE}")]
        public string[] EXPOSE { get; set; }

        [JsonPropertyName("{FEAR}")]
        public string[] FEAR { get; set; }

        [JsonPropertyName("{FFAMILY}")]
        public string[] FFAMILY { get; set; }

        [JsonPropertyName("{FOREIGN}")]
        public string[] FOREIGN { get; set; }

        [JsonPropertyName("{FUCKED}")]
        public string[] FUCKED { get; set; }

        [JsonPropertyName("{FUCKING}")]
        public string[] FUCKING { get; set; }

        [JsonPropertyName("{FUCKS}")]
        public string[] FUCKS { get; set; }

        [JsonPropertyName("{FUCK}")]
        public string[] FUCK { get; set; }

        [JsonPropertyName("{GENWT}")]
        public string[] GENWT { get; set; }

        [JsonPropertyName("{GIRTH}")]
        public string[] GIRTH { get; set; }

        [JsonPropertyName("{HEAVING}")]
        public string[] HEAVING { get; set; }

        [JsonPropertyName("{HOLE}")]
        public string[] HOLE { get; set; }

        [JsonPropertyName("{HOLES}")]
        public string[] HOLES { get; set; }

        [JsonPropertyName("{HORNY}")]
        public string[] HORNY { get; set; }

        [JsonPropertyName("{HUGELOAD}")]
        public string[] HUGELOAD { get; set; }

        [JsonPropertyName("{HUGE}")]
        public string[] HUGE { get; set; }

        [JsonPropertyName("{INSERT}")]
        public string[] INSERT { get; set; }

        [JsonPropertyName("{INSERTS}")]
        public string[] INSERTS { get; set; }

        [JsonPropertyName("{INSERTED}")]
        public string[] INSERTED { get; set; }

        [JsonPropertyName("{INSERTING}")]
        public string[] INSERTING { get; set; }

        [JsonPropertyName("{JIGGLE}")]
        public string[] JIGGLE { get; set; }

        [JsonPropertyName("{JUICY}")]
        public string[] JUICY { get; set; }

        [JsonPropertyName("{LARGELOAD}")]
        public string[] LARGELOAD { get; set; }

        [JsonPropertyName("{LOUDLY}")]
        public string[] LOUDLY { get; set; }

        [JsonPropertyName("{MACHINESLIME}")]
        public string[] MACHINESLIME { get; set; }

        [JsonPropertyName("{MACHINESLIMY}")]
        public string[] MACHINESLIMY { get; set; }

        [JsonPropertyName("{MACHINE}")]
        public string[] MACHINE { get; set; }

        [JsonPropertyName("{METAL}")]
        public string[] METAL { get; set; }

        [JsonPropertyName("{MFAMILY}")]
        public string[] MFAMILY { get; set; }

        [JsonPropertyName("{MNONFAMILY}")]
        public string[] MNONFAMILY { get; set; }

        [JsonPropertyName("{MOANING}")]
        public string[] MOANING { get; set; }

        [JsonPropertyName("{MOANS}")]
        public string[] MOANS { get; set; }

        [JsonPropertyName("{MOAN}")]
        public string[] MOAN { get; set; }

        [JsonPropertyName("{MOUTH}")]
        public string[] MOUTH { get; set; }

        [JsonPropertyName("{OPENING}")]
        public string[] OPENING { get; set; }

        [JsonPropertyName("{PAIN}")]
        public string[] PAIN { get; set; }

        [JsonPropertyName("{PENIS}")]
        public string[] PENIS { get; set; }

        [JsonPropertyName("{PROBE}")]
        public string[] PROBE { get; set; }

        [JsonPropertyName("{PUSSY}")]
        public string[] PUSSY { get; set; }

        [JsonPropertyName("{QUIVERING}")]
        public string[] QUIVERING { get; set; }

        [JsonPropertyName("{RAPED}")]
        public string[] RAPED { get; set; }

        [JsonPropertyName("{RAPE}")]
        public string[] RAPE { get; set; }

        [JsonPropertyName("{SALTY}")]
        public string[] SALTY { get; set; }

        [JsonPropertyName("{SCREAM}")]
        public string[] SCREAM { get; set; }

        [JsonPropertyName("{SCREAMS}")]
        public string[] SCREAMS { get; set; }

        [JsonPropertyName("{SCUM}")]
        public string[] SCUM { get; set; }

        [JsonPropertyName("{SLIME}")]
        public string[] SLIME { get; set; }

        [JsonPropertyName("{SLIMY}")]
        public string[] SLIMY { get; set; }

        [JsonPropertyName("{SLOPPY}")]
        public string[] SLOPPY { get; set; }

        [JsonPropertyName("{SLOWLY}")]
        public string[] SLOWLY { get; set; }

        [JsonPropertyName("{SLUTTY}")]
        public string[] SLUTTY { get; set; }

        [JsonPropertyName("{SODOMIZED}")]
        public string[] SODOMIZED { get; set; }

        [JsonPropertyName("{SODOMIZES}")]
        public string[] SODOMIZES { get; set; }

        [JsonPropertyName("{SODOMIZE}")]
        public string[] SODOMIZE { get; set; }

        [JsonPropertyName("{SODOMIZING}")]
        public string[] SODOMIZING { get; set; }

        [JsonPropertyName("{SODOMY}")]
        public string[] SODOMY { get; set; }

        [JsonPropertyName("{SOLID}")]
        public string[] SOLID { get; set; }

        [JsonPropertyName("{STRAPON}")]
        public string[] STRAPON { get; set; }

        [JsonPropertyName("{SUBMISSIVE}")]
        public string[] SUBMISSIVE { get; set; }

        [JsonPropertyName("{SUBMIT}")]
        public string[] SUBMIT { get; set; }

        [JsonPropertyName("{SWEARING}")]
        public string[] SWEARING { get; set; }

        [JsonPropertyName("{TASTY}")]
        public string[] TASTY { get; set; }

        [JsonPropertyName("{THICK}")]
        public string[] THICK { get; set; }

        [JsonPropertyName("{TIGHTNESS}")]
        public string[] TIGHTNESS { get; set; }

        [JsonPropertyName("{UNTHINKING}")]
        public string[] UNTHINKING { get; set; }

        [JsonPropertyName("{VILE}")]
        public string[] VILE { get; set; }

        [JsonPropertyName("{WET}")]
        public string[] WET { get; set; }

        [JsonPropertyName("{WHORE}")]
        public string[] WHORE { get; set; }
    }


    public class WearAndTearItem
    {
        [JsonPropertyName("descriptors")]
        public WearAndTearDescriptors descriptors { get; set; }

        [JsonPropertyName("descriptors-mcm")]
        public string[] descriptorsmcm { get; set; }
    }

    public class WearAndTearDescriptors
    {
        public string[] level0 { get; set; }
        public string[] level1 { get; set; }
        public string[] level2 { get; set; }
        public string[] level3 { get; set; }
        public string[] level4 { get; set; }
        public string[] level5 { get; set; }
        public string[] level6 { get; set; }
        public string[] level7 { get; set; }
        public string[] level8 { get; set; }
        public string[] level9 { get; set; }
    }




    public class ArousalItem
    {
        [JsonPropertyName("{READINESS}")]
        public READINESS READINESS { get; set; }

        [JsonPropertyName("{FAROUSAL}")]
        public FAROUSAL FAROUSAL { get; set; }

        [JsonPropertyName("{MAROUSAL}")]
        public MAROUSAL MAROUSAL { get; set; }
    }

    public class READINESS
    {
        public string[] level0 { get; set; }
        public string[] level1 { get; set; }
        public string[] level2 { get; set; }
        public string[] level3 { get; set; }
        public string[] level4 { get; set; }
    }

    public class FAROUSAL
    {
        public string[] level0 { get; set; }
        public string[] level1 { get; set; }
        public string[] level2 { get; set; }
        public string[] level3 { get; set; }
        public string[] level4 { get; set; }
    }

    public class MAROUSAL
    {
        public string[] level0 { get; set; }
        public string[] level1 { get; set; }
        public string[] level2 { get; set; }
        public string[] level3 { get; set; }
        public string[] level4 { get; set; }
    }


    public class ACodeSign
    {
        public string SignName = "";
        public string SignContent = "";

        public ACodeSign(string SignName, string SignContent)
        {
            this.SignName = SignName;
            this.SignContent = SignContent;
        }
    }
    public class ACodeParse
    {
        public string Content = "";
        public List<ACodeSign> ACodeSigns = new List<ACodeSign>();

        public ACodeParse(string Content)
        {
            bool IsSign = false;

            int Offset = 0;

            string RichText = "";
            string SignCode = "";

            for (int i = 0; i < Content.Length; i++)
            {
                string GetChar = Content.Substring(i, 1);

                if (GetChar == "{")
                {
                    IsSign = true;
                    Offset++;
                }
                else
                if (GetChar == "}")
                {
                    string CreatName = string.Format("9{0}9", Offset);
                    ACodeSigns.Add(new ACodeSign(CreatName, "{" + SignCode + "}"));

                    RichText += "(" + CreatName + ")";
                    SignCode = string.Empty;

                    IsSign = false;
                }
                else
                if (IsSign)
                {
                    SignCode += GetChar;
                }
                else
                {
                    RichText += GetChar;
                }
            }

            this.Content = RichText;

        }



        public string UsingCode(string OneContent)
        {
            string GetContent = OneContent;
            foreach (var Get in ACodeSigns)
            {
                GetContent = GetContent.Replace("(" + Get.SignName + ")", Get.SignContent);
                GetContent = GetContent.Replace("( " + Get.SignName + " )", Get.SignContent);
                GetContent = GetContent.Replace("（" + Get.SignName + "）", Get.SignContent);
                GetContent = GetContent.Replace("（ " + Get.SignName + " ）", Get.SignContent);
                GetContent = GetContent.Replace(Get.SignName, Get.SignContent);
            }
            return GetContent;
        }
    }
}
