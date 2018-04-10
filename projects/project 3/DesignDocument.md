# P3_TMurphy_GoogleVisionAPI
This app should take photos, save them to your gallery in an album called 'GoogleVisionApp', display them to the screen, and then send them to Google's Vision to analyze and label. These labels should then be displayed via a series of 'toasts'. I wrote this application to learn more about interacting with a web service, in this case specifically Google's Vision API. It serves as a catalyst for that learning process.

## System Design 
This app requires access to your camera, the ability to read and write to external memory, and access to the internet.
The idea is simple, take a picture, upload it to Google for a quick analysis, and then display the results returned. I'd like to add gamification to this, but am currently only adding headaches.

## Usage
To use this app, open it. Click on the 'OPEN CAMERA' button. Take a photo. Hit 'RETRY' to take another photo or hit 'OK' when you're happy with your photo. This will (attempt?) to send the photo to Google Vision's API to analyze and return with labels, which will then be shown on the screen in a series of "toasts". (Expect a 15+ second delay, depending on your connection and current compression rate.) Currently, this app is mostly just useful for roasting your friends by allowing Google to label them as 'furniture', 'lifting accessories', or 'darkness'.
