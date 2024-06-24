[TOC]

# Target
This template is aimed towards providing a good entry point into a managed project featuring
- efilnukefesin.NET base libs
- unity base classes providing injected services
- a set of preconfigured prefabs to use 

# Structure
The basic structure of the template explained:
- assets_src
    - any source files for assets being used
- docs
    - put any documentation, image references etc here
- src
    - Assets
        - Template
        - Base
            - Base classes to be used like BaseBehaviour
            - Builder Script for automating Unity Compilation Processes
            - Special Editors
            - Property Drawers
        - BaseEditor
        - Plugins
            - here you can find all dll files from the base libs, to be updated by the tool described in the tools section
    
- tools
    - UpdateNugetPackages.bat
        - will start the tool "BagetDownloader" which downloads the latest set of service dlls, unzip them into the project Plugins folder

# Usage
- Just copy the files to another directory, the project name will always be "src" anyway.
- Update destination path in tools/UpdateNugetPackages.bat
- call "git submodule update --init --recursive" in root folder
- in Unity Editor, install the package "Cinemachine" and "Input System"
- change Origin in Git config

# Todo
- Fix bug in Feature branch
