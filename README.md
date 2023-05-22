This is an AR project developed for CityU test. This project uses Vuforia engine.  
How to deploy:  
Step 1: Open this project in Unity.  
Step 2: Find out the IPv4 address of your computer.  
Step 3: In Unity Editor, find "ImageTarget" game object in hierarchy.  
Step 4: Expand the "ImageTarget", click on "Aqara".  
Step 5: In Illuminance Fetcher (Script), change the Url to "http://" + the IPv4 address + ":8080"  
For example, the IP address of your computer is "192.168.3.1", change the Url string to "http://192.168.3.1:8080"  
Step 6: Build the project.  
Step 7: Run the HTTPServer.py on your computer.  
Step 8: Run the built app.  