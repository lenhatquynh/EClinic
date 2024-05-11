mongosh "mongodb://localhost:27018/BlogService" --eval "db.createCollection('Blog');db.createCollection('Hashtag')"
mongoimport --db BlogService --collection Blog --file ../Database/DataMongo/Blog.json --jsonArray --uri "mongodb://localhost:27018/"
mongoimport --db BlogService --collection Hashtag --file ../Database/DataMongo/BlogHashtag.json --jsonArray --uri "mongodb://localhost:27018/"