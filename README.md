# Run

The Run prompt from Windows, but for macOS.

![](/Run/Assets.xcassets/AppIcon.appiconset/AppIcon-256.png)

## What?
So you know how in Windows, if you press <kbd>Win</kbd>+<kbd>R</kbd> you get a little dialogue box that lets you open files, web addresses etc.? Yeah it's literally that. You can enter apps, files, URLs and terminal commands, and just open them.

When you open Run, a `>_` icon will be added to the menu bar. You can click that to open or quit Run. When using Run, apps in `/Applications/` and `/Applications/Utilities/` can be opened by just typing in their filename (for example, `Calendar.app` or `Terminal.app`). Shell commands can be run just by typing their name and any arguments. Files can be opened by providing their path or using the `file://` protocol. URLs will also be opened in your default browser. The dropdown in the text box will show recent runs (persistent across sessions).

<img width="568" alt="image" src="https://user-images.githubusercontent.com/31840547/177662586-ccb4ac9e-e51d-4e93-9177-1536ae3d9e7e.png">

## Why?
Well you'd be right in asking this considering Spotlight exists. However, I developed this for two reasons anyway. Firstly, Spotlight can't run terminal commands. Try starting `vim` from Spotlight, it'll just prompt you to install the Vimeo app instead; not particularly useful. Secondly, I was bored! I made the blank UI as a joke, then thought it'd be a good learning experience to make it work since I'd never made a macOS application before.

## Anything else?
Well to install it, just head on over to the [Releases](https://github.com/torchgm/run/releases) page and download the latest .dmg. Then, drag-and-drop the app into Applications and run it. I'm sure macOS will complain about it not being signed or whatever, and as someone who spends the majority of time in Windows you'll just have to figure that one out yourself I'm afraid :c

Anywho, this isn't really a proper app that I'm supporting - the code is a mess (I was very ill and tired whilst writing it and I've never really dealt with macOS before), and there's a bunch of wonky/broken functionality. Notably, there's no keyboard shortcut to launch it because I wasn't able to make that work!! This app is mostly a joke, if you want a bit of Windows UI in macOS for some crazy reason. Thanks for checking it out anyway!
