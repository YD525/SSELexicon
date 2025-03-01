.info
	.source "asucimnasyivwvsyktc"
	.modifyTime 1709408083 ;Sun Mar 03 03:34:43 2024 Local
	.compileTime 1709408088 ;Sun Mar 03 03:34:48 2024 Local
	.user "zleovxbzslzqj"
	.computer "YFLWFTSNXVFCXUV"
.endInfo
.userFlagsRef
	.flag conditional 1	; 0x00000001
	.flag hidden 0	; 0x00000000
.endUserFlagsRef
.objectTable
	.object zadPlayerScript ReferenceAlias
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
			.variable ::ArmorJewelry_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable voiceslots Int[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::questScript_var zadbq00
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::libs_var zadlibs
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::npcQuestScript_var zadnpcquestscript
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::Voices_var zadgagvoices
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zad_DeviceHider_var armor
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::cameraState_var zadcamerastate
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::SexLabNoStrip_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::SitBlockKeywords_var formlist
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
		.endVariableTable
		.propertyTable
			.property libs zadlibs auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::libs_var
			.endProperty
			.property questScript zadbq00 auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::questScript_var
			.endProperty
			.property cameraState zadcamerastate auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::cameraState_var
			.endProperty
			.property SexLabNoStrip keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::SexLabNoStrip_var
			.endProperty
			.property Voices zadgagvoices auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Voices_var
			.endProperty
			.property npcQuestScript zadnpcquestscript auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::npcQuestScript_var
			.endProperty
			.property zad_DeviceHider armor auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zad_DeviceHider_var
			.endProperty
			.property ArmorJewelry keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ArmorJewelry_var
			.endProperty
			.property SitBlockKeywords formlist auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::SitBlockKeywords_var
			.endProperty
		.endPropertyTable
		.stateTable
			.state 
				.function OnObjectEquipped
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akBaseObject form
						.param akReference ObjectReference
					.endParamTable
					.localTable
						.local ::temp57 actor
						.local ::temp58 armor
						.local ::temp65 weapon
						.local ::temp66 spell
						.local ::temp67 light
						.local ::temp68 Bool
						.local ::temp69 form
						.local akActor actor
						.local ::temp59 armor
						.local ::temp60 Int
						.local ::temp61 keyword
						.local ::temp62 Bool
						.local akArmor armor
						.local ::temp63 Bool
						.local ::temp64 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 157
						Assign akActor ::temp57                                  ;@line 157
						Cast ::temp58 akBaseObject                               ;@line 158
						JumpF ::temp58 _label0                                   ;@line 158
						Cast ::temp59 akBaseObject                               ;@line 159
						Assign akArmor ::temp59                                  ;@line 159
						CallMethod GetSlotMask akArmor ::temp60                  ;@line 160
						CallStatic math LogicalAnd ::temp60 ::temp60 4           ;@line 160
						Cast ::temp62 ::temp60                                   ;@line 160
						JumpF ::temp62 _label1                                   ;@line 160
						PropGet zad_DeviousHarness ::libs_var ::temp61           ;@line 160
						CallMethod HasKeyword akArmor ::temp62 ::temp61          ;@line 160
						Not ::temp62 ::temp62                                    ;@line 160
						Cast ::temp62 ::temp62                                   ;@line 160
					_label1:
						JumpF ::temp62 _label2                                   ;@line 160
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 161
						PropGet zad_DeviousBra ::libs_var ::temp61               ;@line 161
						CallMethod WornHasKeyword ::temp57 ::temp63 ::temp61     ;@line 161
						Cast ::temp63 ::temp63                                   ;@line 161
						JumpF ::temp63 _label3                                   ;@line 161
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 161
						PropGet zad_EffectCompressBreasts ::libs_var ::temp61    ;@line 161
						CallMethod HasMagicEffectWithKeyword ::temp57 ::temp64 ::temp61  ;@line 161
						Cast ::temp63 ::temp64                                   ;@line 161
					_label3:
						JumpF ::temp63 _label4                                   ;@line 161
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 162
						CallMethod ShowBreasts ::libs_var ::NoneVar ::temp57     ;@line 162
						Jump _label4                                             ;@line 162
					_label4:
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 164
						PropGet zad_DeviousCorset ::libs_var ::temp61            ;@line 164
						CallMethod WornHasKeyword ::temp57 ::temp64 ::temp61     ;@line 164
						Cast ::temp64 ::temp64                                   ;@line 164
						JumpF ::temp64 _label5                                   ;@line 164
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 164
						PropGet zad_EffectCompressBelly ::libs_var ::temp61      ;@line 164
						CallMethod HasMagicEffectWithKeyword ::temp57 ::temp63 ::temp61  ;@line 164
						Cast ::temp64 ::temp63                                   ;@line 164
					_label5:
						JumpF ::temp64 _label6                                   ;@line 164
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 165
						CallMethod ShowBelly ::libs_var ::NoneVar ::temp57       ;@line 165
						Jump _label6                                             ;@line 165
					_label6:
						Jump _label2                                             ;@line 165
					_label2:
						Jump _label0                                             ;@line 165
					_label0:
						CallStatic utility IsInMenuMode ::temp63                 ;@line 170
						Not ::temp64 ::temp63                                    ;@line 170
						Cast ::temp63 ::temp64                                   ;@line 170
						JumpF ::temp63 _label7                                   ;@line 170
						Cast ::temp65 akBaseObject                               ;@line 170
						Cast ::temp62 ::temp65                                   ;@line 170
						JumpT ::temp62 _label8                                   ;@line 170
						Cast ::temp66 akBaseObject                               ;@line 170
						Cast ::temp62 ::temp66                                   ;@line 170
					_label8:
						Cast ::temp63 ::temp62                                   ;@line 170
						JumpT ::temp63 _label9                                   ;@line 170
						Cast ::temp67 akBaseObject                               ;@line 170
						Cast ::temp63 ::temp67                                   ;@line 170
					_label9:
						Not ::temp62 ::temp63                                    ;@line 170
						Cast ::temp63 ::temp62                                   ;@line 170
					_label7:
						JumpF ::temp63 _label10                                  ;@line 170
						Return None                                              ;@line 171
						Jump _label10                                            ;@line 171
					_label10:
						PropGet zad_DeviousHeavyBondage ::libs_var ::temp61      ;@line 173
						CallMethod WornHasKeyword akActor ::temp64 ::temp61      ;@line 173
						Cast ::temp63 ::temp64                                   ;@line 173
						JumpF ::temp63 _label11                                  ;@line 173
						Cast ::temp65 akBaseObject                               ;@line 173
						Cast ::temp62 ::temp65                                   ;@line 173
						JumpT ::temp62 _label12                                  ;@line 173
						Cast ::temp66 akBaseObject                               ;@line 173
						Cast ::temp62 ::temp66                                   ;@line 173
					_label12:
						Cast ::temp63 ::temp62                                   ;@line 173
						JumpT ::temp63 _label13                                  ;@line 173
						Cast ::temp67 akBaseObject                               ;@line 173
						Cast ::temp63 ::temp67                                   ;@line 173
					_label13:
						Cast ::temp62 ::temp63                                   ;@line 173
						JumpT ::temp62 _label14                                  ;@line 173
						Cast ::temp59 akBaseObject                               ;@line 173
						Cast ::temp68 ::temp59                                   ;@line 173
						JumpF ::temp68 _label15                                  ;@line 173
						CallMethod isDeviousDevice self ::temp62 akBaseObject    ;@line 173
						Not ::temp62 ::temp62                                    ;@line 173
						Cast ::temp62 ::temp62                                   ;@line 173
						JumpF ::temp62 _label16                                  ;@line 173
						CallMethod isStrapOn self ::temp68 akBaseObject          ;@line 173
						Not ::temp68 ::temp68                                    ;@line 173
						Cast ::temp62 ::temp68                                   ;@line 173
					_label16:
						Cast ::temp62 ::temp62                                   ;@line 173
						JumpF ::temp62 _label17                                  ;@line 173
						Cast ::temp69 ::zad_DeviceHider_var                      ;@line 173
						CompareEQ ::temp68 akBaseObject ::temp69                 ;@line 173
						Not ::temp68 ::temp68                                    ;@line 173
						Cast ::temp62 ::temp68                                   ;@line 173
					_label17:
						Cast ::temp68 ::temp62                                   ;@line 173
					_label15:
						Cast ::temp62 ::temp68                                   ;@line 173
					_label14:
						Cast ::temp63 ::temp62                                   ;@line 173
					_label11:
						JumpF ::temp63 _label18                                  ;@line 173
						CallStatic ui IsMenuOpen ::temp68 "InventoryMenu"        ;@line 174
						JumpF ::temp68 _label19                                  ;@line 174
						CallMethod notify ::libs_var ::NoneVar "你不能在双手被束缚的时候装备这个!" False  ;@line 175
						Jump _label19                                            ;@line 175
					_label19:
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 177
						CallMethod UnequipItem ::temp57 ::NoneVar akBaseObject False False  ;@line 177
					_label21:
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 178
						CallMethod hasAnyWeaponEquipped ::libs_var ::temp64 ::temp57  ;@line 178
						JumpF ::temp64 _label20                                  ;@line 178
						PropGet PlayerRef ::libs_var ::temp57                    ;@line 179
						CallMethod stripweapons ::libs_var ::NoneVar ::temp57 True  ;@line 179
						Jump _label21                                            ;@line 179
					_label20:
						Jump _label18                                            ;@line 179
					_label18:
					.endCode
				.endFunction
				.function InitGagSpeak
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param firsttime Bool
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp0 Int[]
					.endLocalTable
					.code
						CallStatic utility Wait ::NoneVar 2.000000               ;@line 15
						JumpF firsttime _label22                                 ;@line 16
						CallMethod log ::libs_var ::NoneVar "正在注册封口装置声音." 0  ;@line 17
						ArrayCreate ::temp0 2                                    ;@line 18
						Assign voiceslots ::temp0                                ;@line 18
						CallMethod RegisterVoices ::Voices_var ::NoneVar         ;@line 19
						Jump _label22                                            ;@line 19
					_label22:
						CallMethod RegisterVoiceEvent ::Voices_var ::NoneVar     ;@line 21
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
				.function isStrapOn
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param device form
					.endParamTable
					.localTable
						.local ::temp55 Bool
						.local ::temp56 Bool
					.endLocalTable
					.code
						CallMethod HasKeyword device ::temp55 ::SexLabNoStrip_var  ;@line 150
						Cast ::temp55 ::temp55                                   ;@line 150
						JumpF ::temp55 _label23                                  ;@line 150
						CallMethod HasKeyword device ::temp56 ::ArmorJewelry_var  ;@line 150
						Cast ::temp55 ::temp56                                   ;@line 150
					_label23:
						JumpF ::temp55 _label24                                  ;@line 150
						Return True                                              ;@line 151
						Jump _label24                                            ;@line 151
					_label24:
						Return False                                             ;@line 153
					.endCode
				.endFunction
				.function OnInit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod RegisterEvents self ::NoneVar                 ;@line 55
						CallMethod InitGagSpeak self ::NoneVar True              ;@line 56
					.endCode
				.endFunction
				.function OnSpellCast
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akSpell form
					.endParamTable
					.localTable
						.local ::temp45 spell
						.local ::temp46 spell
						.local ::temp47 Bool
						.local ::temp48 actor
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp45 akSpell                                    ;@line 130
						Cast ::temp46 None                                       ;@line 130
						CompareEQ ::temp47 ::temp45 ::temp46                     ;@line 130
						Not ::temp47 ::temp47                                    ;@line 130
						JumpF ::temp47 _label25                                  ;@line 130
						PropGet PlayerRef ::libs_var ::temp48                    ;@line 131
						CallMethod SpellCastVibrate ::libs_var ::NoneVar ::temp48 akSpell  ;@line 131
						Jump _label25                                            ;@line 131
					_label25:
					.endCode
				.endFunction
				.function OnSit
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akFurniture ObjectReference
					.endParamTable
					.localTable
						.local ::temp33 actor
						.local ::temp34 Int
						.local ::temp35 Bool
						.local akActor actor
						.local numKeywords Int
						.local i Int
						.local ::temp36 form
						.local ::temp37 keyword
						.local ::temp38 Bool
						.local ::temp39 String
						.local ::temp40 String
						.local ::NoneVar None
						.local ::temp41 Bool
						.local ::temp42 zadconfig
						.local ::temp43 Int
						.local ::temp44 Bool
					.endLocalTable
					.code
						PropGet PlayerRef ::libs_var ::temp33                    ;@line 96
						Assign akActor ::temp33                                  ;@line 96
						CallMethod GetSize ::SitBlockKeywords_var ::temp34       ;@line 97
						Assign numKeywords ::temp34                              ;@line 97
						Assign i 0                                               ;@line 98
					_label28:
						CompareLT ::temp35 i numKeywords                         ;@line 99
						JumpF ::temp35 _label26                                  ;@line 99
						CallMethod GetAt ::SitBlockKeywords_var ::temp36 i       ;@line 100
						Cast ::temp37 ::temp36                                   ;@line 100
						CallMethod HasKeyword akFurniture ::temp38 ::temp37      ;@line 100
						JumpF ::temp38 _label27                                  ;@line 100
						Cast ::temp39 akFurniture                                ;@line 101
						StrCat ::temp39 ::temp39 " 包含被阻止的关键字 "  ;@line 101
						CallMethod GetAt ::SitBlockKeywords_var ::temp36 i       ;@line 101
						Cast ::temp40 ::temp36                                   ;@line 101
						StrCat ::temp40 ::temp39 ::temp40                        ;@line 101
						StrCat ::temp39 ::temp40 ". 无法坐着."               ;@line 101
						CallMethod log ::libs_var ::NoneVar ::temp39 0           ;@line 101
						Return None                                              ;@line 102
						Jump _label27                                            ;@line 102
					_label27:
						IAdd ::temp34 i 1                                        ;@line 104
						Assign i ::temp34                                        ;@line 104
						Jump _label28                                            ;@line 104
					_label26:
						CallMethod GetBaseObject akFurniture ::temp36            ;@line 107
						CallMethod GetName ::temp36 ::temp40                     ;@line 107
						CallStatic stringutil Find ::temp34 ::temp40 "Vein" 0    ;@line 107
						CompareEQ ::temp38 ::temp34 -1                           ;@line 107
						Not ::temp38 ::temp38                                    ;@line 107
						JumpF ::temp38 _label29                                  ;@line 107
						Return None                                              ;@line 109
						Jump _label29                                            ;@line 109
					_label29:
						PropGet zad_DeviousDevice ::questScript_var ::temp37     ;@line 112
						CallMethod WornHasKeyword akActor ::temp35 ::temp37      ;@line 112
						JumpF ::temp35 _label30                                  ;@line 112
						CallMethod log ::libs_var ::NoneVar "OnSit()" 0          ;@line 113
						CallMethod SendModEvent self ::NoneVar "EventOnSit" "" 0.000000  ;@line 114
						PropGet zad_DeviousPlug ::libs_var ::temp37              ;@line 115
						CallMethod WornHasKeyword akActor ::temp38 ::temp37      ;@line 115
						JumpF ::temp38 _label31                                  ;@line 115
						PropGet zad_HasPumps ::libs_var ::temp37                 ;@line 116
						CallMethod WornHasKeyword akActor ::temp41 ::temp37      ;@line 116
						Cast ::temp41 ::temp41                                   ;@line 116
						JumpF ::temp41 _label32                                  ;@line 116
						CallStatic utility RandomInt ::temp34 0 100              ;@line 116
						PropGet Config ::libs_var ::temp42                       ;@line 116
						PropGet BaseBumpPumpChance ::temp42 ::temp43             ;@line 116
						IMultiply ::temp43 ::temp43 2                            ;@line 116
						CompareLTE ::temp44 ::temp34 ::temp43                    ;@line 116
						Cast ::temp41 ::temp44                                   ;@line 116
					_label32:
						JumpF ::temp41 _label33                                  ;@line 116
						CallMethod NotifyPlayer ::libs_var ::NoneVar "你错误地坐在了悬挂在屁股间的一个充气泵上." False  ;@line 117
						CallMethod InflateRandomPlug ::libs_var ::NoneVar akActor 1  ;@line 118
						Jump _label34                                            ;@line 118
					_label33:
						CallMethod UpdateExposure ::libs_var ::NoneVar akActor 0.150000 False  ;@line 120
						CallMethod NotifyPlayer ::libs_var ::NoneVar "你尴尬地坐下来,尽量避免碰到你体内的插头." False  ;@line 121
					_label34:
						Jump _label35                                            ;@line 121
					_label31:
						CallMethod NotifyPlayer ::libs_var ::NoneVar "你佩戴的贞操带让你要坐下来变得相当尴尬." False  ;@line 124
					_label35:
						Jump _label30                                            ;@line 124
					_label30:
					.endCode
				.endFunction
				.function OnLocationChange
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akOldLoc Location
						.param akNewLoc Location
					.endParamTable
					.localTable
						.local ::temp49 zadconfig
						.local ::temp50 Int
						.local ::temp51 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						PropGet Config ::libs_var ::temp49                       ;@line 137
						PropGet numNpcs ::temp49 ::temp50                        ;@line 137
						CompareEQ ::temp51 ::temp50 0                            ;@line 137
						Not ::temp51 ::temp51                                    ;@line 137
						JumpF ::temp51 _label36                                  ;@line 137
						CallMethod RepopulateNpcs ::libs_var ::NoneVar           ;@line 138
						Jump _label36                                            ;@line 138
					_label36:
					.endCode
				.endFunction
				.function RegisterGagSound
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param eventName String
						.param argString String
						.param argNum Float
						.param sender form
					.endParamTable
					.localTable
						.local ::temp14 Bool
						.local ::temp17 Bool
						.local ::temp15 Int
						.local ::temp16 Int
					.endLocalTable
					.code
						CompareEQ ::temp14 argString "FemaleGagged"              ;@line 42
						JumpF ::temp14 _label37                                  ;@line 42
						Cast ::temp16 argNum                                     ;@line 43
						Assign ::temp15 ::temp16                                 ;@line 43
						ArraySetElement voiceslots 1 ::temp15                    ;@line 43
						Jump _label38                                            ;@line 43
					_label37:
						CompareEQ ::temp17 argString "MaleGagged"                ;@line 44
						JumpF ::temp17 _label38                                  ;@line 44
						Cast ::temp16 argNum                                     ;@line 45
						Assign ::temp15 ::temp16                                 ;@line 45
						ArraySetElement voiceslots 0 ::temp15                    ;@line 45
						Jump _label38                                            ;@line 45
					_label38:
					.endCode
				.endFunction
				.function OnItemRemoved
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akBaseItem form
						.param aiItemCount Int
						.param akItemReference ObjectReference
						.param akDestContainer ObjectReference
					.endParamTable
					.localTable
						.local ::temp24 soulgem
						.local ::temp25 form
						.local ::temp26 Bool
						.local ::temp27 ObjectReference
						.local ::temp28 Bool
						.local ::temp29 Bool
						.local ::temp30 actor
						.local ::temp31 actor
						.local ::temp32 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						PropGet SoulgemFilled ::libs_var ::temp24                ;@line 79
						Cast ::temp25 ::temp24                                   ;@line 79
						CompareEQ ::temp26 akBaseItem ::temp25                   ;@line 79
						JumpF ::temp26 _label39                                  ;@line 79
						Cast ::temp27 None                                       ;@line 81
						CompareEQ ::temp28 akItemReference ::temp27              ;@line 81
						Not ::temp28 ::temp28                                    ;@line 81
						Cast ::temp28 ::temp28                                   ;@line 81
						JumpF ::temp28 _label40                                  ;@line 81
						Cast ::temp27 None                                       ;@line 81
						CompareEQ ::temp29 akDestContainer ::temp27              ;@line 81
						Cast ::temp28 ::temp29                                   ;@line 81
					_label40:
						JumpF ::temp28 _label41                                  ;@line 81
						CallMethod log ::libs_var ::NoneVar "可充电灵魂宝石已移除:在世界中,什么也不做." 0  ;@line 82
						Jump _label42                                            ;@line 82
					_label41:
						Cast ::temp27 None                                       ;@line 83
						CompareEQ ::temp29 akDestContainer ::temp27              ;@line 83
						Not ::temp29 ::temp29                                    ;@line 83
						Cast ::temp29 ::temp29                                   ;@line 83
						JumpF ::temp29 _label43                                  ;@line 83
						Cast ::temp30 akDestContainer                            ;@line 83
						PropGet PlayerRef ::libs_var ::temp31                    ;@line 83
						CompareEQ ::temp32 ::temp30 ::temp31                     ;@line 83
						Not ::temp32 ::temp32                                    ;@line 83
						Cast ::temp29 ::temp32                                   ;@line 83
					_label43:
						JumpF ::temp29 _label44                                  ;@line 83
						CallMethod log ::libs_var ::NoneVar "可充电灵魂宝石已移除:交给NPC,或存放在箱子中." 0  ;@line 84
						Jump _label42                                            ;@line 84
					_label44:
						CallMethod log ::libs_var ::NoneVar "可充电灵魂宝石已移除:用完,替换为空." 0  ;@line 86
						PropGet PlayerRef ::libs_var ::temp30                    ;@line 87
						PropGet SoulgemEmpty ::libs_var ::temp24                 ;@line 87
						Cast ::temp25 ::temp24                                   ;@line 87
						CallMethod AddItem ::temp30 ::NoneVar ::temp25 1 True    ;@line 87
						JumpF akItemReference _label42                           ;@line 88
						CallMethod Delete akItemReference ::NoneVar              ;@line 89
						Jump _label42                                            ;@line 89
					_label42:
						Jump _label39                                            ;@line 89
					_label39:
					.endCode
				.endFunction
				.function RegisterEvents
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod RegisterForModEvent self ::NoneVar "AnimationStart" "OnAnimationStart"  ;@line 50
						CallMethod RegisterForModEvent self ::NoneVar "GagSoundsRegistered" "RegisterGagSound"  ;@line 51
					.endCode
				.endFunction
				.function OnAnimationStart
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param eventName String
						.param argString String
						.param argNum Float
						.param sender form
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp1 sexlabframework
						.local ::temp2 sslthreadcontroller
						.local ::temp3 actor[]
						.local ::temp4 Int
						.local ::temp5 Bool
						.local controller sslthreadcontroller
						.local SceneActors actor[]
						.local i Int
						.local actor_count Int
						.local ::temp6 actor
						.local ::temp7 keyword
						.local ::temp8 Bool
						.local ::temp9 actor
						.local ::temp10 actorbase
						.local ::temp11 Int
						.local ::temp12 sslbasevoice
						.local ::temp13 String
					.endLocalTable
					.code
						CallMethod log ::libs_var ::NoneVar "SL scene started: 检查是否有堵嘴的声音" 0  ;@line 25
						PropGet SexLab ::libs_var ::temp1                        ;@line 26
						CallMethod HookController ::temp1 ::temp2 argString      ;@line 26
						Assign controller ::temp2                                ;@line 26
						PropGet SexLab ::libs_var ::temp1                        ;@line 27
						CallMethod HookActors ::temp1 ::temp3 argString          ;@line 27
						Assign SceneActors ::temp3                               ;@line 27
						Assign i 0                                               ;@line 28
						ArrayLength ::temp4 SceneActors                          ;@line 29
						Assign actor_count ::temp4                               ;@line 29
					_label48:
						CompareLT ::temp5 i actor_count                          ;@line 30
						JumpF ::temp5 _label45                                   ;@line 30
						ArrayGetElement ::temp6 SceneActors i                    ;@line 31
						PropGet zad_DeviousGag ::libs_var ::temp7                ;@line 31
						CallMethod WornHasKeyword ::temp6 ::temp8 ::temp7        ;@line 31
						JumpF ::temp8 _label46                                   ;@line 31
						ArrayGetElement ::temp6 SceneActors i                    ;@line 32
						PropGet SexLab ::libs_var ::temp1                        ;@line 32
						ArrayGetElement ::temp9 SceneActors i                    ;@line 32
						CallMethod GetActorBase ::temp9 ::temp10                 ;@line 32
						CallMethod GetSex ::temp10 ::temp4                       ;@line 32
						ArrayGetElement ::temp11 voiceslots ::temp4              ;@line 32
						CallMethod GetVoiceBySlot ::temp1 ::temp12 ::temp11      ;@line 32
						CallMethod SetVoice controller ::NoneVar ::temp6 ::temp12 False  ;@line 32
						ArrayGetElement ::temp9 SceneActors i                    ;@line 33
						CallMethod GetLeveledActorBase ::temp9 ::temp10          ;@line 33
						CallMethod GetName ::temp10 ::temp13                     ;@line 33
						StrCat ::temp13 ::temp13 " 被堵住了嘴,声音变了."  ;@line 33
						CallMethod log ::libs_var ::NoneVar ::temp13 0           ;@line 33
						Jump _label47                                            ;@line 33
					_label46:
						ArrayGetElement ::temp6 SceneActors i                    ;@line 35
						CallMethod GetLeveledActorBase ::temp6 ::temp10          ;@line 35
						CallMethod GetName ::temp10 ::temp13                     ;@line 35
						StrCat ::temp13 ::temp13 " 没有被堵住嘴."          ;@line 35
						CallMethod log ::libs_var ::NoneVar ::temp13 0           ;@line 35
					_label47:
						IAdd ::temp4 i 1                                         ;@line 37
						Assign i ::temp4                                         ;@line 37
						Jump _label48                                            ;@line 37
					_label45:
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
				.function OnObjectUnequipped
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akBaseObject form
						.param akReference ObjectReference
					.endParamTable
					.localTable
						.local ::temp70 actor
						.local ::temp71 armor
						.local akActor actor
						.local ::temp72 armor
						.local ::temp73 Int
						.local akArmor armor
						.local ::temp74 keyword
						.local ::temp75 Bool
						.local ::temp76 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						PropGet PlayerRef ::libs_var ::temp70                    ;@line 186
						Assign akActor ::temp70                                  ;@line 186
						Cast ::temp71 akBaseObject                               ;@line 187
						JumpF ::temp71 _label49                                  ;@line 187
						Cast ::temp72 akBaseObject                               ;@line 188
						Assign akArmor ::temp72                                  ;@line 188
						CallMethod GetSlotMask akArmor ::temp73                  ;@line 189
						CallStatic math LogicalAnd ::temp73 ::temp73 4           ;@line 189
						JumpF ::temp73 _label50                                  ;@line 189
						PropGet zad_DeviousBra ::libs_var ::temp74               ;@line 190
						CallMethod HasKeyword akArmor ::temp75 ::temp74          ;@line 190
						Not ::temp75 ::temp75                                    ;@line 190
						Cast ::temp75 ::temp75                                   ;@line 190
						JumpF ::temp75 _label51                                  ;@line 190
						PropGet PlayerRef ::libs_var ::temp70                    ;@line 190
						PropGet zad_DeviousBra ::libs_var ::temp74               ;@line 190
						CallMethod WornHasKeyword ::temp70 ::temp76 ::temp74     ;@line 190
						Cast ::temp75 ::temp76                                   ;@line 190
					_label51:
						Cast ::temp75 ::temp75                                   ;@line 190
						JumpF ::temp75 _label52                                  ;@line 190
						PropGet PlayerRef ::libs_var ::temp70                    ;@line 190
						PropGet zad_EffectCompressBreasts ::libs_var ::temp74    ;@line 190
						CallMethod HasMagicEffectWithKeyword ::temp70 ::temp76 ::temp74  ;@line 190
						Cast ::temp75 ::temp76                                   ;@line 190
					_label52:
						JumpF ::temp75 _label53                                  ;@line 190
						PropGet PlayerRef ::libs_var ::temp70                    ;@line 191
						CallMethod HideBreasts ::libs_var ::NoneVar ::temp70     ;@line 191
						Jump _label53                                            ;@line 191
					_label53:
						PropGet zad_DeviousCorset ::libs_var ::temp74            ;@line 193
						CallMethod HasKeyword akArmor ::temp76 ::temp74          ;@line 193
						Not ::temp75 ::temp76                                    ;@line 193
						Cast ::temp75 ::temp75                                   ;@line 193
						JumpF ::temp75 _label54                                  ;@line 193
						PropGet PlayerRef ::libs_var ::temp70                    ;@line 193
						PropGet zad_DeviousCorset ::libs_var ::temp74            ;@line 193
						CallMethod WornHasKeyword ::temp70 ::temp76 ::temp74     ;@line 193
						Cast ::temp75 ::temp76                                   ;@line 193
					_label54:
						Cast ::temp75 ::temp75                                   ;@line 193
						JumpF ::temp75 _label55                                  ;@line 193
						PropGet PlayerRef ::libs_var ::temp70                    ;@line 193
						PropGet zad_EffectCompressBelly ::libs_var ::temp74      ;@line 193
						CallMethod HasMagicEffectWithKeyword ::temp70 ::temp76 ::temp74  ;@line 193
						Cast ::temp75 ::temp76                                   ;@line 193
					_label55:
						JumpF ::temp75 _label56                                  ;@line 193
						PropGet PlayerRef ::libs_var ::temp70                    ;@line 194
						CallMethod HideBelly ::libs_var ::NoneVar ::temp70       ;@line 194
						Jump _label56                                            ;@line 194
					_label56:
						Jump _label50                                            ;@line 194
					_label50:
						Jump _label49                                            ;@line 194
					_label49:
					.endCode
				.endFunction
				.function OnPlayerLoadGame
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp18 actor
						.local ::temp19 Float
						.local ::NoneVar None
						.local ::temp20 soulgem
						.local ::temp21 form
						.local ::temp22 keyword
						.local ::temp23 Bool
						.local akActor actor
					.endLocalTable
					.code
						PropGet PlayerRef ::libs_var ::temp18                    ;@line 60
						Assign akActor ::temp18                                  ;@line 60
						Assign ::temp19 0.000000                                 ;@line 61
						PropSet SpellCastVibrateCooldown ::libs_var ::temp19     ;@line 61
						CallMethod Maintenance ::questScript_var ::NoneVar       ;@line 62
						CallMethod Maintenance ::cameraState_var ::NoneVar       ;@line 63
						CallMethod ResetDialogue ::libs_var ::NoneVar            ;@line 64
						PropGet SoulgemFilled ::libs_var ::temp20                ;@line 65
						Cast ::temp21 ::temp20                                   ;@line 65
						CallMethod AddInventoryEventFilter self ::NoneVar ::temp21  ;@line 65
						PropGet zad_DeviousHobbleSkirt ::libs_var ::temp22       ;@line 66
						CallMethod WornHasKeyword akActor ::temp23 ::temp22      ;@line 66
						JumpF ::temp23 _label57                                  ;@line 66
						CallStatic utility SetINIBool ::NoneVar "bDampenPlayerControls:Controls" False  ;@line 67
						Jump _label57                                            ;@line 67
					_label57:
						PropGet zad_EffectForcedWalk ::libs_var ::temp22         ;@line 69
						CallMethod WornHasKeyword akActor ::temp23 ::temp22      ;@line 69
						JumpF ::temp23 _label58                                  ;@line 69
						CallMethod MuteOverEncumberedMSG ::libs_var ::NoneVar    ;@line 70
						Jump _label58                                            ;@line 70
					_label58:
						CallStatic game UpdateHairColor ::NoneVar                ;@line 72
						CallMethod RegisterEvents self ::NoneVar                 ;@line 73
						CallMethod InitGagSpeak self ::NoneVar False             ;@line 74
					.endCode
				.endFunction
				.function isDeviousDevice
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param device form
					.endParamTable
					.localTable
						.local ::temp52 keyword
						.local ::temp53 Bool
						.local ::temp54 Bool
					.endLocalTable
					.code
						PropGet zad_InventoryDevice ::libs_var ::temp52          ;@line 143
						CallMethod HasKeyword device ::temp53 ::temp52           ;@line 143
						Cast ::temp53 ::temp53                                   ;@line 143
						JumpT ::temp53 _label59                                  ;@line 143
						PropGet zad_Lockable ::libs_var ::temp52                 ;@line 143
						CallMethod HasKeyword device ::temp54 ::temp52           ;@line 143
						Cast ::temp53 ::temp54                                   ;@line 143
					_label59:
						Cast ::temp53 ::temp53                                   ;@line 143
						JumpT ::temp53 _label60                                  ;@line 143
						PropGet zad_DeviousPlug ::libs_var ::temp52              ;@line 143
						CallMethod HasKeyword device ::temp54 ::temp52           ;@line 143
						Cast ::temp53 ::temp54                                   ;@line 143
					_label60:
						JumpF ::temp53 _label61                                  ;@line 143
						Return True                                              ;@line 144
						Jump _label61                                            ;@line 144
					_label61:
						Return False                                             ;@line 146
					.endCode
				.endFunction
			.endState
		.endStateTable
	.endObject
.endObjectTable
