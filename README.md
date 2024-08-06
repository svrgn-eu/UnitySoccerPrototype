[TOC]

# Target
This Prototype aims to simulate all of the components which made "Nintendo World Cup" such a fun game to play.

Base technology is Unity with the MoreMountains Topdown Engine. 

We aim to create a close, but not 1:1 rebuild of the mechanic here and certain changes are obvious and most likely wanted.

This prototype is completed for around 20%.

# Structure
The basic structure of the template explained:
- src
    - Assets
        - Template
        - Base
            - Base classes to be used like BaseBehaviour
            - Builder Script for automating Unity Compilation Processes
            - Special Editors
            - Property Drawers
            - (currently not referenced)
        - BaseEditor
            - (currently not referenced)
        - Plugins
            - here you can find all dll files from the base libs, to be updated by the tool described in the tools section
        - Runtime
            - the Game Code itself
        - ThirdParty
            - all of the Third Party libs used for this project
                - Moremountains Feel https://feel-docs.moremountains.com/
                - Moremountains Topdown Engine https://topdown-engine-docs.moremountains.com/
