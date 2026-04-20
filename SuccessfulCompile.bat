chcp 65001
set datestr=%date:~10,4%-%date:~4,2%-%date:~7,2%
SET HOUR=%time:~0,2%
SET dtStamp9=0%time:~1,1%_%time:~3,2%_%time:~6,2%
SET dtStamp24=%time:~0,2%_%time:~3,2%_%time:~6,2%

if "%HOUR:~0,1%" == " " (SET dtStamp=%dtStamp9%) else (SET dtStamp=%dtStamp24%)

if exist "A:\" (
   mkdir "A:\DBCode"
   CD /D "A:\DBCode"
   mkdir %datestr%
   CD %datestr%
   mkdir %dtStamp%
   CD %dtStamp%

   xcopy /e /c /h /k /y /i "E:\DBCode\"
)
Exit 0
