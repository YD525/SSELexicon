.info
	.source "afiriixbbhiattqkt"
	.modifyTime 1709417369 ;Sun Mar 03 06:09:29 2024 Local
	.compileTime 1709417374 ;Sun Mar 03 06:09:34 2024 Local
	.user "jmpeocsiwecyo"
	.computer "QONQGYKCJJVEKZE"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object zadBeltScript zadEquipScript
		.userFlags 0	; Flags: 0x00000000
		.docString ""
		.autoState 
		.variableTable
			.variable ::SexLab_var sexlabframework
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zad_DeviousPlug_var keyword
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
		.endVariableTable
		.propertyTable
			.property zad_DeviousPlug keyword auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zad_DeviousPlug_var
			.endProperty
			.property SexLab sexlabframework auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::SexLab_var
			.endProperty
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
						.local ::NoneVar None
						.local ::temp10 actor
						.local ::temp11 Bool
						.local msg String
						.local ::temp12 Int
						.local ::temp13 Int
						.local ::temp14 Bool
						.local ::temp15 Bool
						.local ::temp16 actorbase
						.local ::temp17 String
					.endLocalTable
					.code
						CallMethod StoreExposureRate ::libs_var ::NoneVar akActor  ;@line 43
						Assign msg ""                                            ;@line 44
						PropGet playerref ::libs_var ::temp10                    ;@line 45
						CompareEQ ::temp11 akActor ::temp10                      ;@line 45
						JumpF ::temp11 _label0                                   ;@line 45
						CallMethod GetActorExposure ::Aroused_var ::temp12 akActor  ;@line 47
						CallMethod ArousalThreshold ::libs_var ::temp13 "Desire"  ;@line 47
						CompareLT ::temp14 ::temp12 ::temp13                     ;@line 47
						JumpF ::temp14 _label1                                   ;@line 47
						Assign msg "你冷静而自信地把贞操带锁在腰上."  ;@line 48
						Jump _label2                                             ;@line 48
					_label1:
						CallMethod GetActorExposure ::Aroused_var ::temp12 akActor  ;@line 49
						CallMethod ArousalThreshold ::libs_var ::temp13 "Horny"  ;@line 49
						CompareLT ::temp15 ::temp12 ::temp13                     ;@line 49
						JumpF ::temp15 _label3                                   ;@line 49
						Assign msg "你将贞操带锁在腰间,信心胜过远见."  ;@line 50
						Jump _label2                                             ;@line 50
					_label3:
						CallMethod GetActorExposure ::Aroused_var ::temp12 akActor  ;@line 51
						CallMethod ArousalThreshold ::libs_var ::temp13 "Desperate"  ;@line 51
						CompareLT ::temp15 ::temp12 ::temp13                     ;@line 51
						JumpF ::temp15 _label4                                   ;@line 51
						Assign msg "为了对抗体内的冲动,你将贞操带锁在腰间."  ;@line 52
						Jump _label2                                             ;@line 52
					_label4:
						Assign msg "在这看起来完全疯狂的行为中,你(勉强)设法将贞操带锁在腰上."  ;@line 54
					_label2:
						Jump _label5                                             ;@line 54
					_label0:
						CallMethod GetLeveledActorBase akActor ::temp16          ;@line 57
						CallMethod GetName ::temp16 ::temp17                     ;@line 57
						StrCat ::temp17 ::temp17 " 当你把贞操带锁在她的臀部时,她脸红了."  ;@line 57
						Assign msg ::temp17                                      ;@line 57
					_label5:
						Not ::temp15 silent                                      ;@line 59
						JumpF ::temp15 _label6                                   ;@line 59
						CallMethod NotifyActor ::libs_var ::NoneVar msg akActor True  ;@line 60
						Jump _label6                                             ;@line 60
					_label6:
					.endCode
				.endFunction
				.function BeltMenuMasturbate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp8 actor
						.local ::temp9 Int
					.endLocalTable
					.code
						CallMethod NotifyPlayer ::libs_var ::NoneVar "你试图从内心燃烧的欲望中寻求解脱……" False  ;@line 37
						PropGet playerref ::libs_var ::temp8                     ;@line 38
						CallMethod UpdateActorExposure ::Aroused_var ::temp9 ::temp8 3 ""  ;@line 38
						PropGet playerref ::libs_var ::temp8                     ;@line 39
						CallMethod Masturbate ::libs_var ::NoneVar ::temp8 False  ;@line 39
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
				.function RestoreSettings
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
					.endParamTable
					.localTable
						.local ::temp21 Float
						.local ::temp22 String
						.local ::NoneVar None
						.local ::temp23 form
						.local ::temp24 Bool
						.local originalExposureRate Float
					.endLocalTable
					.code
						CallMethod GetOriginalRate ::libs_var ::temp21 akActor   ;@line 79
						Assign originalExposureRate ::temp21                     ;@line 79
						Cast ::temp22 originalExposureRate                       ;@line 80
						StrCat ::temp22 "将原来的曝光率恢复为 " ::temp22  ;@line 80
						CallMethod Log ::libs_var ::NoneVar ::temp22 0           ;@line 80
						CallMethod SetActorExposureRate ::Aroused_var ::temp21 akActor originalExposureRate  ;@line 81
						Cast ::temp23 akActor                                    ;@line 82
						CallStatic storageutil UnSetFloatValue ::temp24 ::temp23 "zad.StoredExposureRate"  ;@line 82
					.endCode
				.endFunction
				.function OnRemoveDevice
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
						CallMethod RestoreSettings self ::NoneVar akActor        ;@line 75
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
						.local ::temp18 Float
						.local ::temp19 String
						.local ::temp20 String
						.local ::NoneVar None
						.local modRate Float
					.endLocalTable
					.code
						CallMethod GetModifiedRate ::libs_var ::temp18 akActor   ;@line 65
						Assign modRate ::temp18                                  ;@line 65
						CallMethod GetOriginalRate ::libs_var ::temp18 akActor   ;@line 66
						Cast ::temp19 ::temp18                                   ;@line 66
						StrCat ::temp19 "原始曝光率为 " ::temp19           ;@line 66
						StrCat ::temp19 ::temp19 ". 设置为 "                  ;@line 66
						Cast ::temp20 modRate                                    ;@line 66
						StrCat ::temp20 ::temp19 ::temp20                        ;@line 66
						StrCat ::temp19 ::temp20 "."                             ;@line 66
						CallMethod Log ::libs_var ::NoneVar ::temp19 0           ;@line 66
						CallMethod SetActorExposureRate ::Aroused_var ::temp18 akActor modRate  ;@line 67
					.endCode
				.endFunction
				.function DeviceMenuExt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param msgChoice Int
					.endParamTable
					.localTable
						.local ::temp0 Bool
						.local ::temp1 actor
						.local ::NoneVar None
					.endLocalTable
					.code
						CompareEQ ::temp0 msgChoice 3                            ;@line 11
						JumpF ::temp0 _label7                                    ;@line 11
						PropGet playerref ::libs_var ::temp1                     ;@line 12
						CallMethod ChastityBeltStruggle ::libs_var ::NoneVar ::temp1  ;@line 12
						Jump _label7                                             ;@line 12
					_label7:
						CompareEQ ::temp0 msgChoice 4                            ;@line 15
						JumpF ::temp0 _label8                                    ;@line 15
						CallMethod BeltMenuMasturbate self ::NoneVar             ;@line 16
						Jump _label8                                             ;@line 16
					_label8:
					.endCode
				.endFunction
				.function OnContainerChangedPre
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akNewContainer ObjectReference
						.param akOldContainer ObjectReference
					.endParamTable
					.localTable
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod NotifyPlayer ::libs_var ::NoneVar "贞操带仍然牢牢地锁在你的臀部上." False  ;@line 71
					.endCode
				.endFunction
				.function DeviceMenuRemoveWithKey
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp2 Bool
						.local ::temp3 actor
						.local ::temp4 Int
						.local ::temp5 Int
						.local ::temp6 Bool
						.local ::temp7 Bool
						.local ::NoneVar None
						.local msg String
					.endLocalTable
					.code
						CallMethod RemoveDeviceWithKey self ::temp2 None False   ;@line 21
						JumpF ::temp2 _label9                                    ;@line 21
						Assign msg ""                                            ;@line 22
						PropGet playerref ::libs_var ::temp3                     ;@line 23
						CallMethod GetActorExposure ::Aroused_var ::temp4 ::temp3  ;@line 23
						CallMethod ArousalThreshold ::libs_var ::temp5 "Desire"  ;@line 23
						CompareLT ::temp6 ::temp4 ::temp5                        ;@line 23
						JumpF ::temp6 _label10                                   ;@line 23
						Assign msg "你解锁并取下贞操带 - 更多的是由于不适而不是对快乐的渴望."  ;@line 24
						Jump _label11                                            ;@line 24
					_label10:
						PropGet playerref ::libs_var ::temp3                     ;@line 25
						CallMethod GetActorExposure ::Aroused_var ::temp4 ::temp3  ;@line 25
						CallMethod ArousalThreshold ::libs_var ::temp5 "3"       ;@line 25
						CompareLT ::temp7 ::temp4 ::temp5                        ;@line 25
						JumpF ::temp7 _label12                                   ;@line 25
						Assign msg "你解开腰间的贞操带,为新获得的自由松了一口气."  ;@line 26
						Jump _label11                                            ;@line 26
					_label12:
						PropGet playerref ::libs_var ::temp3                     ;@line 27
						CallMethod GetActorExposure ::Aroused_var ::temp4 ::temp3  ;@line 27
						CallMethod ArousalThreshold ::libs_var ::temp5 "Desperate"  ;@line 27
						CompareLT ::temp7 ::temp4 ::temp5                        ;@line 27
						JumpF ::temp7 _label13                                   ;@line 27
						Assign msg "再也无法抗拒肉体的欲望,焦急地解开腰间的贞操带."  ;@line 28
						Jump _label11                                            ;@line 28
					_label13:
						Assign msg "经过几次疯狂的尝试,你颤抖的手指终于设法转动钥匙并解锁贞操带."  ;@line 30
					_label11:
						CallMethod Notify ::libs_var ::NoneVar msg True          ;@line 32
						Jump _label9                                             ;@line 32
					_label9:
					.endCode
				.endFunction
			.endState
		.endStateTable
	.endObject
.endObjectTable
