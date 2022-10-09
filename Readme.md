


# RabbitMQ
```bash
docker pull rabbitmq:3.7-management
docker run --name myrabbitmq -p 15672:15672 -p 5672:5672 -d rabbitmq:3.7-management
```
[Management](http://localhost:15672)



# redis
```bash
docker run --name some-redis -p 6379:6379 -d redis
```



# Docker
```bash
docker system prune -a
```

# mysql
```bash
docker run --name some-mysql -e MYSQL_ROOT_PASSWORD=123456 -p 3306:3306 -d mysql:5.7.34
docker exec -it some-mysql bash
mysql -uroot -p
```


# EFCore
```sql
CREATE DATABASE TodoApp;
CREATE TABLE TodoApp.TodoItem (
  Id INT NOT NULL AUTO_INCREMENT,
  Name VARCHAR(255) NOT NULL,
  IsComplete BOOLEAN NOT NULL,
  PRIMARY KEY (Id)
);
```



