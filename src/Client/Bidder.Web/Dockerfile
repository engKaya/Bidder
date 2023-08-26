 

FROM node:18 AS build

WORKDIR /app

COPY package*.json . 

RUN npm install --force

COPY . .
COPY ./src/environment/* /src/environment/
RUN npm run build
 
FROM nginx

RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d
COPY --from=build /app/dist/Bidder /usr/share/nginx/html

EXPOSE 80