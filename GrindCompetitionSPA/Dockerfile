# node:14.15.4 is an LTS version
FROM node:14.15.4 as env

ARG GrindCompetitionSPADir

WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY ${GrindCompetitionSPADir}/package.json ./
COPY ${GrindCompetitionSPADir}/package-lock.json ./
RUN npm ci
COPY ${GrindCompetitionSPADir}/ ./

###################
FROM env as lint

ENTRYPOINT [ "npm", "run", "lint" ]

###################
FROM env as build

ARG Environment

RUN npm run build -- --configuration=${Environment} --output-path=dist

###################
FROM nginx:1.19-alpine as runtime
COPY --from=build /app/dist /usr/share/nginx/html
COPY --from=build /app/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
