# Development
FROM node:18-alpine AS development
WORKDIR /app
COPY package.json .
COPY yarn.lock .
RUN yarn install
COPY . .
EXPOSE 3000
CMD [ "yarn", "dev" ]

# ==========================

# Production

FROM node:18-alpine AS production
WORKDIR /app
COPY package.json .
COPY yarn.lock .
RUN yarn install
COPY . .
EXPOSE 3000
RUN yarn build
CMD [ "yarn", "start" ]