# Team2withGitIgnore

![example workflow](https://github.com/tant-rares-30127/Team2withGitIgnore/actions/workflows/dotnet.yml/badge.svg)
      Heroku deployed app: https://team2application.herokuapp.com/
      
      
## User types:

```
### Administrator
```
Permissions: 
- view list of all registered users
- assign roles to users

```
### Operator
```
Permissions: 
- CRUD operations on all entities
- assign roles to users

```
### Regular User
```
Permissions: 
- see informations about all entities in readonly mode and every moment have actual information from server 

```
### Visitor (not logged in)
```
Permissions: 
- register to the system
- see privacy policies
- see version of the product 

```
### DevOps (not logged in)
```
Permissions: 
- access to deploy instructions and product version
- specify version on deploy


## How to deploy to Heroku

1. Login to heroku 
```
heroku login
```

2. Build container

Build docker image:
```
docker build -t team2application .
```

3. Create and run docker container
```
docker run -d -p 8081:80 --name team2application_container team2application
```

4. Heroku container login
```
heroku container:login
```

5. Heroku push
```
heroku container:push -a team2application web
```

6. Release the container
```
heroku container:release -a team2application web
```
