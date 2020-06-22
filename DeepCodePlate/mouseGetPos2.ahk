
;~LButton::
LButton::
    MouseGetPos, xpos, ypos 
    ;clipboard = {{}coord{}}%xpos%,%ypos%
    clipboard = {coord}%xpos%,%ypos%
    ;MsgBox, %xpos%, %ypos%
    ExitApp
return

