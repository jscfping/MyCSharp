#!/bin/bash

docker build -t web-core31-image .
docker run -d -it --rm -p 5000:80 \
--link my-redis:redis \
-e ASPNETCORE_ENVIRONMENT=Development \
-e MyValue=myDockerVal \
-e RedisConnectString=my-redis:6379 \
web-core31-image