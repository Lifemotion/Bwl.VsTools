release\vs-version-set.exe
copy release\vs-build-all.exe vs-build-all.exe
vs-build-all.exe -release *
del vs-build-all.exe