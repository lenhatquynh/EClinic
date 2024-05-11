mongosh "mongodb://localhost:27018/ForumService" --eval "db.createCollection('Post'); db.createCollection('Answer'); db.createCollection('Comment');db.createCollection('Hashtag')"
# /usr/local/bin/mongoimport --db ForumService --collection Post --file ../Database/DataMongo/Post.json --jsonArray --uri "mongodb://localhost:27018/" # for linux template
mongoimport --db ForumService --collection Post --file ../Database/DataMongo/Post.json --jsonArray --uri "mongodb://localhost:27018/"
mongoimport --db ForumService --collection Answer --file ../Database/DataMongo/Answer.json --jsonArray --uri "mongodb://localhost:27018/"
mongoimport --db ForumService --collection Comment --file ../Database/DataMongo/Comment.json --jsonArray --uri "mongodb://localhost:27018/"
mongoimport --db ForumService --collection Hashtag --file ../Database/DataMongo/Hashtag.json --jsonArray --uri "mongodb://localhost:27018/"