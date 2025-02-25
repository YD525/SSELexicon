.info
	.source "BetterThirdPersonSelection_MCM.psc"
	.modifyTime 1649027878 ;Mon Apr 04 07:17:58 2022 Local
	.compileTime 1649027880 ;Mon Apr 04 07:18:00 2022 Local
	.user "Shrim"
	.computer "PC-DANIEL"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object BetterThirdPersonSelection_MCM MCM_ConfigBase
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
			.variable ::filterObjects_var Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue -1
			.endVariable
			.variable ::filterObjectDescriptions_var Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue -1
			.endVariable
		.endVariableTable
		.propertyTable
			.property filterObjects Int auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::filterObjects_var
			.endProperty
			.property filterObjectDescriptions Int auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::filterObjectDescriptions_var
			.endProperty
		.endPropertyTable
		.stateTable
			.state 
				.function AddFilter
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param a_filter String
						.param a_description String
						.param isEnabled Bool
					.endParamTable
					.localTable
						.local ::temp15 Int
						.local ::NoneVar None
						.local innerObj Int
						.local innerDescObj Int
					.endLocalTable
					.code
						CallStatic jmap object ::temp15                          ;@line 102
						Assign innerObj ::temp15                                 ;@line 102
						Cast ::temp15 isEnabled                                  ;@line 103
						CallStatic jmap setInt ::NoneVar innerObj "isEnabled" ::temp15  ;@line 103
						CallStatic jmap setObj ::NoneVar ::filterObjects_var a_filter innerObj  ;@line 105
						CallStatic jmap object ::temp15                          ;@line 107
						Assign innerDescObj ::temp15                             ;@line 107
						CallStatic jmap setStr ::NoneVar innerDescObj "description" a_description  ;@line 108
						CallStatic jmap setObj ::NoneVar ::filterObjectDescriptions_var a_filter innerDescObj  ;@line 110
					.endCode
				.endFunction
				.function SetFilterState
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param a_filter String
						.param isEnabled Bool
					.endParamTable
					.localTable
						.local ::temp16 Int
						.local ::NoneVar None
						.local filterObj Int
					.endLocalTable
					.code
						CallStatic jmap getObj ::temp16 ::filterObjects_var a_filter 0  ;@line 116
						Assign filterObj ::temp16                                ;@line 116
						Cast ::temp16 isEnabled                                  ;@line 117
						CallStatic jmap setInt ::NoneVar filterObj "isEnabled" ::temp16  ;@line 117
					.endCode
				.endFunction
				.function BuildCustomFiltersPage
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp8 Int
						.local ::NoneVar None
						.local ::temp9 Bool
						.local i Int
					.endLocalTable
					.code
						PropGet TOP_TO_BOTTOM self ::temp8                       ;@line 62
						CallMethod SetCursorFillMode self ::NoneVar ::temp8      ;@line 62
						CallMethod SetCursorPosition self ::NoneVar 0            ;@line 63
						CallMethod AddHeaderOption self ::temp8 "$BetterThirdPersonSelection_FilterSettings_HeaderText" 0  ;@line 65
						CallMethod AddTextOption self ::temp8 "" "$BetterThirdPersonSelection_Filter_InfoText" 0  ;@line 66
						CallStatic jmap count ::temp8 ::filterObjects_var        ;@line 70
						ISubtract ::temp8 ::temp8 1                              ;@line 70
						Assign i ::temp8                                         ;@line 70
					_label1:
						CompareGTE ::temp9 i 0                                   ;@line 71
						JumpF ::temp9 _label0                                    ;@line 71
						CallMethod constructFilter self ::NoneVar i              ;@line 72
						ISubtract ::temp8 i 1                                    ;@line 74
						Assign i ::temp8                                         ;@line 74
						Jump _label1                                             ;@line 74
					_label0:
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
				.function OnSelectST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp1 String
						.local ::temp2 String[]
						.local ::temp3 Int
						.local ::temp4 Bool
						.local splitString String[]
						.local filterName String
						.local filterObj Int
						.local isEnabled Bool
					.endLocalTable
					.code
						CallParent OnSelectST ::NoneVar                          ;@line 31
						CallMethod GetState self ::temp1                         ;@line 33
						CallStatic stringutil Split ::temp2 ::temp1 "___"        ;@line 33
						Assign splitString ::temp2                               ;@line 33
						ArrayGetElement ::temp1 splitString 1                    ;@line 34
						Assign filterName ::temp1                                ;@line 34
						CallStatic jmap getObj ::temp3 ::filterObjects_var filterName 0  ;@line 35
						Assign filterObj ::temp3                                 ;@line 35
						CallStatic jmap getInt ::temp3 filterObj "3" 0           ;@line 37
						Cast ::temp4 ::temp3                                     ;@line 37
						Assign isEnabled ::temp4                                 ;@line 37
						Not ::temp4 isEnabled                                    ;@line 38
						Cast ::temp3 ::temp4                                     ;@line 38
						CallStatic jmap setInt ::NoneVar filterObj "isEnabled" ::temp3  ;@line 38
						Not ::temp4 isEnabled                                    ;@line 40
						CallMethod GetState self ::temp1                         ;@line 40
						CallMethod SetToggleOptionValueST self ::NoneVar ::temp4 False ::temp1  ;@line 40
					.endCode
				.endFunction
				.function OnHighlightST
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp5 String
						.local ::temp6 String[]
						.local ::temp7 Int
						.local ::NoneVar None
						.local splitString String[]
						.local filterName String
						.local filterDescObj Int
						.local description String
					.endLocalTable
					.code
						CallMethod GetState self ::temp5                         ;@line 45
						CallStatic stringutil Split ::temp6 ::temp5 "___"        ;@line 45
						Assign splitString ::temp6                               ;@line 45
						ArrayGetElement ::temp5 splitString 1                    ;@line 46
						Assign filterName ::temp5                                ;@line 46
						CallStatic jmap getObj ::temp7 ::filterObjectDescriptions_var filterName 0  ;@line 48
						Assign filterDescObj ::temp7                             ;@line 48
						CallStatic jmap getStr ::temp5 filterDescObj "description" "-"  ;@line 49
						Assign description ::temp5                               ;@line 49
						CallMethod SetInfoText self ::NoneVar description        ;@line 51
					.endCode
				.endFunction
				.function OnPageReset
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param a_page String
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp0 Bool
					.endLocalTable
					.code
						CallParent OnPageReset ::NoneVar a_page                  ;@line 20
						CompareEQ ::temp0 a_page "$BetterThirdPersonSelection_FilterPage"  ;@line 22
						JumpF ::temp0 _label2                                    ;@line 22
						CallMethod OnPageReset_CLib self ::NoneVar               ;@line 24
						CallMethod BuildCustomFiltersPage self ::NoneVar         ;@line 25
						Jump _label2                                             ;@line 25
					_label2:
					.endCode
				.endFunction
				.function WriteFilterStatesToFile
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallStatic jvalue writeToFile ::NoneVar ::filterObjects_var "Data/MCM/Settings/FilterStates.json"  ;@line 122
					.endCode
				.endFunction
				.function OnConfigOpen native
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
				.endFunction
				.function OnConfigClose_CLib
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
				.function OnConfigClose
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallStatic jvalue cleanPool ::NoneVar "2"                ;@line 10
						CallParent OnConfigClose ::NoneVar                       ;@line 12
						CallMethod WriteFilterStatesToFile self ::NoneVar        ;@line 14
						CallMethod OnConfigClose_CLib self ::NoneVar             ;@line 15
					.endCode
				.endFunction
				.function constructFilter
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param filterIdx Int
					.endParamTable
					.localTable
						.local ::temp10 String
						.local ::temp11 Int
						.local ::temp12 Bool
						.local ::temp13 String
						.local ::NoneVar None
						.local currKey String
						.local currFilter Int
						.local isEnabled Bool
					.endLocalTable
					.code
						CallStatic jmap getNthKey ::temp10 ::filterObjects_var filterIdx  ;@line 80
						Assign currKey ::temp10                                  ;@line 80
						CallStatic jmap getObj ::temp11 ::filterObjects_var currKey 0  ;@line 81
						Assign currFilter ::temp11                               ;@line 81
						CallStatic jmap getInt ::temp11 currFilter "isEnabled" 0  ;@line 82
						Cast ::temp12 ::temp11                                   ;@line 82
						Assign isEnabled ::temp12                                ;@line 82
						StrCat ::temp10 "toggleOption___" currKey                ;@line 84
						StrCat ::temp13 "." currKey                              ;@line 84
						StrCat ::temp13 ::temp13 ".isEnabled"                    ;@line 84
						CallStatic jvalue SolveInt ::temp11 ::filterObjects_var ::temp13 0  ;@line 84
						Cast ::temp12 ::temp11                                   ;@line 84
						CallMethod AddToggleOptionST self ::NoneVar ::temp10 currKey ::temp12 0  ;@line 84
					.endCode
				.endFunction
				.function ClearFilters
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp14 Int
					.endLocalTable
					.code
						CallStatic jvalue cleanPool ::NoneVar "BTPS_Filters"     ;@line 89
						CallStatic jmap object ::temp14                          ;@line 91
						CallStatic jvalue addToPool ::temp14 ::temp14 "BTPS_Filters"  ;@line 91
						Assign ::filterObjects_var ::temp14                      ;@line 91
						CallStatic jmap object ::temp14                          ;@line 92
						CallStatic jvalue addToPool ::temp14 ::temp14 "BTPS_Filters"  ;@line 92
						Assign ::filterObjectDescriptions_var ::temp14           ;@line 92
						CallStatic jvalue retain ::temp14 ::filterObjects_var ""  ;@line 94
						CallStatic jvalue retain ::temp14 ::filterObjectDescriptions_var ""  ;@line 95
					.endCode
				.endFunction
				.function OnPageReset_CLib
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
			.endState
		.endStateTable
	.endObject
.endObjectTable
