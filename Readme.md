


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




