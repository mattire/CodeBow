
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
