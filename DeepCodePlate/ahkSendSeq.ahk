
;content:=clipboard
;spl = StrSplit(content, "|")

; spl := StrSplit("test|string|to|split", "|")
spl := StrSplit(Clipboard, "`r`n")

len := spl.MaxIndex() + 1

ind:=1

^v::
	Suspend, on
    ;MsgBox, %ind%
    current = % spl[ind]
    clipboard := current
    ;Send, %current%
    Send, ^v
    ;Send, {Esc}
    ind:=ind+1
    if(ind==len)
    {
        ind:=1
    }
	Suspend, off
return


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
