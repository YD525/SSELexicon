.info
	.source "WetFunctionMCM.psc"
	.modifyTime 1561191196 ;Sat Jun 22 16:13:16 2019 Local
	.compileTime 1561191199 ;Sat Jun 22 16:13:19 2019 Local
	.user "claud"
	.computer "W-CPU"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object WetFunctionMCM ecMCM
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
			.variable ::HasDDi_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable target Actor
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable jsonPath String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "WetFunction.json"
			.endVariable
			.variable ::Frostfall_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable autoWornKwds Form[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable storePrefix String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "WetFunction_Actor_"
			.endVariable
			.variable textureFiles String[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::SLAfac_var faction
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable autoWornStore String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "autoWorn"
			.endVariable
			.variable ::SLSWp_var globalvariable
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable autoWornPrefix String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "autoWorn"
			.endVariable
			.variable ::ClothingBody_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::effect_var magiceffect
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable textureMenuShow Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::IsBeastRace_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable lastPage String
				.userFlags 0	; Flags: 0x00000000
				.initialValue ""
			.endVariable
			.variable ::SexLab_var quest
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable iniLog1 String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "bEnableLogging:Papyrus"
			.endVariable
			.variable ::SLSWmk_var globalvariable
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::ArmorCuirass_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::Vampire_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::SLSWm_var globalvariable
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::SLSWai_var globalvariable
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable systemDumped Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::HasDDa_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::CurrentFollowerFaction_var faction
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::ability_var spell
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable iniLog2 String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "bEnableTrace:Papyrus"
			.endVariable
			.variable ::HasZaZ_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::ActorTypeNPC_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable player Actor
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
		.endVariableTable
		.propertyTable
			.property Frostfall Bool auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::Frostfall_var
			.endProperty
			.property HasDDa Bool auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::HasDDa_var
			.endProperty
			.property ArmorCuirass keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ArmorCuirass_var
			.endProperty
			.property SLSWp globalvariable auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::SLSWp_var
			.endProperty
			.property SexLab quest auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::SexLab_var
			.endProperty
			.property HasDDi Bool auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::HasDDi_var
			.endProperty
			.property HasZaZ Bool auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::HasZaZ_var
			.endProperty
			.property SLSWai globalvariable auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::SLSWai_var
			.endProperty
			.property Vampire keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Vampire_var
			.endProperty
			.property SLSWm globalvariable auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::SLSWm_var
			.endProperty
			.property effect magiceffect auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::effect_var
			.endProperty
			.property SLSWmk globalvariable auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::SLSWmk_var
			.endProperty
			.property CurrentFollowerFaction faction auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::CurrentFollowerFaction_var
			.endProperty
			.property ActorTypeNPC keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ActorTypeNPC_var
			.endProperty
			.property ClothingBody keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ClothingBody_var
			.endProperty
			.property SLAfac faction auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::SLAfac_var
			.endProperty
			.property ability spell auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ability_var
			.endProperty
			.property IsBeastRace keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::IsBeastRace_var
			.endProperty
		.endPropertyTable
		.stateTable
			.state 
				.function FLog static
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param msg String
						.param level Int
					.endParamTable
					.localTable
						.local ::temp215 Int
						.local ::temp216 Bool
						.local first Bool
						.local ::temp217 Bool
					.endLocalTable
					.code
						Assign first True                                        ;@line 1175
					_label2:
						CallStatic papyrusutil ClampInt ::temp215 level 0 2      ;@line 1176
						CallStatic debug TraceUser ::temp216 "wetfunction" msg ::temp215  ;@line 1176
						Not ::temp216 ::temp216                                  ;@line 1176
						Cast ::temp216 ::temp216                                 ;@line 1176
						JumpF ::temp216 _label0                                  ;@line 1176
						Cast ::temp216 first                                     ;@line 1176
					_label0:
						JumpF ::temp216 _label1                                  ;@line 1176
						Assign first False                                       ;@line 1177
						CallStatic debug OpenUserLog ::temp217 "wetfunction"     ;@line 1178
						Jump _label2                                             ;@line 1178
					_label1:
					.endCode
				.endFunction
				.function acGetFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param act Actor
						.param storeKey String
						.param default Float
					.endParamTable
					.localTable
						.local ::temp67 String
						.local ::temp68 form
						.local ::temp69 Float
					.endLocalTable
					.code
						StrCat ::temp67 storePrefix storeKey                     ;@line 585
						Cast ::temp68 act                                        ;@line 585
						CallStatic storageutil GetFloatValue ::temp69 ::temp68 ::temp67 default  ;@line 585
						Return ::temp69                                          ;@line 585
					.endCode
				.endFunction
				.function ActorConfigured
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param act Actor
					.endParamTable
					.localTable
						.local ::temp91 Bool
						.local ::temp92 Bool
					.endLocalTable
					.code
						CallMethod acHasFloat self ::temp91 act "wetnessForce"   ;@line 623
						Cast ::temp91 ::temp91                                   ;@line 623
						JumpT ::temp91 _label3                                   ;@line 623
						CallMethod acHasFloat self ::temp92 act "specularForce"  ;@line 623
						Cast ::temp91 ::temp92                                   ;@line 623
					_label3:
						Cast ::temp91 ::temp91                                   ;@line 623
						JumpT ::temp91 _label4                                   ;@line 623
						CallMethod acHasFloat self ::temp92 act "glossinessForce"  ;@line 623
						Cast ::temp91 ::temp92                                   ;@line 623
					_label4:
						Return ::temp91                                          ;@line 623
					.endCode
				.endFunction
				.function ecPage
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param page String
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp35 Bool
						.local wetMax Float
						.local wetStep Float
						.local rateMax Float
						.local rateStep Float
						.local specularMax Float
						.local specularStep Float
						.local specularMin Float
						.local glossinessMax Float
						.local glossinessStep Float
						.local glossinessMin Float
						.local loopMax Float
						.local ::temp36 Float
						.local ::temp37 Bool
						.local ::temp38 Bool
						.local removeNote String
						.local status String
						.local statusAction String
						.local refresh String
						.local ::temp39 String
						.local ::temp40 Bool
						.local ::temp41 Bool
						.local ::temp42 Bool
						.local ::temp43 Float
						.local hours Float
						.local disabled Bool
						.local ::temp44 String[]
						.local ::temp45 Int
						.local devs String[]
						.local i Int
						.local ::temp46 String
						.local ::mangled_devs_0 String[]
						.local ::mangled_i_1 Int
						.local autoBonusDesc String
						.local notActors String
						.local dumpSystem String
					.endLocalTable
					.code
						CallMethod ecFillMode self ::NoneVar True False          ;@line 200
						Assign wetMax 20.000000                                  ;@line 202
						Assign wetStep 0.200000                                  ;@line 203
						Assign rateMax 10.000000                                 ;@line 204
						Assign rateStep 0.100000                                 ;@line 205
						Assign specularMax 20.000000                             ;@line 206
						Assign specularStep 0.200000                             ;@line 207
						Assign specularMin specularStep                          ;@line 208
						Assign glossinessMax 1000.000000                         ;@line 209
						Assign glossinessStep 5.000000                           ;@line 210
						Assign glossinessMin glossinessStep                      ;@line 211
						Assign loopMax 30.000000                                 ;@line 212
						CompareEQ ::temp35 page ""                               ;@line 214
						JumpF ::temp35 _label5                                   ;@line 214
						Assign page lastPage                                     ;@line 215
						CallMethod SetTitleText self ::NoneVar page              ;@line 216
						Jump _label6                                             ;@line 216
					_label5:
						Assign lastPage page                                     ;@line 218
					_label6:
						CompareEQ ::temp35 page "Wetness"                        ;@line 221
						JumpF ::temp35 _label7                                   ;@line 221
						CallMethod ecCursor self ::NoneVar 0                     ;@line 222
						CallMethod ecHeader self ::NoneVar "Wetness points" False  ;@line 223
						CallMethod ecSlider self ::NoneVar "wetnessDry" "wqeqe" 1.000000 0.000000 wetMax wetStep "" 1 "" False False True  ;@line 224
						CallMethod ecSlider self ::NoneVar "wetnessStart" "企鹅去" 2.000000 0.000000 wetMax wetStep "" 1 "Above this wetness, it will be visible. Drying will be slower the getting more wet, untils the skin is considered soaked." False False True  ;@line 225
						CallMethod ecSlider self ::NoneVar "wetnessSoaked" "企鹅去" 4.000000 0.000000 wetMax wetStep "" 1 "" False False True  ;@line 226
						CallMethod ecSlider self ::NoneVar "wetnessCap" "q而且33" 10.000000 0.000000 wetMax wetStep "" 1 "" False False True  ;@line 227
						CallMethod ecSlider self ::NoneVar "wetnessForce" "force" -1.000000 -1.000000 wetMax wetStep "" 1 "If this is not negative, the wetness will be forced to the given value." False False True  ;@line 228
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 229
						CallMethod ecHeader self ::NoneVar "Wetness Sources" False  ;@line 230
						CallMethod ecSlider self ::NoneVar "generateStamina" "Stamina usage" 1.500000 0.000000 rateMax rateStep "" 1 "Amount of wetness generated by using the entire Stamina." False False True  ;@line 231
						CallMethod ecSlider self ::NoneVar "generateMagicka" "Magicka usage" 2.000000 0.000000 rateMax rateStep "" 1 "Amount of wetness generated by using the entire Magicka." False False True  ;@line 232
						CallMethod ecSlider self ::NoneVar "generateSprinting" "Sprinting rate" 1.000000 0.000000 rateMax rateStep "" 1 "Wetness generation rate while sprinting." False False True  ;@line 233
						CallMethod ecSlider self ::NoneVar "generateRunning" "Running rate" 0.500000 0.000000 rateMax rateStep "" 1 "Wetness generation rate while running." False False True  ;@line 234
						CallMethod ecSlider self ::NoneVar "generateSneaking" "Sneaking rate" 0.300000 0.000000 rateMax rateStep "" 1 "Wetness generation rate while sneaking." False False True  ;@line 235
						CallMethod ecSlider self ::NoneVar "generateGallop" "Galloping rate" 0.300000 0.000000 rateMax rateStep "" 1 "Wetness generation rate while galloping." False False True  ;@line 236
						CallMethod ecSlider self ::NoneVar "generateWorking" "Working rate" 0.000000 0.000000 rateMax rateStep "" 1 "Wetness generation rate while working. Eg: Mining, chopping wood, smithing, smelting, fletching." False False True  ;@line 237
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 238
						CallMethod ecHeader self ::NoneVar "Misc" False          ;@line 239
						CallMethod ecSlider self ::NoneVar "lateSweat" "Late sweat" 0.100000 0.000000 1.000000 0.050000 "" 1 "Add sweaty effect if wetness is raised again by the given ratio. This occurs only when entering the water while being dry - only droplets will form then, the sweat may be added later when exercising during the drying process." False False True  ;@line 240
						CallMethod ecSlider self ::NoneVar "otherAdd" "Non-player rate bonus" 0.000000 0.000000 rateMax rateStep "" 1 "For non-player targets, added to the total wetness rate." False False True  ;@line 241
						CallMethod ecSlider self ::NoneVar "otherMult" "Non-player rate multiplier" 1.000000 0.000000 rateMax rateStep "" 1 "For non-player targets, multiply all wetness rates by this value." False False True  ;@line 242
						CallMethod ecSlider self ::NoneVar "multGlobal" "Global rate multiplier" 1.000000 0.000000 rateMax rateStep "" 1 "Globally multiply all wetness rates by this value." False False True  ;@line 243
						CallMethod ecCursor self ::NoneVar 1                     ;@line 245
						CallMethod ecHeader self ::NoneVar "Weather influence" False  ;@line 246
						FNegate ::temp36 rateMax                                 ;@line 247
						CallMethod ecSlider self ::NoneVar "weatherPleasant" "Pleasant" -2.000000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate during pleasant weather. Negative values mean drying." False False True  ;@line 247
						FNegate ::temp36 rateMax                                 ;@line 248
						CallMethod ecSlider self ::NoneVar "weatherCloudy" "Cloudy" -0.200000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate during cloudy weather. Negative values mean drying." False False True  ;@line 248
						FNegate ::temp36 rateMax                                 ;@line 249
						CallMethod ecSlider self ::NoneVar "weatherRainy" "Rainy" 1.500000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate during rainy weather. Negative values mean drying." False False True  ;@line 249
						FNegate ::temp36 rateMax                                 ;@line 250
						CallMethod ecSlider self ::NoneVar "weatherSnow" "Snow" 0.200000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate during snow weather. Negative values mean drying." False False True  ;@line 250
						FNegate ::temp36 rateMax                                 ;@line 251
						CallMethod ecSlider self ::NoneVar "weatherNone" "None" -2.000000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate during no weather - this usually means indoors. Negative values mean drying." False False True  ;@line 251
						CallMethod dynModEntry self ::temp37 ::Frostfall_var "Frostfall" "Frostfall"  ;@line 253
						JumpF ::temp37 _label8                                   ;@line 253
						FNegate ::temp36 rateMax                                 ;@line 254
						CallMethod ecSlider self ::NoneVar "frostfallFire" "Fire" -6.000000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate when near a fire. Scales with size of the fire: e.g. x1 (embers), x2 (campfire), x3 (bonfire)" False False True  ;@line 254
						FNegate ::temp36 rateMax                                 ;@line 255
						CallMethod ecSlider self ::NoneVar "frostfallShelterRain" "Shelter (Rain)" -0.200000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate when in during rain." False False True  ;@line 255
						FNegate ::temp36 rateMax                                 ;@line 256
						CallMethod ecSlider self ::NoneVar "frostfallShelterSnow" "Shelter (Snow)" -0.800000 ::temp36 rateMax rateStep "" 1 "Wetness generation rate when in during snow." False False True  ;@line 256
						Jump _label8                                             ;@line 256
					_label8:
						Cast ::temp37 ::SexLab_var                               ;@line 258
						CallMethod dynModEntry self ::temp37 ::temp37 "SexLab" "SexLab"  ;@line 258
						JumpF ::temp37 _label9                                   ;@line 258
						CallMethod ecSlider self ::NoneVar "sexlabBase" "Base rate" 3.000000 0.000000 rateMax rateStep "" 1 "Base wetness generation rate for all SexLab animations." False False True  ;@line 259
						CallMethod ecSlider self ::NoneVar "sexlabEnjoyment" "Enjoyment rate" 6.000000 0.000000 rateMax rateStep "" 1 "Additional wetness generation rate when at full enjoyment.\nCan also be upto 25% negative." False False True  ;@line 260
						FNegate ::temp36 rateMax                                 ;@line 261
						CallMethod ecSlider self ::NoneVar "sexlabPain" "Pain rate" -1.000000 ::temp36 rateMax rateStep "" 1 "Additional wetness generation rate when at full pain.\nSum of pain and enjoyment is usually around 100%." False False True  ;@line 261
						CallMethod ecSlider self ::NoneVar "SexLabOrgasm" "Orgasm" 2.000000 0.000000 rateMax rateStep "" 1 "Wetness generated from an orgasm." False False True  ;@line 262
						CallMethod ecToggle self ::NoneVar "sexlabPlayerOnly" "Player Only" False "Only process SexLeb events for the player. This can greatly reduce script load when multiple scenes are running." False False True  ;@line 263
						Jump _label9                                             ;@line 263
					_label9:
						Cast ::temp37 ::SLAfac_var                               ;@line 265
						CallMethod dynModEntry self ::temp37 ::temp37 "SexLab Aroused" "aroused"  ;@line 265
						JumpF ::temp37 _label10                                  ;@line 265
						CallMethod ecSlider self ::NoneVar "generateArousal" "Arousal rate" 3.000000 0.000000 rateMax rateStep "" 1 "Wetness generation rate while being fully aroused. Requires SexLab aroused." False False True  ;@line 266
						CallMethod ecSlider self ::NoneVar "arousedThreshold" "aroused Threshold" 75.000000 0.000000 100.000000 1.000000 "" 0 "A wet pussy texture can be applied when the arousal exceedes this amount." False False True  ;@line 267
						Jump _label10                                            ;@line 267
					_label10:
						CallMethod dynModEntry self ::temp37 ::HasDDi_var "Devious Devices" "zad"  ;@line 269
						JumpF ::temp37 _label11                                  ;@line 269
						CallMethod ecSlider self ::NoneVar "zadVibrate" "Vibration" 0.500000 0.000000 rateMax rateStep "" 1 "Wetness generation rate during vibration of devious devices. Is multiplied with vibration strength (which can be >10)." False False True  ;@line 270
						CallMethod ecSlider self ::NoneVar "zadOrgasm" "Orgasm" 3.000000 0.000000 rateMax rateStep "" 1 "Wetness generated when a devious device causes and orgasm." False False True  ;@line 271
						CallMethod ecSlider self ::NoneVar "zadEdge" "Edge" 3.000000 0.000000 rateMax rateStep "" 1 "Wetness generated when edged by a devious device." False False True  ;@line 272
						CallMethod ecToggle self ::NoneVar "zadPlayerOnly" "Player Only" True "Only process devious devices events for the player.\nIf disabled, it is more like that an event triggers the effect on multiple targets. This is because the zad events are distinguishing by name - which could be the same for two actors." False False True  ;@line 273
						Jump _label11                                            ;@line 273
					_label11:
						Cast ::temp37 ::SLSWai_var                               ;@line 275
						CallMethod dynModEntry self ::temp37 ::temp37 "Skooma Whore" "skooma"  ;@line 275
						JumpF ::temp37 _label12                                  ;@line 275
						CallMethod ecSlider self ::NoneVar "skoomaPhysical" "Physical" 0.000000 0.000000 rateMax rateStep "" 1 "Wetness generation rate for each 100 physical addiction." False False True  ;@line 276
						CallMethod ecSlider self ::NoneVar "skoomaMental" "Mental" 0.000000 0.000000 rateMax rateStep "" 1 "Wetness generation rate for each 100 mental addiction." False False True  ;@line 277
						CallMethod ecSlider self ::NoneVar "skoomaMagical" "Magical" 0.000000 0.000000 rateMax rateStep "" 1 "Wetness generation rate for each 100 magical addiction." False False True  ;@line 278
						CallMethod ecSlider self ::NoneVar "skoomaAddicted" "Addiction" 0.200000 0.000000 rateMax rateStep "" 1 "Wetness generation rate for each addiction stage.\nBelow \"experimentation\" is it 0, during \"regular use\" it twice the value given here, and so on." False False True  ;@line 279
						Jump _label12                                            ;@line 279
					_label12:
						Jump _label13                                            ;@line 279
					_label7:
						CompareEQ ::temp37 page "Visuals"                        ;@line 281
						JumpF ::temp37 _label14                                  ;@line 281
						CallMethod ecCursor self ::NoneVar 0                     ;@line 282
						CallMethod ecHeader self ::NoneVar "specular" False      ;@line 283
						CallMethod ecSlider self ::NoneVar "specularMin" "min" 3.000000 specularMin specularMax specularStep "" 1 "Sets the specular value when considered dry." False False True  ;@line 284
						CallMethod ecSlider self ::NoneVar "specularMax" "max" 12.000000 specularMin specularMax specularStep "" 1 "Sets the specular value when considered soaked to the limit (eg. after swimming)." False False True  ;@line 285
						CallMethod ecToggle self ::NoneVar "specularHand" "Hands" True "Apply the specular values for the hands." False False True  ;@line 286
						CallMethod ecToggle self ::NoneVar "specularBody" "Body" True "Apply the specular values for the body." False False True  ;@line 287
						CallMethod ecToggle self ::NoneVar "specularFeet" "Feet" True "Apply the specular values for the feet." False False True  ;@line 288
						CallMethod ecToggle self ::NoneVar "specularHead" "Head" True "Apply the specular values for the head." False False True  ;@line 289
						CallMethod ecSlider self ::NoneVar "specularForce" "force" 0.000000 0.000000 specularMax specularStep "" 1 "If positive overrides all calculations and forces this specular value." False False True  ;@line 290
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 291
						CallMethod ecHeader self ::NoneVar "Texture effects" False  ;@line 292
						CallMethod ecToggle self ::NoneVar "textureBodyDrops" "Body - Drops" True "Use sweaty body textures with droplet effect." False False True  ;@line 293
						CallMethod ecToggle self ::NoneVar "textureBodySweat" "Body - Sweat" True "Use sweaty body textures with sweaty effect." False False True  ;@line 294
						CallMethod ecToggle self ::NoneVar "textureBodyPussy" "Body - Pussy" True "Use sweaty body textures with wet pussy effect (when aroused)." False False True  ;@line 295
						CallMethod ecToggle self ::NoneVar "textureFeetDrops" "Feet - Drops" True "Use sweaty feet textures with droplet effect." False False True  ;@line 296
						CallMethod ecToggle self ::NoneVar "textureFeetSweat" "Feet - Sweat" True "Use sweaty feet textures with sweaty effect." False False True  ;@line 297
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 298
						CallMethod ecHeader self ::NoneVar "General" False       ;@line 299
						CallMethod ecSlider self ::NoneVar "loopTimePc" "Refresh time (pc)" 2.000000 1.000000 loopMax 0.500000 "" 1 "The refresh time for wetness & visual updates. Too low values can be a serious performance problem. Only for the player." False False True  ;@line 300
						CallMethod ecSlider self ::NoneVar "loopTimeNpc" "Refresh time (npc)" 4.000000 1.000000 loopMax 0.500000 "" 1 "The refresh time for wetness & visual updates. Too low values can be a serious performance problem. Only for non-player actors." False False True  ;@line 301
						CallMethod ecSlider self ::NoneVar "loopTimeForced" "Refresh time (froced)" 10.000000 1.000000 loopMax 0.500000 "" 1 "The refresh time for wetness & visual updates. Too low values can be a serious performance problem. Only when wetness is forced to a fixed value." False False True  ;@line 302
						CallMethod ecToggle self ::NoneVar "firstPersonToo" "Update First person model" True "Also update the visuals of the first person model." False False True  ;@line 303
						CallMethod ecToggle self ::NoneVar "alwaysOperate" "Always operate on slots" False "Always operate on slots (everything but the head) even if there was no skin detected (may be caused by buggy overlays)." False False True  ;@line 304
						CallMethod ecCursor self ::NoneVar 1                     ;@line 306
						CallMethod ecHeader self ::NoneVar "glossiness" False    ;@line 307
						CallMethod ecSlider self ::NoneVar "glossinessMin" "min" 40.000000 glossinessMin glossinessMax glossinessStep "" 0 "Sets the glossiness value when considered dry." False False True  ;@line 308
						CallMethod ecSlider self ::NoneVar "glossinessMax" "max" 25.000000 glossinessMin glossinessMax glossinessStep "" 0 "Sets the glossiness value when considered soaked to the limit (eg. after swimming)." False False True  ;@line 309
						CallMethod ecToggle self ::NoneVar "glossinessHand" "Hands" False "Apply the glossiness values for the hands." False False True  ;@line 310
						CallMethod ecToggle self ::NoneVar "glossinessBody" "Body" False "Apply the glossiness values for the body." False False True  ;@line 311
						CallMethod ecToggle self ::NoneVar "glossinessFeet" "Feet" False "Apply the glossiness values for the feet." False False True  ;@line 312
						CallMethod ecToggle self ::NoneVar "glossinessHead" "Head" False "Apply the glossiness values for the head." False False True  ;@line 313
						CallMethod ecSlider self ::NoneVar "glossinessForce" "force" 0.000000 0.000000 glossinessMax glossinessStep "" 0 "If positive overrides all calculations and forces this glossiness value." False False True  ;@line 314
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 315
						CallMethod TextureMenu self ::NoneVar True               ;@line 316
						Jump _label13                                            ;@line 316
					_label14:
						CompareEQ ::temp37 page "Targets"                        ;@line 318
						JumpF ::temp37 _label15                                  ;@line 318
						Not ::temp38 ::ecBurnIn_var                              ;@line 319
						JumpF ::temp38 _label16                                  ;@line 319
						Assign removeNote "\nThe game needs to run (all menus closed) for the effects to be removed. Until then, targets will still show up as affected but can't be selected anymore."  ;@line 320
						Assign status ""                                         ;@line 323
						Assign statusAction ""                                   ;@line 324
						Assign refresh ""                                        ;@line 325
						JumpF target _label17                                    ;@line 326
						CallMethod ActorStatus self ::temp39 target              ;@line 327
						Assign status ::temp39                                   ;@line 327
						CompareEQ ::temp40 status "Broken"                       ;@line 328
						JumpF ::temp40 _label18                                  ;@line 328
						Assign statusAction "Restart"                            ;@line 329
						Jump _label19                                            ;@line 329
					_label18:
						CompareEQ ::temp41 status "Outdated"                     ;@line 330
						JumpF ::temp41 _label20                                  ;@line 330
						Assign statusAction "Update"                             ;@line 331
						Jump _label19                                            ;@line 331
					_label20:
						CompareEQ ::temp41 status "Configured"                   ;@line 332
						Cast ::temp41 ::temp41                                   ;@line 332
						JumpT ::temp41 _label21                                  ;@line 332
						CompareEQ ::temp42 status "Dormant"                      ;@line 332
						Cast ::temp41 ::temp42                                   ;@line 332
					_label21:
						Cast ::temp41 ::temp41                                   ;@line 332
						JumpT ::temp41 _label22                                  ;@line 332
						CompareEQ ::temp42 status ""                             ;@line 332
						Cast ::temp41 ::temp42                                   ;@line 332
					_label22:
						JumpF ::temp41 _label23                                  ;@line 332
						Assign statusAction "Start Effect"                       ;@line 333
						Jump _label19                                            ;@line 333
					_label23:
						CompareEQ ::temp42 status "Stopping"                     ;@line 334
						JumpF ::temp42 _label24                                  ;@line 334
						Assign statusAction ""                                   ;@line 335
						Jump _label19                                            ;@line 335
					_label24:
						Assign statusAction "Stop Effect"                        ;@line 337
						CompareEQ ::temp41 status "Running"                      ;@line 338
						Not ::temp41 ::temp41                                    ;@line 338
						JumpF ::temp41 _label19                                  ;@line 338
						Assign refresh "Back to game"                            ;@line 339
						Jump _label19                                            ;@line 339
					_label19:
						Jump _label17                                            ;@line 339
					_label17:
						CallMethod ecCursor self ::NoneVar 0                     ;@line 345
						JumpF target _label25                                    ;@line 346
						CallMethod ecHeader self ::NoneVar "Current values" False  ;@line 347
						CallMethod acHasFloat self ::temp42 target "Wetness"     ;@line 348
						JumpF ::temp42 _label26                                  ;@line 348
						CallMethod acGetFloat self ::temp36 target "Wetness" 0.000000  ;@line 349
						CallMethod ecSlider self ::NoneVar "currentWetness" "Wetness" ::temp36 0.000000 wetMax 0.100000 "" 1 "The current wetness." False True False  ;@line 349
						Jump _label27                                            ;@line 349
					_label26:
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 351
					_label27:
						CompareEQ ::temp41 status "Running"                      ;@line 353
						JumpF ::temp41 _label28                                  ;@line 353
						CallMethod acGetFloat self ::temp36 target "wetnessRate" 0.000000  ;@line 354
						CallMethod ecSlider self ::NoneVar "currentWetnessRate" "Wetness Rate" ::temp36 0.000000 0.000000 0.000000 "" 2 "The current wetness generation rate." True False False  ;@line 354
						CallMethod acGetFloat self ::temp36 target "specular" 0.000000  ;@line 355
						CallMethod ecSlider self ::NoneVar "currentSpecular" "specular" ::temp36 0.000000 0.000000 0.000000 "" 1 "The current specular value." True False False  ;@line 355
						CallMethod acGetFloat self ::temp36 target "glossiness" 0.000000  ;@line 356
						CallMethod ecSlider self ::NoneVar "currentGlossiniess" "glossiness" ::temp36 0.000000 0.000000 0.000000 "" 0 "The current glossiness value." True False False  ;@line 356
						Jump _label29                                            ;@line 356
					_label28:
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 358
						CompareEQ ::temp40 status "Stopping"                     ;@line 359
						Not ::temp40 ::temp40                                    ;@line 359
						JumpF ::temp40 _label30                                  ;@line 359
						Not ::temp42 refresh                                     ;@line 360
						CallMethod ecText self ::NoneVar "currentNA" "Effect not running" refresh "" ::temp42 False False  ;@line 360
						Jump _label31                                            ;@line 360
					_label30:
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 362
					_label31:
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 364
					_label29:
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 366
						CallMethod ecHeader self ::NoneVar "Force Values" False  ;@line 367
						CallMethod acGetFloat self ::temp36 target "wetnessForce" -1.000000  ;@line 368
						CallMethod ecSlider self ::NoneVar "forceWetness" "Wetness" ::temp36 -1.000000 wetMax 0.100000 "" 1 "If this is not negative, the wetness will be forced to the given value." False True False  ;@line 368
						CallMethod acGetFloat self ::temp36 target "specularForce" 0.000000  ;@line 369
						CallMethod ecSlider self ::NoneVar "forceSpecular" "specular" ::temp36 0.000000 specularMax specularStep "" 1 "If positive overrides all calculations and forces this specular value." False True False  ;@line 369
						CallMethod acGetFloat self ::temp36 target "glossinessForce" 0.000000  ;@line 370
						CallMethod ecSlider self ::NoneVar "forceGlossiness" "glossiness" ::temp36 0.000000 glossinessMax glossinessStep "" 0 "If positive overrides all calculations and forces this glossiness value." False True False  ;@line 370
						CallMethod ActorConfigured self ::temp42 target          ;@line 371
						JumpF ::temp42 _label32                                  ;@line 371
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 372
						CallMethod ecText self ::NoneVar "controlReset" "Forced values" "Reset" "" False False False  ;@line 373
						Jump _label32                                            ;@line 373
					_label32:
						Jump _label33                                            ;@line 373
					_label25:
						CallMethod ecEmpty self ::NoneVar 5                      ;@line 376
						CallMethod ecText self ::NoneVar "targetNone" "No target selected!" "" "Select a target from the right side of the menu.\nThe effect can also be added to targets." True False False  ;@line 377
					_label33:
						CallMethod ecCursor self ::NoneVar 1                     ;@line 380
						CallMethod ecHeader self ::NoneVar "Select Target" False  ;@line 381
						CallMethod SelectText self ::NoneVar "player"            ;@line 382
						CallMethod SelectText self ::NoneVar "Crosshair"         ;@line 383
						CallMethod SelectText self ::NoneVar "Console"           ;@line 384
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 385
						CallMethod ecHeader self ::NoneVar "Current Target" False  ;@line 386
						CallMethod ActorName self ::temp39 target "N/A"          ;@line 387
						CallMethod ecText self ::NoneVar "targetStatus" ::temp39 status "The status of the current target." True False False  ;@line 387
						JumpF target _label34                                    ;@line 388
						JumpF statusAction _label35                              ;@line 389
						CallMethod ecText self ::NoneVar "targetAction" "" statusAction "" False False False  ;@line 390
						CompareEQ ::temp40 statusAction "Stop Effect"            ;@line 391
						Cast ::temp42 ::temp40                                   ;@line 391
						JumpF ::temp42 _label36                                  ;@line 391
						CallMethod acHasFloat self ::temp41 target "autoStop"    ;@line 391
						Cast ::temp42 ::temp41                                   ;@line 391
					_label36:
						JumpF ::temp42 _label37                                  ;@line 391
						CallMethod acGetFloat self ::temp36 target "autoStop" 0.000000  ;@line 392
						CallStatic utility GetCurrentGameTime ::temp43           ;@line 392
						FSubtract ::temp36 ::temp36 ::temp43                     ;@line 392
						FMultiply ::temp43 ::temp36 24.000000                    ;@line 392
						Assign hours ::temp43                                    ;@line 392
						CallMethod ecSlider self ::NoneVar "targetAutoStop" "Auto stop after" hours 0.000000 0.000000 0.000000 "{1} hours" 1 "The effect will automatically stop after the given amount of hours (game time). " True False False  ;@line 393
						Jump _label38                                            ;@line 393
					_label37:
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 395
					_label38:
						Jump _label39                                            ;@line 395
					_label35:
						CallMethod ecEmpty self ::NoneVar 2                      ;@line 398
					_label39:
						Jump _label40                                            ;@line 398
					_label34:
						CallMethod ecEmpty self ::NoneVar 2                      ;@line 401
					_label40:
						CallMethod ecEmpty self ::NoneVar 2                      ;@line 403
						CallMethod ecText self ::NoneVar "targetTip" "Tip for faster usage" "Show" "A tip for faster usage when setting up multiple targets." False False False  ;@line 404
						Jump _label16                                            ;@line 404
					_label16:
						Jump _label13                                            ;@line 404
					_label15:
						CompareEQ ::temp40 page "Auto-Apply"                     ;@line 406
						JumpF ::temp40 _label41                                  ;@line 406
						CallMethod ecCursor self ::NoneVar 0                     ;@line 407
						CallMethod ecHeader self ::NoneVar "Auto-Apply" False    ;@line 409
						CallMethod ecToggle self ::NoneVar "autoGlobal" "Globally" False "Automatically apply the effect to all eligible targets (NPCs) near the player." False False True  ;@line 410
						CallMethod ecToggle self ::NoneVar "autoNaked" "Naked" False "Automatically apply the effect to naked eligible targets (NPCs) near the player. One is considered naked when not wearing body armor or clothes." False False True  ;@line 411
						CallMethod ecToggle self ::NoneVar "autoFollower" "Followers" False "Automatically apply the effect to your followers (NPC olny)." False False True  ;@line 412
						CallMethod ecToggle self ::NoneVar "autoFix" "Aut-fix broken" False "Automatically attemp to fix broken effects (due to game-engine bugs)." False False True  ;@line 413
						CallMethod ecSlider self ::NoneVar "autoTimeout" "Scan timeout" 8.000000 1.000000 100.000000 1.000000 "{0} seconds" 1 "Timeout between scans." False False True  ;@line 414
						CallMethod ecSlider self ::NoneVar "autoRange" "Scan range" 5000.000000 100.000000 10000.000000 100.000000 "{0} units" 1 "Range of a scan. Limited to current cell of player." False False True  ;@line 415
						CallMethod ecSlider self ::NoneVar "autoDuration" "Effect linger for" 1.000000 0.100000 10.000000 0.100000 "{1} hours" 1 "The automatically applied effect will linger for the given amount of time after the last refresh.\nTime is given in game hours. Setting this too low (compared to the timeout) will remove the effect before it could be refreshed." False False True  ;@line 416
						Cast ::temp41 ::SexLab_var                               ;@line 418
						CallMethod dynModEntry self ::temp42 ::temp41 "SexLab" "SexLab"  ;@line 418
						JumpF ::temp42 _label42                                  ;@line 418
						CallMethod ecGetBool self ::temp38 "sexlabAuto"          ;@line 419
						Not ::temp37 ::temp38                                    ;@line 419
						Assign disabled ::temp37                                 ;@line 419
						CallMethod ecToggle self ::NoneVar "sexlabAuto" "Automatically apply" False "The effect will be automatically applied to all participants of an SexLab scene." False False True  ;@line 420
						CallMethod ecToggle self ::NoneVar "sexlabAutoPlayer" "Only player-scenes" False "The effect will be automatically applied only if the scene is involving the player." disabled False True  ;@line 421
						CallMethod ecSlider self ::NoneVar "sexlabAutoLinger" "Effects lingers for" 12.000000 0.000000 48.000000 0.500000 "{1} hours" 1 "An automatically applied effect will linger the given amount of time after the scene ended.\nTime is given game hours." disabled False True  ;@line 422
						Jump _label42                                            ;@line 422
					_label42:
						CallMethod dynModEntry self ::temp41 ::HasDDa_var "Devious Devices" "zad"  ;@line 425
						JumpF ::temp41 _label43                                  ;@line 425
						CallMethod toggleAutoWornKeyword self ::NoneVar "zad_Lockable" "Lockable device" "All devices below are usually also lockable."  ;@line 426
						CallStatic stringutil Split ::temp44 "Armbinder,ArmCuffs,Belt,Blindfold,Boots,Bra,Clamps,Collar,Corset,Gag,Gloves,Harness,Hood,LegCuffs,PiercingsNipple,PiercingsVaginal,Plug,PlugAnal,PlugVaginal,Suit,Yoke" ","  ;@line 427
						Assign devs ::temp44                                     ;@line 427
						CallStatic papyrusutil SortStringArray ::NoneVar devs True  ;@line 428
						ArrayLength ::temp45 devs                                ;@line 429
						Assign i ::temp45                                        ;@line 429
					_label45:
						CompareGT ::temp38 i 0                                   ;@line 430
						JumpF ::temp38 _label44                                  ;@line 430
						ISubtract ::temp45 i 1                                   ;@line 431
						Assign i ::temp45                                        ;@line 431
						ArrayGetElement ::temp39 devs i                          ;@line 432
						StrCat ::temp39 "zad_Devious" ::temp39                   ;@line 432
						ArrayGetElement ::temp46 devs i                          ;@line 432
						CallMethod toggleAutoWornKeyword self ::NoneVar ::temp39 ::temp46 ""  ;@line 432
						Jump _label45                                            ;@line 432
					_label44:
						Jump _label43                                            ;@line 432
					_label43:
						CallMethod dynModEntry self ::temp37 ::HasZaZ_var "ZaZ Animation Pack" "zaz"  ;@line 436
						JumpF ::temp37 _label46                                  ;@line 436
						CallStatic stringutil Split ::temp44 "Ankles,Belt,Blindfold,Bra,Collar,Device,Gag,Hood,Wrist,Yoke" ","  ;@line 438
						Assign ::mangled_devs_0 ::temp44                         ;@line 438
						CallStatic papyrusutil SortStringArray ::NoneVar ::mangled_devs_0 True  ;@line 439
						ArrayLength ::temp45 ::mangled_devs_0                    ;@line 440
						Assign ::mangled_i_1 ::temp45                            ;@line 440
					_label48:
						CompareGT ::temp42 ::mangled_i_1 0                       ;@line 441
						JumpF ::temp42 _label47                                  ;@line 441
						ISubtract ::temp45 ::mangled_i_1 1                       ;@line 442
						Assign ::mangled_i_1 ::temp45                            ;@line 442
						ArrayGetElement ::temp39 ::mangled_devs_0 ::mangled_i_1  ;@line 443
						StrCat ::temp46 "zbfWorn" ::temp39                       ;@line 443
						ArrayGetElement ::temp39 ::mangled_devs_0 ::mangled_i_1  ;@line 443
						CallMethod toggleAutoWornKeyword self ::NoneVar ::temp46 ::temp39 ""  ;@line 443
						Jump _label48                                            ;@line 443
					_label47:
						Jump _label46                                            ;@line 443
					_label46:
						CallMethod ecCursor self ::NoneVar 1                     ;@line 447
						CallMethod ecHeader self ::NoneVar "Auto-Apply filter" False  ;@line 448
						CallMethod ecToggle self ::NoneVar "autoFemale" "females" True "Whether the auto-application operates on female NPCs." False False True  ;@line 449
						CallMethod ecToggle self ::NoneVar "autoMale" "males" False "Whether the auto-application operates on male NPCs." False False True  ;@line 450
						CallMethod ecToggle self ::NoneVar "autoBeast" "beast races" False "Whether the auto-application operates on beast races." False False True  ;@line 451
						CallMethod ecToggle self ::NoneVar "autoVampire" "vampires" False "Whether the auto-application operates on vampires." False False True  ;@line 452
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 453
						CallMethod ecHeader self ::NoneVar "First auto-apply" False  ;@line 454
						Assign autoBonusDesc ""                                  ;@line 455
						CallMethod ecSlider self ::NoneVar "autoBonusHours" "Inital activity" 1.000000 0.000000 48.000000 0.500000 "{1} hours" 1 "When the effect is auto-applied the first time, the inital wetness will be boosted. This is calculated by pretending the actor had the current wetness rate for the given amount of hours before the effect got applied." False False True  ;@line 456
						CallMethod ecSlider self ::NoneVar "autoBonusNormal" "Inital bonus wetness" 0.000000 0.000000 wetMax 0.100000 "" 1 "When the effect is auto-applied the first time, the wetness is initalized with the given amount of bonus wetness." False False True  ;@line 457
						CallMethod ecSlider self ::NoneVar "autoBonusRandom" "Inital random wetness" 0.000000 0.000000 wetMax 0.100000 "" 1 "When the effect is auto-applied the first time, the wetness is increased by an additional random amount of up to the given amount." False False True  ;@line 458
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 459
						CallMethod ecHeader self ::NoneVar "Misc" False          ;@line 460
						CallMethod ecToggle self ::NoneVar "autoKeepWetness" "Keep wetness when suspending" True "The current wetness will not be reset when the automatically applied effect is removed." False False True  ;@line 461
						CallMethod ecToggle self ::NoneVar "autoRemoveNear" "Auto-stop: not near player" True "Automatically stop the effect when not near the player (e.g.: different cell)." True False False  ;@line 462
						CallMethod ecToggle self ::NoneVar "autoRemove3D" "Auto-stop: model not loaded" True "Automatically stop the effect when its world model is not loaded (e.g.: different cell)." True False False  ;@line 463
						Jump _label13                                            ;@line 463
					_label41:
						CompareEQ ::temp38 page "Misc"                           ;@line 464
						JumpF ::temp38 _label13                                  ;@line 464
						Assign notActors " This does not include which actors are affected and their possibly forced values."  ;@line 465
						Assign dumpSystem "Dump"                                 ;@line 466
						JumpF systemDumped _label49                              ;@line 467
						Assign dumpSystem "Done"                                 ;@line 468
						Jump _label49                                            ;@line 468
					_label49:
						CallMethod ecCursor self ::NoneVar 0                     ;@line 471
						CallMethod ecHeader self ::NoneVar "Export/Import" False  ;@line 472
						StrCat ::temp46 "Export all current settings." notActors  ;@line 473
						CallMethod ecText self ::NoneVar "jsonExport" "Export settings" "企鹅去" ::temp46 False False False  ;@line 473
						StrCat ::temp39 "Import all settings." notActors         ;@line 474
						CallMethod ecText self ::NoneVar "jsonImport" "Import settings" "企鹅去" ::temp39 False False False  ;@line 474
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 475
						CallMethod ecHeader self ::NoneVar "Logging" False       ;@line 476
						CallMethod ecSlider self ::NoneVar "logLevel" "Logging level" 0.000000 -4.000000 2.000000 1.000000 "" 0 "Loggin level. -4 most Verbose, ..., 0 Info, 1 Error, 2 Critical; everything below the set value will not be output." False False True  ;@line 477
						Cast ::temp40 ::ecBurnIn_var                             ;@line 478
						JumpT ::temp40 _label50                                  ;@line 478
						CallStatic utility GetINIBool ::temp41 iniLog1           ;@line 478
						Cast ::temp37 ::temp41                                   ;@line 478
						JumpF ::temp37 _label51                                  ;@line 478
						CallStatic utility GetINIBool ::temp42 iniLog2           ;@line 478
						Cast ::temp37 ::temp42                                   ;@line 478
					_label51:
						Cast ::temp40 ::temp37                                   ;@line 478
					_label50:
						JumpF ::temp40 _label52                                  ;@line 478
						CallMethod ecSlider self ::NoneVar "logFile" "File logging level" 3.000000 -4.000000 3.000000 1.000000 "" 0 "File logging level - same as above. 3 means no file logging." False False True  ;@line 479
						CallMethod ecText self ::NoneVar "logSystem" "System into" dumpSystem "Write extensive information for the system to the log file." systemDumped False False  ;@line 480
						CallMethod ActorName self ::temp46 target "N/A"          ;@line 481
						StrCat ::temp39 "Target info: " ::temp46                 ;@line 481
						Not ::temp41 target                                      ;@line 481
						CallMethod ecText self ::NoneVar "logTarget" ::temp39 "Dump" "Write extensive information for the current target to the log file." ::temp41 False False  ;@line 481
						Jump _label53                                            ;@line 481
					_label52:
						CallMethod ecText self ::NoneVar "logDisabled" "Papyrus logging is disabled" "" "" True False False  ;@line 483
						CallMethod ecText self ::NoneVar "logEnable" "" "Enable for this session" "" False False False  ;@line 484
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 485
					_label53:
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 487
						CallMethod ecHeader self ::NoneVar "Version" False       ;@line 488
						CallMethod GetVersion self ::temp45                      ;@line 489
						ISubtract ::temp45 ::temp45 1000                         ;@line 489
						Cast ::temp46 ::temp45                                   ;@line 489
						StrCat ::temp39 "The version of " ::ModName_var          ;@line 489
						CallMethod ecText self ::NoneVar "Version" "Version" ::temp46 ::temp39 True False False  ;@line 489
						CallMethod ecCursor self ::NoneVar 1                     ;@line 491
						CallMethod ecHeader self ::NoneVar "Global information" False  ;@line 492
						CallMethod acNumFloat self ::temp45 "wetnessRate"        ;@line 493
						Cast ::temp46 ::temp45                                   ;@line 493
						CallMethod ecText self ::NoneVar "controlShouldRun" "Should Run" ::temp46 "Total number of effects that shuld run (are starting) or are running." True False False  ;@line 493
						CallMethod acNumFloat self ::temp45 "specular"           ;@line 494
						Cast ::temp39 ::temp45                                   ;@line 494
						CallMethod ecText self ::NoneVar "controlIsRunning" "Is running" ::temp39 "Total number of effects that are running or are stoppping." True False False  ;@line 494
						CallMethod acNumFloat self ::temp45 "autoStop"           ;@line 495
						Cast ::temp46 ::temp45                                   ;@line 495
						CallMethod ecText self ::NoneVar "controlAutoApplied" "Auto-applied" ::temp46 "Total number of effects that were automatically applied." True False False  ;@line 495
						CallMethod ecText self ::NoneVar "controlRefresh" "" "refresh" "" False False False  ;@line 496
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 497
						CallMethod ecHeader self ::NoneVar "Global control" False  ;@line 498
						CallMethod ecText self ::NoneVar "controlResetAllForced" "" "Reset all forced" "Reset all forced values." False False False  ;@line 499
						CallMethod ecText self ::NoneVar "controlStopAll" "" "Stop all effecs" "Starts the removal of all effects." False False False  ;@line 500
						CallMethod ecText self ::NoneVar "controlClearAllData" "" "Clear all data" "Removes all data, this will also stop all effects." False False False  ;@line 501
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 502
						CallMethod ecToggle self ::NoneVar "zombieRevive" "Zombies attempt self revival" False "Wrongfully terminated effects (\"forgotten\" by the game-engine) will attempt to self-revive.\nUntil aborted or successful the script will be baked into the save-file. The \"Auto-fix broken\" option of \"Auto-apply\" should be preferred." False False True  ;@line 503
						Jump _label13                                            ;@line 503
					_label13:
					.endCode
				.endFunction
				.function acDelFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param act Actor
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp76 String
						.local ::temp77 form
						.local ::temp78 Bool
					.endLocalTable
					.code
						StrCat ::temp76 storePrefix storeKey                     ;@line 594
						Cast ::temp77 act                                        ;@line 594
						CallStatic storageutil UnsetFloatValue ::temp78 ::temp77 ::temp76  ;@line 594
					.endCode
				.endFunction
				.function ActorStatus
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String
					.paramTable
						.param act Actor
					.endParamTable
					.localTable
						.local ::temp93 form
						.local ::temp94 Bool
						.local ::temp95 Bool
						.local ::temp96 Bool
						.local ::temp97 Bool
						.local ::temp98 Bool
					.endLocalTable
					.code
						Cast ::temp93 ::ability_var                              ;@line 626
						CallMethod HasSpell act ::temp94 ::temp93                ;@line 626
						JumpF ::temp94 _label54                                  ;@line 626
						CallMethod acHasFloat self ::temp95 act "wetnessRate"    ;@line 627
						JumpF ::temp95 _label55                                  ;@line 627
						CallMethod acHasFloat self ::temp96 act "specular"       ;@line 628
						JumpF ::temp96 _label56                                  ;@line 628
						CallMethod HasMagicEffect act ::temp97 ::effect_var      ;@line 629
						JumpF ::temp97 _label57                                  ;@line 629
						Return "Running"                                         ;@line 630
						Jump _label58                                            ;@line 630
					_label57:
						CallMethod acHasFloat self ::temp98 act "glossiness"     ;@line 632
						JumpF ::temp98 _label59                                  ;@line 632
						Return "Broken"                                          ;@line 633
						Jump _label58                                            ;@line 633
					_label59:
						Return "Restarting"                                      ;@line 635
					_label58:
						Jump _label60                                            ;@line 635
					_label56:
						Return "Starting"                                        ;@line 639
					_label60:
						Jump _label61                                            ;@line 639
					_label55:
						CallMethod acHasFloat self ::temp98 act "specular"       ;@line 642
						JumpF ::temp98 _label62                                  ;@line 642
						Return "Stopping"                                        ;@line 643
						Jump _label61                                            ;@line 643
					_label62:
						Return "Outdated"                                        ;@line 645
					_label61:
						Jump _label63                                            ;@line 645
					_label54:
						CallMethod HasMagicEffect act ::temp97 ::effect_var      ;@line 649
						JumpF ::temp97 _label64                                  ;@line 649
						Return "Stopping"                                        ;@line 650
						Jump _label63                                            ;@line 650
					_label64:
						CallMethod ActorConfigured self ::temp96 act             ;@line 651
						JumpF ::temp96 _label65                                  ;@line 651
						Return "Configured"                                      ;@line 652
						Jump _label63                                            ;@line 652
					_label65:
						CallMethod acHasFloat self ::temp98 act "Wetness"        ;@line 653
						JumpF ::temp98 _label66                                  ;@line 653
						Return "Dormant"                                         ;@line 654
						Jump _label63                                            ;@line 654
					_label66:
						Return ""                                                ;@line 656
					_label63:
					.endCode
				.endFunction
				.function acNumFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Int
					.paramTable
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp79 String
						.local ::temp80 Int
					.endLocalTable
					.code
						StrCat ::temp79 storePrefix storeKey                     ;@line 597
						CallStatic storageutil CountFloatValuePrefix ::temp80 ::temp79  ;@line 597
						Return ::temp80                                          ;@line 597
					.endCode
				.endFunction
				.function GetVersion
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Int
					.paramTable
					.endParamTable
					.localTable
					.endLocalTable
					.code
						Return 1039                                              ;@line 188
					.endCode
				.endFunction
				.function dumpSlotString
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param slotMask Int
						.param i Int
					.endParamTable
					.localTable
						.local ::temp160 objectreference
						.local ::temp161 String
						.local ::temp162 Bool
						.local a String
						.local b String
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp160 target                                    ;@line 1003
						CallStatic nioverride GetSkinPropertyString ::temp161 ::temp160 True slotMask 9 i  ;@line 1003
						Assign a ::temp161                                       ;@line 1003
						Cast ::temp160 target                                    ;@line 1004
						CallStatic nioverride GetSkinPropertyString ::temp161 ::temp160 False slotMask 9 i  ;@line 1004
						Assign b ::temp161                                       ;@line 1004
						Cast ::temp162 a                                         ;@line 1005
						JumpT ::temp162 _label67                                 ;@line 1005
						Cast ::temp162 b                                         ;@line 1005
					_label67:
						JumpF ::temp162 _label68                                 ;@line 1005
						Cast ::temp161 i                                         ;@line 1006
						StrCat ::temp161 " t" ::temp161                          ;@line 1006
						StrCat ::temp161 ::temp161 ": "                          ;@line 1006
						StrCat ::temp161 ::temp161 a                             ;@line 1006
						StrCat ::temp161 ::temp161 " "                           ;@line 1006
						StrCat ::temp161 ::temp161 b                             ;@line 1006
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp161 0     ;@line 1006
						Jump _label68                                            ;@line 1006
					_label68:
					.endCode
				.endFunction
				.function GotoState
					.userFlags 0	; Flags: 0x00000000
					.docString "Function that switches this object to the specified state"
					.return None
					.paramTable
						.param newState String
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod onEndState self ::NoneVar                     ;@line ??
						Assign ::State newState                                  ;@line ??
						CallMethod onBeginState self ::NoneVar                   ;@line ??
					.endCode
				.endFunction
				.function GetState
					.userFlags 0	; Flags: 0x00000000
					.docString "Function that returns the current state"
					.return String
					.paramTable
					.endParamTable
					.localTable
					.endLocalTable
					.code
						Return ::State                                           ;@line ??
					.endCode
				.endFunction
				.function Log
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param msg String
						.param level Int
						.param notify Bool
					.endParamTable
					.localTable
						.local ::temp210 String
						.local ::temp211 Float
						.local ::temp212 Int
						.local ::temp213 Bool
						.local msgLong String
						.local ::NoneVar None
						.local ::temp214 Bool
					.endLocalTable
					.code
						StrCat ::temp210 "[WF @ MCM] " msg                       ;@line 1163
						Assign msgLong ::temp210                                 ;@line 1163
						CallMethod ecGetFloat self ::temp211 "logLevel"          ;@line 1164
						Cast ::temp212 ::temp211                                 ;@line 1164
						CompareGTE ::temp213 level ::temp212                     ;@line 1164
						JumpF ::temp213 _label69                                 ;@line 1164
						CallStatic miscutil PrintConsole ::NoneVar msgLong       ;@line 1165
						Cast ::temp214 notify                                    ;@line 1166
						JumpF ::temp214 _label70                                 ;@line 1166
						CompareGT ::temp214 level 0                              ;@line 1166
						Cast ::temp214 ::temp214                                 ;@line 1166
					_label70:
						JumpF ::temp214 _label71                                 ;@line 1166
						CallStatic debug Notification ::NoneVar msg              ;@line 1167
						Jump _label71                                            ;@line 1167
					_label71:
						Jump _label69                                            ;@line 1167
					_label69:
						CallMethod ecGetFloat self ::temp211 "logFile"           ;@line 1170
						Cast ::temp212 ::temp211                                 ;@line 1170
						CompareGTE ::temp214 level ::temp212                     ;@line 1170
						JumpF ::temp214 _label72                                 ;@line 1170
						CallStatic WetFunctionMCM FLog ::NoneVar msgLong level   ;@line 1171
						Jump _label72                                            ;@line 1171
					_label72:
					.endCode
				.endFunction
				.function acSetFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param act Actor
						.param storeKey String
						.param value Float
					.endParamTable
					.localTable
						.local ::temp70 String
						.local ::temp71 form
						.local ::temp72 Float
					.endLocalTable
					.code
						StrCat ::temp70 storePrefix storeKey                     ;@line 588
						Cast ::temp71 act                                        ;@line 588
						CallStatic storageutil SetFloatValue ::temp72 ::temp71 ::temp70 value  ;@line 588
					.endCode
				.endFunction
				.function TextureMenu
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param showMenu Bool
					.endParamTable
					.localTable
						.local ::temp20 Bool
						.local ::temp21 String[]
						.local folder String
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp20 showMenu                                   ;@line 108
						JumpT ::temp20 _label73                                  ;@line 108
						Cast ::temp20 ::ecBurnIn_var                             ;@line 108
					_label73:
						Assign textureMenuShow ::temp20                          ;@line 108
						Assign folder "Data\\Textures\\Actors\\character\\WetFunction\\"  ;@line 109
						CallStatic miscutil FilesInFolder ::temp21 folder "dds"  ;@line 110
						Assign textureFiles ::temp21                             ;@line 110
						JumpF textureMenuShow _label74                           ;@line 112
						CallMethod ecHeader self ::NoneVar "Textures swapping - female" False  ;@line 113
						Jump _label74                                            ;@line 113
					_label74:
						CallMethod TextureCheck self ::NoneVar "wet_" "Body" "Body (specular)" "Use sweaty textures for the body." "111,110,101,100,011,010,001" "_s.dds" True  ;@line 115
						CallMethod TextureCheck self ::NoneVar "wethand_s" "Hand" "Hands (specular)" "Use sweaty texture for hands." "" ".dds" True  ;@line 116
						CallMethod TextureCheck self ::NoneVar "wethead_s" "Head" "Head (specular)" "Use sweaty texture for the head. This will cause a brief visual glich when swapping textures and may severely impact performance." "" ".dds" True  ;@line 117
						JumpF textureMenuShow _label75                           ;@line 118
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 119
						CallMethod ecHeader self ::NoneVar "Textures swapping - male" False  ;@line 120
						Jump _label75                                            ;@line 120
					_label75:
						CallMethod TextureCheck self ::NoneVar "male_wet_" "MaleBody" "Body (specular)" "Use sweaty textures for the body." "11,10,01" "_s.dds" True  ;@line 122
						CallMethod TextureCheck self ::NoneVar "male_wethand_s" "MaleHand" "Hands (specular)" "Use sweaty texture for hands." "" ".dds" True  ;@line 123
						CallMethod TextureCheck self ::NoneVar "male_wethead_s" "MaleHead" "Head (specular)" "Use sweaty texture for the head. This will cause a brief visual glich when swapping textures and may severely impact performance." "" ".dds" True  ;@line 124
						CallMethod TextureCheck self ::NoneVar "male_wet_d" "MaleBodyDiffuse" "Body (diffuse)" "Use sweaty diffue texture for the body. (e.g. for wet body hair)" "" ".dds" False  ;@line 125
						CallMethod TextureCheck self ::NoneVar "male_wetschlong_s" "MaleSchlong" "Schlong (specular)" "Use sweaty texture for the schlong." "" ".dds" True  ;@line 126
						Cast ::temp20 textureMenuShow                            ;@line 127
						JumpF ::temp20 _label76                                  ;@line 127
						Cast ::temp21 None                                       ;@line 127
						CompareEQ ::temp20 textureFiles ::temp21                 ;@line 127
						Cast ::temp20 ::temp20                                   ;@line 127
					_label76:
						JumpF ::temp20 _label77                                  ;@line 127
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 128
						CallMethod ecHeader self ::NoneVar "Texture folder missing?" False  ;@line 129
						CallMethod ecText self ::NoneVar "textureFolderMissing2" folder "" folder True False False  ;@line 130
						Jump _label77                                            ;@line 130
					_label77:
						Cast ::temp21 None                                       ;@line 133
						Assign textureFiles ::temp21                             ;@line 133
						Assign textureMenuShow False                             ;@line 134
					.endCode
				.endFunction
				.function Int2Hex
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String
					.paramTable
						.param num Int
						.param minLength Int
						.param nullX Bool
					.endParamTable
					.localTable
						.local ::temp207 Bool
						.local hex String
						.local out String
						.local ::temp208 Int
						.local ::temp209 String
						.local i Int
					.endLocalTable
					.code
						Assign hex "0123456789ABCDEF"                            ;@line 1144
						Assign out ""                                            ;@line 1145
					_label79:
						CompareGT ::temp207 num 0                                ;@line 1146
						JumpF ::temp207 _label78                                 ;@line 1146
						CallStatic math LogicalAnd ::temp208 num 15              ;@line 1147
						CallStatic stringutil GetNthChar ::temp209 hex ::temp208  ;@line 1147
						StrCat ::temp209 ::temp209 out                           ;@line 1147
						Assign out ::temp209                                     ;@line 1147
						CallStatic math RightShift ::temp208 num 4               ;@line 1148
						Assign num ::temp208                                     ;@line 1148
						Jump _label79                                            ;@line 1148
					_label78:
						CallStatic stringutil GetLength ::temp208 out            ;@line 1150
						ISubtract ::temp208 minLength ::temp208                  ;@line 1150
						Assign i ::temp208                                       ;@line 1150
					_label81:
						CompareGT ::temp207 i 0                                  ;@line 1151
						JumpF ::temp207 _label80                                 ;@line 1151
						StrCat ::temp209 "0" out                                 ;@line 1152
						Assign out ::temp209                                     ;@line 1152
						ISubtract ::temp208 i 1                                  ;@line 1153
						Assign i ::temp208                                       ;@line 1153
						Jump _label81                                            ;@line 1153
					_label80:
						JumpF nullX _label82                                     ;@line 1155
						StrCat ::temp209 "0x" out                                ;@line 1156
						Assign out ::temp209                                     ;@line 1156
						Jump _label82                                            ;@line 1156
					_label82:
						Return out                                               ;@line 1158
					.endCode
				.endFunction
				.function AutoApply
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param a Actor
						.param hours Float
						.param max Bool
						.param doFix Bool
					.endParamTable
					.localTable
						.local ::temp195 form
						.local ::temp196 Bool
						.local ::temp202 Float
						.local ::temp203 Float
						.local ::NoneVar None
						.local ::temp197 Bool
						.local ::temp198 Bool
						.local ::temp199 Bool
						.local ::temp200 actorbase
						.local ::temp201 Int
						.local sex Int
						.local S Float
						.local n Float
					.endLocalTable
					.code
						Cast ::temp195 ::ability_var                             ;@line 1105
						CallMethod HasSpell a ::temp196 ::temp195                ;@line 1105
						JumpF ::temp196 _label83                                 ;@line 1105
						CallMethod HasMagicEffect a ::temp197 ::effect_var       ;@line 1106
						Not ::temp197 ::temp197                                  ;@line 1106
						JumpF ::temp197 _label84                                 ;@line 1106
						CallMethod AddSpell a ::temp198 ::ability_var False      ;@line 1107
						Jump _label84                                            ;@line 1107
					_label84:
						CallMethod acHasFloat self ::temp198 a "autoStop"        ;@line 1109
						Not ::temp197 ::temp198                                  ;@line 1109
						JumpF ::temp197 _label85                                 ;@line 1109
						Return False                                             ;@line 1110
						Jump _label85                                            ;@line 1110
					_label85:
						Jump _label86                                            ;@line 1110
					_label83:
						CallMethod HasKeyword a ::temp198 ::ActorTypeNPC_var     ;@line 1113
						Not ::temp197 ::temp198                                  ;@line 1113
						Cast ::temp199 ::temp197                                 ;@line 1113
						JumpT ::temp199 _label87                                 ;@line 1113
						CallMethod ecGetBool self ::temp198 "autoBeast"          ;@line 1113
						Not ::temp198 ::temp198                                  ;@line 1113
						Cast ::temp198 ::temp198                                 ;@line 1113
						JumpF ::temp198 _label88                                 ;@line 1113
						CallMethod HasKeyword a ::temp199 ::IsBeastRace_var      ;@line 1113
						Cast ::temp198 ::temp199                                 ;@line 1113
					_label88:
						Cast ::temp199 ::temp198                                 ;@line 1113
					_label87:
						Cast ::temp197 ::temp199                                 ;@line 1113
						JumpT ::temp197 _label89                                 ;@line 1113
						CallMethod ecGetBool self ::temp197 "autoVampire"        ;@line 1113
						Not ::temp198 ::temp197                                  ;@line 1113
						Cast ::temp198 ::temp198                                 ;@line 1113
						JumpF ::temp198 _label90                                 ;@line 1113
						CallMethod HasKeyword a ::temp197 ::Vampire_var          ;@line 1113
						Cast ::temp198 ::temp197                                 ;@line 1113
					_label90:
						Cast ::temp197 ::temp198                                 ;@line 1113
					_label89:
						JumpF ::temp197 _label91                                 ;@line 1113
						Return False                                             ;@line 1114
						Jump _label91                                            ;@line 1114
					_label91:
						CallMethod GetLeveledActorBase a ::temp200               ;@line 1116
						CallMethod GetSex ::temp200 ::temp201                    ;@line 1116
						Assign sex ::temp201                                     ;@line 1116
						CompareEQ ::temp199 sex 0                                ;@line 1117
						Cast ::temp198 ::temp199                                 ;@line 1117
						JumpF ::temp198 _label92                                 ;@line 1117
						CallMethod ecGetBool self ::temp198 "autoMale"           ;@line 1117
						Not ::temp197 ::temp198                                  ;@line 1117
						Cast ::temp198 ::temp197                                 ;@line 1117
					_label92:
						JumpF ::temp198 _label93                                 ;@line 1117
						Return False                                             ;@line 1118
						Jump _label93                                            ;@line 1118
					_label93:
						CompareEQ ::temp199 sex 1                                ;@line 1120
						Cast ::temp197 ::temp199                                 ;@line 1120
						JumpF ::temp197 _label94                                 ;@line 1120
						CallMethod ecGetBool self ::temp197 "autoFemale"         ;@line 1120
						Not ::temp198 ::temp197                                  ;@line 1120
						Cast ::temp197 ::temp198                                 ;@line 1120
					_label94:
						JumpF ::temp197 _label86                                 ;@line 1120
						Return False                                             ;@line 1121
						Jump _label86                                            ;@line 1121
					_label86:
						CallMethod acGetFloat self ::temp202 a "autoStop" 0.000000  ;@line 1124
						Assign S ::temp202                                       ;@line 1124
						CallStatic utility GetCurrentGameTime ::temp202          ;@line 1125
						FDivide ::temp203 hours 24.000000                        ;@line 1125
						FAdd ::temp202 ::temp202 ::temp203                       ;@line 1125
						Assign n ::temp202                                       ;@line 1125
						Cast ::temp198 max                                       ;@line 1126
						JumpF ::temp198 _label95                                 ;@line 1126
						CompareLT ::temp199 n S                                  ;@line 1126
						Cast ::temp198 ::temp199                                 ;@line 1126
					_label95:
						JumpF ::temp198 _label96                                 ;@line 1126
						Assign n S                                               ;@line 1127
						Jump _label96                                            ;@line 1127
					_label96:
						CallMethod acSetFloat self ::NoneVar a "autoStop" n      ;@line 1129
						Cast ::temp195 ::ability_var                             ;@line 1130
						CallMethod HasSpell a ::temp197 ::temp195                ;@line 1130
						Not ::temp196 ::temp197                                  ;@line 1130
						JumpF ::temp196 _label97                                 ;@line 1130
						CallMethod acSetFloat self ::NoneVar a "wetnessRate" 0.000000  ;@line 1131
						CallMethod AddSpell a ::temp199 ::ability_var True       ;@line 1132
						Jump _label97                                            ;@line 1132
					_label97:
						Return True                                              ;@line 1134
					.endCode
				.endFunction
				.function dumpSlot
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param slot Int
					.endParamTable
					.localTable
						.local ::temp155 Int
						.local ::temp156 form
						.local ::NoneVar None
						.local ::temp159 Bool
						.local slotMask Int
						.local f form
						.local n String
						.local ::temp157 String
						.local ::temp158 String
						.local i Int
					.endLocalTable
					.code
						IAdd ::temp155 slot -30                                  ;@line 984
						CallStatic math LeftShift ::temp155 1 ::temp155          ;@line 984
						Assign slotMask ::temp155                                ;@line 984
						CallMethod GetWornForm target ::temp156 slotMask         ;@line 985
						Assign f ::temp156                                       ;@line 985
						Assign n "N/A"                                           ;@line 986
						JumpF f _label98                                         ;@line 987
						CallMethod GetName f ::temp157                           ;@line 988
						StrCat ::temp157 ::temp157 " "                           ;@line 988
						CallStatic wornobject GetDisplayName ::temp158 target 0 slotMask  ;@line 988
						StrCat ::temp157 ::temp157 ::temp158                     ;@line 988
						Assign n ::temp157                                       ;@line 988
						Jump _label98                                            ;@line 988
					_label98:
						Cast ::temp158 slot                                      ;@line 990
						StrCat ::temp157 "slot:" ::temp158                       ;@line 990
						StrCat ::temp158 ::temp157 " worn:"                      ;@line 990
						Cast ::temp157 f                                         ;@line 990
						StrCat ::temp157 ::temp158 ::temp157                     ;@line 990
						StrCat ::temp158 ::temp157 " "                           ;@line 990
						StrCat ::temp157 ::temp158 n                             ;@line 990
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp157 0     ;@line 990
						Assign i 2                                               ;@line 991
					_label100:
						CompareLTE ::temp159 i 3                                 ;@line 992
						JumpF ::temp159 _label99                                 ;@line 992
						CallMethod dumpSlotFloat self ::NoneVar slotMask i       ;@line 993
						IAdd ::temp155 i 1                                       ;@line 994
						Assign i ::temp155                                       ;@line 994
						Jump _label100                                           ;@line 994
					_label99:
						Assign i 0                                               ;@line 996
					_label102:
						CompareLTE ::temp159 i 8                                 ;@line 997
						JumpF ::temp159 _label101                                ;@line 997
						CallMethod dumpSlotString self ::NoneVar slotMask i      ;@line 998
						IAdd ::temp155 i 1                                       ;@line 999
						Assign i ::temp155                                       ;@line 999
						Jump _label102                                           ;@line 999
					_label101:
					.endCode
				.endFunction
				.function dumpNodeFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param node String
						.param i Int
					.endParamTable
					.localTable
						.local ::temp150 objectreference
						.local ::temp151 Float
						.local ::temp152 Bool
						.local a Float
						.local b Float
						.local ::temp153 String
						.local ::temp154 String
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp150 target                                    ;@line 976
						CallStatic nioverride GetNodePropertyFloat ::temp151 ::temp150 True node i -1  ;@line 976
						Assign a ::temp151                                       ;@line 976
						Cast ::temp150 target                                    ;@line 977
						CallStatic nioverride GetNodePropertyFloat ::temp151 ::temp150 False node i -1  ;@line 977
						Assign b ::temp151                                       ;@line 977
						Cast ::temp152 a                                         ;@line 978
						JumpT ::temp152 _label103                                ;@line 978
						Cast ::temp152 b                                         ;@line 978
					_label103:
						JumpF ::temp152 _label104                                ;@line 978
						Cast ::temp153 i                                         ;@line 979
						StrCat ::temp153 " f" ::temp153                          ;@line 979
						StrCat ::temp153 ::temp153 ": "                          ;@line 979
						Cast ::temp154 a                                         ;@line 979
						StrCat ::temp154 ::temp153 ::temp154                     ;@line 979
						StrCat ::temp153 ::temp154 " "                           ;@line 979
						Cast ::temp154 b                                         ;@line 979
						StrCat ::temp154 ::temp153 ::temp154                     ;@line 979
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp154 0     ;@line 979
						Jump _label104                                           ;@line 979
					_label104:
					.endCode
				.endFunction
				.function InitSoftDeps
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp3 Int
						.local ::temp4 Bool
						.local ::temp5 Bool
						.local ::temp10 Float
						.local ::temp11 Float
						.local ::temp6 form
						.local ::temp7 faction
						.local ::temp8 quest
						.local ::temp9 globalvariable
					.endLocalTable
					.code
						CallStatic game GetModByName ::temp3 "SexLabAroused.esm"  ;@line 49
						CompareEQ ::temp4 ::temp3 255                            ;@line 49
						Not ::temp4 ::temp4                                      ;@line 49
						Cast ::temp4 ::temp4                                     ;@line 49
						JumpF ::temp4 _label105                                  ;@line 49
						CallStatic game GetModByName ::temp3 "SexLabAroused.esm"  ;@line 49
						CompareEQ ::temp5 ::temp3 -1                             ;@line 49
						Not ::temp5 ::temp5                                      ;@line 49
						Cast ::temp4 ::temp5                                     ;@line 49
					_label105:
						JumpF ::temp4 _label106                                  ;@line 49
						CallStatic game GetFormFromFile ::temp6 261174 "SexLabAroused.esm"  ;@line 50
						Cast ::temp7 ::temp6                                     ;@line 50
						Assign ::SLAfac_var ::temp7                              ;@line 50
						Jump _label107                                           ;@line 50
					_label106:
						Cast ::temp7 None                                        ;@line 52
						Assign ::SLAfac_var ::temp7                              ;@line 52
					_label107:
						CallStatic game GetModByName ::temp3 "SexLab.esm"        ;@line 54
						CompareEQ ::temp5 ::temp3 255                            ;@line 54
						Not ::temp5 ::temp5                                      ;@line 54
						Cast ::temp5 ::temp5                                     ;@line 54
						JumpF ::temp5 _label108                                  ;@line 54
						CallStatic game GetModByName ::temp3 "SexLab.esm"        ;@line 54
						CompareEQ ::temp4 ::temp3 -1                             ;@line 54
						Not ::temp4 ::temp4                                      ;@line 54
						Cast ::temp5 ::temp4                                     ;@line 54
					_label108:
						JumpF ::temp5 _label109                                  ;@line 54
						CallStatic game GetFormFromFile ::temp6 3426 "SexLab.esm"  ;@line 55
						Cast ::temp8 ::temp6                                     ;@line 55
						Assign ::SexLab_var ::temp8                              ;@line 55
						Jump _label110                                           ;@line 55
					_label109:
						Cast ::temp8 None                                        ;@line 57
						Assign ::SexLab_var ::temp8                              ;@line 57
					_label110:
						CallStatic game GetModByName ::temp3 "SexLabSkoomaWhore.esp"  ;@line 59
						CompareEQ ::temp4 ::temp3 255                            ;@line 59
						Not ::temp4 ::temp4                                      ;@line 59
						Cast ::temp4 ::temp4                                     ;@line 59
						JumpF ::temp4 _label111                                  ;@line 59
						CallStatic game GetModByName ::temp3 "SexLabSkoomaWhore.esp"  ;@line 59
						CompareEQ ::temp5 ::temp3 -1                             ;@line 59
						Not ::temp5 ::temp5                                      ;@line 59
						Cast ::temp4 ::temp5                                     ;@line 59
					_label111:
						JumpF ::temp4 _label112                                  ;@line 59
						CallStatic game GetFormFromFile ::temp6 7581 "SexLabSkoomaWhore.esp"  ;@line 60
						Cast ::temp9 ::temp6                                     ;@line 60
						Assign ::SLSWp_var ::temp9                               ;@line 60
						CallStatic game GetFormFromFile ::temp6 69138 "SexLabSkoomaWhore.esp"  ;@line 61
						Cast ::temp9 ::temp6                                     ;@line 61
						Assign ::SLSWmk_var ::temp9                              ;@line 61
						CallStatic game GetFormFromFile ::temp6 69139 "SexLabSkoomaWhore.esp"  ;@line 62
						Cast ::temp9 ::temp6                                     ;@line 62
						Assign ::SLSWm_var ::temp9                               ;@line 62
						CallStatic game GetFormFromFile ::temp6 82973 "SexLabSkoomaWhore.esp"  ;@line 63
						Cast ::temp9 ::temp6                                     ;@line 63
						Assign ::SLSWai_var ::temp9                              ;@line 63
						Cast ::temp5 ::SLSWp_var                                 ;@line 64
						JumpF ::temp5 _label113                                  ;@line 64
						Cast ::temp5 ::SLSWmk_var                                ;@line 64
					_label113:
						Cast ::temp5 ::temp5                                     ;@line 64
						JumpF ::temp5 _label114                                  ;@line 64
						Cast ::temp5 ::SLSWm_var                                 ;@line 64
					_label114:
						Cast ::temp5 ::temp5                                     ;@line 64
						JumpF ::temp5 _label115                                  ;@line 64
						Cast ::temp5 ::SLSWai_var                                ;@line 64
					_label115:
						Not ::temp5 ::temp5                                      ;@line 64
						JumpF ::temp5 _label116                                  ;@line 64
						Cast ::temp9 None                                        ;@line 65
						Assign ::SLSWai_var ::temp9                              ;@line 65
						Jump _label116                                           ;@line 65
					_label116:
						Jump _label117                                           ;@line 65
					_label112:
						Cast ::temp9 None                                        ;@line 68
						Assign ::SLSWai_var ::temp9                              ;@line 68
					_label117:
						CallStatic game GetModByName ::temp3 "Frostfall.esp"     ;@line 70
						CompareEQ ::temp5 ::temp3 255                            ;@line 70
						Not ::temp5 ::temp5                                      ;@line 70
						Cast ::temp5 ::temp5                                     ;@line 70
						JumpF ::temp5 _label118                                  ;@line 70
						CallStatic game GetModByName ::temp3 "Frostfall.esp"     ;@line 70
						CompareEQ ::temp4 ::temp3 -1                             ;@line 70
						Not ::temp4 ::temp4                                      ;@line 70
						Cast ::temp5 ::temp4                                     ;@line 70
					_label118:
						Cast ::temp5 ::temp5                                     ;@line 70
						JumpF ::temp5 _label119                                  ;@line 70
						CallStatic frostutil GetAPIVersion ::temp10              ;@line 70
						Cast ::temp11 1                                          ;@line 70
						CompareGTE ::temp4 ::temp10 ::temp11                     ;@line 70
						Cast ::temp5 ::temp4                                     ;@line 70
					_label119:
						Assign ::Frostfall_var ::temp5                           ;@line 70
						CallStatic game GetModByName ::temp3 "ZaZAnimationPack.esm"  ;@line 71
						CompareEQ ::temp4 ::temp3 255                            ;@line 71
						Not ::temp4 ::temp4                                      ;@line 71
						Cast ::temp4 ::temp4                                     ;@line 71
						JumpF ::temp4 _label120                                  ;@line 71
						CallStatic game GetModByName ::temp3 "ZaZAnimationPack.esm"  ;@line 71
						CompareEQ ::temp5 ::temp3 -1                             ;@line 71
						Not ::temp5 ::temp5                                      ;@line 71
						Cast ::temp4 ::temp5                                     ;@line 71
					_label120:
						Assign ::HasZaZ_var ::temp4                              ;@line 71
						CallStatic game GetModByName ::temp3 "Devious Devices - Integration.esm"  ;@line 72
						CompareEQ ::temp5 ::temp3 255                            ;@line 72
						Not ::temp5 ::temp5                                      ;@line 72
						Cast ::temp5 ::temp5                                     ;@line 72
						JumpF ::temp5 _label121                                  ;@line 72
						CallStatic game GetModByName ::temp3 "Devious Devices - Integration.esm"  ;@line 72
						CompareEQ ::temp4 ::temp3 -1                             ;@line 72
						Not ::temp4 ::temp4                                      ;@line 72
						Cast ::temp5 ::temp4                                     ;@line 72
					_label121:
						Assign ::HasDDi_var ::temp5                              ;@line 72
						CallStatic game GetModByName ::temp3 "Devious Devices - Assets.esm"  ;@line 73
						CompareEQ ::temp4 ::temp3 255                            ;@line 73
						Not ::temp4 ::temp4                                      ;@line 73
						Cast ::temp4 ::temp4                                     ;@line 73
						JumpF ::temp4 _label122                                  ;@line 73
						CallStatic game GetModByName ::temp3 "Devious Devices - Assets.esm"  ;@line 73
						CompareEQ ::temp5 ::temp3 -1                             ;@line 73
						Not ::temp5 ::temp5                                      ;@line 73
						Cast ::temp4 ::temp5                                     ;@line 73
					_label122:
						Assign ::HasDDa_var ::temp4                              ;@line 73
					.endCode
				.endFunction
				.function dumpNode
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param node String
						.param suffix String
					.endParamTable
					.localTable
						.local ::temp144 String
						.local ::NoneVar None
						.local ::temp145 Bool
						.local i Int
						.local ::temp146 Int
					.endLocalTable
					.code
						StrCat ::temp144 "node:" node                            ;@line 956
						StrCat ::temp144 ::temp144 " "                           ;@line 956
						StrCat ::temp144 ::temp144 suffix                        ;@line 956
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp144 0     ;@line 956
						Assign i 2                                               ;@line 957
					_label124:
						CompareLTE ::temp145 i 3                                 ;@line 958
						JumpF ::temp145 _label123                                ;@line 958
						CallMethod dumpNodeFloat self ::NoneVar node i           ;@line 959
						IAdd ::temp146 i 1                                       ;@line 960
						Assign i ::temp146                                       ;@line 960
						Jump _label124                                           ;@line 960
					_label123:
						Assign i 0                                               ;@line 962
					_label126:
						CompareLTE ::temp145 i 8                                 ;@line 963
						JumpF ::temp145 _label125                                ;@line 963
						CallMethod dumpNodeString self ::NoneVar node i          ;@line 964
						IAdd ::temp146 i 1                                       ;@line 965
						Assign i ::temp146                                       ;@line 965
						Jump _label126                                           ;@line 965
					_label125:
					.endCode
				.endFunction
				.function TextureCheck
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param base String
						.param storeKey String
						.param label String
						.param desc String
						.param partList String
						.param suffix String
						.param default Bool
					.endParamTable
					.localTable
						.local ::temp12 String
						.local ::temp13 String[]
						.local ::temp14 Bool
						.local ::temp15 Int
						.local ::temp18 form
						.local texMissing String
						.local hasTex Bool
						.local parts String[]
						.local i Int
						.local ::temp16 Bool
						.local ::temp17 Bool
						.local fn String
						.local ::NoneVar None
						.local ::temp19 String
					.endLocalTable
					.code
						StrCat ::temp12 "texture" storeKey                       ;@line 77
						Assign storeKey ::temp12                                 ;@line 77
						Assign texMissing ""                                     ;@line 78
						Assign hasTex True                                       ;@line 79
						ArrayCreate ::temp13 1                                   ;@line 80
						Assign parts ::temp13                                    ;@line 80
						Assign ::temp12 ""                                       ;@line 81
						ArraySetElement parts 0 ::temp12                         ;@line 81
						CompareEQ ::temp14 partList ""                           ;@line 82
						Not ::temp14 ::temp14                                    ;@line 82
						JumpF ::temp14 _label127                                 ;@line 82
						CallStatic stringutil Split ::temp13 partList ","        ;@line 83
						Assign parts ::temp13                                    ;@line 83
						Jump _label127                                           ;@line 83
					_label127:
						Assign i 0                                               ;@line 85
					_label133:
						ArrayLength ::temp15 parts                               ;@line 86
						CompareLT ::temp14 i ::temp15                            ;@line 86
						JumpF ::temp14 _label128                                 ;@line 86
						ArrayGetElement ::temp12 parts i                         ;@line 87
						StrCat ::temp12 base ::temp12                            ;@line 87
						StrCat ::temp12 ::temp12 suffix                          ;@line 87
						Assign fn ::temp12                                       ;@line 87
						Cast ::temp13 None                                       ;@line 88
						CompareEQ ::temp16 textureFiles ::temp13                 ;@line 88
						Cast ::temp16 ::temp16                                   ;@line 88
						JumpT ::temp16 _label129                                 ;@line 88
						ArrayFindElement textureFiles ::temp15 fn 0              ;@line 88
						CompareLT ::temp17 ::temp15 0                            ;@line 88
						Cast ::temp16 ::temp17                                   ;@line 88
					_label129:
						JumpF ::temp16 _label130                                 ;@line 88
						Assign hasTex False                                      ;@line 89
						CompareEQ ::temp17 texMissing ""                         ;@line 90
						JumpF ::temp17 _label131                                 ;@line 90
						Assign texMissing fn                                     ;@line 91
						Jump _label132                                           ;@line 91
					_label131:
						StrCat ::temp12 fn ", ..."                               ;@line 93
						Assign texMissing ::temp12                               ;@line 93
					_label132:
						Jump _label130                                           ;@line 93
					_label130:
						IAdd ::temp15 i 1                                        ;@line 96
						Assign i ::temp15                                        ;@line 96
						Jump _label133                                           ;@line 96
					_label128:
						StrCat ::temp12 "wf_aux_" storeKey                       ;@line 98
						Cast ::temp15 hasTex                                     ;@line 98
						Cast ::temp18 self                                       ;@line 98
						CallStatic storageutil SetIntValue ::temp15 ::temp18 ::temp12 ::temp15  ;@line 98
						JumpF textureMenuShow _label134                          ;@line 99
						Not ::temp17 hasTex                                      ;@line 100
						CallMethod ecToggle self ::NoneVar storeKey label default desc ::temp17 False True  ;@line 100
						Not ::temp16 hasTex                                      ;@line 101
						Cast ::temp14 ::temp16                                   ;@line 101
						JumpT ::temp14 _label135                                 ;@line 101
						Cast ::temp14 ::ecBurnIn_var                             ;@line 101
					_label135:
						JumpF ::temp14 _label136                                 ;@line 101
						StrCat ::temp12 storeKey "_missing"                      ;@line 102
						StrCat ::temp19 "Missing: " texMissing                   ;@line 102
						CallMethod ecText self ::NoneVar ::temp12 texMissing "missing" ::temp19 True False False  ;@line 102
						Jump _label136                                           ;@line 102
					_label136:
						Jump _label134                                           ;@line 102
					_label134:
					.endCode
				.endFunction
				.function dumpAc
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String
					.paramTable
						.param k String
					.endParamTable
					.localTable
						.local ::temp140 String
						.local ::temp141 Bool
						.local msg String
						.local ::temp142 Float
						.local ::temp143 String
					.endLocalTable
					.code
						StrCat ::temp140 " " k                                   ;@line 947
						StrCat ::temp140 ::temp140 "="                           ;@line 947
						Assign msg ::temp140                                     ;@line 947
						CallMethod acHasFloat self ::temp141 target k            ;@line 948
						JumpF ::temp141 _label137                                ;@line 948
						CallMethod acGetFloat self ::temp142 target k 0.000000   ;@line 949
						Cast ::temp140 ::temp142                                 ;@line 949
						StrCat ::temp140 msg ::temp140                           ;@line 949
						Return ::temp140                                         ;@line 949
						Jump _label138                                           ;@line 949
					_label137:
						StrCat ::temp143 msg "N/A"                               ;@line 951
						Return ::temp143                                         ;@line 951
					_label138:
					.endCode
				.endFunction
				.function OnGameReload
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod InitSoftDeps self ::NoneVar                   ;@line 138
						CallMethod TextureMenu self ::NoneVar False              ;@line 139
						Assign systemDumped False                                ;@line 140
						CallParent OnGameReload ::NoneVar                        ;@line 141
					.endCode
				.endFunction
				.function UpdateKeywords
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp55 form
						.local ::temp56 String[]
						.local ::temp57 Int
						.local ::temp58 Form[]
						.local ::temp59 Bool
						.local ::NoneVar None
						.local kwdCommon String
						.local kwdNames String[]
						.local kwds Form[]
						.local kwdCount Int
						.local i Int
						.local ::temp60 String
						.local ::temp61 Bool
						.local kwdName String
					.endLocalTable
					.code
						Assign kwdCommon "zad_Lockable"                          ;@line 536
						Cast ::temp55 self                                       ;@line 537
						CallStatic storageutil StringListToArray ::temp56 ::temp55 autoWornStore  ;@line 537
						Assign kwdNames ::temp56                                 ;@line 537
						ArrayLength ::temp57 kwdNames                            ;@line 538
						IAdd ::temp57 ::temp57 1                                 ;@line 538
						Cast ::temp55 None                                       ;@line 538
						CallStatic utility CreateFormArray ::temp58 ::temp57 ::temp55  ;@line 538
						Assign kwds ::temp58                                     ;@line 538
						Assign kwdCount 0                                        ;@line 539
						ArrayLength ::temp57 kwdNames                            ;@line 540
						Assign i ::temp57                                        ;@line 540
					_label141:
						CompareGT ::temp59 i 0                                   ;@line 541
						JumpF ::temp59 _label139                                 ;@line 541
						ISubtract ::temp57 i 1                                   ;@line 542
						Assign i ::temp57                                        ;@line 542
						ArrayGetElement ::temp60 kwdNames i                      ;@line 543
						Assign kwdName ::temp60                                  ;@line 543
						CompareEQ ::temp61 kwdName kwdCommon                     ;@line 544
						Not ::temp61 ::temp61                                    ;@line 544
						JumpF ::temp61 _label140                                 ;@line 544
						CallMethod ProcessKeyword self ::temp57 kwds kwdCount kwdName  ;@line 545
						Assign kwdCount ::temp57                                 ;@line 545
						Jump _label140                                           ;@line 545
					_label140:
						Jump _label141                                           ;@line 545
					_label139:
						ArrayFindElement kwdNames ::temp57 kwdCommon 0           ;@line 548
						CompareGTE ::temp61 ::temp57 0                           ;@line 548
						JumpF ::temp61 _label142                                 ;@line 548
						CallMethod ProcessKeyword self ::temp57 kwds kwdCount kwdCommon  ;@line 549
						Assign kwdCount ::temp57                                 ;@line 549
						Jump _label142                                           ;@line 549
					_label142:
						Cast ::temp60 kwdCount                                   ;@line 551
						StrCat ::temp60 "Updated worn keywords: " ::temp60       ;@line 551
						CallMethod Log self ::NoneVar ::temp60 -2 True           ;@line 551
						JumpF kwdCount _label143                                 ;@line 552
						CallStatic utility ResizeFormArray ::temp58 kwds kwdCount None  ;@line 553
						Assign autoWornKwds ::temp58                             ;@line 553
						Jump _label144                                           ;@line 553
					_label143:
						Cast ::temp58 None                                       ;@line 555
						Assign autoWornKwds ::temp58                             ;@line 555
					_label144:
					.endCode
				.endFunction
				.function OnSexLabAnim
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param ev String
						.param tid String
						.param n Float
						.param sender form
					.endParamTable
					.localTable
						.local ::temp183 sslthreadcontroller
						.local ::temp184 Bool
						.local ::temp185 Bool
						.local ::temp186 Float
						.local ::temp187 Bool
						.local ::temp188 sslactoralias[]
						.local ::temp189 Int
						.local c sslthreadcontroller
						.local playerOnly Bool
						.local isStart Bool
						.local isEnd Bool
						.local isUpdate Bool
						.local a Actor
						.local rateBase Float
						.local rateEnjoy Float
						.local ratePain Float
						.local autoStart Bool
						.local aa sslactoralias[]
						.local i Int
						.local ::temp190 sslactoralias
						.local ::temp191 Actor
						.local ::temp192 Float
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp183 sender                                    ;@line 1063
						Assign c ::temp183                                       ;@line 1063
						CallMethod ecGetBool self ::temp184 "sexlabPlayerOnly"   ;@line 1064
						Assign playerOnly ::temp184                              ;@line 1064
						Not ::temp184 c                                          ;@line 1065
						Cast ::temp184 ::temp184                                 ;@line 1065
						JumpT ::temp184 _label145                                ;@line 1065
						PropGet HasPlayer c ::temp185                            ;@line 1065
						Not ::temp185 ::temp185                                  ;@line 1065
						Cast ::temp185 ::temp185                                 ;@line 1065
						JumpF ::temp185 _label146                                ;@line 1065
						Cast ::temp185 playerOnly                                ;@line 1065
					_label146:
						Cast ::temp184 ::temp185                                 ;@line 1065
					_label145:
						JumpF ::temp184 _label147                                ;@line 1065
						Return None                                              ;@line 1066
						Jump _label147                                           ;@line 1066
					_label147:
						CompareEQ ::temp185 ev "AnimationStart"                  ;@line 1069
						Cast ::temp185 ::temp185                                 ;@line 1069
						JumpT ::temp185 _label148                                ;@line 1069
						CompareEQ ::temp184 ev "ActorChangeEnd"                  ;@line 1069
						Cast ::temp185 ::temp184                                 ;@line 1069
					_label148:
						Assign isStart ::temp185                                 ;@line 1069
						CompareEQ ::temp184 ev "AnimationEnd"                    ;@line 1070
						Cast ::temp184 ::temp184                                 ;@line 1070
						JumpT ::temp184 _label149                                ;@line 1070
						CompareEQ ::temp185 ev "ActorChangeStart"                ;@line 1070
						Cast ::temp184 ::temp185                                 ;@line 1070
					_label149:
						Assign isEnd ::temp184                                   ;@line 1070
						Not ::temp185 isStart                                    ;@line 1071
						Cast ::temp185 ::temp185                                 ;@line 1071
						JumpF ::temp185 _label150                                ;@line 1071
						Not ::temp184 isEnd                                      ;@line 1071
						Cast ::temp185 ::temp184                                 ;@line 1071
					_label150:
						Assign isUpdate ::temp185                                ;@line 1071
						CallMethod ecGetFloat self ::temp186 "sexlabBase"        ;@line 1073
						Assign rateBase ::temp186                                ;@line 1073
						CallMethod ecGetFloat self ::temp186 "sexlabEnjoyment"   ;@line 1074
						FMultiply ::temp186 ::temp186 0.010000                   ;@line 1074
						Assign rateEnjoy ::temp186                               ;@line 1074
						CallMethod ecGetFloat self ::temp186 "sexlabPain"        ;@line 1075
						FMultiply ::temp186 ::temp186 0.010000                   ;@line 1075
						Assign ratePain ::temp186                                ;@line 1075
						CallMethod ecGetBool self ::temp184 "sexlabAuto"         ;@line 1076
						Cast ::temp187 ::temp184                                 ;@line 1076
						JumpF ::temp187 _label151                                ;@line 1076
						CallMethod ecGetBool self ::temp185 "sexlabAutoPlayer"   ;@line 1076
						Not ::temp185 ::temp185                                  ;@line 1076
						Cast ::temp185 ::temp185                                 ;@line 1076
						JumpT ::temp185 _label152                                ;@line 1076
						PropGet HasPlayer c ::temp187                            ;@line 1076
						Cast ::temp185 ::temp187                                 ;@line 1076
					_label152:
						Cast ::temp187 ::temp185                                 ;@line 1076
					_label151:
						Assign autoStart ::temp187                               ;@line 1076
						PropGet ActorAlias c ::temp188                           ;@line 1077
						Assign aa ::temp188                                      ;@line 1077
						PropGet ActorCount c ::temp189                           ;@line 1078
						Assign i ::temp189                                       ;@line 1078
					_label162:
						CompareGT ::temp184 i 0                                  ;@line 1079
						JumpF ::temp184 _label153                                ;@line 1079
						ISubtract ::temp189 i 1                                  ;@line 1080
						Assign i ::temp189                                       ;@line 1080
						ArrayGetElement ::temp190 aa i                           ;@line 1081
						PropGet ActorRef ::temp190 ::temp191                     ;@line 1081
						Assign a ::temp191                                       ;@line 1081
						Not ::temp185 playerOnly                                 ;@line 1082
						Cast ::temp185 ::temp185                                 ;@line 1082
						JumpT ::temp185 _label154                                ;@line 1082
						CompareEQ ::temp187 a player                             ;@line 1082
						Cast ::temp185 ::temp187                                 ;@line 1082
					_label154:
						JumpF ::temp185 _label155                                ;@line 1082
						JumpF isUpdate _label156                                 ;@line 1083
						ArrayGetElement ::temp190 aa i                           ;@line 1084
						CallMethod GetEnjoyment ::temp190 ::temp189              ;@line 1084
						Cast ::temp186 ::temp189                                 ;@line 1084
						FMultiply ::temp186 rateEnjoy ::temp186                  ;@line 1084
						FAdd ::temp186 rateBase ::temp186                        ;@line 1084
						ArrayGetElement ::temp190 aa i                           ;@line 1084
						CallMethod GetPain ::temp190 ::temp189                   ;@line 1084
						Cast ::temp192 ::temp189                                 ;@line 1084
						FMultiply ::temp192 ratePain ::temp192                   ;@line 1084
						FAdd ::temp186 ::temp186 ::temp192                       ;@line 1084
						CallMethod acSetFloat self ::NoneVar a "sexLabRate" ::temp186  ;@line 1084
						Jump _label157                                           ;@line 1084
					_label156:
						JumpF isEnd _label158                                    ;@line 1085
						CallMethod acDelFloat self ::NoneVar a "sexLabRate"      ;@line 1086
						Jump _label157                                           ;@line 1086
					_label158:
						JumpF isStart _label157                                  ;@line 1087
						CallMethod acSetFloat self ::NoneVar a "sexLabRate" rateBase  ;@line 1088
						Jump _label157                                           ;@line 1088
					_label157:
						JumpF autoStart _label159                                ;@line 1090
						JumpF isEnd _label160                                    ;@line 1091
						CallMethod ecGetFloat self ::temp192 "sexlabAutoLinger"  ;@line 1092
						CallMethod AutoApply self ::temp187 a ::temp192 False True  ;@line 1092
						Jump _label161                                           ;@line 1092
					_label160:
						CallMethod AutoApply self ::temp187 a 24.000000 True True  ;@line 1094
					_label161:
						Jump _label159                                           ;@line 1094
					_label159:
						Jump _label155                                           ;@line 1094
					_label155:
						Jump _label162                                           ;@line 1094
					_label153:
					.endCode
				.endFunction
				.function acClearFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp81 String
						.local ::temp82 Int
					.endLocalTable
					.code
						StrCat ::temp81 storePrefix storeKey                     ;@line 600
						CallStatic storageutil ClearFloatValuePrefix ::temp82 ::temp81  ;@line 600
					.endCode
				.endFunction
				.function SelectText
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param targetKind String
					.endParamTable
					.localTable
						.local ::temp107 Actor
						.local ::NoneVar None
						.local act Actor
						.local label String
						.local value String
						.local desc String
						.local disabled Bool
						.local ::temp108 String
						.local ::temp109 Bool
						.local status String
					.endLocalTable
					.code
						CallMethod GetTargetActor self ::temp107 targetKind      ;@line 752
						Assign act ::temp107                                     ;@line 752
						Assign label targetKind                                  ;@line 753
						Assign value "N/A"                                       ;@line 754
						Assign desc ""                                           ;@line 755
						Assign disabled True                                     ;@line 756
						JumpF act _label163                                      ;@line 757
						CallMethod ActorName self ::temp108 act "N/A"            ;@line 758
						StrCat ::temp108 ": " ::temp108                          ;@line 758
						StrCat ::temp108 label ::temp108                         ;@line 758
						Assign label ::temp108                                   ;@line 758
						CallMethod ActorStatus self ::temp108 act                ;@line 759
						Assign status ::temp108                                  ;@line 759
						JumpF status _label164                                   ;@line 760
						StrCat ::temp108 " [" status                             ;@line 761
						StrCat ::temp108 ::temp108 "]"                           ;@line 761
						StrCat ::temp108 label ::temp108                         ;@line 761
						Assign label ::temp108                                   ;@line 761
						Jump _label164                                           ;@line 761
					_label164:
						CompareEQ ::temp109 act target                           ;@line 763
						JumpF ::temp109 _label165                                ;@line 763
						Assign value "Selected"                                  ;@line 764
						Jump _label166                                           ;@line 764
					_label165:
						Assign disabled False                                    ;@line 766
						Assign value "Select"                                    ;@line 767
					_label166:
						Jump _label163                                           ;@line 767
					_label163:
						StrCat ::temp108 "target" targetKind                     ;@line 770
						CallMethod ecText self ::NoneVar ::temp108 label value desc disabled True False  ;@line 770
					.endCode
				.endFunction
				.function ecSliderHookMaxLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
					.endLocalTable
					.code
						Return max                                               ;@line ??
					.endCode
				.endFunction
				.function OnSexLabOrgasm
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param actForm form
						.param fullEnjoy Int
						.param orgasms Int
					.endParamTable
					.localTable
						.local ::temp193 Actor
						.local ::temp194 Float
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp193 actForm                                   ;@line 1101
						CallMethod ecGetFloat self ::temp194 "SexLabOrgasm"      ;@line 1101
						CallMethod BumpWetness self ::NoneVar ::temp193 ::temp194  ;@line 1101
					.endCode
				.endFunction
				.function ProcessKeyword
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Int
					.paramTable
						.param kwds Form[]
						.param kwdCount Int
						.param kwdName String
					.endParamTable
					.localTable
						.local ::temp62 String
						.local ::temp63 Bool
						.local ::temp64 keyword
						.local ::temp65 form
						.local kwd form
						.local ::temp66 Int
					.endLocalTable
					.code
						StrCat ::temp62 autoWornPrefix kwdName                   ;@line 559
						CallMethod ecGetBool self ::temp63 ::temp62              ;@line 559
						JumpF ::temp63 _label167                                 ;@line 559
						CallStatic keyword GetKeyword ::temp64 kwdName           ;@line 560
						Cast ::temp65 ::temp64                                   ;@line 560
						Assign kwd ::temp65                                      ;@line 560
						JumpF kwd _label168                                      ;@line 561
						Assign ::temp65 kwd                                      ;@line 562
						ArraySetElement kwds kwdCount ::temp65                   ;@line 562
						IAdd ::temp66 kwdCount 1                                 ;@line 563
						Return ::temp66                                          ;@line 563
						Jump _label168                                           ;@line 563
					_label168:
						Jump _label167                                           ;@line 563
					_label167:
						Return kwdCount                                          ;@line 566
					.endCode
				.endFunction
				.function ActorName
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String
					.paramTable
						.param act Actor
						.param default String
					.endParamTable
					.localTable
						.local ::temp89 actorbase
						.local ::temp90 String
					.endLocalTable
					.code
						JumpF act _label169                                      ;@line 616
						CallMethod GetLeveledActorBase act ::temp89              ;@line 617
						CallMethod GetName ::temp89 ::temp90                     ;@line 617
						Return ::temp90                                          ;@line 617
						Jump _label170                                           ;@line 617
					_label169:
						Return default                                           ;@line 619
					_label170:
					.endCode
				.endFunction
				.function dumpNodeString
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param node String
						.param i Int
					.endParamTable
					.localTable
						.local ::temp147 objectreference
						.local ::temp148 String
						.local ::temp149 Bool
						.local a String
						.local b String
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp147 target                                    ;@line 969
						CallStatic nioverride GetNodePropertyString ::temp148 ::temp147 True node 9 i  ;@line 969
						Assign a ::temp148                                       ;@line 969
						Cast ::temp147 target                                    ;@line 970
						CallStatic nioverride GetNodePropertyString ::temp148 ::temp147 False node 9 i  ;@line 970
						Assign b ::temp148                                       ;@line 970
						Cast ::temp149 a                                         ;@line 971
						JumpT ::temp149 _label171                                ;@line 971
						Cast ::temp149 b                                         ;@line 971
					_label171:
						JumpF ::temp149 _label172                                ;@line 971
						Cast ::temp148 i                                         ;@line 972
						StrCat ::temp148 " t" ::temp148                          ;@line 972
						StrCat ::temp148 ::temp148 ": "                          ;@line 972
						StrCat ::temp148 ::temp148 a                             ;@line 972
						StrCat ::temp148 ::temp148 " "                           ;@line 972
						StrCat ::temp148 ::temp148 b                             ;@line 972
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp148 0     ;@line 972
						Jump _label172                                           ;@line 972
					_label172:
					.endCode
				.endFunction
				.function OnVersionUpdate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param newVersion Int
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp25 Bool
						.local ::temp26 Bool
						.local ::temp31 String
						.local ::temp27 Int
						.local ::temp28 Bool
						.local ::temp29 Bool
						.local nActive Int
						.local ::temp30 String
					.endLocalTable
					.code
						Assign ::ecUpdate_var True                               ;@line 166
						CallMethod OnConfigInit self ::NoneVar                   ;@line 167
						Assign ::ecUpdate_var False                              ;@line 168
						CompareGTE ::temp25 newVersion 1035                      ;@line 170
						Cast ::temp25 ::temp25                                   ;@line 170
						JumpF ::temp25 _label173                                 ;@line 170
						CompareGT ::temp26 1035 ::CurrentVersion_var             ;@line 170
						Cast ::temp25 ::temp26                                   ;@line 170
					_label173:
						JumpF ::temp25 _label174                                 ;@line 170
						CallMethod acNumFloat self ::temp27 "wetnessRate"        ;@line 171
						Assign nActive ::temp27                                  ;@line 171
						CompareGT ::temp26 nActive 1                             ;@line 172
						Cast ::temp29 ::temp26                                   ;@line 172
						JumpT ::temp29 _label175                                 ;@line 172
						CompareEQ ::temp28 nActive 1                             ;@line 172
						Cast ::temp28 ::temp28                                   ;@line 172
						JumpF ::temp28 _label176                                 ;@line 172
						CallMethod acHasFloat self ::temp29 player "wetnessRate"  ;@line 172
						Not ::temp29 ::temp29                                    ;@line 172
						Cast ::temp28 ::temp29                                   ;@line 172
					_label176:
						Cast ::temp29 ::temp28                                   ;@line 172
					_label175:
						JumpF ::temp29 _label177                                 ;@line 172
						Cast ::temp30 nActive                                    ;@line 173
						StrCat ::temp30 "weq委屈" ::temp30                     ;@line 173
						StrCat ::temp30 ::temp30 ") will be stopped to preform a proper update.\nSettings and forced values remain untouched."  ;@line 173
						CallStatic debug MessageBox ::NoneVar ::temp30           ;@line 173
						Cast ::temp30 nActive                                    ;@line 174
						StrCat ::temp30 "企鹅去" ::temp30                     ;@line 174
						StrCat ::temp30 ::temp30 " effects for updating purpose"  ;@line 174
						CallMethod Log self ::NoneVar ::temp30 0 True            ;@line 174
						CallMethod acClearFloat self ::NoneVar "wetnessRate"     ;@line 175
						CallMethod acClearFloat self ::NoneVar "specular"        ;@line 177
						CallMethod acClearFloat self ::NoneVar "glossiness"      ;@line 178
						CallStatic utility Wait ::NoneVar 1.000000               ;@line 179
						CallMethod acClearFloat self ::NoneVar "specular"        ;@line 180
						CallMethod acClearFloat self ::NoneVar "glossiness"      ;@line 181
						Jump _label177                                           ;@line 181
					_label177:
						Jump _label174                                           ;@line 181
					_label174:
						ISubtract ::temp27 ::CurrentVersion_var 1000             ;@line 184
						Cast ::temp30 ::temp27                                   ;@line 184
						StrCat ::temp30 "updated: " ::temp30                     ;@line 184
						StrCat ::temp30 ::temp30 " => "                          ;@line 184
						ISubtract ::temp27 newVersion 1000                       ;@line 184
						Cast ::temp31 ::temp27                                   ;@line 184
						StrCat ::temp31 ::temp30 ::temp31                        ;@line 184
						CallMethod Log self ::NoneVar ::temp31 0 True            ;@line 184
					.endCode
				.endFunction
				.function dumpSlotFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param slotMask Int
						.param i Int
					.endParamTable
					.localTable
						.local ::temp163 objectreference
						.local ::temp164 Float
						.local ::temp165 Bool
						.local a Float
						.local b Float
						.local ::temp166 String
						.local ::temp167 String
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp163 target                                    ;@line 1010
						CallStatic nioverride GetSkinPropertyFloat ::temp164 ::temp163 True slotMask i -1  ;@line 1010
						Assign a ::temp164                                       ;@line 1010
						Cast ::temp163 target                                    ;@line 1011
						CallStatic nioverride GetSkinPropertyFloat ::temp164 ::temp163 False slotMask i -1  ;@line 1011
						Assign b ::temp164                                       ;@line 1011
						Cast ::temp165 a                                         ;@line 1012
						JumpT ::temp165 _label178                                ;@line 1012
						Cast ::temp165 b                                         ;@line 1012
					_label178:
						JumpF ::temp165 _label179                                ;@line 1012
						Cast ::temp166 i                                         ;@line 1013
						StrCat ::temp166 " f" ::temp166                          ;@line 1013
						StrCat ::temp166 ::temp166 ": "                          ;@line 1013
						Cast ::temp167 a                                         ;@line 1013
						StrCat ::temp167 ::temp166 ::temp167                     ;@line 1013
						StrCat ::temp166 ::temp167 " "                           ;@line 1013
						Cast ::temp167 b                                         ;@line 1013
						StrCat ::temp167 ::temp166 ::temp167                     ;@line 1013
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp167 0     ;@line 1013
						Jump _label179                                           ;@line 1013
					_label179:
					.endCode
				.endFunction
				.function OnMenuClose
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param menuName String
					.endParamTable
					.localTable
						.local ::temp32 Bool
						.local ::temp33 String
						.local ::temp34 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CompareEQ ::temp32 menuName "Console"                    ;@line 192
						JumpF ::temp32 _label180                                 ;@line 192
						PropGet CurrentPage self ::temp33                        ;@line 193
						CompareEQ ::temp34 ::temp33 "Targets"                    ;@line 193
						JumpF ::temp34 _label181                                 ;@line 193
						CallMethod ForcePageReset self ::NoneVar                 ;@line 194
						Jump _label181                                           ;@line 194
					_label181:
						Jump _label180                                           ;@line 194
					_label180:
					.endCode
				.endFunction
				.function GetTargetActor
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Actor
					.paramTable
						.param targetKind String
					.endParamTable
					.localTable
						.local ::temp83 Bool
						.local ::temp84 Bool
						.local ::temp85 objectreference
						.local ::temp86 Actor
						.local ::temp87 Actor
						.local ::temp88 Actor
					.endLocalTable
					.code
						CompareEQ ::temp83 targetKind "player"                   ;@line 605
						JumpF ::temp83 _label182                                 ;@line 605
						Return player                                            ;@line 606
						Jump _label183                                           ;@line 606
					_label182:
						CompareEQ ::temp84 targetKind "Crosshair"                ;@line 607
						JumpF ::temp84 _label184                                 ;@line 607
						CallStatic game GetCurrentCrosshairRef ::temp85          ;@line 608
						Cast ::temp86 ::temp85                                   ;@line 608
						Return ::temp86                                          ;@line 608
						Jump _label183                                           ;@line 608
					_label184:
						CompareEQ ::temp84 targetKind "Console"                  ;@line 609
						JumpF ::temp84 _label185                                 ;@line 609
						CallStatic game GetCurrentConsoleRef ::temp85            ;@line 610
						Cast ::temp87 ::temp85                                   ;@line 610
						Return ::temp87                                          ;@line 610
						Jump _label183                                           ;@line 610
					_label185:
						Cast ::temp88 None                                       ;@line 612
						Return ::temp88                                          ;@line 612
					_label183:
					.endCode
				.endFunction
				.function toggleAutoWornKeyword
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param kwdName String
						.param name String
						.param extraDesc String
					.endParamTable
					.localTable
						.local ::temp49 String
						.local ::temp50 Bool
						.local ::temp51 Bool
						.local ::temp52 String
						.local ::NoneVar None
						.local p String
						.local ::temp53 form
						.local ::temp54 Int
					.endLocalTable
					.code
						CallStatic stringutil GetNthChar ::temp49 name 0         ;@line 524
						Assign p ::temp49                                        ;@line 524
						CompareEQ ::temp50 p "a"                                 ;@line 525
						Cast ::temp50 ::temp50                                   ;@line 525
						JumpT ::temp50 _label186                                 ;@line 525
						CompareEQ ::temp51 p "e"                                 ;@line 525
						Cast ::temp50 ::temp51                                   ;@line 525
					_label186:
						Cast ::temp50 ::temp50                                   ;@line 525
						JumpT ::temp50 _label187                                 ;@line 525
						CompareEQ ::temp51 p "i"                                 ;@line 525
						Cast ::temp50 ::temp51                                   ;@line 525
					_label187:
						Cast ::temp50 ::temp50                                   ;@line 525
						JumpT ::temp50 _label188                                 ;@line 525
						CompareEQ ::temp51 p "o"                                 ;@line 525
						Cast ::temp50 ::temp51                                   ;@line 525
					_label188:
						Cast ::temp50 ::temp50                                   ;@line 525
						JumpT ::temp50 _label189                                 ;@line 525
						CompareEQ ::temp51 p "u"                                 ;@line 525
						Cast ::temp50 ::temp51                                   ;@line 525
					_label189:
						JumpF ::temp50 _label190                                 ;@line 525
						Assign p "an"                                            ;@line 526
						Jump _label191                                           ;@line 526
					_label190:
						Assign p "a"                                             ;@line 528
					_label191:
						StrCat ::temp49 autoWornPrefix kwdName                   ;@line 530
						StrCat ::temp52 "The effect will be automatically applied wearing at least one item flagged as " p  ;@line 530
						StrCat ::temp52 ::temp52 " "                             ;@line 530
						StrCat ::temp52 ::temp52 name                            ;@line 530
						StrCat ::temp52 ::temp52 ". "                            ;@line 530
						StrCat ::temp52 ::temp52 extraDesc                       ;@line 530
						CallMethod ecToggle self ::NoneVar ::temp49 name False ::temp52 False False True  ;@line 530
						JumpF ::ecBurnIn_var _label192                           ;@line 531
						Cast ::temp53 self                                       ;@line 532
						CallStatic storageutil StringListAdd ::temp54 ::temp53 autoWornStore kwdName False  ;@line 532
						Jump _label192                                           ;@line 532
					_label192:
					.endCode
				.endFunction
				.function dynModEntry
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param available Bool
						.param name String
						.param prefix String
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp47 String
						.local ::temp48 Bool
					.endLocalTable
					.code
						CallMethod ecEmpty self ::NoneVar 1                      ;@line 509
						StrCat ::temp47 name " Integration"                      ;@line 510
						CallMethod ecHeader self ::NoneVar ::temp47 False        ;@line 510
						Cast ::temp48 available                                  ;@line 511
						JumpT ::temp48 _label193                                 ;@line 511
						Cast ::temp48 ::ecBurnIn_var                             ;@line 511
					_label193:
						JumpF ::temp48 _label194                                 ;@line 511
						Return True                                              ;@line 512
						Jump _label195                                           ;@line 512
					_label194:
						StrCat ::temp47 prefix "NA"                              ;@line 514
						CallMethod ecText self ::NoneVar ::temp47 "" "not found" "" True False False  ;@line 514
						Return False                                             ;@line 515
					_label195:
					.endCode
				.endFunction
				.function ecCheck
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp0 Bool
						.local ::temp1 Int
						.local ::temp2 Bool
						.local ok Bool
						.local niov Int
					.endLocalTable
					.code
						CallParent ecCheck ::temp0                               ;@line 42
						Assign ok ::temp0                                        ;@line 42
						CallStatic nioverride GetScriptVersion ::temp1           ;@line 43
						Assign niov ::temp1                                      ;@line 43
						CompareGTE ::temp0 niov 0                                ;@line 44
						CallStatic nioverride GetScriptVersion ::temp1           ;@line 44
						CompareEQ ::temp2 niov ::temp1                           ;@line 44
						Not ::temp2 ::temp2                                      ;@line 44
						CallMethod ecCheckItem self ::temp0 "nioverride" "4.0+" ::temp0 ::temp2  ;@line 44
						Cast ::temp2 ::temp0                                     ;@line 44
						JumpF ::temp2 _label196                                  ;@line 44
						Cast ::temp2 ok                                          ;@line 44
					_label196:
						Assign ok ::temp2                                        ;@line 44
						Return ok                                                ;@line 45
					.endCode
				.endFunction
				.function ecConfigChanged
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod UpdateKeywords self ::NoneVar                 ;@line 570
						CallMethod OnUpdate self ::NoneVar                       ;@line 571
					.endCode
				.endFunction
				.function acHasFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param act Actor
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp73 String
						.local ::temp74 form
						.local ::temp75 Bool
					.endLocalTable
					.code
						StrCat ::temp73 storePrefix storeKey                     ;@line 591
						Cast ::temp74 act                                        ;@line 591
						CallStatic storageutil HasFloatValue ::temp75 ::temp74 ::temp73  ;@line 591
						Return ::temp75                                          ;@line 591
					.endCode
				.endFunction
				.function ecSliderHookMinLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
					.endLocalTable
					.code
						Return min                                               ;@line 836
					.endCode
				.endFunction
				.function OnConfigInit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp22 String[]
						.local ::temp23 String
						.local ::temp24 Actor
						.local ::NoneVar None
					.endLocalTable
					.code
						Assign ::ModName_var "WetFunction Redux"                 ;@line 145
						ArrayCreate ::temp22 5                                   ;@line 146
						Assign ::Pages_var ::temp22                              ;@line 146
						Assign ::temp23 "Wetness"                                ;@line 147
						ArraySetElement ::Pages_var 0 ::temp23                   ;@line 147
						Assign ::temp23 "Visuals"                                ;@line 148
						ArraySetElement ::Pages_var 1 ::temp23                   ;@line 148
						Assign ::temp23 "Targets"                                ;@line 149
						ArraySetElement ::Pages_var 2 ::temp23                   ;@line 149
						Assign ::temp23 "Auto-Apply"                             ;@line 150
						ArraySetElement ::Pages_var 3 ::temp23                   ;@line 150
						Assign ::temp23 "Misc"                                   ;@line 151
						ArraySetElement ::Pages_var 4 ::temp23                   ;@line 151
						CallStatic game GetPlayer ::temp24                       ;@line 152
						Assign player ::temp24                                   ;@line 152
						CallMethod InitSoftDeps self ::NoneVar                   ;@line 153
						CallMethod ecStartup self ::NoneVar                      ;@line 154
						CallMethod RegisterForMenu self ::NoneVar "Console"      ;@line 156
						CallMethod RegisterForModEvent self ::NoneVar "AnimationStart" "OnSexLabAnim"  ;@line 157
						CallMethod RegisterForModEvent self ::NoneVar "ActorChangeStart" "OnSexLabAnim"  ;@line 158
						CallMethod RegisterForModEvent self ::NoneVar "ActorChangeEnd" "OnSexLabAnim"  ;@line 159
						CallMethod RegisterForModEvent self ::NoneVar "AnimationEnd" "OnSexLabAnim"  ;@line 160
						CallMethod RegisterForModEvent self ::NoneVar "StageStart" "OnSexLabAnim"  ;@line 161
						CallMethod RegisterForModEvent self ::NoneVar "SexLabOrgasm" "OnSexLabOrgasm"  ;@line 162
					.endCode
				.endFunction
				.function TargetSelect
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp110 String
						.local ::temp111 Actor
						.local ::NoneVar None
						.local targetKind String
					.endLocalTable
					.code
						CallMethod ecKey self ::temp110 "" 6                     ;@line 773
						Assign targetKind ::temp110                              ;@line 773
						CallMethod GetTargetActor self ::temp111 targetKind      ;@line 774
						Assign target ::temp111                                  ;@line 774
						CallMethod ForcePageReset self ::NoneVar                 ;@line 775
					.endCode
				.endFunction
				.function BumpWetness
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param a Actor
						.param amount Float
					.endParamTable
					.localTable
						.local ::temp204 Bool
						.local ::temp205 Float
						.local ::temp206 Float
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp204 a                                         ;@line 1137
						JumpF ::temp204 _label197                                ;@line 1137
						CallMethod acHasFloat self ::temp204 a "Wetness"         ;@line 1137
						Cast ::temp204 ::temp204                                 ;@line 1137
					_label197:
						JumpF ::temp204 _label198                                ;@line 1137
						CallMethod acGetFloat self ::temp205 a "Wetness" 0.000000  ;@line 1138
						FAdd ::temp205 ::temp205 amount                          ;@line 1138
						CallMethod ecGetFloat self ::temp206 "wetnessCap"        ;@line 1138
						CallStatic papyrusutil ClampFloat ::temp205 ::temp205 0.000000 ::temp206  ;@line 1138
						CallMethod acSetFloat self ::NoneVar a "Wetness" ::temp205  ;@line 1138
						Jump _label198                                           ;@line 1138
					_label198:
					.endCode
				.endFunction
				.function OnUpdate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp168 Bool
						.local autoGlobal Bool
						.local autoNaked Bool
						.local autoFollower Bool
						.local autoFix Bool
						.local ::temp169 Float
						.local ::temp170 objectreference
						.local ::temp171 actor[]
						.local ::temp172 Int
						.local ::temp173 Bool
						.local ::temp182 String
						.local autoDuration Float
						.local pt actor[]
						.local a Actor
						.local i Int
						.local ::temp174 Actor
						.local ::temp175 Bool
						.local ::temp176 Bool
						.local doApply Bool
						.local ::temp177 Bool
						.local ::temp178 Bool
						.local ::temp179 Bool
						.local j Int
						.local ::temp180 form
						.local ::temp181 keyword
					.endLocalTable
					.code
						CallMethod UnregisterForUpdate self ::NoneVar            ;@line 1019
						CallMethod ecGetBool self ::temp168 "autoGlobal"         ;@line 1020
						Assign autoGlobal ::temp168                              ;@line 1020
						CallMethod ecGetBool self ::temp168 "autoNaked"          ;@line 1021
						Assign autoNaked ::temp168                               ;@line 1021
						CallMethod ecGetBool self ::temp168 "autoFollower"       ;@line 1022
						Assign autoFollower ::temp168                            ;@line 1022
						CallMethod ecGetBool self ::temp168 "autoFix"            ;@line 1023
						Assign autoFix ::temp168                                 ;@line 1023
						Cast ::temp168 autoGlobal                                ;@line 1024
						JumpT ::temp168 _label199                                ;@line 1024
						Cast ::temp168 autoFollower                              ;@line 1024
					_label199:
						Cast ::temp168 ::temp168                                 ;@line 1024
						JumpT ::temp168 _label200                                ;@line 1024
						Cast ::temp168 autoFix                                   ;@line 1024
					_label200:
						Cast ::temp168 ::temp168                                 ;@line 1024
						JumpT ::temp168 _label201                                ;@line 1024
						Cast ::temp168 autoNaked                                 ;@line 1024
					_label201:
						Cast ::temp168 ::temp168                                 ;@line 1024
						JumpT ::temp168 _label202                                ;@line 1024
						Cast ::temp168 autoWornKwds                              ;@line 1024
					_label202:
						Not ::temp168 ::temp168                                  ;@line 1024
						JumpF ::temp168 _label203                                ;@line 1024
						Return None                                              ;@line 1025
						Jump _label203                                           ;@line 1025
					_label203:
						CallStatic utility IsInMenuMode ::temp168                ;@line 1027
						JumpF ::temp168 _label204                                ;@line 1027
						CallMethod RegisterForSingleUpdate self ::NoneVar 2.000000  ;@line 1028
						Jump _label205                                           ;@line 1028
					_label204:
						CallMethod ecGetFloat self ::temp169 "autoDuration"      ;@line 1030
						Assign autoDuration ::temp169                            ;@line 1030
						CallMethod ecGetFloat self ::temp169 "autoRange"         ;@line 1031
						Cast ::temp170 player                                    ;@line 1031
						CallStatic miscutil ScanCellActors ::temp171 ::temp170 ::temp169 None  ;@line 1031
						Assign pt ::temp171                                      ;@line 1031
						ArrayLength ::temp172 pt                                 ;@line 1033
						Assign i ::temp172                                       ;@line 1033
					_label221:
						CompareGT ::temp173 i 0                                  ;@line 1034
						JumpF ::temp173 _label206                                ;@line 1034
						ISubtract ::temp172 i 1                                  ;@line 1035
						Assign i ::temp172                                       ;@line 1035
						ArrayGetElement ::temp174 pt i                           ;@line 1036
						Assign a ::temp174                                       ;@line 1036
						CompareEQ ::temp175 a player                             ;@line 1037
						Not ::temp175 ::temp175                                  ;@line 1037
						JumpF ::temp175 _label207                                ;@line 1037
						Assign doApply False                                     ;@line 1038
						CallMethod HasKeyword a ::temp176 ::ActorTypeNPC_var     ;@line 1039
						JumpF ::temp176 _label208                                ;@line 1039
						Cast ::temp177 autoGlobal                                ;@line 1040
						JumpT ::temp177 _label209                                ;@line 1040
						Cast ::temp177 autoFollower                              ;@line 1040
						JumpF ::temp177 _label210                                ;@line 1040
						CallMethod IsInFaction a ::temp177 ::CurrentFollowerFaction_var  ;@line 1040
						Cast ::temp177 ::temp177                                 ;@line 1040
					_label210:
						Cast ::temp177 ::temp177                                 ;@line 1040
					_label209:
						Cast ::temp179 ::temp177                                 ;@line 1040
						JumpT ::temp179 _label211                                ;@line 1040
						Cast ::temp178 autoNaked                                 ;@line 1040
						JumpF ::temp178 _label212                                ;@line 1040
						CallMethod WornHasKeyword a ::temp178 ::ArmorCuirass_var  ;@line 1040
						Not ::temp178 ::temp178                                  ;@line 1040
						Cast ::temp178 ::temp178                                 ;@line 1040
					_label212:
						Cast ::temp178 ::temp178                                 ;@line 1040
						JumpF ::temp178 _label213                                ;@line 1040
						CallMethod WornHasKeyword a ::temp179 ::ClothingBody_var  ;@line 1040
						Not ::temp179 ::temp179                                  ;@line 1040
						Cast ::temp178 ::temp179                                 ;@line 1040
					_label213:
						Cast ::temp179 ::temp178                                 ;@line 1040
					_label211:
						Assign doApply ::temp179                                 ;@line 1040
						ArrayLength ::temp172 autoWornKwds                       ;@line 1041
						Assign j ::temp172                                       ;@line 1041
					_label216:
						Not ::temp177 doApply                                    ;@line 1042
						Cast ::temp179 ::temp177                                 ;@line 1042
						JumpF ::temp179 _label214                                ;@line 1042
						CompareGT ::temp178 j 0                                  ;@line 1042
						Cast ::temp179 ::temp178                                 ;@line 1042
					_label214:
						JumpF ::temp179 _label215                                ;@line 1042
						ISubtract ::temp172 j 1                                  ;@line 1043
						Assign j ::temp172                                       ;@line 1043
						ArrayGetElement ::temp180 autoWornKwds j                 ;@line 1044
						Cast ::temp181 ::temp180                                 ;@line 1044
						CallMethod WornHasKeyword a ::temp177 ::temp181          ;@line 1044
						Assign doApply ::temp177                                 ;@line 1044
						Jump _label216                                           ;@line 1044
					_label215:
						Jump _label208                                           ;@line 1044
					_label208:
						JumpF doApply _label217                                  ;@line 1047
						CallMethod AutoApply self ::temp178 a autoDuration True autoFix  ;@line 1048
						Jump _label218                                           ;@line 1048
					_label217:
						Cast ::temp180 ::ability_var                             ;@line 1049
						CallMethod HasSpell a ::temp177 ::temp180                ;@line 1049
						JumpF ::temp177 _label218                                ;@line 1049
						Cast ::temp178 autoFix                                   ;@line 1050
						JumpF ::temp178 _label219                                ;@line 1050
						CallMethod HasMagicEffect a ::temp179 ::effect_var       ;@line 1050
						Not ::temp176 ::temp179                                  ;@line 1050
						Cast ::temp178 ::temp176                                 ;@line 1050
					_label219:
						JumpF ::temp178 _label220                                ;@line 1050
						CallMethod AddSpell a ::temp179 ::ability_var True       ;@line 1051
						Jump _label220                                           ;@line 1051
					_label220:
						Jump _label218                                           ;@line 1051
					_label218:
						Jump _label207                                           ;@line 1051
					_label207:
						Jump _label221                                           ;@line 1051
					_label206:
						ArrayLength ::temp172 pt                                 ;@line 1056
						IAdd ::temp172 ::temp172 -1                              ;@line 1056
						Cast ::temp182 ::temp172                                 ;@line 1056
						StrCat ::temp182 "Scan complete: " ::temp182             ;@line 1056
						CallMethod Log self ::NoneVar ::temp182 -4 True          ;@line 1056
						CallMethod ecGetFloat self ::temp169 "autoTimeout"       ;@line 1057
						CallMethod RegisterForSingleUpdate self ::NoneVar ::temp169  ;@line 1057
					_label205:
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_targetcrosshair
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod TargetSelect self ::NoneVar                   ;@line 784
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_wetnessstart
				.function ecSliderHookMinLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp119 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp119 "wetnessDry"        ;@line ??
						Return ::temp119                                         ;@line ??
					.endCode
				.endFunction
				.function ecSliderHookMaxLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp120 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp120 "wetnessSoaked"     ;@line ??
						Return ::temp120                                         ;@line ??
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_logsystem
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp127 String
						.local ::temp128 String[]
						.local ::temp129 Int
						.local ::temp130 Bool
						.local hash String
						.local Items String[]
						.local i Int
						.local textures String[]
						.local n Int
						.local ::temp131 String
					.endLocalTable
					.code
						Assign systemDumped True                                 ;@line 882
						CallMethod ecFlagsUpdate self ::NoneVar True             ;@line 883
						CallMethod ecTextUpdate self ::NoneVar "Dumping"         ;@line 884
						Assign hash "#######################################"    ;@line 885
						StrCat ::temp127 hash " start system dump "              ;@line 886
						StrCat ::temp127 ::temp127 hash                          ;@line 886
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp127 0     ;@line 886
						CallStatic WetFunctionMCM FLog ::NoneVar "config:" 0     ;@line 887
						CallMethod ecDump self ::temp128 True                    ;@line 888
						Assign Items ::temp128                                   ;@line 888
						ArrayLength ::temp129 Items                              ;@line 889
						Assign i ::temp129                                       ;@line 889
					_label223:
						CompareGT ::temp130 i 0                                  ;@line 890
						JumpF ::temp130 _label222                                ;@line 890
						ISubtract ::temp129 i 1                                  ;@line 891
						Assign i ::temp129                                       ;@line 891
						ArrayGetElement ::temp127 Items i                        ;@line 892
						StrCat ::temp127 " " ::temp127                           ;@line 892
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp127 0     ;@line 892
						Jump _label223                                           ;@line 892
					_label222:
						CallStatic WetFunctionMCM FLog ::NoneVar " " 0           ;@line 894
						CallStatic WetFunctionMCM FLog ::NoneVar "textures:" 0   ;@line 895
						CallStatic miscutil FilesInFolder ::temp128 "Data\\Textures\\Actors\\character\\WetFunction\\" "dds"  ;@line 896
						Assign textures ::temp128                                ;@line 896
						ArrayLength ::temp129 textures                           ;@line 897
						Assign i ::temp129                                       ;@line 897
					_label225:
						CompareGT ::temp130 i 0                                  ;@line 898
						JumpF ::temp130 _label224                                ;@line 898
						ISubtract ::temp129 i 1                                  ;@line 899
						Assign i ::temp129                                       ;@line 899
						ArrayGetElement ::temp127 textures i                     ;@line 900
						StrCat ::temp127 " " ::temp127                           ;@line 900
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp127 0     ;@line 900
						Jump _label225                                           ;@line 900
					_label224:
						CallStatic WetFunctionMCM FLog ::NoneVar " " 0           ;@line 902
						CallStatic WetFunctionMCM FLog ::NoneVar "mods:" 0       ;@line 903
						CallStatic game GetModCount ::temp129                    ;@line 904
						Assign n ::temp129                                       ;@line 904
						Assign i 0                                               ;@line 905
					_label227:
						CompareLT ::temp130 i n                                  ;@line 906
						JumpF ::temp130 _label226                                ;@line 906
						CallMethod Int2Hex self ::temp127 i 2 False              ;@line 907
						StrCat ::temp127 " " ::temp127                           ;@line 907
						StrCat ::temp127 ::temp127 ": "                          ;@line 907
						CallStatic game GetModName ::temp131 i                   ;@line 907
						StrCat ::temp127 ::temp127 ::temp131                     ;@line 907
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp127 0     ;@line 907
						IAdd ::temp129 i 1                                       ;@line 908
						Assign i ::temp129                                       ;@line 908
						Jump _label227                                           ;@line 908
					_label226:
						StrCat ::temp131 hash " end system dump "                ;@line 910
						StrCat ::temp127 ::temp131 hash                          ;@line 910
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp127 0     ;@line 910
						CallMethod ecTextUpdate self ::NoneVar "Done"            ;@line 911
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_wetnessdry
				.function ecSliderHookMaxLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp118 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp118 "wetnessStart"      ;@line 823
						Return ::temp118                                         ;@line 823
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_targetaction
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp113 String
						.local ::temp114 Bool
						.local ::temp115 Bool
						.local status String
						.local ::NoneVar None
						.local ::temp116 Bool
					.endLocalTable
					.code
						CallMethod ActorStatus self ::temp113 target             ;@line 800
						Assign status ::temp113                                  ;@line 800
						CompareEQ ::temp114 status "Running"                     ;@line 801
						Cast ::temp114 ::temp114                                 ;@line 801
						JumpT ::temp114 _label228                                ;@line 801
						CompareEQ ::temp115 status "Starting"                    ;@line 801
						Cast ::temp114 ::temp115                                 ;@line 801
					_label228:
						Cast ::temp114 ::temp114                                 ;@line 801
						JumpT ::temp114 _label229                                ;@line 801
						CompareEQ ::temp115 status "Restarting"                  ;@line 801
						Cast ::temp114 ::temp115                                 ;@line 801
					_label229:
						JumpF ::temp114 _label230                                ;@line 801
						CallMethod acDelFloat self ::NoneVar target "wetnessRate"  ;@line 802
						CompareEQ ::temp115 status "Starting"                    ;@line 803
						JumpF ::temp115 _label231                                ;@line 803
						CallMethod RemoveSpell target ::temp116 ::ability_var    ;@line 804
						Jump _label231                                           ;@line 804
					_label231:
						Jump _label232                                           ;@line 804
					_label230:
						CallMethod acSetFloat self ::NoneVar target "wetnessRate" 0.000000  ;@line 807
						CallMethod AddSpell target ::temp116 ::ability_var True  ;@line 808
					_label232:
						CallMethod ForcePageReset self ::NoneVar                 ;@line 810
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_controlclearalldata
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp106 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ShowMessage self ::temp106 "Clear all data?\nThis will also stop all effect and reset all forced values." True "Clear all data" "Cancel"  ;@line 743
						JumpF ::temp106 _label233                                ;@line 743
						CallMethod acClearFloat self ::NoneVar ""                ;@line 744
						CallMethod ecTextUpdate self ::NoneVar "Done"            ;@line 745
						Jump _label233                                           ;@line 745
					_label233:
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_controlreset
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod acDelFloat self ::NoneVar target "wetnessForce"  ;@line 712
						CallMethod acDelFloat self ::NoneVar target "specularForce"  ;@line 713
						CallMethod acDelFloat self ::NoneVar target "glossinessForce"  ;@line 714
						CallMethod ForcePageReset self ::NoneVar                 ;@line 715
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_forceglossiness
				.function OnSliderAcceptST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param value Float
					.endParamTable
					.localTable
						.local ::temp103 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CompareGT ::temp103 value 0.000000                       ;@line 701
						JumpF ::temp103 _label234                                ;@line 701
						CallMethod acSetFloat self ::NoneVar target "glossinessForce" value  ;@line 702
						Jump _label235                                           ;@line 702
					_label234:
						CallMethod acDelFloat self ::NoneVar target "glossinessForce"  ;@line 704
					_label235:
						CallMethod ForcePageReset self ::NoneVar                 ;@line 706
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_logtarget
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp132 String
						.local ::temp133 String
						.local ::temp134 form
						.local ::temp135 Bool
						.local ::temp136 actorbase
						.local hash String
						.local ab actorbase
						.local ::temp137 Int
						.local n Int
						.local i Int
						.local ::temp138 headpart
						.local ::temp139 String
					.endLocalTable
					.code
						JumpF target _label236                                   ;@line 916
						CallMethod ecFlagsUpdate self ::NoneVar True             ;@line 917
						CallMethod ecTextUpdate self ::NoneVar "Dumping"         ;@line 918
						Assign hash "#######################################"    ;@line 919
						StrCat ::temp132 hash " start target dump "              ;@line 920
						StrCat ::temp132 ::temp132 hash                          ;@line 920
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp132 0     ;@line 920
						Cast ::temp132 target                                    ;@line 921
						StrCat ::temp132 "target: " ::temp132                    ;@line 921
						StrCat ::temp132 ::temp132 " name:"                      ;@line 921
						CallMethod ActorName self ::temp133 target "N/A"         ;@line 921
						StrCat ::temp132 ::temp132 ::temp133                     ;@line 921
						StrCat ::temp133 ::temp132 " ability:"                   ;@line 921
						Cast ::temp134 ::ability_var                             ;@line 921
						CallMethod HasSpell target ::temp135 ::temp134           ;@line 921
						Cast ::temp132 ::temp135                                 ;@line 921
						StrCat ::temp132 ::temp133 ::temp132                     ;@line 921
						StrCat ::temp133 ::temp132 " effect:"                    ;@line 921
						CallMethod HasMagicEffect target ::temp135 ::effect_var  ;@line 921
						Cast ::temp132 ::temp135                                 ;@line 921
						StrCat ::temp132 ::temp133 ::temp132                     ;@line 921
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp132 0     ;@line 921
						CallMethod dumpAc self ::temp133 "Wetness"               ;@line 922
						StrCat ::temp132 "status:" ::temp133                     ;@line 922
						CallMethod dumpAc self ::temp133 "wetnessRate"           ;@line 922
						StrCat ::temp132 ::temp132 ::temp133                     ;@line 922
						CallMethod dumpAc self ::temp133 "specular"              ;@line 922
						StrCat ::temp132 ::temp132 ::temp133                     ;@line 922
						CallMethod dumpAc self ::temp133 "glossiness"            ;@line 922
						StrCat ::temp132 ::temp132 ::temp133                     ;@line 922
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp132 0     ;@line 922
						CallMethod dumpAc self ::temp133 "wetnessForce"          ;@line 923
						StrCat ::temp132 "config:" ::temp133                     ;@line 923
						CallMethod dumpAc self ::temp133 "specularForce"         ;@line 923
						StrCat ::temp132 ::temp132 ::temp133                     ;@line 923
						CallMethod dumpAc self ::temp133 "glossinessForce"       ;@line 923
						StrCat ::temp132 ::temp132 ::temp133                     ;@line 923
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp132 0     ;@line 923
						CallStatic WetFunctionMCM FLog ::NoneVar "" 0            ;@line 924
						CallMethod GetActorBase target ::temp136                 ;@line 925
						Assign ab ::temp136                                      ;@line 925
						Cast ::temp133 ab                                        ;@line 926
						StrCat ::temp132 "actor base:" ::temp133                 ;@line 926
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp132 0     ;@line 926
						Assign i 30                                              ;@line 927
					_label238:
						CompareLTE ::temp135 i 61                                ;@line 928
						JumpF ::temp135 _label237                                ;@line 928
						CallMethod dumpSlot self ::NoneVar i                     ;@line 929
						IAdd ::temp137 i 1                                       ;@line 930
						Assign i ::temp137                                       ;@line 930
						Jump _label238                                           ;@line 930
					_label237:
						CallStatic WetFunctionMCM FLog ::NoneVar "" 0            ;@line 932
						CallMethod GetNumHeadParts ab ::temp137                  ;@line 933
						Assign n ::temp137                                       ;@line 933
						Assign i 0                                               ;@line 934
					_label240:
						CompareLT ::temp135 i n                                  ;@line 935
						JumpF ::temp135 _label239                                ;@line 935
						CallMethod GetNthHeadPart ab ::temp138 i                 ;@line 936
						CallMethod GetName ::temp138 ::temp133                   ;@line 936
						Cast ::temp132 i                                         ;@line 936
						StrCat ::temp132 "(" ::temp132                           ;@line 936
						StrCat ::temp132 ::temp132 "/"                           ;@line 936
						Cast ::temp139 n                                         ;@line 936
						StrCat ::temp139 ::temp132 ::temp139                     ;@line 936
						StrCat ::temp132 ::temp139 ")"                           ;@line 936
						CallMethod dumpNode self ::NoneVar ::temp133 ::temp132   ;@line 936
						IAdd ::temp137 i 1                                       ;@line 937
						Assign i ::temp137                                       ;@line 937
						Jump _label240                                           ;@line 937
					_label239:
						StrCat ::temp139 hash " end target dump "                ;@line 939
						StrCat ::temp133 ::temp139 hash                          ;@line 939
						CallStatic WetFunctionMCM FLog ::NoneVar ::temp133 0     ;@line 939
						CallMethod ecFlagsUpdate self ::NoneVar False            ;@line 940
						CallMethod ecTextUpdate self ::NoneVar "Done"            ;@line 941
						Jump _label236                                           ;@line 941
					_label236:
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_targetplayer
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod TargetSelect self ::NoneVar                   ;@line 779
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_jsonexport
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp125 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ShowMessage self ::temp125 "Exporting the current setting will overwrite the last exported ones." True "Export and overwrite" "Cancel"  ;@line 857
						JumpF ::temp125 _label241                                ;@line 857
						CallMethod ecExport self ::NoneVar jsonPath              ;@line 858
						Jump _label241                                           ;@line 858
					_label241:
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_currentwetness
				.function OnSliderAcceptST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param value Float
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod acSetFloat self ::NoneVar target "Wetness" value  ;@line ??
						CallMethod ecSliderUpdate self ::NoneVar value ""        ;@line ??
					.endCode
				.endFunction
				.function ecSliderHookMaxLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp99 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp99 "wetnessCap"         ;@line ??
						Return ::temp99                                          ;@line ??
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_wetnesssoaked
				.function ecSliderHookMinLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp121 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp121 "wetnessStart"      ;@line ??
						Return ::temp121                                         ;@line ??
					.endCode
				.endFunction
				.function ecSliderHookMaxLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp122 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp122 "wetnessCap"        ;@line ??
						Return ::temp122                                         ;@line ??
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_currentna
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ecCloseToGame self ::NoneVar                  ;@line 673
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_wetnesscap
				.function ecSliderHookMinLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp123 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp123 "wetnessSoaked"     ;@line 844
						Return ::temp123                                         ;@line 844
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_targettip
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp112 Bool
						.local msg String
					.endLocalTable
					.code
						Assign msg "The console can be used while in this menu (the key below Esc).\nYou can select any actor (and other objects) with your cursor by clicking on them - even when they are beind the UI. You will get some info in the bottom right corner about what you have selected. Some other objects may partially cover what you trying to select - use the scrollwheel to go through the layers at the postion where've you clicked.\nThis way you can control multiple targets and even apply the effect. When applying the effect, you still need to return to the game (in the end) for the effect to start up and be fully controllable.\nYou also need to refresh the page to show the new selection. Just select the section \"Targets\" again."  ;@line 794
						CallMethod ShowMessage self ::temp112 msg False "Close tip" "$Cancel"  ;@line 795
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_targetconsole
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod TargetSelect self ::NoneVar                   ;@line 789
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_wetnessforce
				.function ecSliderHookMaxLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp124 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp124 "wetnessCap"        ;@line 849
						Return ::temp124                                         ;@line 849
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_controlrefresh
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ForcePageReset self ::NoneVar                 ;@line 720
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_logenable
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallStatic utility SetINIBool ::NoneVar iniLog1 True     ;@line 875
						CallStatic utility SetINIBool ::NoneVar iniLog2 True     ;@line 876
						CallMethod ForcePageReset self ::NoneVar                 ;@line 877
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_sexlabauto
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallParent OnSelectST ::NoneVar                          ;@line 576
						CallMethod ForcePageReset self ::NoneVar                 ;@line 577
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_controlresetallforced
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp104 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ShowMessage self ::temp104 "Reset all forced values?" True "Reset all forced" "Cancel"  ;@line 725
						JumpF ::temp104 _label242                                ;@line 725
						CallMethod acClearFloat self ::NoneVar "wetnessForce"    ;@line 726
						CallMethod acClearFloat self ::NoneVar "specularForce"   ;@line 727
						CallMethod acClearFloat self ::NoneVar "glossinessForce"  ;@line 728
						CallMethod ecTextUpdate self ::NoneVar "Done"            ;@line 729
						Jump _label242                                           ;@line 729
					_label242:
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_controlstopall
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp105 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ShowMessage self ::temp105 "Stop all running effects?" True "Stop all effects" "Cancel"  ;@line 735
						JumpF ::temp105 _label243                                ;@line 735
						CallMethod acClearFloat self ::NoneVar "wetnessRate"     ;@line 736
						CallMethod ecTextUpdate self ::NoneVar "Done"            ;@line 737
						Jump _label243                                           ;@line 737
					_label243:
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_forcespecular
				.function OnSliderAcceptST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param value Float
					.endParamTable
					.localTable
						.local ::temp102 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CompareGT ::temp102 value 0.000000                       ;@line 691
						JumpF ::temp102 _label244                                ;@line 691
						CallMethod acSetFloat self ::NoneVar target "specularForce" value  ;@line 692
						Jump _label245                                           ;@line 692
					_label244:
						CallMethod acDelFloat self ::NoneVar target "specularForce"  ;@line 694
					_label245:
						CallMethod ForcePageReset self ::NoneVar                 ;@line 696
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_jsonimport
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp126 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ShowMessage self ::temp126 "Importing will overwrite all current settings.\nThe list of affected actors and their possibly forced values is not altered." True "Import and overwrite" "Cancel"  ;@line 864
						JumpF ::temp126 _label246                                ;@line 864
						CallMethod ecImport self ::NoneVar jsonPath              ;@line 865
						Jump _label246                                           ;@line 865
					_label246:
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_forcewetness
				.function OnSliderAcceptST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param value Float
					.endParamTable
					.localTable
						.local ::temp101 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CompareLT ::temp101 value 0.000000                       ;@line ??
						JumpF ::temp101 _label247                                ;@line ??
						CallMethod acDelFloat self ::NoneVar target "wetnessForce"  ;@line ??
						Jump _label248                                           ;@line ??
					_label247:
						CallMethod acSetFloat self ::NoneVar target "wetnessForce" value  ;@line ??
					_label248:
						CallMethod ForcePageReset self ::NoneVar                 ;@line ??
					.endCode
				.endFunction
				.function ecSliderHookMaxLimit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp100 Float
					.endLocalTable
					.code
						CallMethod ecGetFloat self ::temp100 "wetnessCap"        ;@line ??
						Return ::temp100                                         ;@line ??
					.endCode
				.endFunction
			.endState
			.state ecmcm_state_targetdeselect
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp117 Actor
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp117 None                                      ;@line 815
						Assign target ::temp117                                  ;@line 815
						CallMethod ForcePageReset self ::NoneVar                 ;@line 816
					.endCode
				.endFunction
			.endState
		.endStateTable
	.endObject
.endObjectTable
