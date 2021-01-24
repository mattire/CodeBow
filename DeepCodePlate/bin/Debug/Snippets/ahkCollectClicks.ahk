;{ahk}

clipboard = `;{ahk}`n`nCoordMode, Mouse, Window`n`n^f1::

~LButton::
    CoordMode, Mouse, Window
    MouseGetPos, xpos, ypos 
    CoordMode, Caret, Window
    ;clipboard = {coord}%xpos%,%ypos%
    clipboard = %clipboard%`n`tMouseClick,left,%xpos%,%ypos%`n`tSleep, 20
return

~RButton::
    CoordMode, Mouse, Window
    MouseGetPos, xpos, ypos 
    CoordMode, Caret, Window
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
    Run %A_AHKPath% "%fn%"
    
    ExitApp
return