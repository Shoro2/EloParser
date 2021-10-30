# EloParser
Using a local file or a custom command for your twitch bot to request and display your current LTD2 elo.

## Via API request:

 ### Nightbot:
  
 > !commands add !elo $(urlfetch https://ltdstats.com/api/playerInfo?playername=<your playername here>)
  
 ### Moobot:
  1. Go to your control panel
  2. Commands -> Custom Commands
  3. in the new window select:
 
    Response: URL fetch - full (plain) response
 
    Url to fetch: 'https://ltdstats.com/api/playerInfo?playername=<your playername here>'
 
    URL response type: plain text
 
    Request Method: GET
    
### Cloudbot:

> !commands add !elo $readapi(https://ltdstats.com/api/playerInfo?playername=<your playername here>)

## Via local File:

1. Download the 'Elo Parser.exe' in bin/realease or use Visual Studio to compile the project.
2. Run the application
3. Enter a name you want to monitor
4. Click 'Start'
5. The elo is parsed in <playername>.txt in the folder with the .exe
6. Use your twitch bot to read the file

How it works:
The tool has an integrated webbrowser that opens 'https://ltdstats.com/api/playerElo?playername=<your playername here>' and parses the element that contains the elo into a txt.
  
 
