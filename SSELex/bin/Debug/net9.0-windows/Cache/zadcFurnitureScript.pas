.info
	.source "kpkedxrvlpwatioktfzlnwq"
	.modifyTime 1709419738 ;Sun Mar 03 06:48:58 2024 Local
	.compileTime 1709419809 ;Sun Mar 03 06:50:09 2024 Local
	.user "gcsfqwdxaabqw"
	.computer "TBJMULORZXQHDTA"
.endInfo
.userFlagsRef
	.flag hidden 0	; 0x00000000
	.flag conditional 1	; 0x00000001
.endUserFlagsRef
.objectTable
	.object zadcFurnitureScript ObjectReference
		.userFlags 0	; Flags: 0x00000000
		.docString "to-do: \nAutonomous escape by NPCs\nNeed to make sure in DDI that NPCs can't unlock/equip invalid devices. Best to forbid item manipulation overall?"
		.autoState 
		.variableTable
			.variable ::CanBePickedUp_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::BoundPose_var package[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::LockShieldTimerMax_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::EscapeCooldown_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.250000
			.endVariable
			.variable ::UnlockCooldown_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::zadc_EscapeLockPickMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::AppliedSpellEffects_var spell[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::ForceStripActor_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue True
			.endVariable
			.variable ::AllowDifficultyModifier_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::PartnerIsInFront_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::NumberOfKeysNeeded_var Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 1
			.endVariable
			.variable FPosY Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::SexAnimations_var String[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::InvalidDevices_var keyword[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable PosX Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::Distance_var Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 45
			.endVariable
			.variable ::zadc_DeviceMsgPlayerLocked_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable CurrentPose Package
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeStruggleSuccessMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeBreakFailureMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::EquipDevices_var armor[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::deviceKey_var key
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::DestroyKey_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::zadc_OnDeviceLockMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::AllowedTool_var keyword[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeLockPickSuccessMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_DeviceMsgNPCNotLocked_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::DestroyOnRemove_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::ReleaseTimerStartedAt_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::AllowPasserbyAction_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::zadc_EscapeDeviceNPCMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::libs_var zadlibs
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::ScriptedDevice_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::ForceTimer_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable OriginalLockPickEscapeChance Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable FAngleX Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable StruggleTick Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0
			.endVariable
			.variable ::isSelfBondage_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable DeviceEquippedAt Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::zadc_OnNoKeyMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable StruggleMutex Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::zadc_OnNotEnoughKeysMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable EscapeLockPickAttemptsMade Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0
			.endVariable
			.variable FPosZ Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable lasthourdisplayed Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::AllowRewardonEscape_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue True
			.endVariable
			.variable OriginalBaseEscapeChance Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::zadc_DeviceMsgNPCLocked_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable EscapeStruggleAttemptsMade Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0
			.endVariable
			.variable ::Reward_var leveleditem[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable PosY Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::PasserbyCooldown_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable LockShieldStartedAt Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable PosZ Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_OnLockDeviceNPCMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::BoundPoseArmbinder_var package[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::LockAccessDifficulty_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable isLockManipulated Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::StrugglePoseArmbinder_var package[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::CatastrophicFailureChance_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable LastBreakEscapeAttemptAt Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable EscapeBreakAttemptsMade Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0
			.endVariable
			.variable ::AllowedLockPicks_var form[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::MercyEscapeAttempts_var Int
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0
			.endVariable
			.variable LastLockPickEscapeAttemptAt Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::zadc_EscapeLockPickFailureMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::clib_var zadclibs
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeStruggleMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_OnLeaveItLockedMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::DisableLockManipulation_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::AllowTimerDialogue_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue True
			.endVariable
			.variable ::AllowLockPick_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue True
			.endVariable
			.variable ::zadc_OnLockDeviceMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable FPosX Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeDeviceMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::BoundPoseYoke_var package[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::BreakDeviceEscapeChance_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::AllowStandardTools_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue True
			.endVariable
			.variable ::StrugglePose_var package[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable LastUnlockAttemptAt Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::PreventWaitandSleep_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue True
			.endVariable
			.variable ::user_var actor
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_OnLockDeviceNoManipulateMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable FAngleY Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable LastPasserbyEventAt Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::zadc_EscapeBreakSuccessMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::LockPickEscapeChance_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::SendDeviceModEvents_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::StrugglePoseYoke_var package[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_OnLeaveItNotLockedMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable LockShieldTimer Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable FAngleZ Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::BaseEscapeChance_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::zadc_SelfbondageMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeStruggleNPCMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::Blueprint_var miscobject
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable LastStruggleEscapeAttemptAt Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable OriginalBreakEscapeChance Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::HideAllDevices_var Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable ::zadc_DeviceMsgPlayerNotLocked_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable Mutex Bool
				.userFlags 0	; Flags: 0x00000000
				.initialValue False
			.endVariable
			.variable CurrentStruggle Package
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeBreakMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::LinkedDevices_var objectreference[]
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_DeviceMsgPlayerNotLockedCanPick_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::zadc_EscapeStruggleFailureMSG_var message
				.userFlags 0	; Flags: 0x00000000
				.initialValue None
			.endVariable
			.variable ::LockShieldTimerMin_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 0.000000
			.endVariable
			.variable ::DeviceName_var String
				.userFlags 0	; Flags: 0x00000000
				.initialValue ""
			.endVariable
			.variable ::SelfBondageReleaseTimer_var Float
				.userFlags 0	; Flags: 0x00000000
				.initialValue 1.000000
			.endVariable
		.endVariableTable
		.propertyTable
			.property LockAccessDifficulty Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::LockAccessDifficulty_var
			.endProperty
			.property ReleaseTimerStartedAt Float auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::ReleaseTimerStartedAt_var
			.endProperty
			.property StrugglePose package[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::StrugglePose_var
			.endProperty
			.property AllowedTool keyword[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowedTool_var
			.endProperty
			.property zadc_EscapeStruggleNPCMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeStruggleNPCMSG_var
			.endProperty
			.property zadc_EscapeLockPickMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeLockPickMSG_var
			.endProperty
			.property AllowDifficultyModifier Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowDifficultyModifier_var
			.endProperty
			.property DestroyOnRemove Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::DestroyOnRemove_var
			.endProperty
			.property DeviceName String auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::DeviceName_var
			.endProperty
			.property zadc_EscapeBreakMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeBreakMSG_var
			.endProperty
			.property PasserbyCooldown Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::PasserbyCooldown_var
			.endProperty
			.property BoundPose package[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::BoundPose_var
			.endProperty
			.property LockPickEscapeChance Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::LockPickEscapeChance_var
			.endProperty
			.property PartnerIsInFront Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::PartnerIsInFront_var
			.endProperty
			.property CatastrophicFailureChance Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::CatastrophicFailureChance_var
			.endProperty
			.property EquipDevices armor[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::EquipDevices_var
			.endProperty
			.property LockShieldTimerMin Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::LockShieldTimerMin_var
			.endProperty
			.property AllowPasserbyAction Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowPasserbyAction_var
			.endProperty
			.property zadc_EscapeLockPickFailureMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeLockPickFailureMSG_var
			.endProperty
			.property AllowTimerDialogue Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowTimerDialogue_var
			.endProperty
			.property BoundPoseYoke package[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::BoundPoseYoke_var
			.endProperty
			.property DisableLockManipulation Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::DisableLockManipulation_var
			.endProperty
			.property UnlockCooldown Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::UnlockCooldown_var
			.endProperty
			.property LockShieldTimerMax Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::LockShieldTimerMax_var
			.endProperty
			.property AllowedLockPicks form[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowedLockPicks_var
			.endProperty
			.property LinkedDevices objectreference[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::LinkedDevices_var
			.endProperty
			.property MercyEscapeAttempts Int auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::MercyEscapeAttempts_var
			.endProperty
			.property zadc_EscapeStruggleFailureMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeStruggleFailureMSG_var
			.endProperty
			.property zadc_DeviceMsgPlayerLocked message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_DeviceMsgPlayerLocked_var
			.endProperty
			.property zadc_OnLockDeviceMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnLockDeviceMSG_var
			.endProperty
			.property isSelfBondage Bool auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::isSelfBondage_var
			.endProperty
			.property zadc_EscapeBreakSuccessMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeBreakSuccessMSG_var
			.endProperty
			.property AllowLockPick Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowLockPick_var
			.endProperty
			.property zadc_OnNoKeyMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnNoKeyMSG_var
			.endProperty
			.property zadc_DeviceMsgPlayerNotLocked message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_DeviceMsgPlayerNotLocked_var
			.endProperty
			.property Reward leveleditem[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Reward_var
			.endProperty
			.property BaseEscapeChance Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::BaseEscapeChance_var
			.endProperty
			.property deviceKey key auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::deviceKey_var
			.endProperty
			.property EscapeCooldown Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::EscapeCooldown_var
			.endProperty
			.property zadc_EscapeBreakFailureMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeBreakFailureMSG_var
			.endProperty
			.property zadc_EscapeLockPickSuccessMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeLockPickSuccessMSG_var
			.endProperty
			.property SendDeviceModEvents Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::SendDeviceModEvents_var
			.endProperty
			.property zadc_OnDeviceLockMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnDeviceLockMSG_var
			.endProperty
			.property zadc_EscapeStruggleSuccessMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeStruggleSuccessMSG_var
			.endProperty
			.property zadc_OnLeaveItNotLockedMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnLeaveItNotLockedMSG_var
			.endProperty
			.property zadc_OnLeaveItLockedMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnLeaveItLockedMSG_var
			.endProperty
			.property PreventWaitandSleep Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::PreventWaitandSleep_var
			.endProperty
			.property AllowStandardTools Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowStandardTools_var
			.endProperty
			.property Blueprint miscobject auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Blueprint_var
			.endProperty
			.property user actor auto
				.userFlags 1	; Flags: 0x00000001
				.docString ""
				.autoVar ::user_var
			.endProperty
			.property ForceTimer Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ForceTimer_var
			.endProperty
			.property zadc_OnLockDeviceNPCMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnLockDeviceNPCMSG_var
			.endProperty
			.property zadc_DeviceMsgNPCLocked message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_DeviceMsgNPCLocked_var
			.endProperty
			.property Distance Int auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::Distance_var
			.endProperty
			.property NumberOfKeysNeeded Int auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::NumberOfKeysNeeded_var
			.endProperty
			.property zadc_SelfbondageMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_SelfbondageMSG_var
			.endProperty
			.property zadc_EscapeDeviceMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeDeviceMSG_var
			.endProperty
			.property AllowRewardonEscape Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AllowRewardonEscape_var
			.endProperty
			.property zadc_EscapeDeviceNPCMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeDeviceNPCMSG_var
			.endProperty
			.property AppliedSpellEffects spell[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::AppliedSpellEffects_var
			.endProperty
			.property BreakDeviceEscapeChance Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::BreakDeviceEscapeChance_var
			.endProperty
			.property StrugglePoseYoke package[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::StrugglePoseYoke_var
			.endProperty
			.property DestroyKey Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::DestroyKey_var
			.endProperty
			.property clib zadclibs auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::clib_var
			.endProperty
			.property CanBePickedUp Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::CanBePickedUp_var
			.endProperty
			.property zadc_OnNotEnoughKeysMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnNotEnoughKeysMSG_var
			.endProperty
			.property ScriptedDevice Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ScriptedDevice_var
			.endProperty
			.property InvalidDevices keyword[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::InvalidDevices_var
			.endProperty
			.property ForceStripActor Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::ForceStripActor_var
			.endProperty
			.property libs zadlibs auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::libs_var
			.endProperty
			.property HideAllDevices Bool auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::HideAllDevices_var
			.endProperty
			.property zadc_DeviceMsgPlayerNotLockedCanPick message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_DeviceMsgPlayerNotLockedCanPick_var
			.endProperty
			.property zadc_DeviceMsgNPCNotLocked message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_DeviceMsgNPCNotLocked_var
			.endProperty
			.property zadc_EscapeStruggleMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_EscapeStruggleMSG_var
			.endProperty
			.property SelfBondageReleaseTimer Float auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::SelfBondageReleaseTimer_var
			.endProperty
			.property zadc_OnLockDeviceNoManipulateMSG message auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::zadc_OnLockDeviceNoManipulateMSG_var
			.endProperty
			.property BoundPoseArmbinder package[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::BoundPoseArmbinder_var
			.endProperty
			.property SexAnimations String[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::SexAnimations_var
			.endProperty
			.property StrugglePoseArmbinder package[] auto
				.userFlags 0	; Flags: 0x00000000
				.docString ""
				.autoVar ::StrugglePoseArmbinder_var
			.endProperty
		.endPropertyTable
		.stateTable
			.state 
				.function UnlockAttemptNPC
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp117 Bool
						.local ::temp114 String
						.local ::temp115 actorbase
						.local ::temp116 String
						.local ::NoneVar None
						.local ::temp118 actor
						.local ::temp119 form
						.local ::temp120 Int
						.local ::temp121 Bool
						.local ::temp122 zadconfig
						.local ::temp123 keyword
					.endLocalTable
					.code
						JumpF isLockManipulated _label0                          ;@line 632
						StrCat ::temp114 "当你操纵了 " ::DeviceName_var     ;@line 633
						StrCat ::temp114 ::temp114 "后,你可以轻易的把 "  ;@line 633
						CallMethod GetLeveledActorBase ::user_var ::temp115      ;@line 633
						CallMethod GetName ::temp115 ::temp116                   ;@line 633
						StrCat ::temp114 ::temp114 ::temp116                     ;@line 633
						StrCat ::temp116 ::temp114 " 从该装置中脱离开来!"  ;@line 633
						CallMethod notify ::libs_var ::NoneVar ::temp116 True    ;@line 633
						CallMethod UnlockActor self ::NoneVar                    ;@line 634
						Return True                                              ;@line 635
						Jump _label0                                             ;@line 635
					_label0:
						CallMethod CheckLockShield self ::temp117                ;@line 637
						Not ::temp117 ::temp117                                  ;@line 637
						JumpF ::temp117 _label1                                  ;@line 637
						Return False                                             ;@line 638
						Jump _label1                                             ;@line 638
					_label1:
						JumpF ::deviceKey_var _label2                            ;@line 640
						PropGet PlayerRef ::libs_var ::temp118                   ;@line 641
						Cast ::temp119 ::deviceKey_var                           ;@line 641
						CallMethod GetItemCount ::temp118 ::temp120 ::temp119    ;@line 641
						CompareLTE ::temp117 ::temp120 0                         ;@line 641
						JumpF ::temp117 _label3                                  ;@line 641
						CallMethod Show ::zadc_OnNoKeyMSG_var ::temp120 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 642
						Return False                                             ;@line 643
						Jump _label4                                             ;@line 643
					_label3:
						PropGet PlayerRef ::libs_var ::temp118                   ;@line 644
						Cast ::temp119 ::deviceKey_var                           ;@line 644
						CallMethod GetItemCount ::temp118 ::temp120 ::temp119    ;@line 644
						CompareLT ::temp121 ::temp120 ::NumberOfKeysNeeded_var   ;@line 644
						JumpF ::temp121 _label4                                  ;@line 644
						CallMethod Show ::zadc_OnNotEnoughKeysMSG_var ::temp120 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 645
						Return False                                             ;@line 646
						Jump _label4                                             ;@line 646
					_label4:
						JumpF ::DestroyKey_var _label5                           ;@line 648
						PropGet PlayerRef ::libs_var ::temp118                   ;@line 649
						Cast ::temp119 ::deviceKey_var                           ;@line 649
						CallMethod RemoveItem ::temp118 ::NoneVar ::temp119 ::NumberOfKeysNeeded_var False None  ;@line 649
						Jump _label6                                             ;@line 649
					_label5:
						PropGet Config ::libs_var ::temp122                      ;@line 650
						PropGet GlobalDestroyKey ::temp122 ::temp121             ;@line 650
						Cast ::temp121 ::temp121                                 ;@line 650
						JumpF ::temp121 _label7                                  ;@line 650
						PropGet zad_NonUniqueKey ::libs_var ::temp123            ;@line 650
						CallMethod HasKeyword ::deviceKey_var ::temp117 ::temp123  ;@line 650
						Cast ::temp121 ::temp117                                 ;@line 650
					_label7:
						JumpF ::temp121 _label6                                  ;@line 650
						PropGet PlayerRef ::libs_var ::temp118                   ;@line 651
						Cast ::temp119 ::deviceKey_var                           ;@line 651
						CallMethod RemoveItem ::temp118 ::NoneVar ::temp119 ::NumberOfKeysNeeded_var False None  ;@line 651
						Jump _label6                                             ;@line 651
					_label6:
						Jump _label2                                             ;@line 651
					_label2:
						CallMethod UnlockActor self ::NoneVar                    ;@line 654
						Return True                                              ;@line 655
					.endCode
				.endFunction
				.function CanMakeUnlockAttempt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp75 Float
						.local ::temp76 Bool
						.local HoursNeeded Float
						.local HoursPassed Float
						.local ::temp77 Int
						.local ::temp78 String
						.local ::NoneVar None
						.local HoursToWait Int
					.endLocalTable
					.code
						CallMethod CalculateCooldownModifier self ::temp75 False  ;@line 417
						FMultiply ::temp75 ::UnlockCooldown_var ::temp75         ;@line 417
						Assign HoursNeeded ::temp75                              ;@line 417
						CallStatic utility GetCurrentGameTime ::temp75           ;@line 418
						FSubtract ::temp75 ::temp75 LastUnlockAttemptAt          ;@line 418
						FMultiply ::temp75 ::temp75 24.000000                    ;@line 418
						Assign HoursPassed ::temp75                              ;@line 418
						CompareGT ::temp76 HoursPassed HoursNeeded               ;@line 419
						JumpF ::temp76 _label8                                   ;@line 419
						CallStatic utility GetCurrentGameTime ::temp75           ;@line 420
						Assign LastUnlockAttemptAt ::temp75                      ;@line 420
						Return True                                              ;@line 421
						Jump _label9                                             ;@line 421
					_label8:
						FSubtract ::temp75 HoursNeeded HoursPassed               ;@line 423
						CallStatic math Ceiling ::temp77 ::temp75                ;@line 423
						Assign HoursToWait ::temp77                              ;@line 423
						Cast ::temp78 HoursToWait                                ;@line 424
						StrCat ::temp78 "213123123" ::temp78                     ;@line 424
						StrCat ::temp78 ::temp78 " 小时后你可以再试一次."  ;@line 424
						CallMethod notify ::libs_var ::NoneVar ::temp78 True     ;@line 424
					_label9:
						Return False                                             ;@line 426
					.endCode
				.endFunction
				.function ApplyEffects
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
					.endParamTable
					.localTable
						.local ::temp7 Int
						.local ::temp8 Bool
						.local i Int
						.local ::temp9 spell
						.local ::temp10 Bool
					.endLocalTable
					.code
						ArrayLength ::temp7 ::AppliedSpellEffects_var            ;@line 159
						Assign i ::temp7                                         ;@line 159
					_label11:
						CompareGT ::temp8 i 0                                    ;@line 160
						JumpF ::temp8 _label10                                   ;@line 160
						ISubtract ::temp7 i 1                                    ;@line 161
						Assign i ::temp7                                         ;@line 161
						ArrayGetElement ::temp9 ::AppliedSpellEffects_var i      ;@line 162
						CallMethod AddSpell akActor ::temp10 ::temp9 False       ;@line 162
						Jump _label11                                            ;@line 162
					_label10:
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
				.function HasValidLockPick
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp311 actor
						.local ::temp312 miscobject
						.local ::temp313 form
						.local ::temp314 Int
						.local ::temp315 Bool
						.local ::temp316 Bool
						.local HasValidItem Bool
						.local i Int
						.local frm form
					.endLocalTable
					.code
						Assign HasValidItem False                                ;@line 1333
						Cast ::temp315 ::AllowLockPick_var                       ;@line 1334
						JumpF ::temp315 _label12                                 ;@line 1334
						PropGet PlayerRef ::libs_var ::temp311                   ;@line 1334
						PropGet Lockpick ::libs_var ::temp312                    ;@line 1334
						Cast ::temp313 ::temp312                                 ;@line 1334
						CallMethod GetItemCount ::temp311 ::temp314 ::temp313    ;@line 1334
						CompareGT ::temp315 ::temp314 0                          ;@line 1334
						Cast ::temp315 ::temp315                                 ;@line 1334
					_label12:
						JumpF ::temp315 _label13                                 ;@line 1334
						Return True                                              ;@line 1335
						Jump _label13                                            ;@line 1335
					_label13:
						ArrayLength ::temp314 ::AllowedLockPicks_var             ;@line 1337
						Assign i ::temp314                                       ;@line 1337
					_label17:
						CompareGT ::temp315 i 0                                  ;@line 1338
						Cast ::temp315 ::temp315                                 ;@line 1338
						JumpF ::temp315 _label14                                 ;@line 1338
						Not ::temp316 HasValidItem                               ;@line 1338
						Cast ::temp315 ::temp316                                 ;@line 1338
					_label14:
						JumpF ::temp315 _label15                                 ;@line 1338
						ISubtract ::temp314 i 1                                  ;@line 1339
						Assign i ::temp314                                       ;@line 1339
						ArrayGetElement ::temp313 ::AllowedLockPicks_var i       ;@line 1340
						Assign frm ::temp313                                     ;@line 1340
						PropGet PlayerRef ::libs_var ::temp311                   ;@line 1341
						CallMethod GetItemCount ::temp311 ::temp314 frm          ;@line 1341
						CompareGT ::temp316 ::temp314 0                          ;@line 1341
						JumpF ::temp316 _label16                                 ;@line 1341
						Assign HasValidItem True                                 ;@line 1342
						Jump _label16                                            ;@line 1342
					_label16:
						Jump _label17                                            ;@line 1342
					_label15:
						Return HasValidItem                                      ;@line 1345
					.endCode
				.endFunction
				.function CanMakeBreakEscapeAttempt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp236 Float
						.local ::temp237 Bool
						.local HoursNeeded Float
						.local HoursPassed Float
						.local ::temp238 Int
						.local ::temp239 actor
						.local ::temp240 Bool
						.local ::temp241 Bool
						.local ::temp243 Bool
						.local HoursToWait Int
						.local ::temp242 String
						.local ::NoneVar None
						.local ::temp244 actorbase
						.local ::temp245 String
					.endLocalTable
					.code
						CallMethod CalculateCooldownModifier self ::temp236 False  ;@line 1089
						FMultiply ::temp236 ::EscapeCooldown_var ::temp236       ;@line 1089
						Assign HoursNeeded ::temp236                             ;@line 1089
						CallStatic utility GetCurrentGameTime ::temp236          ;@line 1090
						FSubtract ::temp236 ::temp236 LastBreakEscapeAttemptAt   ;@line 1090
						FMultiply ::temp236 ::temp236 24.000000                  ;@line 1090
						Assign HoursPassed ::temp236                             ;@line 1090
						CompareGT ::temp237 HoursPassed HoursNeeded              ;@line 1091
						JumpF ::temp237 _label18                                 ;@line 1091
						CallStatic utility GetCurrentGameTime ::temp236          ;@line 1092
						Assign LastBreakEscapeAttemptAt ::temp236                ;@line 1092
						Return True                                              ;@line 1093
						Jump _label19                                            ;@line 1093
					_label18:
						FSubtract ::temp236 HoursNeeded HoursPassed              ;@line 1095
						CallStatic math Ceiling ::temp238 ::temp236              ;@line 1095
						Assign HoursToWait ::temp238                             ;@line 1095
						PropGet PlayerRef ::libs_var ::temp239                   ;@line 1096
						CompareEQ ::temp240 ::user_var ::temp239                 ;@line 1096
						Cast ::temp240 ::temp240                                 ;@line 1096
						JumpF ::temp240 _label20                                 ;@line 1096
						FSubtract ::temp236 HoursNeeded HoursPassed              ;@line 1096
						CompareGTE ::temp241 ::temp236 1.000000                  ;@line 1096
						Cast ::temp240 ::temp241                                 ;@line 1096
					_label20:
						JumpF ::temp240 _label21                                 ;@line 1096
						Cast ::temp242 HoursToWait                               ;@line 1097
						StrCat ::temp242 "你不能在上次尝试之后这么快就尝试打开这个装置! 你可以在大约 " ::temp242  ;@line 1097
						StrCat ::temp242 ::temp242 " 小时后重试."           ;@line 1097
						CallMethod notify ::libs_var ::NoneVar ::temp242 True    ;@line 1097
						Jump _label19                                            ;@line 1097
					_label21:
						PropGet PlayerRef ::libs_var ::temp239                   ;@line 1098
						CompareEQ ::temp241 ::user_var ::temp239                 ;@line 1098
						Cast ::temp241 ::temp241                                 ;@line 1098
						JumpF ::temp241 _label22                                 ;@line 1098
						FSubtract ::temp236 HoursNeeded HoursPassed              ;@line 1098
						CompareLT ::temp243 ::temp236 1.000000                   ;@line 1098
						Cast ::temp241 ::temp243                                 ;@line 1098
					_label22:
						JumpF ::temp241 _label23                                 ;@line 1098
						CallMethod notify ::libs_var ::NoneVar "你不能在上次尝试之后这么快就尝试打开这个装置! 你很快就可以再试一次!" True  ;@line 1099
						Jump _label19                                            ;@line 1099
					_label23:
						CallMethod GetLeveledActorBase ::user_var ::temp244      ;@line 1101
						CallMethod GetName ::temp244 ::temp242                   ;@line 1101
						StrCat ::temp242 "你不能帮助 " ::temp242            ;@line 1101
						StrCat ::temp242 ::temp242 " 在上次尝试后不久就尝试帮她打开此设备! 你可以在大约 "  ;@line 1101
						Cast ::temp245 HoursToWait                               ;@line 1101
						StrCat ::temp245 ::temp242 ::temp245                     ;@line 1101
						StrCat ::temp242 ::temp245 " 小时后重试."           ;@line 1101
						CallMethod notify ::libs_var ::NoneVar ::temp242 True    ;@line 1101
					_label19:
						Return False                                             ;@line 1104
					.endCode
				.endFunction
				.function SexScene
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param Partner actor
						.param AnimationName String
					.endParamTable
					.localTable
						.local ::temp174 Bool
						.local ::temp175 cell
						.local ::temp176 actor
						.local ::temp177 cell
						.local ::temp178 Bool
						.local ::NoneVar None
						.local ::temp182 sslbaseanimation[]
						.local ::temp179 ObjectReference
						.local ::temp180 ObjectReference
						.local ::temp181 Float
						.local Sanims sslbaseanimation[]
						.local ani String
						.local ::temp183 String
						.local ::temp184 sslbaseanimation
						.local ::temp185 sexlabframework
						.local ::temp186 sslbaseanimation
						.local ::temp188 actor[]
						.local ::temp189 Int
						.local ::temp187 Bool
						.local SceneSexActors actor[]
					.endLocalTable
					.code
						CallMethod Is3DLoaded ::user_var ::temp174               ;@line 855
						Not ::temp174 ::temp174                                  ;@line 855
						Cast ::temp174 ::temp174                                 ;@line 855
						JumpT ::temp174 _label24                                 ;@line 855
						CallMethod GetParentCell ::user_var ::temp175            ;@line 855
						PropGet PlayerRef ::libs_var ::temp176                   ;@line 855
						CallMethod GetParentCell ::temp176 ::temp177             ;@line 855
						CompareEQ ::temp178 ::temp175 ::temp177                  ;@line 855
						Not ::temp178 ::temp178                                  ;@line 855
						Cast ::temp174 ::temp178                                 ;@line 855
					_label24:
						Cast ::temp174 ::temp174                                 ;@line 855
						JumpT ::temp174 _label25                                 ;@line 855
						CallMethod GetIsFemale ::clib_var ::temp178 ::user_var   ;@line 855
						Not ::temp178 ::temp178                                  ;@line 855
						Cast ::temp174 ::temp178                                 ;@line 855
					_label25:
						JumpF ::temp174 _label26                                 ;@line 855
						Return False                                             ;@line 856
						Jump _label26                                            ;@line 856
					_label26:
						CallMethod RegisterForModEvent self ::NoneVar "AnimationEnd" "OnSexEnd"  ;@line 858
						CallStatic actorutil RemovePackageOverride ::temp178 ::user_var CurrentStruggle  ;@line 860
						JumpF ::PartnerIsInFront_var _label27                    ;@line 861
						Cast ::temp179 self                                      ;@line 862
						Cast ::temp180 Partner                                   ;@line 862
						Cast ::temp181 ::Distance_var                            ;@line 862
						CallMethod MoveToFront self ::NoneVar ::temp179 ::temp180 ::temp181  ;@line 862
						Jump _label28                                            ;@line 862
					_label27:
						Cast ::temp179 self                                      ;@line 864
						Cast ::temp180 Partner                                   ;@line 864
						Cast ::temp181 ::Distance_var                            ;@line 864
						CallMethod MoveToBehind self ::NoneVar ::temp179 ::temp180 ::temp181  ;@line 864
					_label28:
						CallMethod GetPositionX ::user_var ::temp181             ;@line 866
						Assign FPosX ::temp181                                   ;@line 866
						CallMethod GetPositionY ::user_var ::temp181             ;@line 867
						Assign FPosY ::temp181                                   ;@line 867
						CallMethod GetPositionZ ::user_var ::temp181             ;@line 868
						Assign FPosZ ::temp181                                   ;@line 868
						CallMethod GetAngleX ::user_var ::temp181                ;@line 869
						Assign FAngleX ::temp181                                 ;@line 869
						CallMethod GetAngleY ::user_var ::temp181                ;@line 870
						Assign FAngleY ::temp181                                 ;@line 870
						CallMethod GetAngleZ ::user_var ::temp181                ;@line 871
						Assign FAngleZ ::temp181                                 ;@line 871
						ArrayCreate ::temp182 1                                  ;@line 873
						Assign Sanims ::temp182                                  ;@line 873
						CompareEQ ::temp174 AnimationName ""                     ;@line 875
						Not ::temp174 ::temp174                                  ;@line 875
						JumpF ::temp174 _label29                                 ;@line 875
						Assign ani AnimationName                                 ;@line 876
						Jump _label30                                            ;@line 876
					_label29:
						CallMethod PickRandomSexScene self ::temp183             ;@line 878
						Assign ani ::temp183                                     ;@line 878
					_label30:
						CompareEQ ::temp178 ani ""                               ;@line 880
						Not ::temp178 ::temp178                                  ;@line 880
						JumpF ::temp178 _label31                                 ;@line 880
						PropGet SexLab ::libs_var ::temp185                      ;@line 881
						CallMethod GetAnimationObject ::temp185 ::temp186 ani    ;@line 881
						Assign ::temp184 ::temp186                               ;@line 881
						ArraySetElement Sanims 0 ::temp184                       ;@line 881
						ArrayGetElement ::temp184 Sanims 0                       ;@line 882
						Cast ::temp186 None                                      ;@line 882
						CompareEQ ::temp174 ::temp184 ::temp186                  ;@line 882
						JumpF ::temp174 _label32                                 ;@line 882
						CompareEQ ::temp187 AnimationName ""                     ;@line 883
						Not ::temp187 ::temp187                                  ;@line 883
						JumpF ::temp187 _label33                                 ;@line 883
						PropGet SexLab ::libs_var ::temp185                      ;@line 885
						CallMethod PickRandomSexScene self ::temp183             ;@line 885
						CallMethod GetAnimationObject ::temp185 ::temp184 ::temp183  ;@line 885
						Assign ::temp186 ::temp184                               ;@line 885
						ArraySetElement Sanims 0 ::temp186                       ;@line 885
						Jump _label33                                            ;@line 885
					_label33:
						ArrayGetElement ::temp186 Sanims 0                       ;@line 887
						Cast ::temp184 None                                      ;@line 887
						CompareEQ ::temp187 ::temp186 ::temp184                  ;@line 887
						JumpF ::temp187 _label34                                 ;@line 887
						CallMethod notify ::libs_var ::NoneVar "DDC 警告: 未找到性爱动画!" False  ;@line 888
						Return False                                             ;@line 889
						Jump _label34                                            ;@line 889
					_label34:
						Jump _label32                                            ;@line 889
					_label32:
						ArrayCreate ::temp188 2                                  ;@line 893
						Assign SceneSexActors ::temp188                          ;@line 893
						Assign ::temp176 ::user_var                              ;@line 894
						ArraySetElement SceneSexActors 0 ::temp176               ;@line 894
						Assign ::temp176 Partner                                 ;@line 895
						ArraySetElement SceneSexActors 1 ::temp176               ;@line 895
						PropGet SexLab ::libs_var ::temp185                      ;@line 896
						CallMethod StartSex ::temp185 ::temp189 SceneSexActors Sanims None None False ""  ;@line 896
						Return True                                              ;@line 897
						Jump _label31                                            ;@line 897
					_label31:
						Return False                                             ;@line 899
					.endCode
				.endFunction
				.function CheckSelfBondageRelease
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp23 Bool
						.local ::temp24 Float
						.local HoursNeeded Float
						.local HoursPassed Float
						.local ::temp25 actor
						.local ::temp26 Bool
						.local ::NoneVar None
						.local ::temp27 Int
						.local ::temp28 Bool
						.local HoursToWait Int
						.local ::temp29 String
					.endLocalTable
					.code
						Not ::temp23 ::isSelfBondage_var                         ;@line 199
						JumpF ::temp23 _label35                                  ;@line 199
						Return None                                              ;@line 200
						Jump _label35                                            ;@line 200
					_label35:
						Assign HoursNeeded ::SelfBondageReleaseTimer_var         ;@line 202
						CallStatic utility GetCurrentGameTime ::temp24           ;@line 203
						FSubtract ::temp24 ::temp24 ::ReleaseTimerStartedAt_var  ;@line 203
						FMultiply ::temp24 ::temp24 24.000000                    ;@line 203
						Assign HoursPassed ::temp24                              ;@line 203
						CompareGT ::temp23 HoursPassed HoursNeeded               ;@line 204
						JumpF ::temp23 _label36                                  ;@line 204
						PropGet PlayerRef ::libs_var ::temp25                    ;@line 205
						CompareEQ ::temp26 ::user_var ::temp25                   ;@line 205
						JumpF ::temp26 _label37                                  ;@line 205
						CallMethod notify ::libs_var ::NoneVar "213213123" True  ;@line 206
						Jump _label37                                            ;@line 206
					_label37:
						CallMethod UnlockActor self ::NoneVar                    ;@line 208
						Return None                                              ;@line 209
						Jump _label38                                            ;@line 209
					_label36:
						FSubtract ::temp24 HoursNeeded HoursPassed               ;@line 211
						CallStatic math Ceiling ::temp27 ::temp24                ;@line 211
						Assign HoursToWait ::temp27                              ;@line 211
						PropGet PlayerRef ::libs_var ::temp25                    ;@line 212
						CompareEQ ::temp26 ::user_var ::temp25                   ;@line 212
						Cast ::temp26 ::temp26                                   ;@line 212
						JumpF ::temp26 _label39                                  ;@line 212
						CompareEQ ::temp28 HoursToWait lasthourdisplayed         ;@line 212
						Not ::temp28 ::temp28                                    ;@line 212
						Cast ::temp26 ::temp28                                   ;@line 212
					_label39:
						JumpF ::temp26 _label40                                  ;@line 212
						Cast ::temp29 HoursToWait                                ;@line 213
						StrCat ::temp29 "123213123" ::temp29                     ;@line 213
						StrCat ::temp29 ::temp29 " 小时后才能解锁."       ;@line 213
						CallMethod notify ::libs_var ::NoneVar ::temp29 False    ;@line 213
						Assign lasthourdisplayed HoursToWait                     ;@line 214
						Jump _label40                                            ;@line 214
					_label40:
						Return None                                              ;@line 216
					_label38:
					.endCode
				.endFunction
				.function CalclulateStruggleSuccess
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
					.endParamTable
					.localTable
						.local ::temp282 Bool
						.local result Float
						.local ::temp283 Float
						.local ::temp284 actor
						.local ::temp285 Float
						.local ::temp286 Bool
						.local ::temp287 globalvariable
						.local ::temp288 Int
						.local EscapesMade Int
					.endLocalTable
					.code
						Assign result ::BaseEscapeChance_var                     ;@line 1201
						CompareGT ::temp282 ::BaseEscapeChance_var 0.000000      ;@line 1203
						JumpF ::temp282 _label41                                 ;@line 1203
						Cast ::temp283 EscapeStruggleAttemptsMade                ;@line 1205
						FAdd ::temp283 result ::temp283                          ;@line 1205
						Assign result ::temp283                                  ;@line 1205
						PropGet PlayerRef ::libs_var ::temp284                   ;@line 1207
						CallMethod GetAV ::temp284 ::temp283 "Stamina"           ;@line 1207
						Cast ::temp285 25                                        ;@line 1207
						CompareGT ::temp286 ::temp283 ::temp285                  ;@line 1207
						JumpF ::temp286 _label42                                 ;@line 1207
						FAdd ::temp285 result 1.000000                           ;@line 1208
						Assign result ::temp285                                  ;@line 1208
						Jump _label42                                            ;@line 1208
					_label42:
						PropGet PlayerRef ::libs_var ::temp284                   ;@line 1210
						CallMethod GetAV ::temp284 ::temp283 "Stamina"           ;@line 1210
						Cast ::temp285 50                                        ;@line 1210
						CompareGT ::temp286 ::temp283 ::temp285                  ;@line 1210
						JumpF ::temp286 _label43                                 ;@line 1210
						FAdd ::temp285 result 2.000000                           ;@line 1211
						Assign result ::temp285                                  ;@line 1211
						Jump _label43                                            ;@line 1211
					_label43:
						PropGet PlayerRef ::libs_var ::temp284                   ;@line 1213
						CallMethod GetAV ::temp284 ::temp283 "Stamina"           ;@line 1213
						Cast ::temp285 75                                        ;@line 1213
						CompareGT ::temp286 ::temp283 ::temp285                  ;@line 1213
						JumpF ::temp286 _label44                                 ;@line 1213
						FAdd ::temp285 result 3.000000                           ;@line 1214
						Assign result ::temp285                                  ;@line 1214
						Jump _label44                                            ;@line 1214
					_label44:
						PropGet zadDeviceEscapeSuccessCount ::libs_var ::temp287  ;@line 1217
						CallMethod GetValueInt ::temp287 ::temp288               ;@line 1217
						Assign EscapesMade ::temp288                             ;@line 1217
						CompareGT ::temp286 EscapesMade 10                       ;@line 1218
						JumpF ::temp286 _label45                                 ;@line 1218
						FAdd ::temp283 result 1.000000                           ;@line 1219
						Assign result ::temp283                                  ;@line 1219
						Jump _label45                                            ;@line 1219
					_label45:
						CompareGT ::temp286 EscapesMade 25                       ;@line 1221
						JumpF ::temp286 _label46                                 ;@line 1221
						FAdd ::temp285 result 1.000000                           ;@line 1222
						Assign result ::temp285                                  ;@line 1222
						Jump _label46                                            ;@line 1222
					_label46:
						CompareGT ::temp286 EscapesMade 50                       ;@line 1224
						JumpF ::temp286 _label47                                 ;@line 1224
						FAdd ::temp283 result 1.000000                           ;@line 1225
						Assign result ::temp283                                  ;@line 1225
						Jump _label47                                            ;@line 1225
					_label47:
						CompareGT ::temp286 EscapesMade 100                      ;@line 1227
						JumpF ::temp286 _label48                                 ;@line 1227
						FAdd ::temp285 result 1.000000                           ;@line 1228
						Assign result ::temp285                                  ;@line 1228
						Jump _label48                                            ;@line 1228
					_label48:
						Jump _label41                                            ;@line 1228
					_label41:
						CompareLT ::temp286 result 0.000000                      ;@line 1231
						JumpF ::temp286 _label49                                 ;@line 1231
						Return 0.000000                                          ;@line 1232
						Jump _label50                                            ;@line 1232
					_label49:
						CompareGT ::temp282 result 100.000000                    ;@line 1233
						JumpF ::temp282 _label50                                 ;@line 1233
						Return 100.000000                                        ;@line 1234
						Jump _label50                                            ;@line 1234
					_label50:
						Return result                                            ;@line 1236
					.endCode
				.endFunction
				.function PlaceRelative
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akObject ObjectReference
						.param akReference ObjectReference
						.param afDistance Float
						.param afAngle Float
					.endParamTable
					.localTable
						.local ::temp171 Float
						.local ::temp172 Float
						.local ::temp173 Float
						.local ::NoneVar None
						.local angle Float
					.endLocalTable
					.code
						CallMethod GetAngleZ akReference ::temp171               ;@line 848
						FAdd ::temp171 ::temp171 afAngle                         ;@line 848
						Assign angle ::temp171                                   ;@line 848
						CallStatic math Sin ::temp171 angle                      ;@line 849
						FMultiply ::temp171 afDistance ::temp171                 ;@line 849
						CallStatic math Cos ::temp172 angle                      ;@line 849
						FMultiply ::temp172 afDistance ::temp172                 ;@line 849
						Cast ::temp173 0                                         ;@line 849
						CallMethod moveto akObject ::NoneVar akReference ::temp171 ::temp172 ::temp173 False  ;@line 849
					.endCode
				.endFunction
				.function DeviceMenuUnlock
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp106 Int
						.local ::temp107 Bool
						.local i Int
						.local ::temp108 Bool
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod Show ::zadc_DeviceMsgPlayerLocked_var ::temp106 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 602
						Assign i ::temp106                                       ;@line 602
						CompareEQ ::temp107 i 0                                  ;@line 603
						JumpF ::temp107 _label51                                 ;@line 603
						CallMethod UnlockAttempt self ::temp108                  ;@line 605
						Jump _label52                                            ;@line 605
					_label51:
						CompareEQ ::temp108 i 1                                  ;@line 606
						JumpF ::temp108 _label53                                 ;@line 606
						CallMethod EscapeAttempt self ::NoneVar                  ;@line 608
						Jump _label52                                            ;@line 608
					_label53:
						CompareEQ ::temp108 i 2                                  ;@line 609
						JumpF ::temp108 _label52                                 ;@line 609
						CallMethod DisplayDifficultyMsg self ::NoneVar           ;@line 611
						Jump _label52                                            ;@line 611
					_label52:
					.endCode
				.endFunction
				.function EscapeAttemptStruggle
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp273 Bool
						.local ::temp274 actor
						.local ::temp278 Float
						.local ::temp275 Int
						.local ::temp276 referencealias
						.local ::temp277 ObjectReference
						.local ::NoneVar None
						.local ::temp279 Bool
						.local ::temp280 Bool
						.local ::temp281 String
					.endLocalTable
					.code
						CallMethod CanMakeStruggleEscapeAttempt self ::temp273   ;@line 1173
						Not ::temp273 ::temp273                                  ;@line 1173
						JumpF ::temp273 _label54                                 ;@line 1173
						Return None                                              ;@line 1174
						Jump _label54                                            ;@line 1174
					_label54:
						PropGet PlayerRef ::libs_var ::temp274                   ;@line 1176
						CompareEQ ::temp273 ::user_var ::temp274                 ;@line 1176
						JumpF ::temp273 _label55                                 ;@line 1176
						CallMethod Show ::zadc_EscapeStruggleMSG_var ::temp275 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1177
						Jump _label56                                            ;@line 1177
					_label55:
						PropGet UserRef ::clib_var ::temp276                     ;@line 1179
						Cast ::temp277 ::user_var                                ;@line 1179
						CallMethod ForceRefTo ::temp276 ::NoneVar ::temp277      ;@line 1179
						CallMethod Show ::zadc_EscapeStruggleNPCMSG_var ::temp275 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1180
						PropGet UserRef ::clib_var ::temp276                     ;@line 1181
						CallMethod Clear ::temp276 ::NoneVar                     ;@line 1181
					_label56:
						CallMethod CalclulateStruggleSuccess self ::temp278      ;@line 1183
						CallMethod Escape self ::temp273 ::temp278               ;@line 1183
						JumpF ::temp273 _label57                                 ;@line 1183
						CallMethod Show ::zadc_EscapeStruggleSuccessMSG_var ::temp275 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1184
						CallMethod SelfBondageReward self ::NoneVar              ;@line 1185
						CallMethod UnlockActor self ::NoneVar                    ;@line 1186
						Jump _label58                                            ;@line 1186
					_label57:
						CallStatic utility RandomFloat ::temp278 0.000000 99.900002  ;@line 1189
						CompareLT ::temp279 ::temp278 ::CatastrophicFailureChance_var  ;@line 1189
						Cast ::temp279 ::temp279                                 ;@line 1189
						JumpF ::temp279 _label59                                 ;@line 1189
						PropGet PlayerRef ::libs_var ::temp274                   ;@line 1189
						CompareEQ ::temp280 ::user_var ::temp274                 ;@line 1189
						Cast ::temp279 ::temp280                                 ;@line 1189
					_label59:
						JumpF ::temp279 _label60                                 ;@line 1189
						Assign ::BaseEscapeChance_var 0.000000                   ;@line 1190
						StrCat ::temp281 "你没能逃脱你的 " ::DeviceName_var  ;@line 1191
						StrCat ::temp281 ::temp281 " 并且因为你微弱的尝试导致装置收的如此之紧,以至于你将永远无法摆脱它."  ;@line 1191
						CallMethod notify ::libs_var ::NoneVar ::temp281 True    ;@line 1191
						Jump _label58                                            ;@line 1191
					_label60:
						IAdd ::temp275 EscapeStruggleAttemptsMade 1              ;@line 1194
						Assign EscapeStruggleAttemptsMade ::temp275              ;@line 1194
						CallMethod Show ::zadc_EscapeStruggleFailureMSG_var ::temp275 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1195
					_label58:
					.endCode
				.endFunction
				.function EscapeAttempt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp190 Int
						.local ::temp191 Bool
						.local ::temp192 Bool
						.local EscapeOption Int
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod Show ::zadc_EscapeDeviceMSG_var ::temp190 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 906
						Assign EscapeOption ::temp190                            ;@line 906
						CompareEQ ::temp191 EscapeOption 0                       ;@line 907
						JumpF ::temp191 _label61                                 ;@line 907
						CallMethod EscapeAttemptStruggle self ::NoneVar          ;@line 908
						Jump _label62                                            ;@line 908
					_label61:
						CompareEQ ::temp192 EscapeOption 1                       ;@line 909
						JumpF ::temp192 _label63                                 ;@line 909
						CallMethod EscapeAttemptLockPick self ::NoneVar          ;@line 910
						Jump _label62                                            ;@line 910
					_label63:
						CompareEQ ::temp192 EscapeOption 2                       ;@line 911
						JumpF ::temp192 _label64                                 ;@line 911
						CallMethod EscapeAttemptBreak self ::NoneVar             ;@line 912
						Jump _label62                                            ;@line 912
					_label64:
						CompareEQ ::temp192 EscapeOption 3                       ;@line 913
						JumpF ::temp192 _label62                                 ;@line 913
						CallMethod Show ::zadc_OnLeaveItLockedMSG_var ::temp190 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 914
						Jump _label62                                            ;@line 914
					_label62:
					.endCode
				.endFunction
				.function StruggleScene
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
					.endParamTable
					.localTable
						.local ::temp198 Bool
						.local ::temp199 cell
						.local ::temp200 actor
						.local ::temp201 cell
						.local ::temp202 Bool
						.local ::temp203 Package
						.local ::NoneVar None
						.local ::temp204 Float
					.endLocalTable
					.code
						Cast ::temp198 StruggleMutex                             ;@line 936
						JumpT ::temp198 _label65                                 ;@line 936
						CallMethod Is3DLoaded akActor ::temp198                  ;@line 936
						Not ::temp198 ::temp198                                  ;@line 936
						Cast ::temp198 ::temp198                                 ;@line 936
					_label65:
						Cast ::temp198 ::temp198                                 ;@line 936
						JumpT ::temp198 _label66                                 ;@line 936
						CallMethod GetParentCell akActor ::temp199               ;@line 936
						PropGet PlayerRef ::libs_var ::temp200                   ;@line 936
						CallMethod GetParentCell ::temp200 ::temp201             ;@line 936
						CompareEQ ::temp202 ::temp199 ::temp201                  ;@line 936
						Not ::temp202 ::temp202                                  ;@line 936
						Cast ::temp198 ::temp202                                 ;@line 936
					_label66:
						Cast ::temp198 ::temp198                                 ;@line 936
						JumpT ::temp198 _label67                                 ;@line 936
						CallMethod IsAnimating ::clib_var ::temp202 akActor      ;@line 936
						Cast ::temp198 ::temp202                                 ;@line 936
					_label67:
						JumpF ::temp198 _label68                                 ;@line 936
						Return None                                              ;@line 937
						Jump _label68                                            ;@line 937
					_label68:
						Assign StruggleMutex True                                ;@line 939
						CallMethod PickRandomStruggle self ::temp203             ;@line 940
						Assign CurrentStruggle ::temp203                         ;@line 940
						Not ::temp202 CurrentStruggle                            ;@line 941
						JumpF ::temp202 _label69                                 ;@line 941
						Return None                                              ;@line 943
						Jump _label69                                            ;@line 943
					_label69:
						CallStatic actorutil AddPackageOverride ::NoneVar akActor CurrentStruggle 100 0  ;@line 948
						CallMethod EvaluatePackage akActor ::NoneVar             ;@line 949
						Cast ::temp204 2                                         ;@line 950
						CallStatic utility Wait ::NoneVar ::temp204              ;@line 950
						CallMethod Pant ::libs_var ::NoneVar akActor             ;@line 951
						Cast ::temp204 2                                         ;@line 952
						CallStatic utility Wait ::NoneVar ::temp204              ;@line 952
						CallMethod SexlabMoan ::libs_var ::NoneVar akActor -1 None  ;@line 953
						Cast ::temp204 2                                         ;@line 954
						CallStatic utility Wait ::NoneVar ::temp204              ;@line 954
						CallMethod Pant ::libs_var ::NoneVar akActor             ;@line 955
						Cast ::temp204 2                                         ;@line 956
						CallStatic utility Wait ::NoneVar ::temp204              ;@line 956
						CallMethod SexlabMoan ::libs_var ::NoneVar akActor -1 None  ;@line 957
						Cast ::temp204 2                                         ;@line 958
						CallStatic utility Wait ::NoneVar ::temp204              ;@line 958
						CallStatic actorutil RemovePackageOverride ::temp198 akActor CurrentStruggle  ;@line 959
						CallMethod EvaluatePackage akActor ::NoneVar             ;@line 961
						CallMethod SexlabMoan ::libs_var ::NoneVar akActor -1 None  ;@line 962
						Assign StruggleMutex False                               ;@line 963
					.endCode
				.endFunction
				.function CalculateCooldownModifier
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param operator Bool
					.endParamTable
					.localTable
						.local ::temp205 Bool
						.local ::temp206 zadconfig
						.local ::temp207 Int
						.local ::temp208 String[]
						.local ::temp209 Float
						.local ::temp211 String
						.local ::temp212 String
						.local ::NoneVar None
						.local val Float
						.local mcmValue Int
						.local mcmLength Int
						.local median Int
						.local maxModifier Float
						.local StepLength Float
						.local Steps Int
						.local ::temp210 Float
					.endLocalTable
					.code
						Not ::temp205 ::AllowDifficultyModifier_var              ;@line 967
						JumpF ::temp205 _label70                                 ;@line 967
						CallMethod log ::libs_var ::NoneVar "未应用难度修正 - 模组制作者不允许!" 0  ;@line 968
						Return 1.000000                                          ;@line 969
						Jump _label70                                            ;@line 969
					_label70:
						Assign val 1.000000                                      ;@line 971
						PropGet Config ::libs_var ::temp206                      ;@line 972
						PropGet CooldownDifficulty ::temp206 ::temp207           ;@line 972
						Assign mcmValue ::temp207                                ;@line 972
						PropGet Config ::libs_var ::temp206                      ;@line 973
						PropGet EsccapeDifficultyList ::temp206 ::temp208        ;@line 973
						ArrayLength ::temp207 ::temp208                          ;@line 973
						Assign mcmLength ::temp207                               ;@line 973
						ISubtract ::temp207 mcmLength 1                          ;@line 974
						IDivide ::temp207 ::temp207 2                            ;@line 974
						Cast ::temp207 ::temp207                                 ;@line 974
						Assign median ::temp207                                  ;@line 974
						Assign maxModifier 0.900000                              ;@line 975
						Cast ::temp209 median                                    ;@line 976
						FDivide ::temp209 maxModifier ::temp209                  ;@line 976
						Assign StepLength ::temp209                              ;@line 976
						ISubtract ::temp207 mcmValue median                      ;@line 977
						Assign Steps ::temp207                                   ;@line 977
						JumpF operator _label71                                  ;@line 978
						Cast ::temp209 Steps                                     ;@line 979
						FMultiply ::temp209 ::temp209 StepLength                 ;@line 979
						Cast ::temp210 1                                         ;@line 979
						FAdd ::temp210 ::temp210 ::temp209                       ;@line 979
						Assign val ::temp210                                     ;@line 979
						Jump _label72                                            ;@line 979
					_label71:
						Cast ::temp209 Steps                                     ;@line 981
						FMultiply ::temp210 ::temp209 StepLength                 ;@line 981
						Cast ::temp209 1                                         ;@line 981
						FSubtract ::temp209 ::temp209 ::temp210                  ;@line 981
						Assign val ::temp209                                     ;@line 981
					_label72:
						Cast ::temp211 val                                       ;@line 983
						StrCat ::temp211 "应用难度修正: " ::temp211        ;@line 983
						StrCat ::temp211 ::temp211 " [设置: "                  ;@line 983
						Cast ::temp212 mcmValue                                  ;@line 983
						StrCat ::temp212 ::temp211 ::temp212                     ;@line 983
						StrCat ::temp211 ::temp212 "]"                           ;@line 983
						CallMethod log ::libs_var ::NoneVar ::temp211 0          ;@line 983
						Return val                                               ;@line 984
					.endCode
				.endFunction
				.function CanMakeLockPickEscapeAttempt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp246 Float
						.local ::temp247 Bool
						.local HoursNeeded Float
						.local HoursPassed Float
						.local ::temp248 Int
						.local ::temp249 actor
						.local ::temp250 Bool
						.local ::temp251 Bool
						.local ::temp253 Bool
						.local HoursToWait Int
						.local ::temp252 String
						.local ::NoneVar None
						.local ::temp254 actorbase
						.local ::temp255 String
					.endLocalTable
					.code
						CallMethod CalculateCooldownModifier self ::temp246 False  ;@line 1109
						FMultiply ::temp246 ::EscapeCooldown_var ::temp246       ;@line 1109
						Assign HoursNeeded ::temp246                             ;@line 1109
						CallStatic utility GetCurrentGameTime ::temp246          ;@line 1110
						FSubtract ::temp246 ::temp246 LastLockPickEscapeAttemptAt  ;@line 1110
						FMultiply ::temp246 ::temp246 24.000000                  ;@line 1110
						Assign HoursPassed ::temp246                             ;@line 1110
						CompareGT ::temp247 HoursPassed HoursNeeded              ;@line 1111
						JumpF ::temp247 _label73                                 ;@line 1111
						CallStatic utility GetCurrentGameTime ::temp246          ;@line 1112
						Assign LastLockPickEscapeAttemptAt ::temp246             ;@line 1112
						Return True                                              ;@line 1113
						Jump _label74                                            ;@line 1113
					_label73:
						FSubtract ::temp246 HoursNeeded HoursPassed              ;@line 1115
						CallStatic math Ceiling ::temp248 ::temp246              ;@line 1115
						Assign HoursToWait ::temp248                             ;@line 1115
						PropGet PlayerRef ::libs_var ::temp249                   ;@line 1116
						CompareEQ ::temp250 ::user_var ::temp249                 ;@line 1116
						Cast ::temp250 ::temp250                                 ;@line 1116
						JumpF ::temp250 _label75                                 ;@line 1116
						FSubtract ::temp246 HoursNeeded HoursPassed              ;@line 1116
						CompareGTE ::temp251 ::temp246 1.000000                  ;@line 1116
						Cast ::temp250 ::temp251                                 ;@line 1116
					_label75:
						JumpF ::temp250 _label76                                 ;@line 1116
						Cast ::temp252 HoursToWait                               ;@line 1117
						StrCat ::temp252 "你不能在上次尝试后这么快就尝试选择该设备! 你可以在大约 " ::temp252  ;@line 1117
						StrCat ::temp252 ::temp252 " 小时后重试."           ;@line 1117
						CallMethod notify ::libs_var ::NoneVar ::temp252 True    ;@line 1117
						Jump _label74                                            ;@line 1117
					_label76:
						PropGet PlayerRef ::libs_var ::temp249                   ;@line 1118
						CompareEQ ::temp251 ::user_var ::temp249                 ;@line 1118
						Cast ::temp251 ::temp251                                 ;@line 1118
						JumpF ::temp251 _label77                                 ;@line 1118
						FSubtract ::temp246 HoursNeeded HoursPassed              ;@line 1118
						CompareLT ::temp253 ::temp246 1.000000                   ;@line 1118
						Cast ::temp251 ::temp253                                 ;@line 1118
					_label77:
						JumpF ::temp251 _label78                                 ;@line 1118
						CallMethod notify ::libs_var ::NoneVar "你不能在上次尝试后这么快就尝试选择该设备! 你很快就可以再试一次!" True  ;@line 1119
						Jump _label74                                            ;@line 1119
					_label78:
						CallMethod GetLeveledActorBase ::user_var ::temp254      ;@line 1121
						CallMethod GetName ::temp254 ::temp252                   ;@line 1121
						StrCat ::temp252 "你不能帮助 " ::temp252            ;@line 1121
						StrCat ::temp252 ::temp252 " 在上次尝试后不久就尝试帮她选择此设备! 你可以在大约 "  ;@line 1121
						Cast ::temp255 HoursToWait                               ;@line 1121
						StrCat ::temp255 ::temp252 ::temp255                     ;@line 1121
						StrCat ::temp252 ::temp255 " 小时后重试."           ;@line 1121
						CallMethod notify ::libs_var ::NoneVar ::temp252 True    ;@line 1121
					_label74:
						Return False                                             ;@line 1124
					.endCode
				.endFunction
				.function CheckLockAccess
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp79 Bool
						.local ::temp80 Bool
						.local ::temp81 Float
						.local ::temp82 Bool
						.local ModValue Float
						.local ::temp83 key
						.local ::temp84 Bool
						.local ::temp86 Bool
						.local ::temp85 String
						.local ::NoneVar None
					.endLocalTable
					.code
						CompareGT ::temp79 ::LockAccessDifficulty_var 0.000000   ;@line 430
						JumpF ::temp79 _label79                                  ;@line 430
						CallMethod CanMakeUnlockAttempt self ::temp80            ;@line 431
						Not ::temp80 ::temp80                                    ;@line 431
						JumpF ::temp80 _label80                                  ;@line 431
						Return False                                             ;@line 432
						Jump _label80                                            ;@line 432
					_label80:
						CallMethod CalculateDifficultyModifier self ::temp81 False  ;@line 434
						FMultiply ::temp81 ::LockAccessDifficulty_var ::temp81   ;@line 434
						Assign ModValue ::temp81                                 ;@line 434
						CompareLT ::temp80 ::LockAccessDifficulty_var 100.000000  ;@line 435
						Cast ::temp80 ::temp80                                   ;@line 435
						JumpF ::temp80 _label81                                  ;@line 435
						CompareGTE ::temp82 ModValue 100.000000                  ;@line 435
						Cast ::temp80 ::temp82                                   ;@line 435
					_label81:
						JumpF ::temp80 _label82                                  ;@line 435
						Assign ModValue 95.000000                                ;@line 437
						Jump _label82                                            ;@line 437
					_label82:
						CallStatic utility RandomFloat ::temp81 0.000000 99.900002  ;@line 439
						CompareLT ::temp82 ::temp81 ModValue                     ;@line 439
						JumpF ::temp82 _label83                                  ;@line 439
						Cast ::temp83 None                                       ;@line 440
						CompareEQ ::temp80 ::deviceKey_var ::temp83              ;@line 440
						Not ::temp80 ::temp80                                    ;@line 440
						JumpF ::temp80 _label84                                  ;@line 440
						CompareLT ::temp84 ::LockAccessDifficulty_var 50.000000  ;@line 441
						JumpF ::temp84 _label85                                  ;@line 441
						StrCat ::temp85 "1212" ::DeviceName_var                  ;@line 442
						StrCat ::temp85 ::temp85 "'的锁孔,但发现锁孔有点超出你能够到的范围.在几次尝试将钥匙滑入锁中失败后,你别无选择,只能暂时放弃.但是你最终应该仍然能够解锁它.请稍后再试!"  ;@line 442
						CallMethod notify ::libs_var ::NoneVar ::temp85 True     ;@line 442
						Jump _label86                                            ;@line 442
					_label85:
						CompareLT ::temp86 ::LockAccessDifficulty_var 100.000000  ;@line 443
						JumpF ::temp86 _label87                                  ;@line 443
						StrCat ::temp85 "这个装置的设计目的是让戴着它的人很难接近锁孔来解锁自己.你费尽心思将钥匙插入" ::DeviceName_var  ;@line 444
						StrCat ::temp85 ::temp85 "'的锁孔中 反正..,但发现锁孔远在你够不到的地方.你已经厌倦了挣扎,现在别无选择,只能放弃.也许你可以稍后再试一次!"  ;@line 444
						CallMethod notify ::libs_var ::NoneVar ::temp85 True     ;@line 444
						Jump _label86                                            ;@line 444
					_label87:
						CallMethod notify ::libs_var ::NoneVar "该装置的设计目的是将锁安全地放置在佩戴者接触不到的地方.即使拥有正确的钥匙,你也无法自行解锁.你需要寻求帮助!" True  ;@line 446
					_label86:
						Jump _label88                                            ;@line 446
					_label84:
						CallMethod notify ::libs_var ::NoneVar "你试图逃离该装置,但无法触及其锁定机制!" True  ;@line 449
					_label88:
						Return False                                             ;@line 451
						Jump _label83                                            ;@line 451
					_label83:
						Jump _label79                                            ;@line 451
					_label79:
						Return True                                              ;@line 454
					.endCode
				.endFunction
				.function PickRandomSexScene
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return String
					.paramTable
					.endParamTable
					.localTable
						.local ::temp4 Int
						.local ::temp5 Bool
						.local ::temp6 String
					.endLocalTable
					.code
						ArrayLength ::temp4 ::SexAnimations_var                  ;@line 152
						CompareEQ ::temp5 ::temp4 0                              ;@line 152
						JumpF ::temp5 _label89                                   ;@line 152
						Return ""                                                ;@line 153
						Jump _label89                                            ;@line 153
					_label89:
						ArrayLength ::temp4 ::SexAnimations_var                  ;@line 155
						ISubtract ::temp4 ::temp4 1                              ;@line 155
						CallStatic utility RandomInt ::temp4 0 ::temp4           ;@line 155
						ArrayGetElement ::temp6 ::SexAnimations_var ::temp4      ;@line 155
						Return ::temp6                                           ;@line 155
					.endCode
				.endFunction
				.function CalclulateBreakSuccess
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
					.endParamTable
					.localTable
						.local ::temp296 Bool
						.local result Float
						.local ::temp297 Float
						.local ::temp298 actor
						.local ::temp299 Float
						.local ::temp300 Bool
						.local ::temp301 Bool
						.local ::temp302 globalvariable
						.local ::temp303 Int
						.local EscapesMade Int
					.endLocalTable
					.code
						Assign result ::BreakDeviceEscapeChance_var              ;@line 1262
						CompareGT ::temp296 ::BreakDeviceEscapeChance_var 0.000000  ;@line 1264
						JumpF ::temp296 _label90                                 ;@line 1264
						Cast ::temp297 EscapeBreakAttemptsMade                   ;@line 1266
						FAdd ::temp297 result ::temp297                          ;@line 1266
						Assign result ::temp297                                  ;@line 1266
						PropGet PlayerRef ::libs_var ::temp298                   ;@line 1267
						CallMethod GetAV ::temp298 ::temp297 "OneHanded"         ;@line 1267
						Cast ::temp299 25                                        ;@line 1267
						CompareGT ::temp300 ::temp297 ::temp299                  ;@line 1267
						Cast ::temp300 ::temp300                                 ;@line 1267
						JumpT ::temp300 _label91                                 ;@line 1267
						PropGet PlayerRef ::libs_var ::temp298                   ;@line 1267
						CallMethod GetAV ::temp298 ::temp299 "TwoHanded"         ;@line 1267
						Cast ::temp297 25                                        ;@line 1267
						CompareGT ::temp301 ::temp299 ::temp297                  ;@line 1267
						Cast ::temp300 ::temp301                                 ;@line 1267
					_label91:
						JumpF ::temp300 _label92                                 ;@line 1267
						FAdd ::temp297 result 1.000000                           ;@line 1268
						Assign result ::temp297                                  ;@line 1268
						Jump _label92                                            ;@line 1268
					_label92:
						PropGet PlayerRef ::libs_var ::temp298                   ;@line 1270
						CallMethod GetAV ::temp298 ::temp299 "OneHanded"         ;@line 1270
						Cast ::temp297 50                                        ;@line 1270
						CompareGT ::temp301 ::temp299 ::temp297                  ;@line 1270
						Cast ::temp301 ::temp301                                 ;@line 1270
						JumpT ::temp301 _label93                                 ;@line 1270
						PropGet PlayerRef ::libs_var ::temp298                   ;@line 1270
						CallMethod GetAV ::temp298 ::temp297 "TwoHanded"         ;@line 1270
						Cast ::temp299 50                                        ;@line 1270
						CompareGT ::temp300 ::temp297 ::temp299                  ;@line 1270
						Cast ::temp301 ::temp300                                 ;@line 1270
					_label93:
						JumpF ::temp301 _label94                                 ;@line 1270
						FAdd ::temp299 result 2.000000                           ;@line 1271
						Assign result ::temp299                                  ;@line 1271
						Jump _label94                                            ;@line 1271
					_label94:
						PropGet PlayerRef ::libs_var ::temp298                   ;@line 1273
						CallMethod GetAV ::temp298 ::temp297 "OneHanded"         ;@line 1273
						Cast ::temp299 75                                        ;@line 1273
						CompareGT ::temp300 ::temp297 ::temp299                  ;@line 1273
						Cast ::temp300 ::temp300                                 ;@line 1273
						JumpT ::temp300 _label95                                 ;@line 1273
						PropGet PlayerRef ::libs_var ::temp298                   ;@line 1273
						CallMethod GetAV ::temp298 ::temp299 "TwoHanded"         ;@line 1273
						Cast ::temp297 75                                        ;@line 1273
						CompareGT ::temp301 ::temp299 ::temp297                  ;@line 1273
						Cast ::temp300 ::temp301                                 ;@line 1273
					_label95:
						JumpF ::temp300 _label96                                 ;@line 1273
						FAdd ::temp297 result 3.000000                           ;@line 1274
						Assign result ::temp297                                  ;@line 1274
						Jump _label96                                            ;@line 1274
					_label96:
						PropGet zadDeviceEscapeSuccessCount ::libs_var ::temp302  ;@line 1277
						CallMethod GetValueInt ::temp302 ::temp303               ;@line 1277
						Assign EscapesMade ::temp303                             ;@line 1277
						CompareGT ::temp301 EscapesMade 10                       ;@line 1278
						JumpF ::temp301 _label97                                 ;@line 1278
						FAdd ::temp299 result 1.000000                           ;@line 1279
						Assign result ::temp299                                  ;@line 1279
						Jump _label97                                            ;@line 1279
					_label97:
						CompareGT ::temp300 EscapesMade 25                       ;@line 1281
						JumpF ::temp300 _label98                                 ;@line 1281
						FAdd ::temp297 result 1.000000                           ;@line 1282
						Assign result ::temp297                                  ;@line 1282
						Jump _label98                                            ;@line 1282
					_label98:
						CompareGT ::temp301 EscapesMade 50                       ;@line 1284
						JumpF ::temp301 _label99                                 ;@line 1284
						FAdd ::temp299 result 1.000000                           ;@line 1285
						Assign result ::temp299                                  ;@line 1285
						Jump _label99                                            ;@line 1285
					_label99:
						CompareGT ::temp300 EscapesMade 100                      ;@line 1287
						JumpF ::temp300 _label100                                ;@line 1287
						FAdd ::temp297 result 1.000000                           ;@line 1288
						Assign result ::temp297                                  ;@line 1288
						Jump _label100                                           ;@line 1288
					_label100:
						Jump _label90                                            ;@line 1288
					_label90:
						CompareLT ::temp301 result 0.000000                      ;@line 1291
						JumpF ::temp301 _label101                                ;@line 1291
						Return 0.000000                                          ;@line 1292
						Jump _label102                                           ;@line 1292
					_label101:
						CompareGT ::temp300 result 100.000000                    ;@line 1293
						JumpF ::temp300 _label102                                ;@line 1293
						Return 100.000000                                        ;@line 1294
						Jump _label102                                           ;@line 1294
					_label102:
						Return result                                            ;@line 1296
					.endCode
				.endFunction
				.function ApplyDevices
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
					.endParamTable
					.localTable
						.local ::temp30 Int
						.local ::temp31 Bool
						.local i Int
						.local ::temp32 armor
						.local ::temp33 keyword
						.local ::temp34 Bool
						.local ::temp35 armor
						.local ::temp36 armor
						.local ::NoneVar None
						.local ::temp37 form
					.endLocalTable
					.code
						ArrayLength ::temp30 ::EquipDevices_var                  ;@line 221
						Assign i ::temp30                                        ;@line 221
					_label106:
						CompareGT ::temp31 i 0                                   ;@line 222
						JumpF ::temp31 _label103                                 ;@line 222
						ISubtract ::temp30 i 1                                   ;@line 223
						Assign i ::temp30                                        ;@line 223
						ArrayGetElement ::temp32 ::EquipDevices_var i            ;@line 224
						PropGet zad_InventoryDevice ::libs_var ::temp33          ;@line 224
						CallMethod HasKeyword ::temp32 ::temp34 ::temp33         ;@line 224
						JumpF ::temp34 _label104                                 ;@line 224
						ArrayGetElement ::temp32 ::EquipDevices_var i            ;@line 225
						ArrayGetElement ::temp35 ::EquipDevices_var i            ;@line 225
						CallMethod GetRenderedDevice ::libs_var ::temp35 ::temp35  ;@line 225
						ArrayGetElement ::temp36 ::EquipDevices_var i            ;@line 225
						CallMethod GetDeviceKeyword ::libs_var ::temp33 ::temp36  ;@line 225
						CallMethod EquipDevice ::libs_var ::NoneVar akActor ::temp32 ::temp35 ::temp33 False True  ;@line 225
						Jump _label105                                           ;@line 225
					_label104:
						ArrayGetElement ::temp36 ::EquipDevices_var i            ;@line 227
						Cast ::temp37 ::temp36                                   ;@line 227
						CallMethod EquipItem akActor ::NoneVar ::temp37 False True  ;@line 227
					_label105:
						Jump _label106                                           ;@line 227
					_label103:
					.endCode
				.endFunction
				.function RemoveEffects
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
					.endParamTable
					.localTable
						.local ::temp11 Int
						.local ::temp12 Bool
						.local i Int
						.local ::temp13 spell
						.local ::temp14 Bool
					.endLocalTable
					.code
						ArrayLength ::temp11 ::AppliedSpellEffects_var           ;@line 167
						Assign i ::temp11                                        ;@line 167
					_label108:
						CompareGT ::temp12 i 0                                   ;@line 168
						JumpF ::temp12 _label107                                 ;@line 168
						ISubtract ::temp11 i 1                                   ;@line 169
						Assign i ::temp11                                        ;@line 169
						ArrayGetElement ::temp13 ::AppliedSpellEffects_var i     ;@line 170
						CallMethod RemoveSpell akActor ::temp14 ::temp13         ;@line 170
						Jump _label108                                           ;@line 170
					_label107:
					.endCode
				.endFunction
				.function CheckLockShield
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp19 Bool
						.local ::temp20 Float
						.local HoursNeeded Float
						.local HoursPassed Float
						.local ::temp21 Int
						.local ::temp22 String
						.local ::NoneVar None
						.local HoursToWait Int
					.endLocalTable
					.code
						CompareEQ ::temp19 LockShieldTimer 0.000000              ;@line 184
						JumpF ::temp19 _label109                                 ;@line 184
						Return True                                              ;@line 185
						Jump _label109                                           ;@line 185
					_label109:
						Assign HoursNeeded LockShieldTimer                       ;@line 187
						CallStatic utility GetCurrentGameTime ::temp20           ;@line 188
						FSubtract ::temp20 ::temp20 LockShieldStartedAt          ;@line 188
						FMultiply ::temp20 ::temp20 24.000000                    ;@line 188
						Assign HoursPassed ::temp20                              ;@line 188
						CompareGT ::temp19 HoursPassed HoursNeeded               ;@line 189
						JumpF ::temp19 _label110                                 ;@line 189
						Return True                                              ;@line 190
						Jump _label111                                           ;@line 190
					_label110:
						FSubtract ::temp20 HoursNeeded HoursPassed               ;@line 192
						CallStatic math Ceiling ::temp21 ::temp20                ;@line 192
						Assign HoursToWait ::temp21                              ;@line 192
						Cast ::temp22 HoursToWait                                ;@line 193
						StrCat ::temp22 "23131231231" ::temp22                   ;@line 193
						StrCat ::temp22 ::temp22 " 小时后尝试解锁此装置."  ;@line 193
						CallMethod notify ::libs_var ::NoneVar ::temp22 True     ;@line 193
						Return False                                             ;@line 194
					_label111:
					.endCode
				.endFunction
				.function OnUpdate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp150 Int
						.local ::temp151 Bool
						.local ::temp152 Float
						.local ::NoneVar None
						.local ::temp153 Bool
					.endLocalTable
					.code
						IAdd ::temp150 StruggleTick 1                            ;@line 783
						Assign StruggleTick ::temp150                            ;@line 783
						CompareGT ::temp151 StruggleTick 1                       ;@line 784
						JumpF ::temp151 _label112                                ;@line 784
						Assign StruggleTick 0                                    ;@line 785
						Jump _label112                                           ;@line 785
					_label112:
						CallStatic utility RandomInt ::temp150 20 40             ;@line 787
						Cast ::temp152 ::temp150                                 ;@line 787
						CallMethod RegisterForSingleUpdate self ::NoneVar ::temp152  ;@line 787
						CallMethod PasserbyAction self ::temp151                 ;@line 788
						Not ::temp151 ::temp151                                  ;@line 788
						Cast ::temp151 ::temp151                                 ;@line 788
						JumpF ::temp151 _label113                                ;@line 788
						CompareEQ ::temp153 StruggleTick 0                       ;@line 788
						Cast ::temp151 ::temp153                                 ;@line 788
					_label113:
						JumpF ::temp151 _label114                                ;@line 788
						CallMethod StruggleScene self ::NoneVar ::user_var       ;@line 789
						Jump _label114                                           ;@line 789
					_label114:
						CallMethod IsAnimating ::clib_var ::temp153 ::user_var   ;@line 791
						Not ::temp151 ::temp153                                  ;@line 791
						JumpF ::temp151 _label115                                ;@line 791
						CallMethod CheckSelfBondageRelease self ::NoneVar        ;@line 793
						Jump _label115                                           ;@line 793
					_label115:
					.endCode
				.endFunction
				.function EscapeAttemptNPC
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp193 referencealias
						.local ::temp194 ObjectReference
						.local ::NoneVar None
						.local ::temp195 Int
						.local ::temp196 Bool
						.local ::temp197 Bool
						.local EscapeOption Int
					.endLocalTable
					.code
						PropGet UserRef ::clib_var ::temp193                     ;@line 919
						Cast ::temp194 ::user_var                                ;@line 919
						CallMethod ForceRefTo ::temp193 ::NoneVar ::temp194      ;@line 919
						CallMethod Show ::zadc_EscapeDeviceNPCMSG_var ::temp195 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 920
						Assign EscapeOption ::temp195                            ;@line 920
						PropGet UserRef ::clib_var ::temp193                     ;@line 921
						CallMethod Clear ::temp193 ::NoneVar                     ;@line 921
						CompareEQ ::temp196 EscapeOption 0                       ;@line 922
						JumpF ::temp196 _label116                                ;@line 922
						CallMethod EscapeAttemptStruggle self ::NoneVar          ;@line 923
						Jump _label117                                           ;@line 923
					_label116:
						CompareEQ ::temp197 EscapeOption 1                       ;@line 924
						JumpF ::temp197 _label118                                ;@line 924
						CallMethod EscapeAttemptLockPick self ::NoneVar          ;@line 925
						Jump _label117                                           ;@line 925
					_label118:
						CompareEQ ::temp197 EscapeOption 2                       ;@line 926
						JumpF ::temp197 _label119                                ;@line 926
						CallMethod EscapeAttemptBreak self ::NoneVar             ;@line 927
						Jump _label117                                           ;@line 927
					_label119:
						CompareEQ ::temp197 EscapeOption 3                       ;@line 928
						JumpF ::temp197 _label117                                ;@line 928
						CallMethod Show ::zadc_OnLeaveItLockedMSG_var ::temp195 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 929
						Jump _label117                                           ;@line 929
					_label117:
					.endCode
				.endFunction
				.function DestroyLockPick
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp317 actor
						.local ::temp318 miscobject
						.local ::temp319 form
						.local ::temp320 Int
						.local ::temp321 Bool
						.local ::temp322 Bool
						.local LockPickDestroyed Bool
						.local ::NoneVar None
						.local i Int
						.local ::temp323 keyword
						.local ::temp324 Bool
						.local frm form
					.endLocalTable
					.code
						Assign LockPickDestroyed False                           ;@line 1349
						Cast ::temp321 ::AllowLockPick_var                       ;@line 1350
						JumpF ::temp321 _label120                                ;@line 1350
						PropGet PlayerRef ::libs_var ::temp317                   ;@line 1350
						PropGet Lockpick ::libs_var ::temp318                    ;@line 1350
						Cast ::temp319 ::temp318                                 ;@line 1350
						CallMethod GetItemCount ::temp317 ::temp320 ::temp319    ;@line 1350
						CompareGT ::temp321 ::temp320 0                          ;@line 1350
						Cast ::temp321 ::temp321                                 ;@line 1350
					_label120:
						JumpF ::temp321 _label121                                ;@line 1350
						PropGet PlayerRef ::libs_var ::temp317                   ;@line 1351
						PropGet Lockpick ::libs_var ::temp318                    ;@line 1351
						Cast ::temp319 ::temp318                                 ;@line 1351
						CallMethod RemoveItem ::temp317 ::NoneVar ::temp319 1 False None  ;@line 1351
						Return True                                              ;@line 1352
						Jump _label121                                           ;@line 1352
					_label121:
						ArrayLength ::temp320 ::AllowedLockPicks_var             ;@line 1354
						Assign i ::temp320                                       ;@line 1354
					_label126:
						CompareGT ::temp321 i 0                                  ;@line 1355
						Cast ::temp321 ::temp321                                 ;@line 1355
						JumpF ::temp321 _label122                                ;@line 1355
						Not ::temp322 LockPickDestroyed                          ;@line 1355
						Cast ::temp321 ::temp322                                 ;@line 1355
					_label122:
						JumpF ::temp321 _label123                                ;@line 1355
						ISubtract ::temp320 i 1                                  ;@line 1356
						Assign i ::temp320                                       ;@line 1356
						ArrayGetElement ::temp319 ::AllowedLockPicks_var i       ;@line 1357
						Assign frm ::temp319                                     ;@line 1357
						PropGet PlayerRef ::libs_var ::temp317                   ;@line 1358
						CallMethod GetItemCount ::temp317 ::temp320 frm          ;@line 1358
						CompareGT ::temp322 ::temp320 0                          ;@line 1358
						Cast ::temp322 ::temp322                                 ;@line 1358
						JumpF ::temp322 _label124                                ;@line 1358
						Cast ::temp323 frm                                       ;@line 1358
						Not ::temp324 ::temp323                                  ;@line 1358
						Cast ::temp322 ::temp324                                 ;@line 1358
					_label124:
						JumpF ::temp322 _label125                                ;@line 1358
						PropGet PlayerRef ::libs_var ::temp317                   ;@line 1359
						CallMethod RemoveItem ::temp317 ::NoneVar frm 1 False None  ;@line 1359
						Assign LockPickDestroyed True                            ;@line 1360
						Jump _label125                                           ;@line 1360
					_label125:
						Jump _label126                                           ;@line 1360
					_label123:
						Return LockPickDestroyed                                 ;@line 1363
					.endCode
				.endFunction
				.function FaceObject
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akObject ObjectReference
						.param akReference ObjectReference
						.param afOffset Float
					.endParamTable
					.localTable
						.local ::temp168 Float
						.local ::temp169 Float
						.local ::temp170 Float
						.local ::NoneVar None
						.local angle Float
					.endLocalTable
					.code
						CallMethod GetHeadingAngle akObject ::temp168 akReference  ;@line 843
						Assign angle ::temp168                                   ;@line 843
						CallMethod GetAngleX akObject ::temp168                  ;@line 844
						CallMethod GetAngleY akObject ::temp169                  ;@line 844
						CallMethod GetAngleZ akObject ::temp170                  ;@line 844
						FAdd ::temp170 ::temp170 angle                           ;@line 844
						FAdd ::temp170 ::temp170 afOffset                        ;@line 844
						CallMethod SetAngle akObject ::NoneVar ::temp168 ::temp169 ::temp170  ;@line 844
					.endCode
				.endFunction
				.function CalculateDifficultyModifier
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
						.param operator Bool
					.endParamTable
					.localTable
						.local ::temp213 Bool
						.local ::temp214 zadconfig
						.local ::temp215 Int
						.local ::temp216 String[]
						.local ::temp217 Float
						.local ::temp219 String
						.local ::temp220 String
						.local ::NoneVar None
						.local val Float
						.local mcmValue Int
						.local mcmLength Int
						.local median Int
						.local maxModifier Float
						.local StepLength Float
						.local Steps Int
						.local ::temp218 Float
					.endLocalTable
					.code
						Not ::temp213 ::AllowDifficultyModifier_var              ;@line 988
						JumpF ::temp213 _label127                                ;@line 988
						CallMethod log ::libs_var ::NoneVar "未应用难度修正 - 模组制作者不允许!" 0  ;@line 989
						Return 1.000000                                          ;@line 990
						Jump _label127                                           ;@line 990
					_label127:
						Assign val 1.000000                                      ;@line 992
						PropGet Config ::libs_var ::temp214                      ;@line 993
						PropGet EscapeDifficulty ::temp214 ::temp215             ;@line 993
						Assign mcmValue ::temp215                                ;@line 993
						PropGet Config ::libs_var ::temp214                      ;@line 994
						PropGet EsccapeDifficultyList ::temp214 ::temp216        ;@line 994
						ArrayLength ::temp215 ::temp216                          ;@line 994
						Assign mcmLength ::temp215                               ;@line 994
						ISubtract ::temp215 mcmLength 1                          ;@line 995
						IDivide ::temp215 ::temp215 2                            ;@line 995
						Cast ::temp215 ::temp215                                 ;@line 995
						Assign median ::temp215                                  ;@line 995
						Assign maxModifier 0.750000                              ;@line 996
						Cast ::temp217 median                                    ;@line 997
						FDivide ::temp217 maxModifier ::temp217                  ;@line 997
						Assign StepLength ::temp217                              ;@line 997
						ISubtract ::temp215 mcmValue median                      ;@line 998
						Assign Steps ::temp215                                   ;@line 998
						JumpF operator _label128                                 ;@line 999
						Cast ::temp217 Steps                                     ;@line 1000
						FMultiply ::temp217 ::temp217 StepLength                 ;@line 1000
						Cast ::temp218 1                                         ;@line 1000
						FAdd ::temp218 ::temp218 ::temp217                       ;@line 1000
						Assign val ::temp218                                     ;@line 1000
						Jump _label129                                           ;@line 1000
					_label128:
						Cast ::temp217 Steps                                     ;@line 1002
						FMultiply ::temp218 ::temp217 StepLength                 ;@line 1002
						Cast ::temp217 1                                         ;@line 1002
						FSubtract ::temp217 ::temp217 ::temp218                  ;@line 1002
						Assign val ::temp217                                     ;@line 1002
					_label129:
						Cast ::temp219 val                                       ;@line 1004
						StrCat ::temp219 "应用难度修正: " ::temp219        ;@line 1004
						StrCat ::temp219 ::temp219 " [设置: "                  ;@line 1004
						Cast ::temp220 mcmValue                                  ;@line 1004
						StrCat ::temp220 ::temp219 ::temp220                     ;@line 1004
						StrCat ::temp219 ::temp220 "]"                           ;@line 1004
						CallMethod log ::libs_var ::NoneVar ::temp219 0          ;@line 1004
						Return val                                               ;@line 1005
					.endCode
				.endFunction
				.function MoveToBehind
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akObjB ObjectReference
						.param akObjA ObjectReference
						.param afDistance Float
					.endParamTable
					.localTable
						.local ::temp163 Float
						.local ::temp164 Float
						.local ::temp165 Float
						.local ::NoneVar None
					.endLocalTable
					.code
						FNegate ::temp163 afDistance                             ;@line 835
						CallMethod GetAngleZ akObjB ::temp164                    ;@line 835
						CallStatic math Sin ::temp164 ::temp164                  ;@line 835
						FMultiply ::temp163 ::temp163 ::temp164                  ;@line 835
						FNegate ::temp164 afDistance                             ;@line 835
						CallMethod GetAngleZ akObjB ::temp165                    ;@line 835
						CallStatic math Cos ::temp165 ::temp165                  ;@line 835
						FMultiply ::temp164 ::temp164 ::temp165                  ;@line 835
						CallMethod moveto akObjA ::NoneVar akObjB ::temp163 ::temp164 0.000000 True  ;@line 835
					.endCode
				.endFunction
				.function OnSexEnd
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
						.local ::temp154 sexlabframework
						.local ::temp155 sslthreadcontroller
						.local ::temp156 actor[]
						.local ::temp157 Int
						.local ::temp158 Bool
						.local ::temp161 Float
						.local ::NoneVar None
						.local ::temp162 Package
						.local SLcontroller sslthreadcontroller
						.local actors actor[]
						.local numactors Int
						.local i Int
						.local Userfound Bool
						.local ::temp159 actor
						.local ::temp160 Bool
					.endLocalTable
					.code
						PropGet SexLab ::libs_var ::temp154                      ;@line 798
						CallMethod HookController ::temp154 ::temp155 argString  ;@line 798
						Assign SLcontroller ::temp155                            ;@line 798
						PropGet Positions SLcontroller ::temp156                 ;@line 799
						Assign actors ::temp156                                  ;@line 799
						ArrayLength ::temp157 actors                             ;@line 800
						Assign numactors ::temp157                               ;@line 800
						Assign i 0                                               ;@line 801
						Assign Userfound False                                   ;@line 802
					_label132:
						CompareLT ::temp158 i numactors                          ;@line 804
						JumpF ::temp158 _label130                                ;@line 804
						ArrayGetElement ::temp159 actors i                       ;@line 805
						CompareEQ ::temp160 ::temp159 ::user_var                 ;@line 805
						JumpF ::temp160 _label131                                ;@line 805
						Assign Userfound True                                    ;@line 806
						Jump _label131                                           ;@line 806
					_label131:
						IAdd ::temp157 i 1                                       ;@line 808
						Assign i ::temp157                                       ;@line 808
						Jump _label132                                           ;@line 808
					_label130:
						Not ::temp160 Userfound                                  ;@line 811
						JumpF ::temp160 _label133                                ;@line 811
						Return None                                              ;@line 812
						Jump _label133                                           ;@line 812
					_label133:
						CallStatic utility GetCurrentGameTime ::temp161          ;@line 814
						Assign LastPasserbyEventAt ::temp161                     ;@line 814
						CallMethod UnRegisterForModEvent self ::NoneVar "AnimationEnd"  ;@line 815
						CallMethod SetPosition ::user_var ::NoneVar FPosX FPosY FPosZ  ;@line 816
						CallMethod SetAngle ::user_var ::NoneVar FAngleX FAngleY FAngleZ  ;@line 817
						CallMethod SetDoingFavor ::user_var ::NoneVar False      ;@line 818
						PropGet PlayerRef ::libs_var ::temp159                   ;@line 819
						CompareEQ ::temp158 ::user_var ::temp159                 ;@line 819
						JumpF ::temp158 _label134                                ;@line 819
						CallStatic game SetPlayerAIDriven ::NoneVar True         ;@line 820
						CallStatic game DisablePlayerControls ::NoneVar True True False False True True False True 0  ;@line 821
						CallStatic game ForceThirdPerson ::NoneVar               ;@line 822
						PropGet PlayerRef ::libs_var ::temp159                   ;@line 823
						CallStatic game SetCameraTarget ::NoneVar ::temp159      ;@line 823
						Jump _label135                                           ;@line 823
					_label134:
						CallMethod SetDontMove ::user_var ::NoneVar True         ;@line 825
						CallMethod SetRestrained ::user_var ::NoneVar True       ;@line 826
						CallMethod SetHeadTracking ::user_var ::NoneVar False    ;@line 827
					_label135:
						CallMethod PickRandomPose self ::temp162                 ;@line 829
						CallStatic actorutil AddPackageOverride ::NoneVar ::user_var ::temp162 99 0  ;@line 829
						CallMethod EvaluatePackage ::user_var ::NoneVar          ;@line 830
						CallMethod CheckSelfBondageRelease self ::NoneVar        ;@line 831
					.endCode
				.endFunction
				.function CanMakeStruggleEscapeAttempt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp226 Float
						.local ::temp227 Bool
						.local HoursNeeded Float
						.local HoursPassed Float
						.local ::temp228 Int
						.local ::temp229 actor
						.local ::temp230 Bool
						.local ::temp231 Bool
						.local ::temp233 Bool
						.local HoursToWait Int
						.local ::temp232 String
						.local ::NoneVar None
						.local ::temp234 actorbase
						.local ::temp235 String
					.endLocalTable
					.code
						CallMethod CalculateCooldownModifier self ::temp226 False  ;@line 1069
						FMultiply ::temp226 ::EscapeCooldown_var ::temp226       ;@line 1069
						Assign HoursNeeded ::temp226                             ;@line 1069
						CallStatic utility GetCurrentGameTime ::temp226          ;@line 1070
						FSubtract ::temp226 ::temp226 LastStruggleEscapeAttemptAt  ;@line 1070
						FMultiply ::temp226 ::temp226 24.000000                  ;@line 1070
						Assign HoursPassed ::temp226                             ;@line 1070
						CompareGT ::temp227 HoursPassed HoursNeeded              ;@line 1071
						JumpF ::temp227 _label136                                ;@line 1071
						CallStatic utility GetCurrentGameTime ::temp226          ;@line 1072
						Assign LastStruggleEscapeAttemptAt ::temp226             ;@line 1072
						Return True                                              ;@line 1073
						Jump _label137                                           ;@line 1073
					_label136:
						FSubtract ::temp226 HoursNeeded HoursPassed              ;@line 1075
						CallStatic math Ceiling ::temp228 ::temp226              ;@line 1075
						Assign HoursToWait ::temp228                             ;@line 1075
						PropGet PlayerRef ::libs_var ::temp229                   ;@line 1076
						CompareEQ ::temp230 ::user_var ::temp229                 ;@line 1076
						Cast ::temp230 ::temp230                                 ;@line 1076
						JumpF ::temp230 _label138                                ;@line 1076
						FSubtract ::temp226 HoursNeeded HoursPassed              ;@line 1076
						CompareGTE ::temp231 ::temp226 1.000000                  ;@line 1076
						Cast ::temp230 ::temp231                                 ;@line 1076
					_label138:
						JumpF ::temp230 _label139                                ;@line 1076
						Cast ::temp232 HoursToWait                               ;@line 1077
						StrCat ::temp232 "你不能在上次尝试后这么快就试图挣脱这个装置! 你可以在大约  " ::temp232  ;@line 1077
						StrCat ::temp232 ::temp232 " 小时后重试."           ;@line 1077
						CallMethod notify ::libs_var ::NoneVar ::temp232 True    ;@line 1077
						Jump _label137                                           ;@line 1077
					_label139:
						PropGet PlayerRef ::libs_var ::temp229                   ;@line 1078
						CompareEQ ::temp231 ::user_var ::temp229                 ;@line 1078
						Cast ::temp231 ::temp231                                 ;@line 1078
						JumpF ::temp231 _label140                                ;@line 1078
						FSubtract ::temp226 HoursNeeded HoursPassed              ;@line 1078
						CompareLT ::temp233 ::temp226 1.000000                   ;@line 1078
						Cast ::temp231 ::temp233                                 ;@line 1078
					_label140:
						JumpF ::temp231 _label141                                ;@line 1078
						CallMethod notify ::libs_var ::NoneVar "你不能在上次尝试后这么快就试图挣脱这个装置! 你很快就可以再试一次!" True  ;@line 1079
						Jump _label137                                           ;@line 1079
					_label141:
						CallMethod GetLeveledActorBase ::user_var ::temp234      ;@line 1081
						CallMethod GetName ::temp234 ::temp232                   ;@line 1081
						StrCat ::temp232 "你不能帮助 " ::temp232            ;@line 1081
						StrCat ::temp232 ::temp232 " 在上次尝试后不久就尝试从她的设备中挣扎出来! 你可以在大约 "  ;@line 1081
						Cast ::temp235 HoursToWait                               ;@line 1081
						StrCat ::temp235 ::temp232 ::temp235                     ;@line 1081
						StrCat ::temp232 ::temp235 " 小时后重试."           ;@line 1081
						CallMethod notify ::libs_var ::NoneVar ::temp232 True    ;@line 1081
					_label137:
						Return False                                             ;@line 1084
					.endCode
				.endFunction
				.function DeviceMenuLock
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp88 Bool
						.local i Int
						.local ::temp87 Int
						.local ::temp89 Bool
						.local ::temp90 zadconfig
						.local ::temp91 Bool
						.local Choice Int
						.local ::temp92 Bool
						.local ::temp93 Bool
						.local Choice2 Int
						.local ::temp94 referencealias
						.local ::NoneVar None
						.local ::mangled_choice_0 Int
						.local ::mangled_choice2_1 Int
						.local ::temp95 actor
						.local ::temp96 form
					.endLocalTable
					.code
						JumpF ::CanBePickedUp_var _label142                      ;@line 459
						CallMethod Show ::zadc_DeviceMsgPlayerNotLockedCanPick_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 460
						Assign i ::temp87                                        ;@line 460
						Jump _label143                                           ;@line 460
					_label142:
						CallMethod Show ::zadc_DeviceMsgPlayerNotLocked_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 462
						Assign i ::temp87                                        ;@line 462
					_label143:
						CompareEQ ::temp88 i 0                                   ;@line 464
						JumpF ::temp88 _label144                                 ;@line 464
						Assign isLockManipulated False                           ;@line 465
						Assign ::isSelfBondage_var False                         ;@line 466
						Not ::temp89 ::DisableLockManipulation_var               ;@line 467
						Cast ::temp89 ::temp89                                   ;@line 467
						JumpF ::temp89 _label145                                 ;@line 467
						PropGet Config ::libs_var ::temp90                       ;@line 467
						PropGet UseItemManipulation ::temp90 ::temp91            ;@line 467
						Cast ::temp89 ::temp91                                   ;@line 467
					_label145:
						JumpF ::temp89 _label146                                 ;@line 467
						CallMethod Show ::zadc_OnLockDeviceMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 468
						Assign Choice ::temp87                                   ;@line 468
						CompareEQ ::temp91 Choice 1                              ;@line 469
						JumpF ::temp91 _label147                                 ;@line 469
						Assign ::isSelfBondage_var True                          ;@line 471
						JumpF ::AllowTimerDialogue_var _label148                 ;@line 472
						CallMethod Show ::zadc_SelfbondageMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 473
						Assign Choice2 ::temp87                                  ;@line 473
						CompareEQ ::temp92 Choice2 0                             ;@line 474
						JumpF ::temp92 _label149                                 ;@line 474
						Assign ::SelfBondageReleaseTimer_var 1.000000            ;@line 475
						Jump _label150                                           ;@line 475
					_label149:
						CompareEQ ::temp93 Choice2 1                             ;@line 476
						JumpF ::temp93 _label151                                 ;@line 476
						Assign ::SelfBondageReleaseTimer_var 2.000000            ;@line 477
						Jump _label150                                           ;@line 477
					_label151:
						CompareEQ ::temp93 Choice2 2                             ;@line 478
						JumpF ::temp93 _label152                                 ;@line 478
						Assign ::SelfBondageReleaseTimer_var 5.000000            ;@line 479
						Jump _label150                                           ;@line 479
					_label152:
						CompareEQ ::temp93 Choice2 3                             ;@line 480
						JumpF ::temp93 _label153                                 ;@line 480
						Assign ::SelfBondageReleaseTimer_var 12.000000           ;@line 481
						Jump _label150                                           ;@line 481
					_label153:
						CompareEQ ::temp93 Choice2 4                             ;@line 482
						JumpF ::temp93 _label154                                 ;@line 482
						Assign ::SelfBondageReleaseTimer_var 24.000000           ;@line 483
						Jump _label150                                           ;@line 483
					_label154:
						CompareEQ ::temp93 Choice2 1                             ;@line 484
						JumpF ::temp93 _label150                                 ;@line 484
						PropGet UserRef ::clib_var ::temp94                      ;@line 485
						CallMethod Clear ::temp94 ::NoneVar                      ;@line 485
						Return None                                              ;@line 486
						Jump _label150                                           ;@line 486
					_label150:
						Jump _label148                                           ;@line 486
					_label148:
						Jump _label155                                           ;@line 486
					_label147:
						CompareEQ ::temp93 Choice 2                              ;@line 489
						JumpF ::temp93 _label156                                 ;@line 489
						Assign isLockManipulated True                            ;@line 491
						Jump _label155                                           ;@line 491
					_label156:
						CallMethod Show ::zadc_OnDeviceLockMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 494
					_label155:
						Jump _label157                                           ;@line 494
					_label146:
						CallMethod Show ::zadc_OnLockDeviceNoManipulateMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 497
						Assign ::mangled_choice_0 ::temp87                       ;@line 497
						CompareEQ ::temp92 ::mangled_choice_0 1                  ;@line 498
						JumpF ::temp92 _label158                                 ;@line 498
						Assign ::isSelfBondage_var True                          ;@line 500
						JumpF ::AllowTimerDialogue_var _label159                 ;@line 501
						CallMethod Show ::zadc_SelfbondageMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 502
						Assign ::mangled_choice2_1 ::temp87                      ;@line 502
						CompareEQ ::temp93 ::mangled_choice2_1 0                 ;@line 503
						JumpF ::temp93 _label160                                 ;@line 503
						Assign ::SelfBondageReleaseTimer_var 1.000000            ;@line 504
						Jump _label161                                           ;@line 504
					_label160:
						CompareEQ ::temp91 ::mangled_choice2_1 1                 ;@line 505
						JumpF ::temp91 _label162                                 ;@line 505
						Assign ::SelfBondageReleaseTimer_var 2.000000            ;@line 506
						Jump _label161                                           ;@line 506
					_label162:
						CompareEQ ::temp91 ::mangled_choice2_1 2                 ;@line 507
						JumpF ::temp91 _label163                                 ;@line 507
						Assign ::SelfBondageReleaseTimer_var 5.000000            ;@line 508
						Jump _label161                                           ;@line 508
					_label163:
						CompareEQ ::temp91 ::mangled_choice2_1 3                 ;@line 509
						JumpF ::temp91 _label164                                 ;@line 509
						Assign ::SelfBondageReleaseTimer_var 12.000000           ;@line 510
						Jump _label161                                           ;@line 510
					_label164:
						CompareEQ ::temp91 ::mangled_choice2_1 4                 ;@line 511
						JumpF ::temp91 _label165                                 ;@line 511
						Assign ::SelfBondageReleaseTimer_var 24.000000           ;@line 512
						Jump _label161                                           ;@line 512
					_label165:
						CompareEQ ::temp91 ::mangled_choice2_1 1                 ;@line 513
						JumpF ::temp91 _label161                                 ;@line 513
						PropGet UserRef ::clib_var ::temp94                      ;@line 514
						CallMethod Clear ::temp94 ::NoneVar                      ;@line 514
						Return None                                              ;@line 515
						Jump _label161                                           ;@line 515
					_label161:
						Jump _label159                                           ;@line 515
					_label159:
						Jump _label157                                           ;@line 515
					_label158:
						CallMethod Show ::zadc_OnDeviceLockMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 519
					_label157:
						Jump _label166                                           ;@line 519
					_label144:
						CompareEQ ::temp91 i 1                                   ;@line 522
						JumpF ::temp91 _label166                                 ;@line 522
						CallMethod DisplayDifficultyMsg self ::NoneVar           ;@line 524
						Return None                                              ;@line 525
						Jump _label166                                           ;@line 525
					_label166:
						Not ::temp93 ::CanBePickedUp_var                         ;@line 527
						JumpF ::temp93 _label167                                 ;@line 527
						CompareEQ ::temp92 i 2                                   ;@line 528
						JumpF ::temp92 _label168                                 ;@line 528
						CallMethod Show ::zadc_OnLeaveItNotLockedMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 530
						Return None                                              ;@line 531
						Jump _label168                                           ;@line 531
					_label168:
						Jump _label169                                           ;@line 531
					_label167:
						CompareEQ ::temp89 i 2                                   ;@line 534
						JumpF ::temp89 _label170                                 ;@line 534
						CallMethod disable self ::NoneVar False                  ;@line 536
						CallMethod Delete self ::NoneVar                         ;@line 537
						PropGet PlayerRef ::libs_var ::temp95                    ;@line 538
						Cast ::temp96 ::Blueprint_var                            ;@line 538
						CallMethod AddItem ::temp95 ::NoneVar ::temp96 1 False   ;@line 538
						Return None                                              ;@line 539
						Jump _label169                                           ;@line 539
					_label170:
						CallMethod Show ::zadc_OnLeaveItNotLockedMSG_var ::temp87 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 542
						Return None                                              ;@line 543
					_label169:
						PropGet PlayerRef ::libs_var ::temp95                    ;@line 546
						CallMethod LockActor self ::NoneVar ::temp95             ;@line 546
					.endCode
				.endFunction
				.function UnlockAttempt
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp125 Bool
						.local ::temp124 String
						.local ::NoneVar None
						.local ::temp126 actor
						.local ::temp127 form
						.local ::temp128 Int
						.local ::temp129 Bool
						.local ::temp130 zadconfig
						.local ::temp131 keyword
					.endLocalTable
					.code
						JumpF isLockManipulated _label171                        ;@line 660
						StrCat ::temp124 "当你操纵了 " ::DeviceName_var     ;@line 661
						StrCat ::temp124 ::temp124 "后,你可以轻松地从设备中脱离开来!"  ;@line 661
						CallMethod notify ::libs_var ::NoneVar ::temp124 True    ;@line 661
						CallMethod UnlockActor self ::NoneVar                    ;@line 662
						Return True                                              ;@line 663
						Jump _label171                                           ;@line 663
					_label171:
						CallMethod CheckLockShield self ::temp125                ;@line 665
						Not ::temp125 ::temp125                                  ;@line 665
						JumpF ::temp125 _label172                                ;@line 665
						Return False                                             ;@line 666
						Jump _label172                                           ;@line 666
					_label172:
						JumpF ::deviceKey_var _label173                          ;@line 668
						PropGet PlayerRef ::libs_var ::temp126                   ;@line 669
						Cast ::temp127 ::deviceKey_var                           ;@line 669
						CallMethod GetItemCount ::temp126 ::temp128 ::temp127    ;@line 669
						CompareLTE ::temp125 ::temp128 0                         ;@line 669
						JumpF ::temp125 _label174                                ;@line 669
						CallMethod Show ::zadc_OnNoKeyMSG_var ::temp128 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 670
						Return False                                             ;@line 671
						Jump _label175                                           ;@line 671
					_label174:
						PropGet PlayerRef ::libs_var ::temp126                   ;@line 672
						Cast ::temp127 ::deviceKey_var                           ;@line 672
						CallMethod GetItemCount ::temp126 ::temp128 ::temp127    ;@line 672
						CompareLT ::temp129 ::temp128 ::NumberOfKeysNeeded_var   ;@line 672
						JumpF ::temp129 _label175                                ;@line 672
						CallMethod Show ::zadc_OnNotEnoughKeysMSG_var ::temp128 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 673
						Return False                                             ;@line 674
						Jump _label175                                           ;@line 674
					_label175:
						CallMethod CheckLockAccess self ::temp129                ;@line 676
						Not ::temp125 ::temp129                                  ;@line 676
						JumpF ::temp125 _label176                                ;@line 676
						Return False                                             ;@line 677
						Jump _label176                                           ;@line 677
					_label176:
						PropGet PlayerRef ::libs_var ::temp126                   ;@line 680
						CallMethod StruggleScene self ::NoneVar ::temp126        ;@line 680
						JumpF ::DestroyKey_var _label177                         ;@line 681
						PropGet PlayerRef ::libs_var ::temp126                   ;@line 682
						Cast ::temp127 ::deviceKey_var                           ;@line 682
						CallMethod RemoveItem ::temp126 ::NoneVar ::temp127 ::NumberOfKeysNeeded_var False None  ;@line 682
						Jump _label178                                           ;@line 682
					_label177:
						PropGet Config ::libs_var ::temp130                      ;@line 683
						PropGet GlobalDestroyKey ::temp130 ::temp129             ;@line 683
						Cast ::temp129 ::temp129                                 ;@line 683
						JumpF ::temp129 _label179                                ;@line 683
						PropGet zad_NonUniqueKey ::libs_var ::temp131            ;@line 683
						CallMethod HasKeyword ::deviceKey_var ::temp125 ::temp131  ;@line 683
						Cast ::temp129 ::temp125                                 ;@line 683
					_label179:
						JumpF ::temp129 _label178                                ;@line 683
						PropGet PlayerRef ::libs_var ::temp126                   ;@line 684
						Cast ::temp127 ::deviceKey_var                           ;@line 684
						CallMethod RemoveItem ::temp126 ::NoneVar ::temp127 ::NumberOfKeysNeeded_var False None  ;@line 684
						Jump _label178                                           ;@line 684
					_label178:
						Jump _label180                                           ;@line 684
					_label173:
						CallMethod CheckLockAccess self ::temp125                ;@line 687
						Not ::temp129 ::temp125                                  ;@line 687
						JumpF ::temp129 _label180                                ;@line 687
						Return False                                             ;@line 688
						Jump _label180                                           ;@line 688
					_label180:
						CallMethod UnlockActor self ::NoneVar                    ;@line 691
						Return True                                              ;@line 692
					.endCode
				.endFunction
				.function SetLockShield
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp15 Float
						.local ::temp16 Bool
						.local ::temp17 Bool
						.local ::temp18 Float
					.endLocalTable
					.code
						CallStatic utility GetCurrentGameTime ::temp15           ;@line 175
						Assign LockShieldStartedAt ::temp15                      ;@line 175
						CompareGT ::temp16 ::LockShieldTimerMin_var 0.000000     ;@line 176
						Cast ::temp16 ::temp16                                   ;@line 176
						JumpF ::temp16 _label181                                 ;@line 176
						CompareLTE ::temp17 ::LockShieldTimerMin_var ::LockShieldTimerMax_var  ;@line 176
						Cast ::temp16 ::temp17                                   ;@line 176
					_label181:
						JumpF ::temp16 _label182                                 ;@line 176
						CallStatic utility RandomFloat ::temp15 ::LockShieldTimerMin_var ::LockShieldTimerMax_var  ;@line 177
						CallMethod CalculateCooldownModifier self ::temp18 False  ;@line 177
						FMultiply ::temp15 ::temp15 ::temp18                     ;@line 177
						Assign LockShieldTimer ::temp15                          ;@line 177
						Jump _label183                                           ;@line 177
					_label182:
						Assign LockShieldTimer 0.000000                          ;@line 179
					_label183:
					.endCode
				.endFunction
				.function RemoveDevices
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActor actor
					.endParamTable
					.localTable
						.local ::temp38 Int
						.local ::temp39 Bool
						.local i Int
						.local ::temp40 armor
						.local ::temp41 keyword
						.local ::temp42 Bool
						.local ::temp43 armor
						.local ::temp44 armor
						.local ::NoneVar None
						.local ::temp45 form
					.endLocalTable
					.code
						ArrayLength ::temp38 ::EquipDevices_var                  ;@line 233
						Assign i ::temp38                                        ;@line 233
					_label187:
						CompareGT ::temp39 i 0                                   ;@line 234
						JumpF ::temp39 _label184                                 ;@line 234
						ISubtract ::temp38 i 1                                   ;@line 235
						Assign i ::temp38                                        ;@line 235
						ArrayGetElement ::temp40 ::EquipDevices_var i            ;@line 236
						PropGet zad_InventoryDevice ::libs_var ::temp41          ;@line 236
						CallMethod HasKeyword ::temp40 ::temp42 ::temp41         ;@line 236
						JumpF ::temp42 _label185                                 ;@line 236
						ArrayGetElement ::temp40 ::EquipDevices_var i            ;@line 237
						ArrayGetElement ::temp43 ::EquipDevices_var i            ;@line 237
						CallMethod GetRenderedDevice ::libs_var ::temp43 ::temp43  ;@line 237
						ArrayGetElement ::temp44 ::EquipDevices_var i            ;@line 237
						CallMethod GetDeviceKeyword ::libs_var ::temp41 ::temp44  ;@line 237
						CallMethod RemoveDevice ::libs_var ::NoneVar akActor ::temp40 ::temp43 ::temp41 True False True  ;@line 237
						Jump _label186                                           ;@line 237
					_label185:
						ArrayGetElement ::temp44 ::EquipDevices_var i            ;@line 239
						Cast ::temp45 ::temp44                                   ;@line 239
						CallMethod UnEquipItem akActor ::NoneVar ::temp45 False True  ;@line 239
						ArrayGetElement ::temp40 ::EquipDevices_var i            ;@line 240
						Cast ::temp45 ::temp40                                   ;@line 240
						CallMethod RemoveItem akActor ::NoneVar ::temp45 1 True None  ;@line 240
					_label186:
						Jump _label187                                           ;@line 240
					_label184:
					.endCode
				.endFunction
				.function SendDeviceEvent
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param LockStatus Bool
					.endParamTable
					.localTable
						.local ::temp46 Int
						.local Handle Int
						.local ::temp47 form
						.local ::NoneVar None
						.local ::temp48 Bool
					.endLocalTable
					.code
						CallStatic modevent Create ::temp46 "21312312"           ;@line 247
						Assign Handle ::temp46                                   ;@line 247
						JumpF Handle _label188                                   ;@line 248
						Cast ::temp47 ::user_var                                 ;@line 249
						CallStatic modevent PushForm ::NoneVar Handle ::temp47   ;@line 249
						Cast ::temp47 self                                       ;@line 250
						CallStatic modevent PushForm ::NoneVar Handle ::temp47   ;@line 250
						CallStatic modevent PushBool ::NoneVar Handle LockStatus  ;@line 251
						CallStatic modevent Send ::temp48 Handle                 ;@line 252
						Jump _label188                                           ;@line 252
					_label188:
					.endCode
				.endFunction
				.function PasserbyAction
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
					.endParamTable
					.localTable
						.local ::temp137 Bool
						.local ::temp138 Bool
						.local ::temp139 cell
						.local ::temp140 actor
						.local ::temp141 cell
						.local ::temp142 Float
						.local ::temp143 Float
						.local ::temp144 Int
						.local ::temp145 keyword
						.local ::temp146 ObjectReference
						.local currenttest actor
						.local ::temp147 actorbase
						.local ::temp148 String
						.local ::NoneVar None
						.local ::temp149 String
					.endLocalTable
					.code
						Not ::temp137 ::AllowPasserbyAction_var                  ;@line 754
						Cast ::temp137 ::temp137                                 ;@line 754
						JumpT ::temp137 _label189                                ;@line 754
						CallMethod Is3DLoaded ::user_var ::temp138               ;@line 754
						Not ::temp138 ::temp138                                  ;@line 754
						Cast ::temp137 ::temp138                                 ;@line 754
					_label189:
						Cast ::temp137 ::temp137                                 ;@line 754
						JumpT ::temp137 _label190                                ;@line 754
						CallMethod GetParentCell ::user_var ::temp139            ;@line 754
						PropGet PlayerRef ::libs_var ::temp140                   ;@line 754
						CallMethod GetParentCell ::temp140 ::temp141             ;@line 754
						CompareEQ ::temp138 ::temp139 ::temp141                  ;@line 754
						Not ::temp138 ::temp138                                  ;@line 754
						Cast ::temp137 ::temp138                                 ;@line 754
					_label190:
						Cast ::temp137 ::temp137                                 ;@line 754
						JumpT ::temp137 _label191                                ;@line 754
						CallMethod IsAnimating ::clib_var ::temp138 ::user_var   ;@line 754
						Cast ::temp137 ::temp138                                 ;@line 754
					_label191:
						JumpF ::temp137 _label192                                ;@line 754
						Return False                                             ;@line 755
						Jump _label192                                           ;@line 755
					_label192:
						CallStatic utility GetCurrentGameTime ::temp142          ;@line 757
						FSubtract ::temp142 ::temp142 LastPasserbyEventAt        ;@line 757
						Cast ::temp143 24                                        ;@line 757
						FMultiply ::temp143 ::temp142 ::temp143                  ;@line 757
						CompareLT ::temp138 ::temp143 ::PasserbyCooldown_var     ;@line 757
						JumpF ::temp138 _label193                                ;@line 757
						Return False                                             ;@line 758
						Jump _label193                                           ;@line 758
					_label193:
						ArrayLength ::temp144 ::SexAnimations_var                ;@line 760
						CompareEQ ::temp137 ::temp144 0                          ;@line 760
						Cast ::temp137 ::temp137                                 ;@line 760
						JumpT ::temp137 _label194                                ;@line 760
						CallMethod GetIsFemale ::clib_var ::temp138 ::user_var   ;@line 760
						Not ::temp138 ::temp138                                  ;@line 760
						Cast ::temp137 ::temp138                                 ;@line 760
					_label194:
						JumpF ::temp137 _label195                                ;@line 760
						Return False                                             ;@line 762
						Jump _label195                                           ;@line 762
					_label195:
						PropGet zad_DeviousBelt ::libs_var ::temp145             ;@line 764
						CallMethod WornHasKeyword ::user_var ::temp138 ::temp145  ;@line 764
						JumpF ::temp138 _label196                                ;@line 764
						Return False                                             ;@line 766
						Jump _label196                                           ;@line 766
					_label196:
						Cast ::temp146 ::user_var                                ;@line 768
						CallStatic game FindRandomActorFromRef ::temp140 ::temp146 1000.000000  ;@line 768
						Assign currenttest ::temp140                             ;@line 768
						Cast ::temp138 currenttest                               ;@line 769
						JumpF ::temp138 _label197                                ;@line 769
						CallMethod ValidForInteraction ::libs_var ::temp137 currenttest -1 False False True False True  ;@line 769
						Cast ::temp138 ::temp137                                 ;@line 769
					_label197:
						JumpF ::temp138 _label198                                ;@line 769
						PropGet PlayerRef ::libs_var ::temp140                   ;@line 770
						CompareEQ ::temp137 currenttest ::temp140                ;@line 770
						JumpF ::temp137 _label199                                ;@line 770
						CallMethod GetLeveledActorBase currenttest ::temp147     ;@line 771
						CallMethod GetName ::temp147 ::temp148                   ;@line 771
						StrCat ::temp148 ::temp148 " 会利用你的……"      ;@line 771
						CallMethod notify ::libs_var ::NoneVar ::temp148 False   ;@line 771
						Jump _label200                                           ;@line 771
					_label199:
						CallMethod GetLeveledActorBase currenttest ::temp147     ;@line 773
						CallMethod GetName ::temp147 ::temp148                   ;@line 773
						StrCat ::temp148 ::temp148 " 会占尽 "                 ;@line 773
						CallMethod GetLeveledActorBase ::user_var ::temp147      ;@line 773
						CallMethod GetName ::temp147 ::temp149                   ;@line 773
						StrCat ::temp148 ::temp148 ::temp149                     ;@line 773
						StrCat ::temp149 ::temp148 "的便宜."                  ;@line 773
						CallMethod notify ::libs_var ::NoneVar ::temp149 False   ;@line 773
					_label200:
						CallMethod SexScene self ::temp137 currenttest ""        ;@line 775
						Return True                                              ;@line 776
						Jump _label198                                           ;@line 776
					_label198:
						Return False                                             ;@line 778
					.endCode
				.endFunction
				.function MoveToFront
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akObjB ObjectReference
						.param akObjA ObjectReference
						.param afDistance Float
					.endParamTable
					.localTable
						.local ::temp166 Float
						.local ::temp167 Float
						.local ::NoneVar None
					.endLocalTable
					.code
						CallMethod GetAngleZ akObjB ::temp166                    ;@line 839
						CallStatic math Sin ::temp166 ::temp166                  ;@line 839
						FMultiply ::temp166 afDistance ::temp166                 ;@line 839
						CallMethod GetAngleZ akObjB ::temp167                    ;@line 839
						CallStatic math Cos ::temp167 ::temp167                  ;@line 839
						FMultiply ::temp167 afDistance ::temp167                 ;@line 839
						CallMethod moveto akObjA ::NoneVar akObjB ::temp166 ::temp167 0.000000 True  ;@line 839
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
				.function SelfBondageReward
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp266 Bool
						.local ::temp267 Bool
						.local ::temp268 actor
						.local ::temp269 Int
						.local ::temp272 Float
						.local i Int
						.local ::NoneVar None
						.local ::temp270 leveleditem
						.local ::temp271 form
					.endLocalTable
					.code
						Not ::temp266 ::isSelfBondage_var                        ;@line 1155
						Cast ::temp266 ::temp266                                 ;@line 1155
						JumpT ::temp266 _label201                                ;@line 1155
						Not ::temp267 ::AllowRewardonEscape_var                  ;@line 1155
						Cast ::temp266 ::temp267                                 ;@line 1155
					_label201:
						Cast ::temp266 ::temp266                                 ;@line 1155
						JumpT ::temp266 _label202                                ;@line 1155
						PropGet PlayerRef ::libs_var ::temp268                   ;@line 1155
						CompareEQ ::temp267 ::user_var ::temp268                 ;@line 1155
						Not ::temp267 ::temp267                                  ;@line 1155
						Cast ::temp266 ::temp267                                 ;@line 1155
					_label202:
						JumpF ::temp266 _label203                                ;@line 1155
						Return None                                              ;@line 1157
						Jump _label203                                           ;@line 1157
					_label203:
						ArrayLength ::temp269 ::Reward_var                       ;@line 1159
						Assign i ::temp269                                       ;@line 1159
						CompareGT ::temp267 i 0                                  ;@line 1160
						JumpF ::temp267 _label204                                ;@line 1160
						CallMethod notify ::libs_var ::NoneVar "太有趣了!你在时间到之前成功逃离装置将会得到奖励!" True  ;@line 1161
						Jump _label205                                           ;@line 1161
					_label204:
						CallMethod notify ::libs_var ::NoneVar "太好玩了!你竟然在时间到之前成功逃离了装置!" True  ;@line 1163
					_label205:
						CompareGT ::temp266 i 0                                  ;@line 1165
						JumpF ::temp266 _label206                                ;@line 1165
						ISubtract ::temp269 i 1                                  ;@line 1166
						Assign i ::temp269                                       ;@line 1166
						ArrayGetElement ::temp270 ::Reward_var i                 ;@line 1167
						Cast ::temp271 ::temp270                                 ;@line 1167
						CallMethod AddItem ::user_var ::NoneVar ::temp271 1 False  ;@line 1167
						Jump _label205                                           ;@line 1167
					_label206:
						Cast ::temp272 1                                         ;@line 1169
						CallStatic utility Wait ::NoneVar ::temp272              ;@line 1169
					.endCode
				.endFunction
				.function PickRandomPose
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Package
					.paramTable
					.endParamTable
					.localTable
						.local ::temp0 Int
						.local ::temp1 Package
					.endLocalTable
					.code
						ArrayLength ::temp0 ::BoundPose_var                      ;@line 143
						ISubtract ::temp0 ::temp0 1                              ;@line 143
						CallStatic utility RandomInt ::temp0 0 ::temp0           ;@line 143
						ArrayGetElement ::temp1 ::BoundPose_var ::temp0          ;@line 143
						Assign CurrentPose ::temp1                               ;@line 143
						Return CurrentPose                                       ;@line 144
					.endCode
				.endFunction
				.function LockActor
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param act actor
					.endParamTable
					.localTable
						.local ::temp49 sexlabframework
						.local ::temp50 sslactorlibrary
						.local ::temp51 Bool
						.local ::temp52 actor
						.local ::temp59 ObjectReference
						.local ::temp60 Float
						.local ::temp61 Package
						.local ::NoneVar None
						.local ::temp53 armor
						.local ::temp54 form
						.local ::temp55 Bool
						.local ::temp56 armor
						.local ::temp57 keyword
						.local ::temp58 actor
						.local Pose Package
						.local ::temp62 Int
						.local isValidPose Bool
						.local k Int
					.endLocalTable
					.code
						PropGet SexLab ::libs_var ::temp49                       ;@line 257
						PropGet ActorLib ::temp49 ::temp50                       ;@line 257
						CallMethod CanAnimate ::temp50 ::temp51 act              ;@line 257
						Not ::temp51 ::temp51                                    ;@line 257
						JumpF ::temp51 _label207                                 ;@line 257
						Return None                                              ;@line 258
						Jump _label207                                           ;@line 258
					_label207:
						PropGet PlayerRef ::libs_var ::temp52                    ;@line 260
						CompareEQ ::temp51 act ::temp52                          ;@line 260
						JumpF ::temp51 _label208                                 ;@line 260
						CallStatic game SetPlayerAIDriven ::NoneVar True         ;@line 261
						Jump _label208                                           ;@line 261
					_label208:
						Assign ::user_var act                                    ;@line 263
						CallMethod SetDoingFavor ::user_var ::NoneVar False      ;@line 264
						JumpF CurrentPose _label209                              ;@line 265
						CallStatic actorutil RemovePackageOverride ::temp51 ::user_var CurrentPose  ;@line 266
						Jump _label209                                           ;@line 266
					_label209:
						PropGet PlayerRef ::libs_var ::temp52                    ;@line 268
						CompareEQ ::temp51 ::user_var ::temp52                   ;@line 268
						Not ::temp51 ::temp51                                    ;@line 268
						JumpF ::temp51 _label210                                 ;@line 268
						PropGet zadc_PrisonerChainsRendered ::clib_var ::temp53  ;@line 269
						Cast ::temp54 ::temp53                                   ;@line 269
						CallMethod IsEquipped ::user_var ::temp55 ::temp54       ;@line 269
						JumpF ::temp55 _label211                                 ;@line 269
						PropGet zadc_PrisonerChainsInventory ::clib_var ::temp53  ;@line 270
						PropGet zadc_PrisonerChainsRendered ::clib_var ::temp56  ;@line 270
						PropGet zad_DeviousHeavyBondage ::libs_var ::temp57      ;@line 270
						CallMethod RemoveDevice ::libs_var ::NoneVar ::user_var ::temp53 ::temp56 ::temp57 True False True  ;@line 270
						Jump _label211                                           ;@line 270
					_label211:
						PropGet SelectedUser ::clib_var ::temp52                 ;@line 272
						CompareEQ ::temp55 ::user_var ::temp52                   ;@line 272
						JumpF ::temp55 _label212                                 ;@line 272
						Cast ::temp58 None                                       ;@line 273
						Assign ::temp52 ::temp58                                 ;@line 273
						PropSet SelectedUser ::clib_var ::temp52                 ;@line 273
						Jump _label212                                           ;@line 273
					_label212:
						CallMethod SetDontMove ::user_var ::NoneVar True         ;@line 275
						CallMethod SetRestrained ::user_var ::NoneVar True       ;@line 276
						CallMethod SetHeadTracking ::user_var ::NoneVar False    ;@line 277
						CallStatic utility Wait ::NoneVar 0.500000               ;@line 278
						Jump _label210                                           ;@line 278
					_label210:
						Cast ::temp59 self                                       ;@line 286
						CallMethod moveto ::user_var ::NoneVar ::temp59 0.000000 0.000000 0.000000 True  ;@line 286
						CallMethod GetPositionX ::user_var ::temp60              ;@line 287
						Assign PosX ::temp60                                     ;@line 287
						CallMethod GetPositionY ::user_var ::temp60              ;@line 288
						Assign PosY ::temp60                                     ;@line 288
						CallMethod GetPositionZ ::user_var ::temp60              ;@line 289
						Assign PosZ ::temp60                                     ;@line 289
						CallMethod StoreBondage ::clib_var ::NoneVar ::user_var ::InvalidDevices_var ::HideAllDevices_var  ;@line 290
						Cast ::temp59 self                                       ;@line 291
						CallMethod SetVehicle ::user_var ::NoneVar ::temp59      ;@line 291
						JumpF ::ForceStripActor_var _label213                    ;@line 292
						CallMethod StripOutfit ::clib_var ::NoneVar ::user_var   ;@line 293
						Jump _label213                                           ;@line 293
					_label213:
						CallMethod SetNiOverrideOverride ::clib_var ::NoneVar ::user_var  ;@line 295
						PropGet zadc_pk_donothing ::clib_var ::temp61            ;@line 296
						CallStatic actorutil RemovePackageOverride ::temp55 ::user_var ::temp61  ;@line 296
						Cast ::temp59 self                                       ;@line 297
						CallMethod GetOverridePose ::clib_var ::temp61 ::temp59  ;@line 297
						Assign Pose ::temp61                                     ;@line 297
						JumpF Pose _label214                                     ;@line 298
						Assign isValidPose False                                 ;@line 300
						ArrayLength ::temp62 ::BoundPose_var                     ;@line 301
						ISubtract ::temp62 ::temp62 1                            ;@line 301
						Assign k ::temp62                                        ;@line 301
					_label217:
						CompareGTE ::temp51 k 0                                  ;@line 302
						JumpF ::temp51 _label215                                 ;@line 302
						ArrayGetElement ::temp61 ::BoundPose_var k               ;@line 303
						CompareEQ ::temp55 Pose ::temp61                         ;@line 303
						JumpF ::temp55 _label216                                 ;@line 303
						Assign isValidPose True                                  ;@line 304
						Jump _label216                                           ;@line 304
					_label216:
						ISubtract ::temp62 k 1                                   ;@line 306
						Assign k ::temp62                                        ;@line 306
						Jump _label217                                           ;@line 306
					_label215:
						JumpF isValidPose _label218                              ;@line 308
						CallStatic actorutil AddPackageOverride ::NoneVar ::user_var Pose 99 0  ;@line 309
						Jump _label219                                           ;@line 309
					_label218:
						CallMethod PickRandomPose self ::temp61                  ;@line 311
						CallStatic actorutil AddPackageOverride ::NoneVar ::user_var ::temp61 99 0  ;@line 311
					_label219:
						Jump _label220                                           ;@line 311
					_label214:
						CallMethod PickRandomPose self ::temp61                  ;@line 314
						CallStatic actorutil AddPackageOverride ::NoneVar ::user_var ::temp61 99 0  ;@line 314
					_label220:
						CallMethod EvaluatePackage ::user_var ::NoneVar          ;@line 316
						CallMethod disable self ::NoneVar False                  ;@line 317
						PropGet PlayerRef ::libs_var ::temp58                    ;@line 318
						CompareEQ ::temp55 act ::temp58                          ;@line 318
						JumpF ::temp55 _label221                                 ;@line 318
						CallStatic game DisablePlayerControls ::NoneVar True True False False True True False True 0  ;@line 319
						CallMethod UnregisterForAllKeys self ::NoneVar           ;@line 321
						CallStatic input GetMappedKey ::temp62 "21312312" 0      ;@line 322
						CallMethod RegisterForKey self ::NoneVar ::temp62        ;@line 322
						JumpF ::PreventWaitandSleep_var _label222                ;@line 323
						PropGet zadc_NoWaitItem ::clib_var ::temp53              ;@line 324
						Cast ::temp54 ::temp53                                   ;@line 324
						CallMethod EquipItem act ::NoneVar ::temp54 True True    ;@line 324
						Jump _label222                                           ;@line 324
					_label222:
						CallStatic game ForceThirdPerson ::NoneVar               ;@line 326
						PropGet PlayerRef ::libs_var ::temp52                    ;@line 327
						CallStatic game SetCameraTarget ::NoneVar ::temp52       ;@line 327
						Jump _label221                                           ;@line 327
					_label221:
						Cast ::temp59 self                                       ;@line 329
						CallMethod StoreDevice ::clib_var ::NoneVar ::user_var ::temp59  ;@line 329
						CallMethod SetLockShield self ::NoneVar                  ;@line 330
						CallStatic utility GetCurrentGameTime ::temp60           ;@line 331
						Assign DeviceEquippedAt ::temp60                         ;@line 331
						CallStatic utility GetCurrentGameTime ::temp60           ;@line 332
						Assign ::ReleaseTimerStartedAt_var ::temp60              ;@line 332
						JumpF ::ForceTimer_var _label223                         ;@line 333
						Assign ::isSelfBondage_var True                          ;@line 334
						Jump _label223                                           ;@line 334
					_label223:
						CallMethod ApplyEffects self ::NoneVar ::user_var        ;@line 336
						CallMethod ApplyDevices self ::NoneVar ::user_var        ;@line 337
						Cast ::temp60 30                                         ;@line 338
						CallMethod RegisterForSingleUpdate self ::NoneVar ::temp60  ;@line 338
						Assign LastBreakEscapeAttemptAt 0.000000                 ;@line 339
						Assign LastStruggleEscapeAttemptAt 0.000000              ;@line 340
						Assign LastLockPickEscapeAttemptAt 0.000000              ;@line 341
						Assign LastUnlockAttemptAt 0.000000                      ;@line 342
						Assign EscapeBreakAttemptsMade 0                         ;@line 343
						Assign EscapeStruggleAttemptsMade 0                      ;@line 344
						Assign EscapeLockPickAttemptsMade 0                      ;@line 345
						Assign lasthourdisplayed 0                               ;@line 346
						Assign LastPasserbyEventAt 0.000000                      ;@line 347
						Assign OriginalBaseEscapeChance ::BaseEscapeChance_var   ;@line 348
						Assign OriginalLockPickEscapeChance ::LockPickEscapeChance_var  ;@line 349
						Assign OriginalBreakEscapeChance ::BreakDeviceEscapeChance_var  ;@line 350
						JumpF ::SendDeviceModEvents_var _label224                ;@line 351
						CallMethod SendDeviceEvent self ::NoneVar True           ;@line 352
						Jump _label224                                           ;@line 352
					_label224:
					.endCode
				.endFunction
				.function CalclulateLockPickSuccess
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Float
					.paramTable
					.endParamTable
					.localTable
						.local ::temp325 Bool
						.local result Float
						.local ::temp326 Float
						.local ::temp327 actor
						.local ::temp328 Float
						.local ::temp329 Bool
						.local ::temp330 globalvariable
						.local ::temp331 Int
						.local EscapesMade Int
					.endLocalTable
					.code
						Assign result ::LockPickEscapeChance_var                 ;@line 1367
						CompareGT ::temp325 ::LockPickEscapeChance_var 0.000000  ;@line 1369
						JumpF ::temp325 _label225                                ;@line 1369
						Cast ::temp326 EscapeLockPickAttemptsMade                ;@line 1371
						FAdd ::temp326 result ::temp326                          ;@line 1371
						Assign result ::temp326                                  ;@line 1371
						PropGet PlayerRef ::libs_var ::temp327                   ;@line 1372
						CallMethod GetAV ::temp327 ::temp326 "Lockpicking"       ;@line 1372
						Cast ::temp328 25                                        ;@line 1372
						CompareGT ::temp329 ::temp326 ::temp328                  ;@line 1372
						JumpF ::temp329 _label226                                ;@line 1372
						FAdd ::temp328 result 1.000000                           ;@line 1373
						Assign result ::temp328                                  ;@line 1373
						Jump _label226                                           ;@line 1373
					_label226:
						PropGet PlayerRef ::libs_var ::temp327                   ;@line 1375
						CallMethod GetAV ::temp327 ::temp326 "Lockpicking"       ;@line 1375
						Cast ::temp328 50                                        ;@line 1375
						CompareGT ::temp329 ::temp326 ::temp328                  ;@line 1375
						JumpF ::temp329 _label227                                ;@line 1375
						FAdd ::temp328 result 2.000000                           ;@line 1376
						Assign result ::temp328                                  ;@line 1376
						Jump _label227                                           ;@line 1376
					_label227:
						PropGet PlayerRef ::libs_var ::temp327                   ;@line 1378
						CallMethod GetAV ::temp327 ::temp326 "Lockpicking"       ;@line 1378
						Cast ::temp328 75                                        ;@line 1378
						CompareGT ::temp329 ::temp326 ::temp328                  ;@line 1378
						JumpF ::temp329 _label228                                ;@line 1378
						FAdd ::temp328 result 3.000000                           ;@line 1379
						Assign result ::temp328                                  ;@line 1379
						Jump _label228                                           ;@line 1379
					_label228:
						PropGet zadDeviceEscapeSuccessCount ::libs_var ::temp330  ;@line 1382
						CallMethod GetValueInt ::temp330 ::temp331               ;@line 1382
						Assign EscapesMade ::temp331                             ;@line 1382
						CompareGT ::temp329 EscapesMade 10                       ;@line 1383
						JumpF ::temp329 _label229                                ;@line 1383
						FAdd ::temp326 result 1.000000                           ;@line 1384
						Assign result ::temp326                                  ;@line 1384
						Jump _label229                                           ;@line 1384
					_label229:
						CompareGT ::temp329 EscapesMade 25                       ;@line 1386
						JumpF ::temp329 _label230                                ;@line 1386
						FAdd ::temp328 result 1.000000                           ;@line 1387
						Assign result ::temp328                                  ;@line 1387
						Jump _label230                                           ;@line 1387
					_label230:
						CompareGT ::temp329 EscapesMade 50                       ;@line 1389
						JumpF ::temp329 _label231                                ;@line 1389
						FAdd ::temp326 result 1.000000                           ;@line 1390
						Assign result ::temp326                                  ;@line 1390
						Jump _label231                                           ;@line 1390
					_label231:
						CompareGT ::temp329 EscapesMade 100                      ;@line 1392
						JumpF ::temp329 _label232                                ;@line 1392
						FAdd ::temp328 result 1.000000                           ;@line 1393
						Assign result ::temp328                                  ;@line 1393
						Jump _label232                                           ;@line 1393
					_label232:
						Jump _label225                                           ;@line 1393
					_label225:
						CompareLT ::temp329 result 0.000000                      ;@line 1396
						JumpF ::temp329 _label233                                ;@line 1396
						Return 0.000000                                          ;@line 1397
						Jump _label234                                           ;@line 1397
					_label233:
						CompareGT ::temp325 result 100.000000                    ;@line 1398
						JumpF ::temp325 _label234                                ;@line 1398
						Return 100.000000                                        ;@line 1399
						Jump _label234                                           ;@line 1399
					_label234:
						Return result                                            ;@line 1401
					.endCode
				.endFunction
				.function OnKeyDown
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param KeyCode Int
					.endParamTable
					.localTable
						.local ::temp70 Int
						.local ::temp71 Bool
						.local ::temp72 actor
						.local ::temp73 ObjectReference
						.local ::temp74 Bool
					.endLocalTable
					.code
						CallStatic input GetMappedKey ::temp70 "Activate" 0      ;@line 410
						CompareEQ ::temp71 KeyCode ::temp70                      ;@line 410
						JumpF ::temp71 _label235                                 ;@line 410
						PropGet PlayerRef ::libs_var ::temp72                    ;@line 411
						Cast ::temp73 ::temp72                                   ;@line 411
						CallMethod Activate self ::temp74 ::temp73 False         ;@line 411
						Jump _label235                                           ;@line 411
					_label235:
					.endCode
				.endFunction
				.function PickRandomStruggle
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Package
					.paramTable
					.endParamTable
					.localTable
						.local ::temp2 Int
						.local ::temp3 Package
					.endLocalTable
					.code
						ArrayLength ::temp2 ::StrugglePose_var                   ;@line 148
						ISubtract ::temp2 ::temp2 1                              ;@line 148
						CallStatic utility RandomInt ::temp2 0 ::temp2           ;@line 148
						ArrayGetElement ::temp3 ::StrugglePose_var ::temp2       ;@line 148
						Return ::temp3                                           ;@line 148
					.endCode
				.endFunction
				.function DisplayDifficultyMsg
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp221 Int
						.local ::temp222 String
						.local ::temp223 Bool
						.local ::temp224 Bool
						.local ::temp225 Float
						.local ::NoneVar None
						.local StruggleEscapeChance Int
						.local result String
					.endLocalTable
					.code
						CallStatic math Floor ::temp221 ::BaseEscapeChance_var   ;@line 1009
						Assign StruggleEscapeChance ::temp221                    ;@line 1009
						StrCat ::temp222 "你小心翼翼的检查着 " ::DeviceName_var  ;@line 1010
						StrCat ::temp222 ::temp222 ". "                          ;@line 1010
						Assign result ::temp222                                  ;@line 1010
						CompareGT ::temp223 StruggleEscapeChance 75              ;@line 1011
						JumpF ::temp223 _label236                                ;@line 1011
						StrCat ::temp222 result "这种拘束相当薄弱,不会对挣扎者提供太多的阻力."  ;@line 1012
						Assign result ::temp222                                  ;@line 1012
						Jump _label237                                           ;@line 1012
					_label236:
						CompareGTE ::temp224 StruggleEscapeChance 50             ;@line 1013
						JumpF ::temp224 _label238                                ;@line 1013
						StrCat ::temp222 result "这不是一个非常安全的拘束.挣扎开来应该很容易."  ;@line 1014
						Assign result ::temp222                                  ;@line 1014
						Jump _label237                                           ;@line 1014
					_label238:
						CompareGTE ::temp224 StruggleEscapeChance 25             ;@line 1015
						JumpF ::temp224 _label239                                ;@line 1015
						StrCat ::temp222 result "这种拘束有些安全,但也不是那么过分.挣扎开来将是相当困难的."  ;@line 1016
						Assign result ::temp222                                  ;@line 1016
						Jump _label237                                           ;@line 1016
					_label239:
						CompareGTE ::temp224 StruggleEscapeChance 15             ;@line 1017
						JumpF ::temp224 _label240                                ;@line 1017
						StrCat ::temp222 result "这种拘束装置设计的很安全,但如果你有足够的耐心,有可能会被挣扎开来."  ;@line 1018
						Assign result ::temp222                                  ;@line 1018
						Jump _label237                                           ;@line 1018
					_label240:
						CompareGTE ::temp224 StruggleEscapeChance 10             ;@line 1019
						JumpF ::temp224 _label241                                ;@line 1019
						StrCat ::temp222 result "这种拘束装置设计的相当安全,很难挣脱,但并非不可能."  ;@line 1020
						Assign result ::temp222                                  ;@line 1020
						Jump _label237                                           ;@line 1020
					_label241:
						CompareGTE ::temp224 StruggleEscapeChance 5              ;@line 1021
						JumpF ::temp224 _label242                                ;@line 1021
						StrCat ::temp222 result "这种拘束装置设计的非常安全,并且极难挣脱."  ;@line 1022
						Assign result ::temp222                                  ;@line 1022
						Jump _label237                                           ;@line 1022
					_label242:
						CompareGT ::temp224 StruggleEscapeChance 0               ;@line 1023
						JumpF ::temp224 _label243                                ;@line 1023
						StrCat ::temp222 result "这种拘束装置设计的极其安全,可以承受大多数穿戴者的挣扎尝试.摆脱这个装置几乎是不可能的."  ;@line 1024
						Assign result ::temp222                                  ;@line 1024
						Jump _label237                                           ;@line 1024
					_label243:
						StrCat ::temp222 result "这种拘束装置是专门为挣扎者设计的. 想要摆脱这个装置是完全不可能的!"  ;@line 1026
						Assign result ::temp222                                  ;@line 1026
					_label237:
						StrCat ::temp222 result " "                              ;@line 1028
						Assign result ::temp222                                  ;@line 1028
						Cast ::temp225 75                                        ;@line 1029
						CompareGT ::temp224 ::LockPickEscapeChance_var ::temp225  ;@line 1029
						JumpF ::temp224 _label244                                ;@line 1029
						StrCat ::temp222 result "它的锁很弱,不会对撬锁者提供太大的阻力"  ;@line 1030
						Assign result ::temp222                                  ;@line 1030
						Jump _label245                                           ;@line 1030
					_label244:
						Cast ::temp225 50                                        ;@line 1031
						CompareGTE ::temp223 ::LockPickEscapeChance_var ::temp225  ;@line 1031
						JumpF ::temp223 _label246                                ;@line 1031
						StrCat ::temp222 result "它的锁不是很安全.撬开它应该很容易。"  ;@line 1032
						Assign result ::temp222                                  ;@line 1032
						Jump _label245                                           ;@line 1032
					_label246:
						Cast ::temp225 25                                        ;@line 1033
						CompareGTE ::temp223 ::LockPickEscapeChance_var ::temp225  ;@line 1033
						JumpF ::temp223 _label247                                ;@line 1033
						StrCat ::temp222 result "它的锁有些安全,但也不是太安全.撬开它会比较困难."  ;@line 1034
						Assign result ::temp222                                  ;@line 1034
						Jump _label245                                           ;@line 1034
					_label247:
						Cast ::temp225 15                                        ;@line 1035
						CompareGTE ::temp223 ::LockPickEscapeChance_var ::temp225  ;@line 1035
						JumpF ::temp223 _label248                                ;@line 1035
						StrCat ::temp222 result "它的锁设计的很安全,但可能无法承受高强度的撬锁尝试."  ;@line 1036
						Assign result ::temp222                                  ;@line 1036
						Jump _label245                                           ;@line 1036
					_label248:
						Cast ::temp225 10                                        ;@line 1037
						CompareGTE ::temp223 ::LockPickEscapeChance_var ::temp225  ;@line 1037
						JumpF ::temp223 _label249                                ;@line 1037
						StrCat ::temp222 result "它的锁相当安全,很难撬开,但也并非不可能."  ;@line 1038
						Assign result ::temp222                                  ;@line 1038
						Jump _label245                                           ;@line 1038
					_label249:
						Cast ::temp225 5                                         ;@line 1039
						CompareGTE ::temp223 ::LockPickEscapeChance_var ::temp225  ;@line 1039
						JumpF ::temp223 _label250                                ;@line 1039
						StrCat ::temp222 result "它的锁非常安全,极难撬开."  ;@line 1040
						Assign result ::temp222                                  ;@line 1040
						Jump _label245                                           ;@line 1040
					_label250:
						Cast ::temp225 0                                         ;@line 1041
						CompareGT ::temp223 ::LockPickEscapeChance_var ::temp225  ;@line 1041
						JumpF ::temp223 _label251                                ;@line 1041
						StrCat ::temp222 result "它的锁是特制的,可以承受大多数的撬锁尝试."  ;@line 1042
						Assign result ::temp222                                  ;@line 1042
						Jump _label245                                           ;@line 1042
					_label251:
						StrCat ::temp222 result "它有一个防篡改锁.如果没有正确的钥匙,解锁它是绝对不可能的!"  ;@line 1044
						Assign result ::temp222                                  ;@line 1044
					_label245:
						StrCat ::temp222 result " "                              ;@line 1046
						Assign result ::temp222                                  ;@line 1046
						Cast ::temp225 75                                        ;@line 1047
						CompareGT ::temp223 ::BreakDeviceEscapeChance_var ::temp225  ;@line 1047
						JumpF ::temp223 _label252                                ;@line 1047
						StrCat ::temp222 result "它的制材很脆弱,不会对破坏者提供太多的抗断裂能力"  ;@line 1048
						Assign result ::temp222                                  ;@line 1048
						Jump _label253                                           ;@line 1048
					_label252:
						Cast ::temp225 50                                        ;@line 1049
						CompareGTE ::temp224 ::BreakDeviceEscapeChance_var ::temp225  ;@line 1049
						JumpF ::temp224 _label254                                ;@line 1049
						StrCat ::temp222 result "它的制材是不太坚固的.打破它应该很容易."  ;@line 1050
						Assign result ::temp222                                  ;@line 1050
						Jump _label253                                           ;@line 1050
					_label254:
						Cast ::temp225 25                                        ;@line 1051
						CompareGTE ::temp224 ::BreakDeviceEscapeChance_var ::temp225  ;@line 1051
						JumpF ::temp224 _label255                                ;@line 1051
						StrCat ::temp222 result "它的材质有点坚固,但也不过分.打破它的难度适中."  ;@line 1052
						Assign result ::temp222                                  ;@line 1052
						Jump _label253                                           ;@line 1052
					_label255:
						Cast ::temp225 15                                        ;@line 1053
						CompareGTE ::temp224 ::BreakDeviceEscapeChance_var ::temp225  ;@line 1053
						JumpF ::temp224 _label256                                ;@line 1053
						StrCat ::temp222 result "它的材料是坚固的,但如果有正确的工具和足够的努力,是可能打破的."  ;@line 1054
						Assign result ::temp222                                  ;@line 1054
						Jump _label253                                           ;@line 1054
					_label256:
						Cast ::temp225 10                                        ;@line 1055
						CompareGTE ::temp224 ::BreakDeviceEscapeChance_var ::temp225  ;@line 1055
						JumpF ::temp224 _label257                                ;@line 1055
						StrCat ::temp222 result "它的材料相当坚固的,很难打破,但也绝不是不可能的."  ;@line 1056
						Assign result ::temp222                                  ;@line 1056
						Jump _label253                                           ;@line 1056
					_label257:
						Cast ::temp225 5                                         ;@line 1057
						CompareGTE ::temp224 ::BreakDeviceEscapeChance_var ::temp225  ;@line 1057
						JumpF ::temp224 _label258                                ;@line 1057
						StrCat ::temp222 result "它的材料非常坚固,极难破碎."  ;@line 1058
						Assign result ::temp222                                  ;@line 1058
						Jump _label253                                           ;@line 1058
					_label258:
						Cast ::temp225 0                                         ;@line 1059
						CompareGT ::temp224 ::BreakDeviceEscapeChance_var ::temp225  ;@line 1059
						JumpF ::temp224 _label259                                ;@line 1059
						StrCat ::temp222 result "它的材料异常坚固,可以承受大多数破坏它的尝试."  ;@line 1060
						Assign result ::temp222                                  ;@line 1060
						Jump _label253                                           ;@line 1060
					_label259:
						StrCat ::temp222 result "它是用任何工具和手段都无法破坏的材料制成的!"  ;@line 1062
						Assign result ::temp222                                  ;@line 1062
					_label253:
						CallMethod notify ::libs_var ::NoneVar result True       ;@line 1064
					.endCode
				.endFunction
				.function DeviceMenuNPC
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp109 referencealias
						.local ::temp110 ObjectReference
						.local ::NoneVar None
						.local ::temp111 Int
						.local ::temp112 Bool
						.local i Int
						.local ::temp113 Bool
					.endLocalTable
					.code
						PropGet UserRef ::clib_var ::temp109                     ;@line 616
						Cast ::temp110 ::user_var                                ;@line 616
						CallMethod ForceRefTo ::temp109 ::NoneVar ::temp110      ;@line 616
						CallMethod Show ::zadc_DeviceMsgNPCLocked_var ::temp111 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 617
						Assign i ::temp111                                       ;@line 617
						PropGet UserRef ::clib_var ::temp109                     ;@line 618
						CallMethod Clear ::temp109 ::NoneVar                     ;@line 618
						CompareEQ ::temp112 i 0                                  ;@line 619
						JumpF ::temp112 _label260                                ;@line 619
						CallMethod UnlockAttemptNPC self ::temp113               ;@line 621
						Jump _label261                                           ;@line 621
					_label260:
						CompareEQ ::temp113 i 1                                  ;@line 622
						JumpF ::temp113 _label262                                ;@line 622
						CallMethod EscapeAttemptNPC self ::NoneVar               ;@line 624
						Jump _label261                                           ;@line 624
					_label262:
						CompareEQ ::temp113 i 2                                  ;@line 625
						JumpF ::temp113 _label261                                ;@line 625
						CallMethod DisplayDifficultyMsg self ::NoneVar           ;@line 627
						Jump _label261                                           ;@line 627
					_label261:
					.endCode
				.endFunction
				.function Escape
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return Bool
					.paramTable
						.param Chance Float
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp256 Bool
						.local ::temp257 String
						.local ::temp258 String
						.local ::temp259 Float
						.local ::temp260 Float
						.local ::temp261 globalvariable
						.local ::temp262 globalvariable
						.local ::temp263 Int
						.local ::temp264 Bool
						.local ::temp265 Bool
						.local totalattemptsmade Int
					.endLocalTable
					.code
						CallMethod StruggleScene self ::NoneVar ::user_var       ;@line 1128
						CompareEQ ::temp256 Chance 0.000000                      ;@line 1129
						JumpF ::temp256 _label263                                ;@line 1129
						Return False                                             ;@line 1131
						Jump _label263                                           ;@line 1131
					_label263:
						StrCat ::temp257 "玩家正在尝试/帮助逃脱 " ::DeviceName_var  ;@line 1133
						StrCat ::temp257 ::temp257 ". 修正后逃脱几率: "   ;@line 1133
						Cast ::temp258 Chance                                    ;@line 1133
						StrCat ::temp258 ::temp257 ::temp258                     ;@line 1133
						StrCat ::temp257 ::temp258 "%"                           ;@line 1133
						CallMethod log ::libs_var ::NoneVar ::temp257 0          ;@line 1133
						CallStatic utility RandomFloat ::temp259 0.000000 99.900002  ;@line 1134
						CallMethod CalculateDifficultyModifier self ::temp260 True  ;@line 1134
						FMultiply ::temp260 Chance ::temp260                     ;@line 1134
						CompareLT ::temp256 ::temp259 ::temp260                  ;@line 1134
						JumpF ::temp256 _label264                                ;@line 1134
						StrCat ::temp258 "用户已逃脱 " ::DeviceName_var     ;@line 1135
						CallMethod log ::libs_var ::NoneVar ::temp258 0          ;@line 1135
						PropGet zadDeviceEscapeSuccessCount ::libs_var ::temp261  ;@line 1137
						PropGet zadDeviceEscapeSuccessCount ::libs_var ::temp262  ;@line 1137
						CallMethod GetValueInt ::temp262 ::temp263               ;@line 1137
						IAdd ::temp263 ::temp263 1                               ;@line 1137
						CallMethod SetValueInt ::temp261 ::NoneVar ::temp263     ;@line 1137
						Return True                                              ;@line 1139
						Jump _label265                                           ;@line 1139
					_label264:
						StrCat ::temp257 "用户逃脱失败: " ::DeviceName_var  ;@line 1141
						CallMethod log ::libs_var ::NoneVar ::temp257 0          ;@line 1141
						CompareGT ::temp264 ::MercyEscapeAttempts_var 0          ;@line 1143
						JumpF ::temp264 _label265                                ;@line 1143
						IAdd ::temp263 EscapeLockPickAttemptsMade EscapeBreakAttemptsMade  ;@line 1144
						IAdd ::temp263 ::temp263 EscapeStruggleAttemptsMade      ;@line 1144
						Assign totalattemptsmade ::temp263                       ;@line 1144
						CompareGTE ::temp265 totalattemptsmade ::MercyEscapeAttempts_var  ;@line 1145
						JumpF ::temp265 _label266                                ;@line 1145
						StrCat ::temp258 ::DeviceName_var "触发仁慈逃脱"   ;@line 1146
						CallMethod log ::libs_var ::NoneVar ::temp258 0          ;@line 1146
						Return True                                              ;@line 1147
						Jump _label266                                           ;@line 1147
					_label266:
						Jump _label265                                           ;@line 1147
					_label265:
						Return False                                             ;@line 1151
					.endCode
				.endFunction
				.function EscapeAttemptLockPick
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp304 Bool
						.local ::temp306 Int
						.local ::temp307 Float
						.local ::temp308 actor
						.local ::temp309 Bool
						.local ::temp305 String
						.local ::NoneVar None
						.local ::temp310 Bool
					.endLocalTable
					.code
						CallMethod HasValidLockPick self ::temp304               ;@line 1300
						Not ::temp304 ::temp304                                  ;@line 1300
						JumpF ::temp304 _label267                                ;@line 1300
						StrCat ::temp305 "你没有可以在 " ::DeviceName_var  ;@line 1301
						StrCat ::temp305 ::temp305 "上使用的选择权."      ;@line 1301
						CallMethod notify ::libs_var ::NoneVar ::temp305 True    ;@line 1301
						Return None                                              ;@line 1302
						Jump _label267                                           ;@line 1302
					_label267:
						CallMethod CanMakeLockPickEscapeAttempt self ::temp304   ;@line 1304
						Not ::temp304 ::temp304                                  ;@line 1304
						JumpF ::temp304 _label268                                ;@line 1304
						Return None                                              ;@line 1305
						Jump _label268                                           ;@line 1305
					_label268:
						CallMethod Show ::zadc_EscapeLockPickMSG_var ::temp306 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1307
						CallStatic utility RandomFloat ::temp307 0.000000 99.900002  ;@line 1309
						CompareLT ::temp304 ::temp307 ::LockAccessDifficulty_var  ;@line 1309
						Cast ::temp304 ::temp304                                 ;@line 1309
						JumpF ::temp304 _label269                                ;@line 1309
						PropGet PlayerRef ::libs_var ::temp308                   ;@line 1309
						CompareEQ ::temp309 ::user_var ::temp308                 ;@line 1309
						Cast ::temp304 ::temp309                                 ;@line 1309
					_label269:
						JumpF ::temp304 _label270                                ;@line 1309
						StrCat ::temp305 "你没能达到你的 " ::DeviceName_var  ;@line 1310
						StrCat ::temp305 ::temp305 "的锁孔,无法尝试撬开锁."  ;@line 1310
						CallMethod notify ::libs_var ::NoneVar ::temp305 True    ;@line 1310
						Return None                                              ;@line 1311
						Jump _label270                                           ;@line 1311
					_label270:
						CallMethod CalclulateLockPickSuccess self ::temp307      ;@line 1313
						CallMethod Escape self ::temp309 ::temp307               ;@line 1313
						JumpF ::temp309 _label271                                ;@line 1313
						CallMethod Show ::zadc_EscapeLockPickSuccessMSG_var ::temp306 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1314
						CallMethod SelfBondageReward self ::NoneVar              ;@line 1315
						CallMethod UnlockActor self ::NoneVar                    ;@line 1316
						Jump _label272                                           ;@line 1316
					_label271:
						CallStatic utility RandomFloat ::temp307 0.000000 99.900002  ;@line 1319
						CompareLT ::temp304 ::temp307 ::CatastrophicFailureChance_var  ;@line 1319
						Cast ::temp304 ::temp304                                 ;@line 1319
						JumpF ::temp304 _label273                                ;@line 1319
						PropGet PlayerRef ::libs_var ::temp308                   ;@line 1319
						CompareEQ ::temp310 ::user_var ::temp308                 ;@line 1319
						Cast ::temp304 ::temp310                                 ;@line 1319
					_label273:
						JumpF ::temp304 _label274                                ;@line 1319
						Assign ::LockPickEscapeChance_var 0.000000               ;@line 1320
						StrCat ::temp305 "你没能逃脱你的 " ::DeviceName_var  ;@line 1321
						StrCat ::temp305 ::temp305 " 并且因为你微弱的尝试导致锁内安全机制启动安全防护罩,防止你进一步的撬锁尝试."  ;@line 1321
						CallMethod notify ::libs_var ::NoneVar ::temp305 True    ;@line 1321
						Jump _label272                                           ;@line 1321
					_label274:
						IAdd ::temp306 EscapeLockPickAttemptsMade 1              ;@line 1324
						Assign EscapeLockPickAttemptsMade ::temp306              ;@line 1324
						CallMethod DestroyLockPick self ::temp310                ;@line 1326
						CallMethod Show ::zadc_EscapeLockPickFailureMSG_var ::temp306 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1327
					_label272:
					.endCode
				.endFunction
				.function EscapeAttemptBreak
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::temp289 Bool
						.local ::temp290 Int
						.local ::temp291 Float
						.local ::NoneVar None
						.local ::temp292 Bool
						.local ::temp293 actor
						.local ::temp294 Bool
						.local ::temp295 String
					.endLocalTable
					.code
						CallMethod CanMakeBreakEscapeAttempt self ::temp289      ;@line 1240
						Not ::temp289 ::temp289                                  ;@line 1240
						JumpF ::temp289 _label275                                ;@line 1240
						Return None                                              ;@line 1241
						Jump _label275                                           ;@line 1241
					_label275:
						CallMethod Show ::zadc_EscapeBreakMSG_var ::temp290 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1243
						CallMethod CalclulateBreakSuccess self ::temp291         ;@line 1244
						CallMethod Escape self ::temp289 ::temp291               ;@line 1244
						JumpF ::temp289 _label276                                ;@line 1244
						CallMethod Show ::zadc_EscapeBreakSuccessMSG_var ::temp290 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1245
						CallMethod SelfBondageReward self ::NoneVar              ;@line 1246
						CallMethod UnlockActor self ::NoneVar                    ;@line 1247
						Jump _label277                                           ;@line 1247
					_label276:
						CallStatic utility RandomFloat ::temp291 0.000000 99.900002  ;@line 1250
						CompareLT ::temp292 ::temp291 ::CatastrophicFailureChance_var  ;@line 1250
						Cast ::temp292 ::temp292                                 ;@line 1250
						JumpF ::temp292 _label278                                ;@line 1250
						PropGet PlayerRef ::libs_var ::temp293                   ;@line 1250
						CompareEQ ::temp294 ::user_var ::temp293                 ;@line 1250
						Cast ::temp292 ::temp294                                 ;@line 1250
					_label278:
						JumpF ::temp292 _label279                                ;@line 1250
						Assign ::BreakDeviceEscapeChance_var 0.000000            ;@line 1251
						StrCat ::temp295 "你没能逃脱你的 " ::DeviceName_var  ;@line 1252
						StrCat ::temp295 ::temp295 " 并且因为你微弱的尝试导致装置拉得太紧,以至于你永远无法将其打开."  ;@line 1252
						CallMethod notify ::libs_var ::NoneVar ::temp295 True    ;@line 1252
						Jump _label277                                           ;@line 1252
					_label279:
						IAdd ::temp290 EscapeBreakAttemptsMade 1                 ;@line 1255
						Assign EscapeBreakAttemptsMade ::temp290                 ;@line 1255
						CallMethod Show ::zadc_EscapeBreakFailureMSG_var ::temp290 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 1256
					_label277:
					.endCode
				.endFunction
				.function UnlockActor
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
					.endParamTable
					.localTable
						.local ::NoneVar None
						.local ::temp63 ObjectReference
						.local ::temp64 Bool
						.local ::temp65 actor
						.local ::temp66 armor
						.local ::temp67 armor
						.local ::temp68 form
						.local ::temp69 Int
					.endLocalTable
					.code
						JumpF ::user_var _label280                               ;@line 357
						CallMethod enable self ::NoneVar False                   ;@line 358
						CallMethod UnregisterForUpdate self ::NoneVar            ;@line 359
						CallMethod UnregisterForAllKeys self ::NoneVar           ;@line 360
						Cast ::temp63 None                                       ;@line 361
						CallMethod SetVehicle ::user_var ::NoneVar ::temp63      ;@line 361
						CallStatic actorutil RemovePackageOverride ::temp64 ::user_var CurrentPose  ;@line 362
						Cast ::temp63 ::user_var                                 ;@line 363
						CallStatic debug SendAnimationEvent ::NoneVar ::temp63 "IdleForceDefaultState"  ;@line 363
						CallStatic game GetPlayer ::temp65                       ;@line 364
						CompareEQ ::temp64 ::user_var ::temp65                   ;@line 364
						JumpF ::temp64 _label281                                 ;@line 364
						CallStatic game SetPlayerAIDriven ::NoneVar False        ;@line 365
						CallStatic game EnablePlayerControls ::NoneVar True True True True True True True True 0  ;@line 366
						PropGet zadc_NoWaitItem ::clib_var ::temp66              ;@line 367
						PropGet zadc_NoWaitItem ::clib_var ::temp67              ;@line 367
						Cast ::temp68 ::temp67                                   ;@line 367
						CallMethod GetItemCount ::user_var ::temp69 ::temp68     ;@line 367
						Cast ::temp68 ::temp66                                   ;@line 367
						CallMethod RemoveItem ::user_var ::NoneVar ::temp68 ::temp69 True None  ;@line 367
						Jump _label282                                           ;@line 367
					_label281:
						CallMethod SetDontMove ::user_var ::NoneVar False        ;@line 369
						CallMethod SetHeadTracking ::user_var ::NoneVar True     ;@line 370
						CallMethod SetRestrained ::user_var ::NoneVar False      ;@line 371
					_label282:
						CallStatic utility Wait ::NoneVar 0.200000               ;@line 373
						Cast ::temp63 ::user_var                                 ;@line 374
						CallMethod moveto ::user_var ::NoneVar ::temp63 0.000000 0.000000 0.000000 True  ;@line 374
						CallMethod SetPosition ::user_var ::NoneVar PosX PosY PosZ  ;@line 375
						CallMethod RemoveEffects self ::NoneVar ::user_var       ;@line 376
						CallMethod ResetNiOverrideOverride ::clib_var ::NoneVar ::user_var  ;@line 377
						CallMethod RemoveDevices self ::NoneVar ::user_var       ;@line 378
						JumpF ::ForceStripActor_var _label283                    ;@line 379
						CallMethod RestoreOutfit ::clib_var ::NoneVar ::user_var  ;@line 380
						Jump _label283                                           ;@line 380
					_label283:
						CallMethod RestoreBondage ::clib_var ::NoneVar ::user_var  ;@line 382
						CallMethod EvaluatePackage ::user_var ::NoneVar          ;@line 383
						JumpF ::SendDeviceModEvents_var _label284                ;@line 384
						CallMethod SendDeviceEvent self ::NoneVar False          ;@line 385
						Jump _label284                                           ;@line 385
					_label284:
						Jump _label285                                           ;@line 385
					_label280:
						Return None                                              ;@line 389
					_label285:
						CallMethod ClearDevice ::clib_var ::NoneVar ::user_var   ;@line 391
						Cast ::temp65 None                                       ;@line 396
						Assign ::user_var ::temp65                               ;@line 396
						Assign ::isSelfBondage_var False                         ;@line 397
						Assign isLockManipulated False                           ;@line 398
						JumpF ::DestroyOnRemove_var _label286                    ;@line 399
						CallMethod disable self ::NoneVar False                  ;@line 400
						CallMethod Delete self ::NoneVar                         ;@line 401
						Jump _label287                                           ;@line 401
					_label286:
						Assign ::BaseEscapeChance_var OriginalBaseEscapeChance   ;@line 403
						Assign ::LockPickEscapeChance_var OriginalLockPickEscapeChance  ;@line 404
						Assign ::BreakDeviceEscapeChance_var OriginalBreakEscapeChance  ;@line 405
					_label287:
					.endCode
				.endFunction
				.function OnActivate
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param akActionRef ObjectReference
					.endParamTable
					.localTable
						.local ::temp132 actor
						.local ::temp133 Bool
						.local act actor
						.local ::temp134 Bool
						.local ::temp135 Bool
						.local ::NoneVar None
						.local ::temp136 Bool
					.endLocalTable
					.code
						JumpF Mutex _label288                                    ;@line 697
						Return None                                              ;@line 698
						Jump _label288                                           ;@line 698
					_label288:
						Assign Mutex True                                        ;@line 700
						Cast ::temp132 akActionRef                               ;@line 701
						Assign act ::temp132                                     ;@line 701
						Not ::temp133 ::user_var                                 ;@line 702
						JumpF ::temp133 _label289                                ;@line 702
						PropGet PlayerRef ::libs_var ::temp132                   ;@line 704
						CompareEQ ::temp134 act ::temp132                        ;@line 704
						JumpF ::temp134 _label290                                ;@line 704
						Not ::temp135 ::ScriptedDevice_var                       ;@line 705
						JumpF ::temp135 _label291                                ;@line 705
						CallMethod DeviceMenuLock self ::NoneVar                 ;@line 706
						Jump _label292                                           ;@line 706
					_label291:
						CallMethod notify ::libs_var ::NoneVar "此装置不能与其交互." False  ;@line 708
					_label292:
						Jump _label293                                           ;@line 708
					_label290:
						PropGet SelectedUser ::clib_var ::temp132                ;@line 712
						CompareEQ ::temp135 act ::temp132                        ;@line 712
						JumpF ::temp135 _label294                                ;@line 712
						Not ::temp136 ::ScriptedDevice_var                       ;@line 713
						JumpF ::temp136 _label295                                ;@line 713
						CallMethod DeviceMenuLockNPC self ::NoneVar act          ;@line 714
						Jump _label296                                           ;@line 714
					_label295:
						CallMethod notify ::libs_var ::NoneVar "此装置不能与其交互." False  ;@line 716
					_label296:
						Jump _label293                                           ;@line 716
					_label294:
						CallMethod LockActor self ::NoneVar act                  ;@line 720
					_label293:
						Jump _label297                                           ;@line 720
					_label289:
						JumpF ::user_var _label297                               ;@line 723
						CompareEQ ::temp136 ::user_var act                       ;@line 725
						Not ::temp136 ::temp136                                  ;@line 725
						JumpF ::temp136 _label298                                ;@line 725
						PropGet PlayerRef ::libs_var ::temp132                   ;@line 728
						CompareEQ ::temp135 act ::temp132                        ;@line 728
						JumpF ::temp135 _label299                                ;@line 728
						Not ::temp134 ::ScriptedDevice_var                       ;@line 729
						JumpF ::temp134 _label300                                ;@line 729
						CallMethod DeviceMenuNPC self ::NoneVar                  ;@line 730
						Jump _label301                                           ;@line 730
					_label300:
						CallMethod notify ::libs_var ::NoneVar "此装置不能与其交互." False  ;@line 732
					_label301:
						Jump _label302                                           ;@line 732
					_label299:
						CallMethod UnlockActor self ::NoneVar                    ;@line 735
					_label302:
						Jump _label303                                           ;@line 735
					_label298:
						PropGet PlayerRef ::libs_var ::temp132                   ;@line 739
						CompareEQ ::temp134 act ::temp132                        ;@line 739
						JumpF ::temp134 _label303                                ;@line 739
						Not ::temp135 ::ScriptedDevice_var                       ;@line 741
						JumpF ::temp135 _label304                                ;@line 741
						CallMethod DeviceMenuUnlock self ::NoneVar               ;@line 742
						Jump _label305                                           ;@line 742
					_label304:
						CallMethod notify ::libs_var ::NoneVar "此装置不能与其交互." False  ;@line 744
					_label305:
						Jump _label303                                           ;@line 744
					_label303:
						Jump _label297                                           ;@line 744
					_label297:
						Assign Mutex False                                       ;@line 749
					.endCode
				.endFunction
				.function DeviceMenuLockNPC
					.userFlags 0	; Flags: 0x00000000
					.docString ""
					.return None
					.paramTable
						.param subject actor
					.endParamTable
					.localTable
						.local ::temp97 referencealias
						.local ::temp98 ObjectReference
						.local ::NoneVar None
						.local ::temp99 Int
						.local ::temp100 Bool
						.local i Int
						.local ::temp101 Bool
						.local ::temp102 zadconfig
						.local ::temp103 Bool
						.local Choice Int
						.local ::temp104 Bool
						.local ::temp105 Bool
						.local Choice2 Int
					.endLocalTable
					.code
						PropGet UserRef ::clib_var ::temp97                      ;@line 550
						Cast ::temp98 subject                                    ;@line 550
						CallMethod ForceRefTo ::temp97 ::NoneVar ::temp98        ;@line 550
						CallMethod Show ::zadc_DeviceMsgNPCNotLocked_var ::temp99 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 552
						Assign i ::temp99                                        ;@line 552
						CompareEQ ::temp100 i 0                                  ;@line 553
						JumpF ::temp100 _label306                                ;@line 553
						Assign isLockManipulated False                           ;@line 554
						Assign ::isSelfBondage_var False                         ;@line 555
						Not ::temp101 ::DisableLockManipulation_var              ;@line 556
						Cast ::temp101 ::temp101                                 ;@line 556
						JumpF ::temp101 _label307                                ;@line 556
						PropGet Config ::libs_var ::temp102                      ;@line 556
						PropGet UseItemManipulation ::temp102 ::temp103          ;@line 556
						Cast ::temp101 ::temp103                                 ;@line 556
					_label307:
						JumpF ::temp101 _label308                                ;@line 556
						CallMethod Show ::zadc_OnLockDeviceNPCMSG_var ::temp99 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 557
						Assign Choice ::temp99                                   ;@line 557
						CompareEQ ::temp103 Choice 1                             ;@line 558
						JumpF ::temp103 _label309                                ;@line 558
						Assign ::isSelfBondage_var True                          ;@line 560
						JumpF ::AllowTimerDialogue_var _label310                 ;@line 561
						CallMethod Show ::zadc_SelfbondageMSG_var ::temp99 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 562
						Assign Choice2 ::temp99                                  ;@line 562
						CompareEQ ::temp104 Choice2 0                            ;@line 563
						JumpF ::temp104 _label311                                ;@line 563
						Assign ::SelfBondageReleaseTimer_var 1.000000            ;@line 564
						Jump _label312                                           ;@line 564
					_label311:
						CompareEQ ::temp105 Choice2 1                            ;@line 565
						JumpF ::temp105 _label313                                ;@line 565
						Assign ::SelfBondageReleaseTimer_var 2.000000            ;@line 566
						Jump _label312                                           ;@line 566
					_label313:
						CompareEQ ::temp105 Choice2 2                            ;@line 567
						JumpF ::temp105 _label314                                ;@line 567
						Assign ::SelfBondageReleaseTimer_var 5.000000            ;@line 568
						Jump _label312                                           ;@line 568
					_label314:
						CompareEQ ::temp105 Choice2 3                            ;@line 569
						JumpF ::temp105 _label315                                ;@line 569
						Assign ::SelfBondageReleaseTimer_var 12.000000           ;@line 570
						Jump _label312                                           ;@line 570
					_label315:
						CompareEQ ::temp105 Choice2 4                            ;@line 571
						JumpF ::temp105 _label316                                ;@line 571
						Assign ::SelfBondageReleaseTimer_var 24.000000           ;@line 572
						Jump _label312                                           ;@line 572
					_label316:
						CompareEQ ::temp105 Choice2 1                            ;@line 573
						JumpF ::temp105 _label312                                ;@line 573
						PropGet UserRef ::clib_var ::temp97                      ;@line 574
						CallMethod Clear ::temp97 ::NoneVar                      ;@line 574
						Return None                                              ;@line 575
						Jump _label312                                           ;@line 575
					_label312:
						Jump _label310                                           ;@line 575
					_label310:
						Jump _label317                                           ;@line 575
					_label309:
						CompareEQ ::temp105 Choice 2                             ;@line 578
						JumpF ::temp105 _label318                                ;@line 578
						Assign isLockManipulated True                            ;@line 580
						Jump _label317                                           ;@line 580
					_label318:
						CallMethod Show ::zadc_OnDeviceLockMSG_var ::temp99 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 583
					_label317:
						Jump _label319                                           ;@line 583
					_label308:
						CallMethod Show ::zadc_OnDeviceLockMSG_var ::temp99 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 586
					_label319:
						Jump _label320                                           ;@line 586
					_label306:
						CompareEQ ::temp104 i 1                                  ;@line 588
						JumpF ::temp104 _label321                                ;@line 588
						CallMethod DisplayDifficultyMsg self ::NoneVar           ;@line 590
						Return None                                              ;@line 591
						Jump _label320                                           ;@line 591
					_label321:
						CompareEQ ::temp105 i 2                                  ;@line 592
						JumpF ::temp105 _label320                                ;@line 592
						CallMethod Show ::zadc_OnLeaveItNotLockedMSG_var ::temp99 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000 0.000000  ;@line 594
						Return None                                              ;@line 595
						Jump _label320                                           ;@line 595
					_label320:
						PropGet UserRef ::clib_var ::temp97                      ;@line 597
						CallMethod Clear ::temp97 ::NoneVar                      ;@line 597
						CallMethod LockActor self ::NoneVar subject              ;@line 598
					.endCode
				.endFunction
			.endState
		.endStateTable
	.endObject
.endObjectTable
