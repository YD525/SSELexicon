.info
	.source "fkclvsyogxbkhenf"
	.modifyTime 1525985453 ;Fri May 11 04:50:53 2018 Local
	.compileTime 1597964121 ;Fri Aug 21 06:55:21 2020 Local
	.user "huxqs"
	.computer "TXFQNQIR"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object zadBraEffect ActiveMagicEffect
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
			.variable ::Terminate_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::target_var actor
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::Libs_var zadlibs
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
		.endVariableTable
		.propertyTable
			.property Terminate Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Terminate_var
			.endProperty
			.property Libs zadlibs auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Libs_var
			.endProperty
			.property target actor auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::target_var
			.endProperty
		.endPropertyTable
		.stateTable
			.state 
				.function OnCellDetach
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod DoUnregister self ::NoneVar                   ;@line 66
					.endCode
				.endFunction
				.function OnEffectStart
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akTarget actor
						.param akCaster actor
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp8 keyword
						.local ::temp9 Bool
						.local ::temp10 form
						.local ::temp11 Bool
						.local ::temp12 Bool
					.endLocalTable
					.code
						CallMethod Log ::Libs_var ::NoneVar "weqeqweq202" 0      ;@line 44
						Assign ::target_var akTarget                             ;@line 45
						Assign ::Terminate_var False                             ;@line 46
						PropGet zad_DeviousBra ::Libs_var ::temp8                ;@line 48
						CallMethod WornHasKeyword ::target_var ::temp9 ::temp8   ;@line 48
						Cast ::temp12 ::temp9                                    ;@line 48
						JumpF ::temp12 _label0                                   ;@line 48
						CallMethod GetWornForm ::target_var ::temp10 4           ;@line 48
						Not ::temp11 ::temp10                                    ;@line 48
						Cast ::temp11 ::temp11                                   ;@line 48
						JumpT ::temp11 _label1                                   ;@line 48
						PropGet zad_DeviousHarness ::Libs_var ::temp8            ;@line 48
						CallMethod WornHasKeyword ::target_var ::temp12 ::temp8  ;@line 48
						Cast ::temp11 ::temp12                                   ;@line 48
					_label1:
						Cast ::temp12 ::temp11                                   ;@line 48
					_label0:
						Cast ::temp9 ::temp12                                    ;@line 48
						JumpF ::temp9 _label2                                    ;@line 48
						PropGet zad_NoCompressBreasts ::Libs_var ::temp8         ;@line 48
						CallMethod WornHasKeyword ::target_var ::temp9 ::temp8   ;@line 48
						Not ::temp11 ::temp9                                     ;@line 48
						Cast ::temp9 ::temp11                                    ;@line 48
					_label2:
						JumpF ::temp9 _label3                                    ;@line 48
						CallMethod HideBreasts ::Libs_var ::NoneVar ::target_var  ;@line 49
						Jump _label3                                             ;@line 49
					_label3:
						CallMethod DoStart self ::NoneVar                        ;@line 51
					.endCode
				.endFunction
				.function OnEffectFinish
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akTarget actor
						.param akCaster actor
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod Log ::Libs_var ::NoneVar "1212" 0             ;@line 56
						Assign ::Terminate_var True                              ;@line 57
						CallMethod ShowBreasts ::Libs_var ::NoneVar ::target_var  ;@line 58
					.endCode
				.endFunction
				.function OnCellAttach
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod DoStart self ::NoneVar                        ;@line 62
					.endCode
				.endFunction
				.function DoRegister
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp0 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						Not ::temp0 ::Terminate_var                              ;@line 9
						Cast ::temp0 ::temp0                                     ;@line 9
						JumpF ::temp0 _label4                                    ;@line 9
						Cast ::temp0 ::target_var                                ;@line 9
					_label4:
						JumpF ::temp0 _label5                                    ;@line 9
						CallMethod RegisterForSingleUpdate self ::NoneVar 5.000000  ;@line 10
						Jump _label5                                             ;@line 10
					_label5:
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
				.function DoStart
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp1 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						Not ::temp1 ::Terminate_var                              ;@line 15
						Cast ::temp1 ::temp1                                     ;@line 15
						JumpF ::temp1 _label6                                    ;@line 15
						Cast ::temp1 ::target_var                                ;@line 15
					_label6:
						JumpF ::temp1 _label7                                    ;@line 15
						CallMethod RegisterForSingleUpdate self ::NoneVar 1.000000  ;@line 16
						Jump _label7                                             ;@line 16
					_label7:
					.endCode
				.endFunction
				.function DoUnregister
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp2 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						Not ::temp2 ::Terminate_var                              ;@line 21
						Cast ::temp2 ::temp2                                     ;@line 21
						JumpF ::temp2 _label8                                    ;@line 21
						Cast ::temp2 ::target_var                                ;@line 21
					_label8:
						JumpF ::temp2 _label9                                    ;@line 21
						CallMethod ShowBreasts ::Libs_var ::NoneVar ::target_var  ;@line 22
						CallMethod UnregisterForUpdate self ::NoneVar            ;@line 23
						Jump _label9                                             ;@line 23
					_label9:
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
				.function OnUpdate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp3 Bool
						.local ::temp4 form
						.local ::temp5 Bool
						.local ::temp6 keyword
						.local ::temp7 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						Not ::temp3 ::Terminate_var                              ;@line 29
						JumpF ::temp3 _label10                                   ;@line 29
						CallMethod GetWornForm ::target_var ::temp4 4            ;@line 30
						Not ::temp5 ::temp4                                      ;@line 30
						Cast ::temp5 ::temp5                                     ;@line 30
						JumpT ::temp5 _label11                                   ;@line 30
						PropGet zad_DeviousHarness ::Libs_var ::temp6            ;@line 30
						CallMethod WornHasKeyword ::target_var ::temp7 ::temp6   ;@line 30
						Cast ::temp5 ::temp7                                     ;@line 30
					_label11:
						Cast ::temp5 ::temp5                                     ;@line 30
						JumpF ::temp5 _label12                                   ;@line 30
						PropGet zad_NoCompressBreasts ::Libs_var ::temp6         ;@line 30
						CallMethod WornHasKeyword ::target_var ::temp7 ::temp6   ;@line 30
						Not ::temp7 ::temp7                                      ;@line 30
						Cast ::temp5 ::temp7                                     ;@line 30
					_label12:
						JumpF ::temp5 _label13                                   ;@line 30
						CallMethod HideBreasts ::Libs_var ::NoneVar ::target_var  ;@line 31
						Jump _label13                                            ;@line 31
					_label13:
						Jump _label14                                            ;@line 31
					_label10:
						CallMethod ShowBreasts ::Libs_var ::NoneVar ::target_var  ;@line 34
					_label14:
						CallMethod DoRegister self ::NoneVar                     ;@line 36
					.endCode
				.endFunction
			.endState
		.endStateTable
	.endObject
.endObjectTable
