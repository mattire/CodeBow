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
    ExitApp
return
	MouseClick,left,751,546
	Sleep, 20