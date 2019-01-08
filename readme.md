How to enable auth
https://medium.com/mongoaudit/how-to-enable-authentication-on-mongodb-b9e8a924efac

How to really enable auth:
https://docs.mongodb.com/manual/tutorial/enable-authentication/

Where to find the auth
https://docs.mongodb.com/manual/reference/configuration-options/#configuration-file

how to create an user:
db.createUser({"user": "tester", "pwd": "tester123", "roles": [{"role":"readWrite", "db": "playGround"}]})

Add DI container to console application (not added by default)
https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/

Connection pooling
https://www.compose.com/articles/connection-pooling-with-mongodb/
Default Pool Size is 100
https://docs.mongodb.com/manual/reference/connection-string/

C# Mongo Driver 2.0: 
http://mongodb.github.io/mongo-csharp-driver/2.0/reference/driver/