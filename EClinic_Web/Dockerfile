# Use an official Node runtime as a parent image
FROM node:18-alpine

RUN npm install -g nodemon
# Set the working directory to /app
WORKDIR /app

# Copy the package.json and yarn.lock files to the container
COPY package.json yarn.lock ./

# Install dependencies
RUN yarn install

# Copy the remaining application files to the container
COPY . .

# Expose port 3000 for the application
EXPOSE 3000

# Start the application
CMD ["yarn", "dev"]
