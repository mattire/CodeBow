;{ahk2}

clipboard = `;{ahk}`n`nCoordMode, Mouse, Window`n`nSetDefaultMouseSpeed, 2`n`n`n`n^f1::

~LButton::
    CoordMode, Mouse, Window
    MouseGetPos, xpos, ypos 
    ;CoordMode, Caret, Window
    clipboard = %clipboard%`n`tMouseClick,left,%xpos%,%ypos%`n`tSleep, 20
return

~RButton::
    CoordMode, Mouse, Window
    MouseGetPos, xpos, ypos 
    ;CoordMode, Caret, Window
    clipboard = %clipboard%`n`tMouseClick,right,%xpos%,%ypos%`n`tSleep, 20
return

^enter::
    clipboard = %clipboard%`nreturn
    clipboard = %clipboard%`n
    clipboard = %clipboard%`n^enter::
    clipboard = %clipboard%`n`tExitApp
    clipboard = %clipboard%`nreturn
    
    clipboard = %clipboard%`n
    clipboard = %clipboard%`n!enter::
    clipboard = %clipboard%`n`tRun %A_ScriptDir%\%A_ScriptName%
    clipboard = %clipboard%`nreturn
    
    fn = %A_ScriptDir%\ahkTemp.ahk
    f := FileOpen(fn, "w")
    ;f.Write(content)
    f.Write(clipboard)
    f.Close()
    ;MsgBox, %A_AHKPath% %fn%
    ;MsgBox, %A_AHKPath% `"%fn%`"
    ;MsgBox, %A_AHKPath% "%fn%"
    ;MsgBox, %A_AHKPath% ahkTemp.ahk
    Run %A_AHKPath% "%fn%"
    ;Run %fn%
    ;Run %A_AHKPath% %fn%
    ;Run "C:\Program Files\AutoHotkey\AutoHotkeyU64.exe" %fn%
    ;Run "C:\Programs\AutoHotkey_1.1.25.02\AutoHotkeyU64.exe" %fn%
    ExitApp
return