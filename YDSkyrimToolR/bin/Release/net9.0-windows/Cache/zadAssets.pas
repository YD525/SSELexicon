.info
	.source "vkygciawanqkg"
	.modifyTime 1479930098 ;Thu Nov 24 03:41:38 2016 Local
	.compileTime 1702770465 ;Sun Dec 17 07:47:45 2023 Local
	.user "tzwwp"
	.computer "XYSIGMUHDSEIPIZ"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object zadAssets Quest
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
		.endVariableTable
		.propertyTable
		.endPropertyTable
		.stateTable
			.state 
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
				.function GetVersion
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
					.endParamTable
					.localTable
						.local ::temp0 Float
					.endLocalTable
					.code
						Cast ::temp0 3                                           ;@line 4
						Return ::temp0                                           ;@line 4
					.endCode
				.endFunction
				.function GetVersionString
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String
					.paramTable
					.endParamTable
					.localTable
					.endLocalTable
					.code
						Return "3.0"                                             ;@line 8
					.endCode
				.endFunction
			.endState
		.endStateTable
	.endObject
.endObjectTable
