# ConsoleBox – Docker Guide

This project template includes a **Dockerfile** to build and run the application in a container.

## Build and Run Instructions

1. Open a terminal and change to the folder containing this `README.md`.  
2. Build the Docker image (remove `wsl` if you are already on Linux or macOS):

   ```sh
   wsl docker build -t consolebox .
   ```

3. Run the container:

   ```sh
   wsl docker run -it --rm -e ConsoleBox__HelloWorld="Docker World!" consolebox
   ```
