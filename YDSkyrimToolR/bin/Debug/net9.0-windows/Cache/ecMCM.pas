.info
	.source "ecMCM.psc"
	.modifyTime 1461503311 ;Sun Apr 24 21:08:31 2016 Local
	.compileTime 1488204724 ;Mon Feb 27 22:12:04 2017 Local
	.user "BFis"
	.computer "INNEB7"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object ecMCM ski_configbase
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
			.variable ecPrefixDesc String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_desc_"
			.endVariable
			.variable ecPrefixFormat String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_format_"
			.endVariable
			.variable ecTypeSlider Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 2
			.endVariable
			.variable ecTypeText Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 3
			.endVariable
			.variable ecTypeBool Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 1
			.endVariable
			.variable ::ecUpdate_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ecVersion String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_version"
			.endVariable
			.variable ecPrefixMax String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_max_"
			.endVariable
			.variable ecPrefixStep String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_step_"
			.endVariable
			.variable ecRegistry String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_registry"
			.endVariable
			.variable ecPrefixType String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_type_"
			.endVariable
			.variable ecCheckOk Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ecCheckRender Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::ecBurnIn_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ecPrefixMin String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_min_"
			.endVariable
			.variable ecPrefixDefault String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_default_"
			.endVariable
			.variable ecState String
				.userFlags 0	; Flags: 0x00000000
				.initialValue "ecMCM_state_"
			.endVariable
		.endVariableTable
		.propertyTable
			.property ecBurnIn Bool auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::ecBurnIn_var
			.endProperty
			.property ecUpdate Bool auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::ecUpdate_var
			.endProperty
		.endPropertyTable
		.stateTable
			.state 
				.function OnPageReset
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param page String
					.endParamTable
					.localTable
						.local ::temp107 Bool
						.local ::temp108 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						Not ::temp107 ecCheckOk                                  ;@line 430
						JumpF ::temp107 _label0                                  ;@line 430
						CallMethod ecCheck self ::temp108                        ;@line 431
						Assign ecCheckOk ::temp108                               ;@line 431
						Jump _label0                                             ;@line 431
					_label0:
						JumpF ecCheckOk _label1                                  ;@line 433
						CallMethod ecPage self ::NoneVar page                    ;@line 434
						Jump _label2                                             ;@line 434
					_label1:
						CallMethod ecCheckPage self ::NoneVar                    ;@line 436
					_label2:
					.endCode
				.endFunction
				.function ecCheckItem
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param name String
						.param needs String
						.param ok Bool
						.param broken Bool
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp101 Bool
						.local ::temp102 Int
						.local val String
					.endLocalTable
					.code
						JumpF ecCheckRender _label3                              ;@line 392
						CallMethod ecHeader self ::NoneVar name True             ;@line 393
						Assign val "ok"                                          ;@line 394
						Not ::temp101 ok                                         ;@line 395
						JumpF ::temp101 _label4                                  ;@line 395
						JumpF broken _label5                                     ;@line 396
						Assign val "broken"                                      ;@line 397
						Jump _label6                                             ;@line 397
					_label5:
						Assign val "missing"                                     ;@line 399
					_label6:
						Jump _label4                                             ;@line 399
					_label4:
						CallMethod ecFlags self ::temp102 True                   ;@line 402
						CallMethod AddTextOption self ::temp102 needs val ::temp102  ;@line 402
						Jump _label3                                             ;@line 402
					_label3:
						Return ok                                                ;@line 404
					.endCode
				.endFunction
				.function ecToggle
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param storeKey String
						.param label String
						.param default Bool
						.param desc String
						.param disabled Bool
						.param update Bool
						.param saved Bool
					.endParamTable
					.localTable
						.local ::temp17 Bool
						.local ::temp18 String
						.local ::temp19 form
						.local ::temp20 Bool
						.local value Bool
						.local ::temp21 Int
						.local ::NoneVar None
					.endLocalTable
					.code
						Assign value default                                     ;@line 104
						Cast ::temp17 update                                     ;@line 105
						JumpT ::temp17 _label7                                   ;@line 105
						Cast ::temp17 ::ecUpdate_var                             ;@line 105
					_label7:
						Cast ::temp17 ::temp17                                   ;@line 105
						JumpT ::temp17 _label8                                   ;@line 105
						StrCat ::temp18 ecPrefixType storeKey                    ;@line 105
						Cast ::temp19 self                                       ;@line 105
						CallStatic storageutil HasIntValue ::temp20 ::temp19 ::temp18  ;@line 105
						Not ::temp20 ::temp20                                    ;@line 105
						Cast ::temp17 ::temp20                                   ;@line 105
					_label8:
						JumpF ::temp17 _label9                                   ;@line 105
						JumpF saved _label10                                     ;@line 106
						Cast ::temp19 self                                       ;@line 107
						CallStatic storageutil StringListAdd ::temp21 ::temp19 ecRegistry storeKey False  ;@line 107
						Jump _label10                                            ;@line 107
					_label10:
						StrCat ::temp18 ecPrefixType storeKey                    ;@line 109
						Cast ::temp19 self                                       ;@line 109
						CallStatic storageutil SetIntValue ::temp21 ::temp19 ::temp18 ecTypeBool  ;@line 109
						StrCat ::temp18 ecPrefixDesc storeKey                    ;@line 110
						Cast ::temp19 self                                       ;@line 110
						CallStatic storageutil SetStringValue ::temp18 ::temp19 ::temp18 desc  ;@line 110
						Jump _label9                                             ;@line 110
					_label9:
						StrCat ::temp18 ecPrefixDefault storeKey                 ;@line 112
						Cast ::temp21 default                                    ;@line 112
						Cast ::temp19 self                                       ;@line 112
						CallStatic storageutil SetIntValue ::temp21 ::temp19 ::temp18 ::temp21  ;@line 112
						JumpF saved _label11                                     ;@line 113
						Cast ::temp21 default                                    ;@line 114
						Cast ::temp19 self                                       ;@line 114
						CallStatic storageutil GetIntValue ::temp21 ::temp19 storeKey ::temp21  ;@line 114
						Cast ::temp20 ::temp21                                   ;@line 114
						Assign value ::temp20                                    ;@line 114
						Cast ::temp21 value                                      ;@line 115
						Cast ::temp19 self                                       ;@line 115
						CallStatic storageutil SetIntValue ::temp21 ::temp19 storeKey ::temp21  ;@line 115
						Jump _label11                                            ;@line 115
					_label11:
						Not ::temp17 ::ecBurnIn_var                              ;@line 117
						JumpF ::temp17 _label12                                  ;@line 117
						StrCat ::temp18 ecState storeKey                         ;@line 118
						CallMethod ecFlags self ::temp21 disabled                ;@line 118
						CallMethod AddToggleOptionST self ::NoneVar ::temp18 label value ::temp21  ;@line 118
						Jump _label12                                            ;@line 118
					_label12:
					.endCode
				.endFunction
				.function ecSlider
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param storeKey String
						.param label String
						.param default Float
						.param min Float
						.param max Float
						.param step Float
						.param format String
						.param prec Int
						.param desc String
						.param disabled Bool
						.param update Bool
						.param saved Bool
					.endParamTable
					.localTable
						.local ::temp22 Bool
						.local ::temp24 form
						.local ::temp25 Bool
						.local value Float
						.local ::temp23 String
						.local ::temp27 Float
						.local ::temp26 Int
						.local ::NoneVar None
					.endLocalTable
					.code
						Assign value default                                     ;@line 123
						Not ::temp22 format                                      ;@line 124
						JumpF ::temp22 _label13                                  ;@line 124
						Cast ::temp23 prec                                       ;@line 125
						StrCat ::temp23 "23123" ::temp23                         ;@line 125
						StrCat ::temp23 ::temp23 "}"                             ;@line 125
						Assign format ::temp23                                   ;@line 125
						Jump _label13                                            ;@line 125
					_label13:
						Cast ::temp22 update                                     ;@line 127
						JumpT ::temp22 _label14                                  ;@line 127
						Cast ::temp22 ::ecUpdate_var                             ;@line 127
					_label14:
						Cast ::temp22 ::temp22                                   ;@line 127
						JumpT ::temp22 _label15                                  ;@line 127
						StrCat ::temp23 ecPrefixType storeKey                    ;@line 127
						Cast ::temp24 self                                       ;@line 127
						CallStatic storageutil HasIntValue ::temp25 ::temp24 ::temp23  ;@line 127
						Not ::temp25 ::temp25                                    ;@line 127
						Cast ::temp22 ::temp25                                   ;@line 127
					_label15:
						JumpF ::temp22 _label16                                  ;@line 127
						JumpF saved _label17                                     ;@line 128
						Cast ::temp24 self                                       ;@line 129
						CallStatic storageutil StringListAdd ::temp26 ::temp24 ecRegistry storeKey False  ;@line 129
						Jump _label17                                            ;@line 129
					_label17:
						StrCat ::temp23 ecPrefixType storeKey                    ;@line 131
						Cast ::temp24 self                                       ;@line 131
						CallStatic storageutil SetIntValue ::temp26 ::temp24 ::temp23 ecTypeSlider  ;@line 131
						StrCat ::temp23 ecPrefixDesc storeKey                    ;@line 132
						Cast ::temp24 self                                       ;@line 132
						CallStatic storageutil SetStringValue ::temp23 ::temp24 ::temp23 desc  ;@line 132
						StrCat ::temp23 ecPrefixMin storeKey                     ;@line 133
						Cast ::temp24 self                                       ;@line 133
						CallStatic storageutil SetFloatValue ::temp27 ::temp24 ::temp23 min  ;@line 133
						StrCat ::temp23 ecPrefixMax storeKey                     ;@line 134
						Cast ::temp24 self                                       ;@line 134
						CallStatic storageutil SetFloatValue ::temp27 ::temp24 ::temp23 max  ;@line 134
						StrCat ::temp23 ecPrefixStep storeKey                    ;@line 135
						Cast ::temp24 self                                       ;@line 135
						CallStatic storageutil SetFloatValue ::temp27 ::temp24 ::temp23 step  ;@line 135
						StrCat ::temp23 ecPrefixFormat storeKey                  ;@line 136
						Cast ::temp24 self                                       ;@line 136
						CallStatic storageutil SetStringValue ::temp23 ::temp24 ::temp23 format  ;@line 136
						Jump _label16                                            ;@line 136
					_label16:
						StrCat ::temp23 ecPrefixDefault storeKey                 ;@line 138
						Cast ::temp24 self                                       ;@line 138
						CallStatic storageutil SetFloatValue ::temp27 ::temp24 ::temp23 default  ;@line 138
						JumpF saved _label18                                     ;@line 139
						Cast ::temp24 self                                       ;@line 140
						CallStatic storageutil GetFloatValue ::temp27 ::temp24 storeKey default  ;@line 140
						Assign value ::temp27                                    ;@line 140
						Cast ::temp24 self                                       ;@line 141
						CallStatic storageutil SetFloatValue ::temp27 ::temp24 storeKey value  ;@line 141
						Jump _label18                                            ;@line 141
					_label18:
						Not ::temp25 ::ecBurnIn_var                              ;@line 143
						JumpF ::temp25 _label19                                  ;@line 143
						StrCat ::temp23 ecState storeKey                         ;@line 144
						CallMethod ecFlags self ::temp26 disabled                ;@line 144
						CallMethod AddSliderOptionST self ::NoneVar ::temp23 label value format ::temp26  ;@line 144
						Jump _label19                                            ;@line 144
					_label19:
					.endCode
				.endFunction
				.function ecTextUpdate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param newValue String
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod SetTextOptionValueST self ::NoneVar newValue False ""  ;@line 287
					.endCode
				.endFunction
				.function ecSliderSetValues
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod SetSliderDialogStartValue self ::NoneVar value  ;@line 259
						CallMethod SetSliderDialogDefaultValue self ::NoneVar default  ;@line 260
						CallMethod SetSliderDialogRange self ::NoneVar min max   ;@line 261
						CallMethod SetSliderDialogInterval self ::NoneVar step   ;@line 262
					.endCode
				.endFunction
				.function ecSliderHook
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param value Float
						.param default Float
						.param min Float
						.param max Float
						.param step Float
					.endParamTable
					.localTable
						.local ::temp60 Float
						.local ::temp61 Float
						.local ::temp62 Float
						.local ::temp63 Float
						.local ::NoneVar None
						.local minLimit Float
						.local maxLimit Float
					.endLocalTable
					.code
						CallMethod ecSliderHookMinLimit self ::temp60 value default min max step  ;@line 248
						Assign minLimit ::temp60                                 ;@line 248
						CallMethod ecSliderHookMaxLimit self ::temp60 value default min max step  ;@line 249
						Assign maxLimit ::temp60                                 ;@line 249
						CallStatic papyrusutil ClampFloat ::temp60 value minLimit maxLimit  ;@line 250
						CallStatic papyrusutil ClampFloat ::temp61 default minLimit maxLimit  ;@line 250
						CallStatic papyrusutil ClampFloat ::temp62 min minLimit maxLimit  ;@line 250
						CallStatic papyrusutil ClampFloat ::temp63 max minLimit maxLimit  ;@line 250
						CallMethod ecSliderSetValues self ::NoneVar ::temp60 ::temp61 ::temp62 ::temp63 step  ;@line 250
					.endCode
				.endFunction
				.function ecCloseToGame
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallStatic ui Invoke ::NoneVar "Journal Menu" "_root.QuestJournalFader.Menu_mc.ConfigPanelClose"  ;@line 64
						CallStatic ui Invoke ::NoneVar "Journal Menu" "_root.QuestJournalFader.Menu_mc.CloseMenu"  ;@line 65
					.endCode
				.endFunction
				.function ecCheck
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp103 Int
						.local ::temp104 Bool
						.local ::temp105 Bool
						.local skser Int
						.local ok Bool
					.endLocalTable
					.code
						CallStatic skse GetVersionRelease ::temp103              ;@line 408
						Assign skser ::temp103                                   ;@line 408
						Assign ok True                                           ;@line 409
						CompareGTE ::temp104 skser 48                            ;@line 410
						CallStatic skse GetScriptVersionRelease ::temp103        ;@line 410
						CompareEQ ::temp105 skser ::temp103                      ;@line 410
						Not ::temp105 ::temp105                                  ;@line 410
						CallMethod ecCheckItem self ::temp104 "SKSE Plugin" "1.7.3+" ::temp104 ::temp105  ;@line 410
						Cast ::temp105 ::temp104                                 ;@line 410
						JumpF ::temp105 _label20                                 ;@line 410
						Cast ::temp105 ok                                        ;@line 410
					_label20:
						Assign ok ::temp105                                      ;@line 410
						CallStatic papyrusutil GetVersion ::temp103              ;@line 411
						CompareGTE ::temp104 ::temp103 32                        ;@line 411
						CallMethod ecCheckItem self ::temp105 "papyrusutil" "3.2+" ::temp104 False  ;@line 411
						Cast ::temp104 ::temp105                                 ;@line 411
						JumpF ::temp104 _label21                                 ;@line 411
						Cast ::temp104 ok                                        ;@line 411
					_label21:
						Assign ok ::temp104                                      ;@line 411
						Return ok                                                ;@line 412
					.endCode
				.endFunction
				.function ecGetFloat
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp37 form
						.local ::temp38 Float
					.endLocalTable
					.code
						Cast ::temp37 self                                       ;@line 175
						CallStatic storageutil GetFloatValue ::temp38 ::temp37 storeKey 0.000000  ;@line 175
						Return ::temp38                                          ;@line 175
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
						Assign ecCheckOk False                                   ;@line 416
						CallParent OnGameReload ::NoneVar                        ;@line 417
					.endCode
				.endFunction
				.function OnSliderAcceptST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param newValue Float
					.endParamTable
					.localTable
						.local ::temp64 String
						.local ::temp65 Bool
						.local ::temp66 form
						.local ::temp67 Int
						.local storeKey String
						.local type Int
						.local ::temp68 Float
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ecKey self ::temp64 "" 0                      ;@line 265
						Assign storeKey ::temp64                                 ;@line 265
						Not ::temp65 storeKey                                    ;@line 266
						JumpF ::temp65 _label22                                  ;@line 266
						Return None                                              ;@line 267
						Jump _label22                                            ;@line 267
					_label22:
						StrCat ::temp64 ecPrefixType storeKey                    ;@line 269
						Cast ::temp66 self                                       ;@line 269
						CallStatic storageutil GetIntValue ::temp67 ::temp66 ::temp64 0  ;@line 269
						Assign type ::temp67                                     ;@line 269
						CompareEQ ::temp65 type ecTypeSlider                     ;@line 270
						JumpF ::temp65 _label23                                  ;@line 270
						Cast ::temp66 self                                       ;@line 271
						CallStatic storageutil SetFloatValue ::temp68 ::temp66 storeKey newValue  ;@line 271
						CallMethod ecSliderUpdate self ::NoneVar newValue storeKey  ;@line 272
						CallMethod ecConfigChanged self ::NoneVar                ;@line 273
						Jump _label23                                            ;@line 273
					_label23:
					.endCode
				.endFunction
				.function ecDump
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String[]
					.paramTable
						.param asc Bool
					.endParamTable
					.localTable
						.local ::temp91 form
						.local ::temp92 String[]
						.local ::NoneVar None
						.local ::temp93 Int
						.local ::temp94 Bool
						.local Items String[]
						.local i Int
						.local ::temp95 String
						.local ::temp96 Bool
						.local ::temp99 Bool
						.local Item String
						.local type Int
						.local ::temp97 String
						.local ::temp98 String
						.local ::temp100 Float
					.endLocalTable
					.code
						Cast ::temp91 self                                       ;@line 372
						CallStatic storageutil StringListToArray ::temp92 ::temp91 ecRegistry  ;@line 372
						Assign Items ::temp92                                    ;@line 372
						CallStatic papyrusutil SortStringArray ::NoneVar Items asc  ;@line 373
						ArrayLength ::temp93 Items                               ;@line 374
						Assign i ::temp93                                        ;@line 374
					_label28:
						CompareGT ::temp94 i 0                                   ;@line 375
						JumpF ::temp94 _label24                                  ;@line 375
						ISubtract ::temp93 i 1                                   ;@line 376
						Assign i ::temp93                                        ;@line 376
						ArrayGetElement ::temp95 Items i                         ;@line 377
						Assign Item ::temp95                                     ;@line 377
						StrCat ::temp95 ecPrefixType Item                        ;@line 378
						Cast ::temp91 self                                       ;@line 378
						CallStatic storageutil GetIntValue ::temp93 ::temp91 ::temp95 0  ;@line 378
						Assign type ::temp93                                     ;@line 378
						CompareEQ ::temp96 type ecTypeBool                       ;@line 379
						JumpF ::temp96 _label25                                  ;@line 379
						StrCat ::temp97 Item "="                                 ;@line 380
						Cast ::temp91 self                                       ;@line 380
						CallStatic storageutil GetIntValue ::temp93 ::temp91 Item 0  ;@line 380
						Cast ::temp98 ::temp93                                   ;@line 380
						StrCat ::temp98 ::temp97 ::temp98                        ;@line 380
						Assign ::temp95 ::temp98                                 ;@line 380
						ArraySetElement Items i ::temp95                         ;@line 380
						Jump _label26                                            ;@line 380
					_label25:
						CompareEQ ::temp99 type ecTypeSlider                     ;@line 381
						JumpF ::temp99 _label27                                  ;@line 381
						StrCat ::temp95 Item "="                                 ;@line 382
						Cast ::temp91 self                                       ;@line 382
						CallStatic storageutil GetFloatValue ::temp100 ::temp91 Item 0.000000  ;@line 382
						Cast ::temp98 ::temp100                                  ;@line 382
						StrCat ::temp98 ::temp95 ::temp98                        ;@line 382
						Assign ::temp97 ::temp98                                 ;@line 382
						ArraySetElement Items i ::temp97                         ;@line 382
						Jump _label26                                            ;@line 382
					_label27:
						CompareEQ ::temp99 type ecTypeText                       ;@line 383
						JumpF ::temp99 _label26                                  ;@line 383
						StrCat ::temp97 Item "="                                 ;@line 384
						Cast ::temp91 self                                       ;@line 384
						CallStatic storageutil GetStringValue ::temp98 ::temp91 Item ""  ;@line 384
						StrCat ::temp97 ::temp97 ::temp98                        ;@line 384
						Assign ::temp95 ::temp97                                 ;@line 384
						ArraySetElement Items i ::temp95                         ;@line 384
						Jump _label26                                            ;@line 384
					_label26:
						Jump _label28                                            ;@line 384
					_label24:
						Return Items                                             ;@line 387
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
					.endLocalTable
					.code
					.endCode
				.endFunction
				.function ecExport
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param fileName String
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp73 form
						.local ::temp74 String[]
						.local ::temp75 Int
						.local ::temp76 Bool
						.local storeKeys String[]
						.local i Int
						.local ::temp77 String
						.local ::temp78 Bool
						.local ::temp79 Bool
						.local storeKey String
						.local type Int
						.local ::temp80 Float
					.endLocalTable
					.code
						CallMethod ecFlagsUpdate self ::NoneVar True             ;@line 296
						CallMethod ecTextUpdate self ::NoneVar "23123213213"     ;@line 297
						Cast ::temp73 self                                       ;@line 298
						CallStatic storageutil StringListToArray ::temp74 ::temp73 ecRegistry  ;@line 298
						Assign storeKeys ::temp74                                ;@line 298
						ArrayLength ::temp75 storeKeys                           ;@line 299
						Assign i ::temp75                                        ;@line 299
					_label33:
						CompareGT ::temp76 i 0                                   ;@line 300
						JumpF ::temp76 _label29                                  ;@line 300
						ISubtract ::temp75 i 1                                   ;@line 301
						Assign i ::temp75                                        ;@line 301
						ArrayGetElement ::temp77 storeKeys i                     ;@line 302
						Assign storeKey ::temp77                                 ;@line 302
						StrCat ::temp77 ecPrefixType storeKey                    ;@line 303
						Cast ::temp73 self                                       ;@line 303
						CallStatic storageutil GetIntValue ::temp75 ::temp73 ::temp77 0  ;@line 303
						Assign type ::temp75                                     ;@line 303
						CompareEQ ::temp78 type ecTypeBool                       ;@line 304
						JumpF ::temp78 _label30                                  ;@line 304
						Cast ::temp73 self                                       ;@line 305
						CallStatic storageutil GetIntValue ::temp75 ::temp73 storeKey 0  ;@line 305
						CallStatic jsonutil SetIntValue ::temp75 fileName storeKey ::temp75  ;@line 305
						Jump _label31                                            ;@line 305
					_label30:
						CompareEQ ::temp79 type ecTypeSlider                     ;@line 306
						JumpF ::temp79 _label32                                  ;@line 306
						Cast ::temp73 self                                       ;@line 307
						CallStatic storageutil GetFloatValue ::temp80 ::temp73 storeKey 0.000000  ;@line 307
						CallStatic jsonutil SetFloatValue ::temp80 fileName storeKey ::temp80  ;@line 307
						Jump _label31                                            ;@line 307
					_label32:
						CompareEQ ::temp79 type ecTypeText                       ;@line 308
						JumpF ::temp79 _label31                                  ;@line 308
						Cast ::temp73 self                                       ;@line 309
						CallStatic storageutil GetStringValue ::temp77 ::temp73 storeKey ""  ;@line 309
						CallStatic jsonutil SetStringValue ::temp77 fileName storeKey ::temp77  ;@line 309
						Jump _label31                                            ;@line 309
					_label31:
						Jump _label33                                            ;@line 309
					_label29:
						CallMethod ecTextUpdate self ::NoneVar ""                ;@line 313
						CallStatic jsonutil Unload ::temp79 fileName True False  ;@line 314
						CallMethod ecTextUpdate self ::NoneVar ""                ;@line 316
						CallMethod ecFlagsUpdate self ::NoneVar False            ;@line 317
					.endCode
				.endFunction
				.function ecFillMode
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param topDown Bool
						.param leftRight Bool
					.endParamTable
					.localTable
						.local ::temp8 Bool
						.local ::temp9 Bool
						.local ::temp10 Int
						.local ::NoneVar None
					.endLocalTable
					.code
						Not ::temp8 ::ecBurnIn_var                               ;@line 70
						JumpF ::temp8 _label34                                   ;@line 70
						CompareEQ ::temp9 topDown leftRight                      ;@line 71
						JumpF ::temp9 _label35                                   ;@line 71
						Assign topDown True                                      ;@line 72
						Jump _label35                                            ;@line 72
					_label35:
						JumpF topDown _label36                                   ;@line 74
						PropGet TOP_TO_BOTTOM self ::temp10                      ;@line 75
						CallMethod SetCursorFillMode self ::NoneVar ::temp10     ;@line 75
						Jump _label37                                            ;@line 75
					_label36:
						PropGet LEFT_TO_RIGHT self ::temp10                      ;@line 77
						CallMethod SetCursorFillMode self ::NoneVar ::temp10     ;@line 77
					_label37:
						Jump _label34                                            ;@line 77
					_label34:
					.endCode
				.endFunction
				.function ecStartup
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp0 form
						.local ::temp1 Int
						.local ::temp2 Int
						.local ::temp3 Bool
						.local ::temp4 String
						.local ::temp5 Bool
						.local ::NoneVar None
						.local lastPage String
						.local lastPageNum Int
						.local i Int
					.endLocalTable
					.code
						Cast ::temp0 self                                        ;@line 30
						CallStatic storageutil GetIntValue ::temp1 ::temp0 ecVersion -1  ;@line 30
						CallMethod GetVersion self ::temp2                       ;@line 30
						CompareLT ::temp3 ::temp1 ::temp2                        ;@line 30
						JumpF ::temp3 _label38                                   ;@line 30
						JumpF ::ecBurnIn_var _label39                            ;@line 32
					_label41:
						JumpF ::ecBurnIn_var _label40                            ;@line 33
						CallStatic utility Wait ::NoneVar 3.000000               ;@line 34
						Jump _label41                                            ;@line 34
					_label40:
						Return None                                              ;@line 36
						Jump _label42                                            ;@line 36
					_label39:
						Assign ::ecBurnIn_var True                               ;@line 38
					_label42:
						PropGet CurrentPage self ::temp4                         ;@line 41
						Assign lastPage ::temp4                                  ;@line 41
						Assign lastPageNum -1                                    ;@line 42
						ArrayLength ::temp1 ::Pages_var                          ;@line 43
						Assign i ::temp1                                         ;@line 43
					_label44:
						CompareGT ::temp5 i 0                                    ;@line 44
						JumpF ::temp5 _label43                                   ;@line 44
						ISubtract ::temp2 i 1                                    ;@line 45
						Assign i ::temp2                                         ;@line 45
						ArrayGetElement ::temp4 ::Pages_var i                    ;@line 46
						CallMethod ecPage self ::NoneVar ::temp4                 ;@line 46
						Jump _label44                                            ;@line 46
					_label43:
						CallMethod GetVersion self ::temp1                       ;@line 49
						Cast ::temp0 self                                        ;@line 49
						CallStatic storageutil SetIntValue ::temp2 ::temp0 ecVersion ::temp1  ;@line 49
						Assign ::ecBurnIn_var False                              ;@line 50
						CallMethod ecConfigChanged self ::NoneVar                ;@line 51
						Jump _label38                                            ;@line 51
					_label38:
					.endCode
				.endFunction
				.function ecConfigChanged
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
					.endLocalTable
					.code
					.endCode
				.endFunction
				.function ecHeader
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param label String
						.param disabled Bool
					.endParamTable
					.localTable
						.local ::temp15 Bool
						.local ::temp16 Int
					.endLocalTable
					.code
						Not ::temp15 ::ecBurnIn_var                              ;@line 98
						JumpF ::temp15 _label45                                  ;@line 98
						CallMethod ecFlags self ::temp16 disabled                ;@line 99
						CallMethod AddHeaderOption self ::temp16 label ::temp16  ;@line 99
						Jump _label45                                            ;@line 99
					_label45:
					.endCode
				.endFunction
				.function ecFlags
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Int
					.paramTable
						.param disabled Bool
					.endParamTable
					.localTable
						.local ::temp6 Int
						.local ::temp7 Int
					.endLocalTable
					.code
						JumpF disabled _label46                                  ;@line 56
						PropGet OPTION_FLAG_DISABLED self ::temp6                ;@line 57
						Return ::temp6                                           ;@line 57
						Jump _label47                                            ;@line 57
					_label46:
						PropGet OPTION_FLAG_NONE self ::temp7                    ;@line 59
						Return ::temp7                                           ;@line 59
					_label47:
					.endCode
				.endFunction
				.function ecImport
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param fileName String
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp81 Bool
						.local ::temp82 form
						.local ::temp83 String[]
						.local ::temp84 Int
						.local ::temp85 Bool
						.local storeKeys String[]
						.local i Int
						.local missing Int
						.local ::temp86 String
						.local ::temp87 Bool
						.local storeKey String
						.local type Int
						.local ::temp88 Bool
						.local ::temp89 Bool
						.local ::temp90 Float
						.local error String
					.endLocalTable
					.code
						CallMethod ecFlagsUpdate self ::NoneVar True             ;@line 320
						CallMethod ecTextUpdate self ::NoneVar ""                ;@line 321
						CallStatic jsonutil Load ::temp81 fileName               ;@line 322
						CallStatic jsonutil IsGood ::temp81 fileName             ;@line 324
						JumpF ::temp81 _label48                                  ;@line 324
						CallMethod ecTextUpdate self ::NoneVar "2131231231231"   ;@line 325
						Cast ::temp82 self                                       ;@line 326
						CallStatic storageutil StringListToArray ::temp83 ::temp82 ecRegistry  ;@line 326
						Assign storeKeys ::temp83                                ;@line 326
						ArrayLength ::temp84 storeKeys                           ;@line 327
						Assign i ::temp84                                        ;@line 327
						Assign missing 0                                         ;@line 328
					_label60:
						CompareGT ::temp85 i 0                                   ;@line 329
						JumpF ::temp85 _label49                                  ;@line 329
						ISubtract ::temp84 i 1                                   ;@line 330
						Assign i ::temp84                                        ;@line 330
						ArrayGetElement ::temp86 storeKeys i                     ;@line 331
						Assign storeKey ::temp86                                 ;@line 331
						StrCat ::temp86 ecPrefixType storeKey                    ;@line 332
						Cast ::temp82 self                                       ;@line 332
						CallStatic storageutil GetIntValue ::temp84 ::temp82 ::temp86 0  ;@line 332
						Assign type ::temp84                                     ;@line 332
						CompareEQ ::temp87 type ecTypeBool                       ;@line 333
						JumpF ::temp87 _label50                                  ;@line 333
						CallStatic jsonutil HasIntValue ::temp88 fileName storeKey  ;@line 334
						JumpF ::temp88 _label51                                  ;@line 334
						CallStatic jsonutil GetIntValue ::temp84 fileName storeKey 0  ;@line 335
						Cast ::temp82 self                                       ;@line 335
						CallStatic storageutil SetIntValue ::temp84 ::temp82 storeKey ::temp84  ;@line 335
						Jump _label52                                            ;@line 335
					_label51:
						Assign type -1                                           ;@line 337
					_label52:
						Jump _label53                                            ;@line 337
					_label50:
						CompareEQ ::temp88 type ecTypeSlider                     ;@line 339
						JumpF ::temp88 _label54                                  ;@line 339
						CallStatic jsonutil HasFloatValue ::temp89 fileName storeKey  ;@line 340
						JumpF ::temp89 _label55                                  ;@line 340
						CallStatic jsonutil GetFloatValue ::temp90 fileName storeKey 0.000000  ;@line 341
						Cast ::temp82 self                                       ;@line 341
						CallStatic storageutil SetFloatValue ::temp90 ::temp82 storeKey ::temp90  ;@line 341
						Jump _label56                                            ;@line 341
					_label55:
						Assign type -1                                           ;@line 343
					_label56:
						Jump _label53                                            ;@line 343
					_label54:
						CompareEQ ::temp89 type ecTypeText                       ;@line 345
						JumpF ::temp89 _label53                                  ;@line 345
						CallStatic jsonutil HasStringValue ::temp88 fileName storeKey  ;@line 346
						JumpF ::temp88 _label57                                  ;@line 346
						CallStatic jsonutil GetStringValue ::temp86 fileName storeKey ""  ;@line 347
						Cast ::temp82 self                                       ;@line 347
						CallStatic storageutil SetStringValue ::temp86 ::temp82 storeKey ::temp86  ;@line 347
						Jump _label58                                            ;@line 347
					_label57:
						Assign type -1                                           ;@line 349
					_label58:
						Jump _label53                                            ;@line 349
					_label53:
						CompareEQ ::temp88 type -1                               ;@line 352
						JumpF ::temp88 _label59                                  ;@line 352
						IAdd ::temp84 missing 1                                  ;@line 353
						Assign missing ::temp84                                  ;@line 353
						StrCat ::temp86 "213123123123123" storeKey               ;@line 354
						CallMethod Log self ::NoneVar ::temp86 1 False           ;@line 354
						Jump _label59                                            ;@line 354
					_label59:
						Jump _label60                                            ;@line 354
					_label49:
						CallStatic jsonutil Unload ::temp89 fileName False False  ;@line 357
						CallMethod ecTextUpdate self ::NoneVar ""                ;@line 358
						CompareGT ::temp87 missing 0                             ;@line 359
						JumpF ::temp87 _label61                                  ;@line 359
						Cast ::temp86 missing                                    ;@line 360
						StrCat ::temp86 "WEQE4" ::temp86                         ;@line 360
						StrCat ::temp86 ::temp86 ") were missing and were skipped.\nMaybe the setting imported are from and older version?\nMore information were written to the console."  ;@line 360
						CallMethod ShowMessage self ::temp88 ::temp86 False "Dismiss" "$Cancel"  ;@line 360
						Jump _label61                                            ;@line 360
					_label61:
						CallMethod ecConfigChanged self ::NoneVar                ;@line 362
						Jump _label62                                            ;@line 362
					_label48:
						CallStatic jsonutil GetErrors ::temp86 fileName          ;@line 364
						Assign error ::temp86                                    ;@line 364
						StrCat ::temp86 "WWW3" fileName                          ;@line 365
						StrCat ::temp86 ::temp86 "\n"                            ;@line 365
						StrCat ::temp86 ::temp86 error                           ;@line 365
						CallMethod Log self ::NoneVar ::temp86 1 False           ;@line 365
						CallMethod ecTextUpdate self ::NoneVar "WEQE4"           ;@line 366
						CallMethod ShowMessage self ::temp85 "WEQ1" False "Dismiss" "$Cancel"  ;@line 367
					_label62:
						CallMethod ecFlagsUpdate self ::NoneVar False            ;@line 369
					.endCode
				.endFunction
				.function OnSliderOpenST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp55 String
						.local ::temp56 Bool
						.local ::temp57 form
						.local ::temp58 Int
						.local storeKey String
						.local type Int
						.local ::temp59 Float
						.local ::NoneVar None
						.local default Float
						.local value Float
						.local min Float
						.local max Float
						.local step Float
					.endLocalTable
					.code
						CallMethod ecKey self ::temp55 "" 0                      ;@line 233
						Assign storeKey ::temp55                                 ;@line 233
						Not ::temp56 storeKey                                    ;@line 234
						JumpF ::temp56 _label63                                  ;@line 234
						Return None                                              ;@line 235
						Jump _label63                                            ;@line 235
					_label63:
						StrCat ::temp55 ecPrefixType storeKey                    ;@line 237
						Cast ::temp57 self                                       ;@line 237
						CallStatic storageutil GetIntValue ::temp58 ::temp57 ::temp55 0  ;@line 237
						Assign type ::temp58                                     ;@line 237
						CompareEQ ::temp56 type ecTypeSlider                     ;@line 238
						JumpF ::temp56 _label64                                  ;@line 238
						StrCat ::temp55 ecPrefixDefault storeKey                 ;@line 239
						Cast ::temp57 self                                       ;@line 239
						CallStatic storageutil GetFloatValue ::temp59 ::temp57 ::temp55 0.000000  ;@line 239
						Assign default ::temp59                                  ;@line 239
						Cast ::temp57 self                                       ;@line 240
						CallStatic storageutil GetFloatValue ::temp59 ::temp57 storeKey default  ;@line 240
						Assign value ::temp59                                    ;@line 240
						StrCat ::temp55 ecPrefixMin storeKey                     ;@line 241
						Cast ::temp57 self                                       ;@line 241
						CallStatic storageutil GetFloatValue ::temp59 ::temp57 ::temp55 0.000000  ;@line 241
						Assign min ::temp59                                      ;@line 241
						StrCat ::temp55 ecPrefixMax storeKey                     ;@line 242
						Cast ::temp57 self                                       ;@line 242
						CallStatic storageutil GetFloatValue ::temp59 ::temp57 ::temp55 0.000000  ;@line 242
						Assign max ::temp59                                      ;@line 242
						StrCat ::temp55 ecPrefixStep storeKey                    ;@line 243
						Cast ::temp57 self                                       ;@line 243
						CallStatic storageutil GetFloatValue ::temp59 ::temp57 ::temp55 0.000000  ;@line 243
						Assign step ::temp59                                     ;@line 243
						CallMethod ecSliderHook self ::NoneVar value default min max step  ;@line 244
						Jump _label64                                            ;@line 244
					_label64:
					.endCode
				.endFunction
				.function OnDefaultST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp44 String
						.local ::temp45 Bool
						.local ::temp46 form
						.local ::temp47 Int
						.local storeKey String
						.local type Int
						.local ::temp48 Bool
						.local ::NoneVar None
						.local default Bool
						.local ::temp49 Float
						.local ::mangled_default_0 Float
					.endLocalTable
					.code
						CallMethod ecKey self ::temp44 "" 0                      ;@line 198
						Assign storeKey ::temp44                                 ;@line 198
						Not ::temp45 storeKey                                    ;@line 199
						JumpF ::temp45 _label65                                  ;@line 199
						Return None                                              ;@line 200
						Jump _label65                                            ;@line 200
					_label65:
						StrCat ::temp44 ecPrefixType storeKey                    ;@line 202
						Cast ::temp46 self                                       ;@line 202
						CallStatic storageutil GetIntValue ::temp47 ::temp46 ::temp44 0  ;@line 202
						Assign type ::temp47                                     ;@line 202
						CompareEQ ::temp45 type ecTypeBool                       ;@line 203
						JumpF ::temp45 _label66                                  ;@line 203
						StrCat ::temp44 ecPrefixDefault storeKey                 ;@line 204
						Cast ::temp46 self                                       ;@line 204
						CallStatic storageutil GetIntValue ::temp47 ::temp46 ::temp44 0  ;@line 204
						Cast ::temp48 ::temp47                                   ;@line 204
						Assign default ::temp48                                  ;@line 204
						Cast ::temp47 default                                    ;@line 205
						Cast ::temp46 self                                       ;@line 205
						CallStatic storageutil SetIntValue ::temp47 ::temp46 storeKey ::temp47  ;@line 205
						CallMethod SetToggleOptionValueST self ::NoneVar default False ""  ;@line 206
						CallMethod ecConfigChanged self ::NoneVar                ;@line 207
						Jump _label67                                            ;@line 207
					_label66:
						CompareEQ ::temp48 type ecTypeSlider                     ;@line 208
						JumpF ::temp48 _label67                                  ;@line 208
						StrCat ::temp44 ecPrefixDefault storeKey                 ;@line 209
						Cast ::temp46 self                                       ;@line 209
						CallStatic storageutil GetFloatValue ::temp49 ::temp46 ::temp44 0.000000  ;@line 209
						Assign ::mangled_default_0 ::temp49                      ;@line 209
						Cast ::temp46 self                                       ;@line 210
						CallStatic storageutil GetFloatValue ::temp49 ::temp46 storeKey ::mangled_default_0  ;@line 210
						CallMethod SetSliderOptionValueST self ::NoneVar ::mangled_default_0 "2312312" False ""  ;@line 211
						CallMethod ecConfigChanged self ::NoneVar                ;@line 212
						Jump _label67                                            ;@line 212
					_label67:
					.endCode
				.endFunction
				.function ecCursor
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param position Int
					.endParamTable
					.localTable
						.local ::temp11 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						Not ::temp11 ::ecBurnIn_var                              ;@line 83
						JumpF ::temp11 _label68                                  ;@line 83
						CallMethod SetCursorPosition self ::NoneVar position     ;@line 84
						Jump _label68                                            ;@line 84
					_label68:
					.endCode
				.endFunction
				.function OnHighlightST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp42 String
						.local storeKey String
						.local ::temp43 form
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ecKey self ::temp42 "" 0                      ;@line 192
						Assign storeKey ::temp42                                 ;@line 192
						JumpF storeKey _label69                                  ;@line 193
						StrCat ::temp42 ecPrefixDesc storeKey                    ;@line 194
						Cast ::temp43 self                                       ;@line 194
						CallStatic storageutil GetStringValue ::temp42 ::temp43 ::temp42 ""  ;@line 194
						CallMethod SetInfoText self ::NoneVar ::temp42           ;@line 194
						Jump _label69                                            ;@line 194
					_label69:
					.endCode
				.endFunction
				.function ecGetBool
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp33 Int
						.local ::temp34 Bool
					.endLocalTable
					.code
						CallMethod ecGetInt self ::temp33 storeKey               ;@line 169
						Cast ::temp34 ::temp33                                   ;@line 169
						Return ::temp34                                          ;@line 169
					.endCode
				.endFunction
				.function ecGetInt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Int
					.paramTable
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp35 form
						.local ::temp36 Int
					.endLocalTable
					.code
						Cast ::temp35 self                                       ;@line 172
						CallStatic storageutil GetIntValue ::temp36 ::temp35 storeKey 0  ;@line 172
						Return ::temp36                                          ;@line 172
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
						Return min                                               ;@line 253
					.endCode
				.endFunction
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp50 String
						.local ::temp51 Bool
						.local ::temp52 form
						.local ::temp53 Int
						.local storeKey String
						.local type Int
						.local ::temp54 Bool
						.local ::NoneVar None
						.local default Bool
						.local value Bool
					.endLocalTable
					.code
						CallMethod ecKey self ::temp50 "" 0                      ;@line 217
						Assign storeKey ::temp50                                 ;@line 217
						Not ::temp51 storeKey                                    ;@line 218
						JumpF ::temp51 _label70                                  ;@line 218
						Return None                                              ;@line 219
						Jump _label70                                            ;@line 219
					_label70:
						StrCat ::temp50 ecPrefixType storeKey                    ;@line 221
						Cast ::temp52 self                                       ;@line 221
						CallStatic storageutil GetIntValue ::temp53 ::temp52 ::temp50 0  ;@line 221
						Assign type ::temp53                                     ;@line 221
						CompareEQ ::temp51 type ecTypeBool                       ;@line 222
						JumpF ::temp51 _label71                                  ;@line 222
						StrCat ::temp50 ecPrefixDefault storeKey                 ;@line 223
						Cast ::temp52 self                                       ;@line 223
						CallStatic storageutil GetIntValue ::temp53 ::temp52 ::temp50 0  ;@line 223
						Cast ::temp54 ::temp53                                   ;@line 223
						Assign default ::temp54                                  ;@line 223
						Cast ::temp53 default                                    ;@line 224
						Cast ::temp52 self                                       ;@line 224
						CallStatic storageutil GetIntValue ::temp53 ::temp52 storeKey ::temp53  ;@line 224
						Cast ::temp54 ::temp53                                   ;@line 224
						Assign value ::temp54                                    ;@line 224
						Not ::temp54 value                                       ;@line 225
						Assign value ::temp54                                    ;@line 225
						Cast ::temp53 value                                      ;@line 226
						Cast ::temp52 self                                       ;@line 226
						CallStatic storageutil SetIntValue ::temp53 ::temp52 storeKey ::temp53  ;@line 226
						CallMethod SetToggleOptionValueST self ::NoneVar value False ""  ;@line 227
						CallMethod ecConfigChanged self ::NoneVar                ;@line 228
						Jump _label71                                            ;@line 228
					_label71:
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
				.function ecCheckPage
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp106 Bool
					.endLocalTable
					.code
						CallMethod ecFillMode self ::NoneVar False True          ;@line 421
						CallMethod ecHeader self ::NoneVar "Requirements" False  ;@line 422
						CallMethod ecEmpty self ::NoneVar 3                      ;@line 423
						Assign ecCheckRender True                                ;@line 424
						CallMethod ecCheck self ::temp106                        ;@line 425
						Assign ecCheckRender False                               ;@line 426
					.endCode
				.endFunction
				.function ecText
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param storeKey String
						.param label String
						.param default String
						.param desc String
						.param disabled Bool
						.param update Bool
						.param saved Bool
					.endParamTable
					.localTable
						.local ::temp28 Bool
						.local ::temp29 String
						.local ::temp30 form
						.local ::temp31 Bool
						.local value String
						.local ::temp32 Int
						.local ::NoneVar None
					.endLocalTable
					.code
						Assign value default                                     ;@line 149
						Cast ::temp28 update                                     ;@line 150
						JumpT ::temp28 _label72                                  ;@line 150
						Cast ::temp28 ::ecUpdate_var                             ;@line 150
					_label72:
						Cast ::temp28 ::temp28                                   ;@line 150
						JumpT ::temp28 _label73                                  ;@line 150
						StrCat ::temp29 ecPrefixType storeKey                    ;@line 150
						Cast ::temp30 self                                       ;@line 150
						CallStatic storageutil HasIntValue ::temp31 ::temp30 ::temp29  ;@line 150
						Not ::temp31 ::temp31                                    ;@line 150
						Cast ::temp28 ::temp31                                   ;@line 150
					_label73:
						JumpF ::temp28 _label74                                  ;@line 150
						JumpF saved _label75                                     ;@line 151
						Cast ::temp30 self                                       ;@line 152
						CallStatic storageutil StringListAdd ::temp32 ::temp30 ecRegistry storeKey False  ;@line 152
						Jump _label75                                            ;@line 152
					_label75:
						StrCat ::temp29 ecPrefixType storeKey                    ;@line 154
						Cast ::temp30 self                                       ;@line 154
						CallStatic storageutil SetIntValue ::temp32 ::temp30 ::temp29 ecTypeText  ;@line 154
						StrCat ::temp29 ecPrefixDesc storeKey                    ;@line 155
						Cast ::temp30 self                                       ;@line 155
						CallStatic storageutil SetStringValue ::temp29 ::temp30 ::temp29 desc  ;@line 155
						Jump _label74                                            ;@line 155
					_label74:
						StrCat ::temp29 ecPrefixDefault storeKey                 ;@line 157
						Cast ::temp30 self                                       ;@line 157
						CallStatic storageutil SetStringValue ::temp29 ::temp30 ::temp29 default  ;@line 157
						JumpF saved _label76                                     ;@line 158
						Cast ::temp30 self                                       ;@line 159
						CallStatic storageutil GetStringValue ::temp29 ::temp30 storeKey default  ;@line 159
						Assign value ::temp29                                    ;@line 159
						Cast ::temp30 self                                       ;@line 160
						CallStatic storageutil SetStringValue ::temp29 ::temp30 storeKey value  ;@line 160
						Jump _label76                                            ;@line 160
					_label76:
						Not ::temp31 ::ecBurnIn_var                              ;@line 162
						JumpF ::temp31 _label77                                  ;@line 162
						StrCat ::temp29 ecState storeKey                         ;@line 163
						CallMethod ecFlags self ::temp32 disabled                ;@line 163
						CallMethod AddTextOptionST self ::NoneVar ::temp29 label value ::temp32  ;@line 163
						Jump _label77                                            ;@line 163
					_label77:
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
				.function ecFlagsUpdate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param disabled Bool
					.endParamTable
					.localTable
						.local ::temp72 Int
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod ecFlags self ::temp72 disabled                ;@line 291
						CallMethod SetOptionFlagsST self ::NoneVar ::temp72 False ""  ;@line 291
					.endCode
				.endFunction
				.function ecEmpty
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param count Int
					.endParamTable
					.localTable
						.local ::temp12 Bool
						.local ::temp13 Bool
						.local ::temp14 Int
					.endLocalTable
					.code
						Not ::temp12 ::ecBurnIn_var                              ;@line 89
						JumpF ::temp12 _label78                                  ;@line 89
					_label80:
						CompareGT ::temp13 count 0                               ;@line 90
						JumpF ::temp13 _label79                                  ;@line 90
						ISubtract ::temp14 count 1                               ;@line 91
						Assign count ::temp14                                    ;@line 91
						CallMethod AddEmptyOption self ::temp14                  ;@line 92
						Jump _label80                                            ;@line 92
					_label79:
						Jump _label78                                            ;@line 92
					_label78:
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
					.endLocalTable
					.code
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
						Return max                                               ;@line 256
					.endCode
				.endFunction
				.function ecSliderUpdate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param newValue Float
						.param storeKey String
					.endParamTable
					.localTable
						.local ::temp69 Bool
						.local ::temp71 form
						.local ::NoneVar None
						.local ::temp70 String
					.endLocalTable
					.code
						Not ::temp69 storeKey                                    ;@line 277
						JumpF ::temp69 _label81                                  ;@line 277
						CallMethod ecKey self ::temp70 "" 0                      ;@line 278
						Assign storeKey ::temp70                                 ;@line 278
						Jump _label81                                            ;@line 278
					_label81:
						Not ::temp69 storeKey                                    ;@line 280
						JumpF ::temp69 _label82                                  ;@line 280
						Return None                                              ;@line 281
						Jump _label82                                            ;@line 281
					_label82:
						StrCat ::temp70 ecPrefixFormat storeKey                  ;@line 283
						Cast ::temp71 self                                       ;@line 283
						CallStatic storageutil GetStringValue ::temp70 ::temp71 ::temp70 ""  ;@line 283
						CallMethod SetSliderOptionValueST self ::NoneVar newValue ::temp70 False ""  ;@line 283
					.endCode
				.endFunction
				.function ecKey
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String
					.paramTable
						.param S String
						.param extraOff Int
					.endParamTable
					.localTable
						.local ::temp39 Bool
						.local ::temp41 Int
						.local ::temp40 String
						.local ecStateLength Int
					.endLocalTable
					.code
						Not ::temp39 S                                           ;@line 180
						JumpF ::temp39 _label83                                  ;@line 180
						CallMethod GetState self ::temp40                        ;@line 181
						Assign S ::temp40                                        ;@line 181
						Jump _label83                                            ;@line 181
					_label83:
						CallStatic stringutil GetLength ::temp41 ecState         ;@line 183
						Assign ecStateLength ::temp41                            ;@line 183
						CallStatic stringutil Substring ::temp40 S 0 ecStateLength  ;@line 184
						CompareEQ ::temp39 ::temp40 ecState                      ;@line 184
						JumpF ::temp39 _label84                                  ;@line 184
						IAdd ::temp41 ecStateLength extraOff                     ;@line 185
						CallStatic stringutil Substring ::temp40 S ::temp41 0    ;@line 185
						Return ::temp40                                          ;@line 185
						Jump _label85                                            ;@line 185
					_label84:
						Return ""                                                ;@line 187
					_label85:
					.endCode
				.endFunction
			.endState
		.endStateTable
	.endObject
.endObjectTable
