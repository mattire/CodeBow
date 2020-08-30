^r::reload

#'::
    msvsFloating := FloatingMSVisualStudioWindow()
    MsgBox, Floating: %msvsFloating%
return

FloatingMSVisualStudioWindow()
{
    ; FIND OUT VISUAL STUDIO FLOATING WINDOW SITUATION
    windows=""
    r=""
    mvsTitle=""
    mvsTitleEnd="- Microsoft Visual Studio"
    mvsTitleStart=""
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
        if(mvsTitleStart==mvsPreviousTitle){
            ;MsgBox, FLOATING!!!!
            return True
        }
    }
    return False
}
    

    ;WinGetTitle, WinTitle, A
    ;SetTitleMatchMode, 2
    ;WinGetTitle, WinTitle2, Title, "Visual Studio"
    ;;WinGet, WinTitle3,,"Visual Studio"
    ;ends := EndsWith(WinTitle, "Visual Studio")
    ;MsgBox, %WinTitle%
    ;MsgBox, %WinTitle2%
    ;;if(EndsWith(WinTitle, "Visual Studio"))
    ;if(ends==1)
    ;{
    ;    ;MsgBox, Ends %ends%
    ;    Send, {Esc}
    ;    Send, ^v
    ;}
return

EndsWith(str1, str2)
{
    len1 := StrLen(str1) 
    len2 := StrLen(str2) 
    if(len2<=len1)
    {
        ;MsgBox, 1
        EndStr = ""
        StringRight, EndStr, str1, len2 + 1
        ;MsgBox, %EndStr%
        ;MsgBox, %str2%
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


;Visual Studio
;1234567890123