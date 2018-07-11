# UnitySpriteMetaParser

Simple project to pull the name, x, y, width and height values out of a sprite map meta file from Unity. 

## Use Case

- You have an old unity project, or are using Unity as a tool. 
- In unity you can add images as sprites, then use an editor to specify different frames for animation. 
- These frames amount to x, y, width and height values, and are stored in a detailed YAML file.
- This project reads such a YAML file, extracts the name, x, y, width and height of each 'frame', and outputs the result as a CSV

## Instructions

Run using at least the path to the meta file. You can option specify a second argument containing the output csv file path.

## Samples

Sample input and output files can be found under /samples. The knight image is from (and all credit to Master484) [here](https://opengameart.org/content/mini-knight).