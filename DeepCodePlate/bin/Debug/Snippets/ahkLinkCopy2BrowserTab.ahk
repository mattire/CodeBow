;{ahk}

;#SingleInstance
;^r::reload

f1::
    ; Send, ^{tab}
    NewTab()
    sleep, 200
    ChangeWindow()
    sleep, 200
    CopyLine()
    Send, {down}
    sleep, 100
    ChangeWindow()
    SelectAddressBarAddress()
    Paste()
return

NewTab()
{
    ;Send, ^{tab}
    ; Send, {ctrldown}{tab}{ctrlup}
    Send, !f
    Send, t
}

^f1::CopyLine()

SelectAddressBarAddress()
{
    Send, !d
    sleep, 100
    Send, ^a
    sleep, 100
}

CopyLine()
{
	Send, {home}
	;Send, {SHIFTDOWN}{end}{SHIFTUP}
    Send, +{End}
    sleep, 500
    Send, ^c
    sleep, 500
    clipwait
    
    ;MsgBox, copied
}

ChangeWindow()
{
    Send, !{tab}
    Sleep, 300
}

Paste()
{
    Send, ^v
    Sleep, 500
    Send, {enter}
}

^enter::
	ExitApp
return

+enter::
	Suspend, permit
	Suspend, toggle
return