Bwl.MSBuilder
========================


Igor Koshelev (igor@lifemotion.ru), 2013-2014.

Simple tool for run MSBuild for all solutions in current folder.

Using:
1. Put msbuilder.exe in your solution folder OR append your PATH variable with path to msbuilder.
2. Run msbuilder.exe directly or from .cmd script.

Build order:

1. Solution with substring _B1 in file name.
2. Solution with substring _B1 in file name.
...
N. Other solutions in alphabetical order.
