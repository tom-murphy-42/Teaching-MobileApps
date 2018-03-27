# OMDB_API_1
This app should receive input from the user in the form of a movie title & optionally also a year. It then validates that info. (Did the user at least attempt to input a movie? If the user inputted a date, is it between 1888-2028.) If the user's input(s) validates, then search IMDB for the movie title (perhaps released in that year). It then outputs the results of that (API call) to the screen. (Currently as raw JSON data.)

## System Design 
This app requires access to the internet.

## Usage
To use this app enter a movie title on the top left input field (hinted: "Enter Movie Title") and optionally enter a year as well (hinted: "Year"). Then click the 'SEARCH' button. If all goes well, the results of your API call should be listed below.