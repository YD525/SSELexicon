.info
	.source "treexogtilluetonfqujywslblgjhd"
	.modifyTime 1709420428 ;Sun Mar 03 07:00:28 2024 Local
	.compileTime 1709420435 ;Sun Mar 03 07:00:35 2024 Local
	.user "usdguwbkouhkr"
	.computer "VRQOWBHBUIBTTHI"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object zaddRadiantMasterBaseQuest Quest
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
			.variable ::HardTimeout_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::libs_var zadlibs
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::MasterScript_var zaddradiantmasterscript
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::Failed_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::SoftTimeout_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
		.endVariableTable
		.propertyTable
			.property HardTimeout Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::HardTimeout_var
			.endProperty
			.property MasterScript zaddradiantmasterscript auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::MasterScript_var
			.endProperty
			.property Failed Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Failed_var
			.endProperty
			.property SoftTimeout Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::SoftTimeout_var
			.endProperty
			.property libs zadlibs auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::libs_var
			.endProperty
		.endPropertyTable
		.stateTable
			.state 
				.function Done
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param end Int
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp4 Bool
						.local ::temp5 Bool
					.endLocalTable
					.code
						CallMethod Log ::libs_var ::NoneVar "baseQuest: Done()" 0  ;@line 38
						CallMethod SetStage self ::temp4 200                     ;@line 39
						CallMethod CompleteQuest self ::NoneVar                  ;@line 40
						CallMethod Stop self ::NoneVar                           ;@line 41
						CompareEQ ::temp4 end 0                                  ;@line 42
						JumpF ::temp4 _label0                                    ;@line 42
						CallMethod TaskCompleted ::MasterScript_var ::NoneVar    ;@line 43
						Jump _label1                                             ;@line 43
					_label0:
						CompareEQ ::temp5 end 1                                  ;@line 44
						JumpF ::temp5 _label2                                    ;@line 44
						CallMethod TaskFailed ::MasterScript_var ::NoneVar       ;@line 45
						Jump _label1                                             ;@line 45
					_label2:
						CompareEQ ::temp5 end 2                                  ;@line 46
						JumpF ::temp5 _label3                                    ;@line 46
						CallMethod TaskDoneNoPostScene ::MasterScript_var ::NoneVar  ;@line 47
						Jump _label1                                             ;@line 47
					_label3:
						CallMethod Error ::libs_var ::NoneVar "任务收到无效的结束代码."  ;@line 49
					_label1:
					.endCode
				.endFunction
				.function Begin
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param eval Bool
					.endParamTable
					.localTable
						.local ::temp0 String
						.local ::NoneVar None
						.local ::temp1 Bool
					.endLocalTable
					.code
						Cast ::temp0 eval                                        ;@line 16
						StrCat ::temp0 "BaseQuest: Begin(" ::temp0               ;@line 16
						StrCat ::temp0 ::temp0 ")"                               ;@line 16
						CallMethod Log ::libs_var ::NoneVar ::temp0 0            ;@line 16
						Assign ::Failed_var False                                ;@line 17
						CallMethod Reset self ::NoneVar                          ;@line 18
						CallMethod Start self ::temp1                            ;@line 19
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
				.function ReturnHome
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp3 Bool
					.endLocalTable
					.code
						CallMethod Log ::libs_var ::NoneVar "BaseQuest: ReturnHome()" 0  ;@line 31
						CallMethod SetStage self ::temp3 180                     ;@line 32
						CallMethod SetObjectiveDisplayed self ::NoneVar 180 True False  ;@line 33
					.endCode
				.endFunction
				.function EndedDialogue
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp2 Bool
					.endLocalTable
					.code
						CallMethod Log ::libs_var ::NoneVar "BaseQuest: EndedDialogue()" 0  ;@line 24
						CallMethod SetStage self ::temp2 10                      ;@line 25
						CallMethod SetObjectiveDisplayed self ::NoneVar 10 True False  ;@line 26
					.endCode
				.endFunction
				.function DetermineEligibility
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Int
					.paramTable
					.endParamTable
					.localTable
					.endLocalTable
					.code
						Return 0                                                 ;@line 11
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
			.endState
		.endStateTable
	.endObject
.endObjectTable
