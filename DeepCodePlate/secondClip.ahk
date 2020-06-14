!^c:: ;extra clipboard copy
KeyWait, g   
ItemOne := ClipboardAll
Clipboard := ""
Send, ^c
ClipWait, 1
ItemTwo := ClipboardAll
Clipboard := ""
Clipboard := ItemOne
CLipWait, 1
ItemOne := ""
return

!^v:: ;extra clipboard paste
KeyWait, h   
ItemOne := ClipboardAll
Clipboard := ""
Clipboard := ItemTwo
ClipWait, 1
Send, ^v
Clipboard := ""
Clipboard := ItemOne
ClipWait, 1
ItemOne := ""
return