;{ahk}

clipboard = `;{ahk}`n`nCoordMode, Mouse, Screen`n`n^f1::

~LButton::
    CoordMode, Mouse, Screen
    MouseGetPos, xpos, ypos 
    CoordMode, Caret, Screen
    ;clipboard = {coord}%xpos%,%ypos%
    clipboard = %clipboard%`n`tMouseClick,left,%xpos%,%ypos%`n`tSleep, 20
return

~RButton::
    CoordMode, Mouse, Screen
    MouseGetPos, xpos, ypos 
    CoordMode, Caret, Screen
    ;clipboard = {rcoord}%xpos%,%ypos%
    clipboard = %clipboard%`n`tMouseClick,right,%xpos%,%ypos%`n`tSleep, 20
return

^enter::
    clipboard = %clipboard%`nreturn
    clipboard = %clipboard%`n
    clipboard = %clipboard%`n^enter::
    clipboard = %clipboard%`n`tExitApp
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
