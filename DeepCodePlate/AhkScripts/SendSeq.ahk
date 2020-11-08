
;content:=clipboard
;spl = StrSplit(content, "|")

if(StartsWith(clipboard, "{pasteseq}"))
{
    MsgBox, run paste seq
    spl2 := StrSplit(clipboard,"`n")
    spl2.Remove(0)
}



spl := StrSplit("test|string|to|split", "|")
len := spl.MaxIndex() + 1
; MsgBox, %len%

ind:=1

^v::
    ;MsgBox, %ind%
    current = % spl[ind]
    Send, %current%
    Send, {Esc}
    ind:=ind+1
    if(ind==len)
    {
        ind:=1
    }
return


; ^Tab::
^Enter::
    ExitApp
return


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
