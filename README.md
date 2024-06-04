# FreeShow-Interface
A C# .NET App to interface with FreeShow for additional Stage Display functionality

FreeShow-Interface is used in conjunction with the Bitfocus Companion REST API in FreeShow when the Bitfocus Companion integration is Enabled.

FreeShow-Interface converts a HTTP query string to a JSON body object which FreeShow can decode and understand.  This 'translation' enables things like FreeShow 'Triggers' to handle Stage Display changes, such as a Lyrics Layout trigger or a Layout with the current slide image for presentations.

## *Note that FreeShow-Interface is designed as a temporary solution only, until these features can be properly developed and integrated into the main FreeShow application.*

## Configuration
Simply enable the Bitfocus Companion integration from the FreeShow Settings > Connection page, start FreeShow-Interface on the local machine, and create FreeShow Triggers with the below URL (customised with actions as per the below table).  

Once done, you can drag and drop Triggers onto any slides you like and, (as long as FreeShow-Interface is running in the background) the Trigger will force all stage displays to change to the desired layout.
Note that internally to FreeShow, this uses the 'Move all connections to this' inbuilt functionality from the Stage editor to force all connected Stage clients to use the desired layout.

### Trigger URL
`http://localhost:8088/stage?action=<action from below>&id=<id of stage layout>`

#### Example
`http://localhost:8088/stage?action=id_select_stage_layout&id=c65273834aa`


### Configuration Options
| action                  | id                                              |
| -------------           | -------------                                   |
| id_select_stage_layout  | (see below for how to get your Stage Layout ID) |


### How to get Stage Layout ID
Open your browser and navigate to the FreeShow StageShow URL (normally `http://localhost:5511`), open the Inspector or DevTools, go to 'Console', and refresh the page.  Then select the stage display you wish from the FreeShow StageShow page, go back to the Console and scroll down through the messages until you see one that contains the name of your stage layout.  There should also be an 'ID' value, this is your Stage Layout ID.  This may change if you delete or recreate the stage layout, so be careful!
