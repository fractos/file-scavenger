# file-scavenger

Utility project that looks for files in a set of folders that match one of a set of wildcards, then filters out all
but the most recent of those files for each bare filename (i.e. without path) before copying them (with overwrite) into
a set of target folders.

Use case for this is copying a set of libraries around that are in pre-NuGet status but are split across multiple repository
folders.

##Parameters
The first parameter is the pathname of a file that contains a list of source folders to search. The file should have a
folder per line. e.g.:

```
C:\git\personal\fractos\inversion-data
C:\git\personal\fractos\inversion-messaging
```

The second parameter is the pathname of a file that contains a list of target folders that files will be copied into. The
file should have a folder per line, as above, e.g.:

```
C:\git\personal\fractos\testing\lib\Inversion
C:\git\personal\fractos\exported-libs
```

The third parameter is one or many wildcards separated by spaces, e.g.:

```
"Inversion*.dll Inversion*.pdb"
```

This was thrown together in less than half an hour, so it's pretty rough. You have been warned.
