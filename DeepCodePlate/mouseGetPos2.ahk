
;~LButton::
LButton::
    CoordMode, Mouse, Screen
    MouseGetPos, xpos, ypos 
    CoordMode, Caret, Screen
    ; AutoHotkey v2
    ;CaretGetPos(xcar, ycar)
    ;MsgBox, %xcar%, %ycar%
    ;clipboard = {{}coord{}}%xpos%,%ypos%
    clipboard = {coord}%xpos%,%ypos%
    ;MsgBox, %xpos%, %ypos%
    ExitApp
return

