    ______           _____
    | ___ \         /  ___|                  
    | |_/ /_ __ ___ \ `--. _ __   __ _ _ __  
    |  __/| '__/ _ \ `--. \ '_ \ / _` | '_ \ 
    | |   | | | (_) /\__/ / | | | (_| | |_) |
    \_|   |_|  \___/\____/|_| |_|\__,_| .__/ 
                                      | |    
                                      |_|    
                                      
### A minimalist screenshot app for documentation quality screenshots
**[Installer for latest version](http://factormystic.net/deploy/prosnap/setup.exe)** _(Actual website, coming soon!)_


### State of the Code
- Mostly operational
- Core application lives in `Program.cs`, including the root action dispatcher `DoActionItem`
- `IActionItem` implementations live in `\Action`. Look here for a rough assessment of what ProSnap can do. This is also where functionality build-out will take place.

### Compiling
- Requires **[FMUtils.Screenshot](https://github.com/factormystic/FMUtils.Screenshot#readme)**, **[FMUtils.KeyboardHook](https://github.com/factormystic/FMUtils.KeyboardHook#readme)**, **[FMUtils.WinApi](https://github.com/factormystic/FMUtils.WinApi#readme)**, **[FMUtils.TaskRUninstaller](https://github.com/factormystic/FMUtils.TaskRUninstaller#readme)**
- Also requires **[GongShell](http://gong-shell.sourceforge.net)**, a neat third party library I use to show the Windows context menu

### Where you can help
While funtional and useful in its current form, ProSnap is currently deficient in several ways (no particular order):
- [[#8](https://github.com/factormystic/ProSnap/issues/8)] Configuration save/load is not yet implemented, so all configuration changes reset when the app is closed
- [[#9] (https://github.com/factormystic/ProSnap/issues/9)] Screenshot metadata isn't persisted across app restarts
- [[#10] (https://github.com/factormystic/ProSnap/issues/10)] All session screenshots are kept in memory, so memory useage only grows during use
- **Fixed** ~~[[#11] (https://github.com/factormystic/ProSnap/issues/11)] Graphics mode changes (I think) sometimes cause the preview form to be solid black instead of glassy (such as after a Remote Desktop session)~~
- [[#2] (https://github.com/factormystic/ProSnap/issues/2)] The region screenshot chooser UI is too minimalist, and needs some UI love
- [[#12] (https://github.com/factormystic/ProSnap/issues/12)] I need to figure out how to implement OAuth consumption in a desktop app, which would allow uploading to Tumblr, and private Imgur accounts
- [[#13] (https://github.com/factormystic/ProSnap/issues/13)] FTP upload support, and [[#14] (https://github.com/factormystic/ProSnap/issues/14)] a general revamp of the upload config UI
- Figure out how to kill off the ActionItems enum and possibly load additional `IActionItem`s via a plugin architecture.
- [[#15] (https://github.com/factormystic/ProSnap/issues/15)] Scrolling screenshot implementation is weak and needs to be rewritten with a less dumb (faster/more successful) algorithm

Feel free to fork this repository and start cracking on any of these issues
