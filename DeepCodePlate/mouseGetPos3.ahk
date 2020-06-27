#singleInstance

MsgBox, press ctrl click to end

SetTitleMatchMode 2          ; Match title of windows by part of the name

;~LButton::
~LButton::
    CoordMode, Mouse, Screen
    MouseGetPos, xpos, ypos 
    CoordMode, Caret, Screen
    ; AutoHotkey v2
    ;CaretGetPos(xcar, ycar)
    ;MsgBox, %xcar%, %ycar%
    ;clipboard = {{}coord{}}%xpos%,%ypos%
    clipboard = %clipboard%`n{coord}%xpos%,%ypos%
    ;clipboard := %clipboard% . {coord}%xpos%,%ypos%
    ;MsgBox, %clipboard%
    ;ExitApp
return

^LButton::
    IfWinExist CodeBow
    {
      WinActivate, CodeBow
      WinWait, CodeBow
      Send, ^v
    }
    ExitApp
return
 
 