#f3::
	Suspend, permit
	Suspend, toggle
return
^r::reload

;^Space::RunSnips()
;+^Space::RunSnipCreator()

#-::RunSnips()
#/::RunSnips()
; +#-::RunSnipCreator()


; ^,::RunSnips()

; ^.::
	; MsgBox, %A_CaretX% %A_CaretY%
;	; VarSetCapacity(GuiThreadInfo, 48)
;	; NumPut(48, GuiThreadInfo,,"UInt")
;	; 
;	; DllCall("GetGUIThreadInfo", int, 0, ptr, &GuiThreadInfo)
;	; 
;	; left := NumGet(&GuiThreadInfo+8*4)
;	; top := NumGet(&GuiThreadInfo+9*4)
;	; 
;	; MsgBox, %left% %top%
; return

RunSnips(){
    Send, ^c
    ; Run %A_ScriptDir%\CodeBow.exe
    
    Run, CodeBow.exe, %A_ScriptDir%
    ;SetTitleMatchMode, 2
    WinWait, CodeBow
    Sleep, 50
    WinWaitClose, CodeBow
    Sleep, 50
    
    
    
    ;bgn := SubStr(clipboard, 1, 7)
    ;if(bgn=="{tab}`r`n")
    ; if(StartsWith(clipboard, "{tab}`r`n"))
    if(StartsWith(clipboard, "{tab}"))
    {
        RunTabSequence()
    }
    else
    {
        WinGetTitle, WinTitle, A
        StringLeft, StartStr, WinTitle, 7
        if(StartStr="MINGW64")
        {
            Send, +{insert}
        }
        else
        {
            Send, ^v
        }
    }
}


RunSnipCreator() {
    Run %A_ScriptDir%\FtlSnippetCreator.exe
}

RunTabSequence() {
    ; theRest := SubStr(clipboard, 8)
    ; Send, %theRest% 
    ; arr := StrSplit(theRest,"`n")
    arr := StrSplit(clipboard,"`n")
    arr.Remove(0)
    
    Loop % arr.MaxIndex()
    {
        txt := arr[A_Index]
        if(StartsWith(txt, "{coord}"))
        {
            coords := SubStr(arr[A_Index], 8)
            c_arr := StrSplit(coords,",")
            MouseClick, Left, c_arr.1, c_arr.2
            Sleep, 2500
        } 
        else if(StartsWith(txt, "{adr}"))
        {
            addr := SubStr(arr[A_Index], 6)
            ; addr := SubStr(arr.1, 6)
            Send, !{d}
            Sleep, 1000
            Send, %addr%
            Sleep, 100
            Send, {enter}
            Sleep, 2500
        }
        else if(StartsWith(txt, "{exe}"))
        {
            location := SubStr(arr[A_Index], 6)    
            ; Run C:\Windows\notepad.exe %A_ScriptDir%\help.txt
            Run %location%
            Sleep, 2500
        }        
        else if(StartsWith(txt, "{ctrl}"))
        {
            chars := SubStr(arr[A_Index], 7)
            Send, {Ctrl Down}
            Send, %chars%
            Send, {Ctrl Up}
        }
        else {
            SendRaw, %txt%
            Send, {tab}
        }
    }
}

StartsWith(str, startsStr)
{
    len := StrLen(startsStr) 
    ; MsgBox, %len%
    start := SubStr(str, 1, len)
    ; MsgBox, %start%
    if(start == startsStr){
        return true
    } else {
        return false
    }
}
