    ______           _____
    | ___ \         /  ___|                  
    | |_/ /_ __ ___ \ `--. _ __   __ _ _ __  
    |  __/| '__/ _ \ `--. \ '_ \ / _` | '_ \ 
    | |   | | | (_) /\__/ / | | | (_| | |_) |
    \_|   |_|  \___/\____/|_| |_|\__,_| .__/ 
                                      | |    
                                      |_|    
                                      
### A minimalist screenshot application for documentation quality screenshots
**[Installer for latest version](http://factormystic.net/deploy/prosnap/setup.exe)** _(Actual website, coming soon!)_


### State of the Code
- Mostly operational
- Core application lives in `Program.cs`, including the root action dispatcher `DoActionItem`
- `IActionItem` implementations live in `\Action`. Look here for a rough assessment of what ProSnap can do. This is also where functionality build-out will take place.

### Usage
- Requires **[FMUtils.Screenshot](https://github.com/factormystic/FMUtils.Screenshot#readme)**, **[FMUtils.KeyboardHook](https://github.com/factormystic/FMUtils.KeyboardHook#readme)**, **[FMUtils.WinApi](https://github.com/factormystic/FMUtils.WinApi#readme)**
- Also requires **[GongShell](http://gong-shell.sourceforge.net)**, a neat third party library I use to show the Windows context menu

### Where you can help
While funtional and useful in its current form, ProSnap is currently deficient in several ways:
- Configuration save/load is not yet implemented, so all configuration changes reset when the app is closed
- Screenshot metadata isn't persisted across app restarts
- All session screenshots are kept in memory, so memory useage only grows during use
- Graphics mode changes (I think) sometimes cause the preview form to be solid black instead of glassy (such as after a Remote Desktop session)
- The region screenshot chooser UI is too minimalist, and needs some UI love
- I need to figure out how to implement OAuth consumption in a desktop app, which would allow uploading to Tumblr, and private Imgur accounts
- FTP upload support, and a general revamp of the upload config UI
- The `IActionItem` structure is functional but less than ideal. It'd be cool to devolve the logic that lives in `DoActionItem` into each separate `IActionItem` implementation. Then that would open the potential to load in `IActionItem`s at runtime, possibly via a plugin architecture.

Feel free to fork this repository and start cracking on any of these issues