# WebApiBox – Docker Guide

This project template includes a **Dockerfile** to build and run the application in a container.

## Build and Run Instructions

1. Open a terminal and change to the folder containing this `README.md`.  
2. Build the Docker image (remove `wsl` if you are already on Linux or macOS):

   ```sh
   wsl docker build -t webapibox .
   ```

3. Run the container in development mode:

   ```sh
   wsl docker run -it --rm -e ASPNETCORE_ENVIRONMENT=Development -p 8080:8080 webapibox
   ```
   
   The application will be available at **http://localhost:8080/swagger/index.html**.

4. Run the container in production mode:

   ```sh
   wsl docker run -it --rm -p 8080:8080 webapibox
   ```
   The application will be available at **http://localhost:8080/api/health**.

