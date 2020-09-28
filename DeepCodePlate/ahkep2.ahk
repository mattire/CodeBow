
if(StartsWith(clipboard, "{tab}"))
{
    RunTabSequence()
}
else
{
    ;WinGetTitle, WinTitle, A
    ;WinGetTitle, WinTitle, ahk_class
    WinGetActiveTitle, WinTitle
    StringLeft, StartStr, WinTitle, 7
    ;MsgBox, %WinTitle%
    if(StartStr="MINGW64")
    {
        Send, +{insert}
    }
    else
    {
        flMsVS := FloatingMSVisualStudioWindow()
        ;endsVS := EndsWith(WinTitle, "Visual Studio")
        ;endsVS := EndsWith(WinTitle, " - Microsoft Visual Studio")
        ;MsgBox, %flMsVS%
        if(flMsVS==1)
        {
            ;MsgBox, esc
            ;Sleep, 10
            Send, {Esc}
        }
        Send, ^v
    }
}
WinSet, Bottom,,CodeBow


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
            Sleep, 150
        } 
        else if(StartsWith(txt, "{rcoord}"))
        {
            coords := SubStr(arr[A_Index], 8)
            c_arr := StrSplit(coords,",")
            MouseClick, Right, c_arr.1, c_arr.2
            Sleep, 150        
        }
        else if(StartsWith(txt, "{adr}"))
        {
            addr := SubStr(arr[A_Index], 6)
            ; addr := SubStr(arr.1, 6)
            Send, !{d}
            Sleep, 1000
            ;Send, %addr%
            Paste(addr)
            Sleep, 1000
            ;tooltip, down
            ;Send {down}
            ;Sleep, 1000
            ;tooltip, esc
            ;Send {esc}
            Sleep, 1000
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
        else if(StartsWith(txt, "{alt}"))
        {
            chars := SubStr(arr[A_Index], 6)
            Send, {Alt Down}
            Send, %chars%
            Send, {Alt Up}
        }
        else if(StartsWith(txt, "{shiftinsert}"))
        {
            ;chars := SubStr(arr[A_Index], 6)
            Send, {Shift Down}
            Send, {insert}
            Send, {Shift Up}
        }
        else if(StartsWith(txt, "{send}"))
        {
            chars := SubStr(arr[A_Index], 7)
            Send, %chars%
        }
        else {
            SendRaw, %txt%
            Send, {tab}
        }
    }
}

Paste(str)
{
    ;MsgBox, %str%
    tmp := clipboard
    clipboard:=str
    ;MsgBox, %clipboard%
    Send, ^v
    Sleep, 50
    clipboard := tmp
}

EndsWith(str1, str2)
{
    len1 := StrLen(str1) 
    len2 := StrLen(str2) 
    if(len2<=len1)
    {
        EndStr = ""
        StringRight, EndStr, str1, len2 + 1
        return StartsWith(EndStr, str2)
    }
    return False
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

FloatingMSVisualStudioWindow()
{
    ; FIND OUT VISUAL STUDIO FLOATING WINDOW SITUATION
    windows=""
    r=""
    mvsTitle=""
    mvsTitleEnd="- Microsoft Visual Studio"
    mvsTitleStart=""
    mvsTitleInd=0
    lastWndTitle=""
    mvsPreviousTitle=""
    WinGet windows, List
    nonWSTitleCount = 0
    mvsFound = -1
    Loop %windows%
    {
        if(nonWSTitleCount>2)
        {
            ;break
            goto break_loop
        }
        id := windows%A_Index%
        WinGetTitle wt, ahk_id %id%
        if(wt!="")
        {            
            mvsFound := InStr( wt, " - Microsoft Visual Studio")
            ;MsgBox, mvsFound %mvsFound%
            if(mvsFound!=0)
            {
                mvsTitle := wt
                mvsTitleInd = %nonWSTitleCount%
                ;mvsTitleInd = %A_Index%
                ;MsgBox, %mvsTitle%
                spl := StrSplit(mvsTitle, " - ")
                for i, str in spl
                {
                    ;MsgBox, %str% %i%
                    if(i==1)
                    {
                        mvsTitleStart = %str%
                    }
                    else {
                        goto break_spl
                    }
                    break
                }
                mvsPreviousTitle=%lastWndTitle%
                break_spl:
            }
            nonWSTitleCount++
            r .= wt . "`n"
            lastWndTitle=%wt%
        }
    }
    break_loop:
    
    if(mvsFound!=-1)
    {
        ;MsgBox, FOUND %mvsTitleStart% 
        ;MsgBox, PREV %mvsPreviousTitle%
        ;MsgBox, %mvsTitleInd%
        if(mvsTitleStart==mvsPreviousTitle && mvsTitleInd==1){
            ;MsgBox, FLOATING!!!!
            return True
        }
    }
    return False
}