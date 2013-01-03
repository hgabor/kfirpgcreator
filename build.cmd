
msbuild /p:Configuration=Release /t:Rebuild

rd /s /q output
mkdir output
copy runner\bin\Release\corelib.dll output\
copy runner\bin\Release\runner.exe output\
copy editor\bin\Release\WeifenLuo.WinFormsUI.Docking.dll output\
copy editor\bin\Release\editor.exe output\

xcopy /s "Misc Files" output
xcopy lib\*.dll output
