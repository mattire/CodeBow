#singleInstance

MsgBox, press start + esc to end

SetTitleMatchMode 2          ; Match title of windows by part of the name


#f1::
    ProcessAndSend("F1.txt")
    ; FileRead file1, F1.txt
    ;if(StartsWith(file1, "{tab}"))
    ; Send, %file1%
return

#f2::
    FileRead file2, F2.txt
    Send, %file2%
return

#f3::
    FileRead file3, F3.txt
    Send, %file3%
return

#f4::
    FileRead file4, F4.txt
    Send, %file4%
return

~#Esc::ExitApp

ProcessAndSend(fn)
{
    FileRead txt, %fn%
    Send, %txt%
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