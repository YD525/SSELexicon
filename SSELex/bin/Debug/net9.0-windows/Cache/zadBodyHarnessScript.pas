.info
	.source "onhpmyofqsckosmtwapssbwh"
	.modifyTime 1707083026 ;Mon Feb 05 05:43:46 2024 Local
	.compileTime 1707083549 ;Mon Feb 05 05:52:29 2024 Local
	.user "ytggyhtiwgjrq"
	.computer "ILHLPZTJLWVQLLE"
.endInfo
.userFlagsRef
	.flag conditional 1	; 0x00000001
	.flag hidden 0	; 0x00000000
.endUserFlagsRef
.objectTable
	.object zadBodyHarnessScript zadEquipScript
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
		.endVariableTable
		.propertyTable
		.endPropertyTable
		.stateTable
			.state 
				.function OnEquippedPre
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
						.param silent Bool
					.endParamTable
					.localTable
						.local ::temp0 Bool
						.local ::temp1 actor
						.local ::temp2 Bool
						.local ::NoneVar None
						.local ::temp3 String
					.endLocalTable
					.code
						Not ::temp0 silent                                       ;@line 4
						JumpF ::temp0 _label0                                    ;@line 4
						PropGet PlayerRef ::libs_var ::temp1                     ;@line 5
						CompareEQ ::temp2 akActor ::temp1                        ;@line 5
						JumpF ::temp2 _label1                                    ;@line 5
						CallMethod NotifyActor ::libs_var ::NoneVar "1212" akActor True  ;@line 6
						Jump _label2                                             ;@line 6
					_label1:
						CallMethod GetMessageName self ::temp3 akActor           ;@line 8
						StrCat ::temp3 ::temp3 "1212"                            ;@line 8
						CallMethod NotifyActor ::libs_var ::NoneVar ::temp3 akActor True  ;@line 8
					_label2:
						Jump _label0                                             ;@line 8
					_label0:
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
				.function OnEquippedPost
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod Log ::libs_var ::NoneVar "RestraintScript OnEquippedPost BodyHarness" 0  ;@line 43
					.endCode
				.endFunction
				.function OnEquippedFilter
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Int
					.paramTable
						.param akActor actor
						.param silent Bool
					.endParamTable
					.localTable
						.local ::temp4 actor
						.local ::temp5 Bool
						.local ::temp7 form
						.local ::temp6 Bool
						.local ::temp8 Bool
						.local ::temp9 keyword
						.local ::NoneVar None
					.endLocalTable
					.code
						Cast ::temp4 None                                        ;@line 15
						CompareEQ ::temp5 akActor ::temp4                        ;@line 15
						JumpF ::temp5 _label3                                    ;@line 15
						PropGet PlayerRef ::libs_var ::temp4                     ;@line 16
						CompareEQ ::temp6 akActor ::temp4                        ;@line 16
						Jump _label3                                             ;@line 16
					_label3:
						Cast ::temp7 ::deviceRendered_var                        ;@line 18
						CallMethod IsEquipped akActor ::temp6 ::temp7            ;@line 18
						Not ::temp5 ::temp6                                      ;@line 18
						JumpF ::temp5 _label4                                    ;@line 18
						PropGet PlayerRef ::libs_var ::temp4                     ;@line 19
						CompareEQ ::temp6 akActor ::temp4                        ;@line 19
						Not ::temp6 ::temp6                                      ;@line 19
						Cast ::temp6 ::temp6                                     ;@line 19
						JumpF ::temp6 _label5                                    ;@line 19
						CallMethod ShouldEquipSilently self ::temp8 akActor      ;@line 19
						Cast ::temp6 ::temp8                                     ;@line 19
					_label5:
						JumpF ::temp6 _label6                                    ;@line 19
						CallMethod Log ::libs_var ::NoneVar "1212" 0             ;@line 20
						Return 0                                                 ;@line 21
						Jump _label6                                             ;@line 21
					_label6:
						PropGet zad_DeviousCorset ::libs_var ::temp9             ;@line 23
						CallMethod WornHasKeyword akActor ::temp8 ::temp9        ;@line 23
						JumpF ::temp8 _label7                                    ;@line 23
						CallMethod MultipleItemFailMessage self ::NoneVar "Corset"  ;@line 24
						Return 2                                                 ;@line 25
						Jump _label7                                             ;@line 25
					_label7:
						PropGet zad_DeviousCollar ::libs_var ::temp9             ;@line 28
						CallMethod WornHasKeyword akActor ::temp6 ::temp9        ;@line 28
						Cast ::temp6 ::temp6                                     ;@line 28
						JumpF ::temp6 _label8                                    ;@line 28
						PropGet zad_DeviousCollar ::libs_var ::temp9             ;@line 28
						CallMethod HasKeyword ::deviceRendered_var ::temp8 ::temp9  ;@line 28
						Cast ::temp6 ::temp8                                     ;@line 28
					_label8:
						JumpF ::temp6 _label9                                    ;@line 28
						CallMethod MultipleItemFailMessage self ::NoneVar "Collar"  ;@line 29
						Return 2                                                 ;@line 30
						Jump _label9                                             ;@line 30
					_label9:
						PropGet zad_DeviousBelt ::libs_var ::temp9               ;@line 33
						CallMethod WornHasKeyword akActor ::temp8 ::temp9        ;@line 33
						Cast ::temp8 ::temp8                                     ;@line 33
						JumpF ::temp8 _label10                                   ;@line 33
						PropGet zad_DeviousBelt ::libs_var ::temp9               ;@line 33
						CallMethod HasKeyword ::deviceRendered_var ::temp6 ::temp9  ;@line 33
						Cast ::temp8 ::temp6                                     ;@line 33
					_label10:
						JumpF ::temp8 _label11                                   ;@line 33
						CallMethod MultipleItemFailMessage self ::NoneVar "Belt"  ;@line 34
						Return 2                                                 ;@line 35
						Jump _label11                                            ;@line 35
					_label11:
						Jump _label4                                             ;@line 35
					_label4:
						Return 0                                                 ;@line 38
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
			.endState
		.endStateTable
	.endObject
.endObjectTable
