# CmdArgsLogger
It's a simple tool that used to log process startup arguments and than proxy this to another process.

## How to use?
Imagine that you wan't to know with which arguments called `C:\Program Files\foo\foo.exe` by another program.

1. Download latest binary `CmdArgsLogger.exe` (or build from source), put it to folder `C:\Program Files\foo`.
2. Rename `foo.exe` to `real_foo.exe`, rename `CmdArgsLogger.exe` to `foo.exe`.
3. Create file `foo.cmd_args_logger.json` in same folder. With content:
```json
{
  exeToRun:"real_foo.exe"
}
```
4. Initiate call of `foo.exe` in another program.
5. See arguments of this call in `C:\cmdArgsLogger` directory.
6. Delete `foo.exe` and `foo.cmd_args_logger.json`, rename `real_foo.exe` back.