Description:

This project is built on the FPS level from chapter 3, however it includes some
adjustments from latter sections. The most notable addition being the UI elements.
The gameplay elements have remained the same though.

I utilized the recommended sound effects/music from the book, however I personalized
the UI to my liking. Additionally, I came around to reorganize the scripts to be in
a separate folder as it was proving to be too cluttered. 

Instructions:

The game can be played using the traditional keybinds:

* W,A,S,D for moving, respectively

* Left click to shoot

There are some new elements to test the audio, however:

* Clicking the "gear" icon will bring up the settings GUI, this menu will contain
the options for interacting with the music/sound effects. The left hand side of the
menu will control everything to do with the music, including a slider to adjust the
values. The right hand side will control the sound effects and their volume. 

Challenges faced:

I had some particular issues with getting the music to work with crossfading the tracks. My
main issue was that I was getting null reference errors because I did not create an
"active" track and an "inactive" track in the Startup method in the AudioManager object.

Additional features:

I decided to include the fireplace sound effect onto the enemies to show off the 3D spacial
audio. I thought it was kind of funny with that particular sound, so I left it in. 
