FROM node:10

# Setting working directory. All the path will be relative to WORKDIR
WORKDIR .

# Installing dependencies
# Specific package and package-lock
COPY package.json ./
COPY package-lock.json ./
RUN npm install

# Copying source files
COPY . .

# Building app
RUN npm run build

# Running the app
CMD [ "npm", "start" ]