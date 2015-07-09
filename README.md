Bwl.VsTools
========================

Small tools for Visual Studio.

Igor Koshelev (igor@lifemotion.ru), 2013-2015.

=== VS Build Tool ===
Simple tool for run MSBuild for all solutions in current folder.

Using:
1. Put vs-build-all.exe in your solution folder OR append your PATH variable with path to vs-build-all.
2. Run vs-build-all.exe directly or from .cmd script.

Build order:

1. Solution with substring _B1 in file name.
2. Solution with substring _B1 in file name.
...
N. Other solutions in alphabetical order.
