# Use the official Node.js image as the base image
FROM node:18-alpine

# Set the working directory in the container
WORKDIR /app

# Install Angular CLI globally
RUN npm install -g @angular/cli

# Create a new Angular project with the name 'new-angular-app'
RUN ng new slamdunk-fe --directory ./slamdunk-fe --skip-install

# Navigate into the project directory
WORKDIR /app/slamdunk-fe

# Install the project dependencies
RUN npm install sweetalert2
RUN npm i --save-dev @types/file-saver
RUN npm install

# Expose the port the app runs on
EXPOSE 4200

# Set the default command to run the Angular development server
CMD ["ng", "serve", "--host", "0.0.0.0"]
