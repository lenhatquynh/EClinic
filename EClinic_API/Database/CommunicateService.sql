USE [CommunicateService]

GO

/*
INSERT INTO [RoomTypes]  (RoomTypeID,RoomTypeName)
VALUES ('0ddb55c3-b551-4e04-b827-07ea8d777130',N'Supporter');
INSERT INTO [RoomTypes]  (RoomTypeID,RoomTypeName)
VALUES ('bde8645f-2760-4729-ac4f-1c5b5ffedc2a',N'Doctor');
*/

GO
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('78b7f789-524d-47a3-a9d1-fe369873c016','0','63da4fe0-de4d-4c8e-b8c8-ec3202c20038','32ab71e0-a75d-4d39-8d5d-e66525477d48','1/1/2023 9:00:00 AM','0ddb55c3-b551-4e04-b827-07ea8d777130');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('ad4210b9-4431-4b78-b28c-0003fc7a77e5','0','63da4fe0-de4d-4c8e-b8c8-ec3202c20038','22849f83-a5b4-49eb-93ed-e2d942254521','1/1/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('b69d8383-6dfb-4763-8710-05b4cbea4d95','0','42ce372a-7078-4a4c-93a3-c5000d58bff1','b7bd24ed-941a-467a-81cb-b12b9bb1aed3','1/2/2023 10:00:00 AM','0ddb55c3-b551-4e04-b827-07ea8d777130');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('93aee4f9-bfb3-4af3-b839-0f4fccb31377','0','c36a9563-e078-4826-a2eb-ce8d9e70eb64','b7bd24ed-941a-467a-81cb-b12b9bb1aed3','1/2/2023 11:00:00 AM','0ddb55c3-b551-4e04-b827-07ea8d777130');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('394ba900-3928-4d80-919f-10b419d415ad','0','f5dfd1eb-e828-4903-aaa9-ddd60642ef0e','b7bd24ed-941a-467a-81cb-b12b9bb1aed3','1/2/2023 12:00:00 PM','0ddb55c3-b551-4e04-b827-07ea8d777130');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('2ba8b19e-5ad4-479b-9a7f-115edd5040b1','0','bd64eb6b-1993-4ad7-ad81-f70eab00d869','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','1/3/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('42c753c5-f0c2-4e93-865e-1314cfc611c6','0','25fec2c5-87c1-4ec4-9014-9515e5d8863b','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','1/3/2023 11:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('b2cd704d-b5b3-4be8-b614-1c07bd7b0770','0','abac9d62-375e-42e9-898d-648864d34985','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','1/3/2023 12:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('b04e49c9-1ada-43d7-a9df-1dfda469d9e0','0','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','1/3/2023 1:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('2f645efd-70aa-4a2a-aaa9-244fa1b719bc','0','9248a7b5-d937-48db-abe6-8ffeb3bf0d0e','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','1/3/2023 2:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('7ee6f70b-d58e-49ab-b70d-6ad83b492e40','0','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','63da4fe0-de4d-4c8e-b8c8-ec3202c20038','1/1/2023 9:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('150927e4-d59d-4d5e-86d3-f17136d1652f','0','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','c36a9563-e078-4826-a2eb-ce8d9e70eb64','1/1/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('e2e8b80c-9c08-4a82-89ad-6c1635e2b0e3','0','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','f5dfd1eb-e828-4903-aaa9-ddd60642ef0e','1/2/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('1859260e-7d95-4e0e-b287-9235747c04c4','0','b514b384-ed9c-4d68-90ae-4f437bdee6b9','25fec2c5-87c1-4ec4-9014-9515e5d8863b','1/2/2023 11:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('41909012-ba0c-4f69-a88d-869604928a48','0','b514b384-ed9c-4d68-90ae-4f437bdee6b9','c36a9563-e078-4826-a2eb-ce8d9e70eb64','1/2/2023 12:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('a6898622-5dca-4e78-844c-9edea778eb4c','0','f8f4c1f6-b620-42e6-9da9-54438bbd13c5','bd64eb6b-1993-4ad7-ad81-f70eab00d869','1/3/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('bb42f8f5-ae8f-4de7-b672-fdeac95ed6e0','0','f8f4c1f6-b620-42e6-9da9-54438bbd13c5','bd64eb6b-1993-4ad7-ad81-f70eab00d869','1/3/2023 11:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('5b555c08-db96-4a1d-ae47-b8d79446da41','0','f8f4c1f6-b620-42e6-9da9-54438bbd13c5','25fec2c5-87c1-4ec4-9014-9515e5d8863b','1/3/2023 12:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('5c9a4bb9-f67d-46a4-8aef-6dbecfd2d422','0','5145193c-51dc-4fed-8aa9-8adc068b9c69','63da4fe0-de4d-4c8e-b8c8-ec3202c20038','1/3/2023 1:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('c3eddb29-6943-4de3-8861-be62ec82db28','0','5145193c-51dc-4fed-8aa9-8adc068b9c69','63da4fe0-de4d-4c8e-b8c8-ec3202c20038','1/1/2023 9:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('3c34af3b-09b9-4a00-9f52-83abaef2a4b1','0','5145193c-51dc-4fed-8aa9-8adc068b9c69','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848','1/1/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('f8e780c9-a32f-40b2-a445-e32dcc63da30','0','5a504858-ec5d-4a7b-9c07-9302e6964bd9','3480d242-8d09-43e8-910d-adbd9cb84012','1/2/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('0b9e6a8d-40a8-472e-ba0b-95c48fe7fb66','0','5a504858-ec5d-4a7b-9c07-9302e6964bd9','bb974f0d-9383-4502-bb94-fc1d0fc3eab6','1/2/2023 11:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('f667d71e-4572-4fc2-abea-b2d19e38cf5c','0','5a504858-ec5d-4a7b-9c07-9302e6964bd9','420f4420-7e8b-4100-943a-fededd5c4cde','1/2/2023 12:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('fbaae15a-7052-4458-8f48-68a45f013b53','0','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','6057ce03-8923-47b0-b0cf-8bcb07f06b44','1/3/2023 10:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('3ecef33a-b6dd-4a92-9191-f5d9787abaa4','0','8edf474e-0fcb-49fc-9c9e-4e902c7472f2','9cce93e4-f88e-47fa-93ce-a6ec0569f76b','1/3/2023 11:00:00 AM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('20662731-62a4-4f79-9b0d-3c11331ee5db','0','b514b384-ed9c-4d68-90ae-4f437bdee6b9','fe9c4099-9b05-45dd-a299-c408ba574878','1/3/2023 12:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('cdf97b5b-95da-4140-9a59-1f2ab7a072c7','0','b514b384-ed9c-4d68-90ae-4f437bdee6b9','ca064e23-1ce0-4ca8-b045-ea8cf23bbc8c','1/3/2023 1:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');
INSERT INTO [Rooms] (RoomID,IsClosed,ReceiverID,SenderID,CreatedAt,RoomTypeID) VALUES ('bc773941-302c-44fa-a5ac-6b81abb674b6','0','f8f4c1f6-b620-42e6-9da9-54438bbd13c5','826e96cd-e68e-4e21-b17f-d5adc5545872','1/3/2023 2:00:00 PM','bde8645f-2760-4729-ac4f-1c5b5ffedc2a');

GO

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('a674634a-4971-4445-a8a7-2d2a3e119cc2','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'Hi Supporter!','0','2023-01-01 09:00:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('017b3985-4b91-4e16-8813-3150678940f4','32ab71e0-a75d-4d39-8d5d-e66525477d48',N'May I help you?','0','2023-01-01 09:01:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('1e530468-9b0c-4d9b-a14b-3a68deb9a5e0','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'Lately I have been having itching and red rashes on my skin.','0','2023-01-01 09:02:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('b012abf2-06e9-4580-8127-40043f4dfbad','32ab71e0-a75d-4d39-8d5d-e66525477d48',N'Can you send me a picture of the symptoms?','0','2023-01-01 09:03:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('6bb30e95-a28c-4257-ae59-45ef412ac0d9','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Chat1.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717031812&Signature=br3q8W7zz1cApVf3jyi86F%2B30Es%3D','1','2023-01-01 09:04:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('2f26bf25-7653-41ec-8884-545a7ab9792a','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'This is my neck and it is floating in other places.','0','2023-01-01 09:05:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('631706df-e505-49e0-8ba9-5641c0e00052','32ab71e0-a75d-4d39-8d5d-e66525477d48',N'Shall I connect you to the doctor?','0','2023-01-01 09:06:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('2c6b9867-df63-45e8-ad95-696081adf149','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'If so, I really thank you.','0','2023-01-01 09:07:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('c5807ca7-cc6e-4dbd-bb7f-74e04430a5b8','32ab71e0-a75d-4d39-8d5d-e66525477d48',N'Please wait a moment.','0','2023-01-01 09:08:00','78b7f789-524d-47a3-a9d1-fe369873c016');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('acca3e01-dd2f-4988-8d46-7776fee96bab','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'Ok, thank you!','0','2023-01-01 09:09:00','78b7f789-524d-47a3-a9d1-fe369873c016');

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('dde738c1-4e35-484e-b479-7aca74d55733','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'Hi Doctor!','0','2023-01-01 10:00:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('9fa9ea4d-8b57-4dfc-8da3-7d1fc7a2396b','22849f83-a5b4-49eb-93ed-e2d942254521',N'May I help you?','0','2023-01-01 10:01:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('e6eafb03-ec59-40e0-96e8-803549da20b4','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'Lately I have been having itching and red rashes on my skin.','0','2023-01-01 10:02:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('ce1c5f65-c286-44bc-9fb4-83bee9715a1a','22849f83-a5b4-49eb-93ed-e2d942254521',N'Can you send me a picture of the symptoms?','0','2023-01-01 10:03:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('88fc1646-c2bf-44c4-b5f3-8786d1ace678','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Chat2.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717032090&Signature=BUW3FHJMEHblaLKHDgG5MHLUDGU%3D','1','2023-01-01 10:04:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('4fbb77bb-b7da-4996-822e-87c20eb1f862','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'This is in my hand, and it floats in many other places.','0','2023-01-01 10:05:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('a693e5e5-f0e6-4a1a-b6a6-880a98ed353c','22849f83-a5b4-49eb-93ed-e2d942254521',N'From what I can see, you have atopic dermatitis.','0','2023-01-01 10:06:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('ed8e50f9-81a1-4365-aec4-98095a7b351e','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'Is it dangerous, doctor?','0','2023-01-01 10:07:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('99feeeda-5ed2-452b-a2fe-99cbb144695b','22849f83-a5b4-49eb-93ed-e2d942254521',N'You should go to the clinic so that we can test and diagnose more accurately.','0','2023-01-01 10:08:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('12674b26-560d-4bae-9b6b-9de00e985877','63da4fe0-de4d-4c8e-b8c8-ec3202c20038',N'Thank you doctor, I will come as soon as possible.','0','2023-01-01 10:09:00','ad4210b9-4431-4b78-b28c-0003fc7a77e5');

/*===============*/

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('35654303-41fc-4b64-ae0c-a4a2b6195933','42ce372a-7078-4a4c-93a3-c5000d58bff1',N'Hi Supporter!','0','2023-01-02 10:00:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('3ee672fa-7f84-4114-a846-a52778b05ae3','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'May I help you?','0','2023-01-02 10:01:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('db07ff4f-3160-40bc-8773-a69643a52857','42ce372a-7078-4a4c-93a3-c5000d58bff1',N'I want to make an appointment to see a doctor.','0','2023-01-02 10:02:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('e2f70bbe-db85-48b8-8efe-aff639c126ff','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'You can choose the SERVICE feature to schedule an appointment.','0','2023-01-02 10:03:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('fc676f21-ba77-4823-bc8a-b4e45fcba836','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'There you can book services and appointments.','0','2023-01-02 10:04:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('6bfa3446-2c47-48de-a78f-b59036a8f347','42ce372a-7078-4a4c-93a3-c5000d58bff1',N'Thank you! I will take an appointment right away.','0','2023-01-02 10:05:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('8fd715b6-4927-4fe9-b281-b6a97977a834','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'Happy to assist you.','0','2023-01-02 10:06:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('aaa7a750-9209-498f-8035-be6150e4849b','42ce372a-7078-4a4c-93a3-c5000d58bff1',N'Yes! Thank you very much.','0','2023-01-02 10:07:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('557e19ee-cd8a-4268-8b29-c27e1a92c8c7','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'Do you still need my help?','0','2023-01-02 10:08:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('61b6887c-e244-4f32-845b-c2b8a88cfdca','42ce372a-7078-4a4c-93a3-c5000d58bff1',N'I have no more questions, thank you very much.','0','2023-01-02 10:09:00','b69d8383-6dfb-4763-8710-05b4cbea4d95');

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('0f629b25-93b0-4ac9-8869-c628e6c5aa63','c36a9563-e078-4826-a2eb-ce8d9e70eb64',N'Hi Supporter!','0','2023-01-02 11:00:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('5c917ab9-8575-4811-80f2-c77f8762873b','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'May I help you?','0','2023-01-02 11:01:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d66960f6-5f26-40ac-bd65-cbd7caeae301','c36a9563-e078-4826-a2eb-ce8d9e70eb64',N'I want to make an appointment to see a doctor.','0','2023-01-02 11:02:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d9c5292e-fbb4-4e69-8198-cf0268b7287f','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'You can choose the SERVICE feature to schedule an appointment.','0','2023-01-02 11:03:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('27e8db3a-3fbb-452a-8c86-d4d335a32032','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'There you can book services and appointments.','0','2023-01-02 11:04:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('356752af-f9b4-4375-ad1f-d5496e0ade23','c36a9563-e078-4826-a2eb-ce8d9e70eb64',N'Thank you! I will take an appointment right away.','0','2023-01-02 11:05:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('b6af409c-7551-494d-a940-e01a3a8b929c','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'Happy to assist you.','0','2023-01-02 11:06:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d406ed3d-90f6-4678-a2f5-e33c90199fa1','c36a9563-e078-4826-a2eb-ce8d9e70eb64',N'Yes! Thank you very much.','0','2023-01-02 11:07:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('3ce5ad6f-905e-4df8-afb2-e4a6098cd0bb','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'Do you still need my help?','0','2023-01-02 11:08:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('09358b02-a7c7-42c1-b598-e737a80540e6','c36a9563-e078-4826-a2eb-ce8d9e70eb64',N'I have no more questions, thank you very much.','0','2023-01-02 11:09:00','93aee4f9-bfb3-4af3-b839-0f4fccb31377');

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('c674adbb-acad-423f-8f68-ee62aaa39f0b','f5dfd1eb-e828-4903-aaa9-ddd60642ef0e',N'Hi Supporter!','0','2023-01-02 12:00:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('63efb01a-1921-4462-a7d2-f5e594dd4f03','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'May I help you?','0','2023-01-02 12:01:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('f0926c8e-9eb9-4cdb-b4d9-f7dd95a0eb9a','f5dfd1eb-e828-4903-aaa9-ddd60642ef0e',N'I want to make an appointment to see a doctor.','0','2023-01-02 12:02:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('c4b38049-ee4a-4eb9-8c11-fc7e44d4d290','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'You can choose the SERVICE feature to schedule an appointment.','0','2023-01-02 12:03:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d549051c-b4a7-4c83-b7b0-02e9c0efe7e5','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'There you can book services and appointments.','0','2023-01-02 12:04:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('0f942919-0ca0-49a5-9a86-0a5449d392e0','f5dfd1eb-e828-4903-aaa9-ddd60642ef0e',N'Thank you! I will take an appointment right away.','0','2023-01-02 12:05:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('6cf55f33-db07-43ff-94c0-0c542ffff233','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'Happy to assist you.','0','2023-01-02 12:06:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('363c2659-4e75-4bff-a4cb-0d1e9dcb8b19','f5dfd1eb-e828-4903-aaa9-ddd60642ef0e',N'Yes! Thank you very much.','0','2023-01-02 12:07:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('060eac1f-b8b8-44af-b36a-141b3cb9c40a','b7bd24ed-941a-467a-81cb-b12b9bb1aed3',N'Do you still need my help?','0','2023-01-02 12:08:00','394ba900-3928-4d80-919f-10b419d415ad');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('4cbf4091-8727-4d6e-b1f0-1730e4953a9d','f5dfd1eb-e828-4903-aaa9-ddd60642ef0e',N'I have no more questions, thank you very much.','0','2023-01-02 12:09:00','394ba900-3928-4d80-919f-10b419d415ad');

/*===============*/

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('7379015b-3516-44b5-aace-194aa5e6baa5','bd64eb6b-1993-4ad7-ad81-f70eab00d869',N'Hi Doctor!','0','2023-01-03 10:00:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('2a6840b2-b054-48c6-90b4-1bf8bbee6ea2','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'May I help you?','0','2023-01-03 10:01:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('c3f04a21-0c58-4987-a1ce-1d42bfc7dfe8','bd64eb6b-1993-4ad7-ad81-f70eab00d869',N'I was bitten by an insect causing itchiness and redness on my hand.','0','2023-01-03 10:02:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('44f89f09-3226-4458-9042-220e670ff2ea','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'Can you send me a picture of the symptoms?','0','2023-01-03 10:03:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('288b6ef9-e654-4bfe-a376-2571f60b3980','bd64eb6b-1993-4ad7-ad81-f70eab00d869',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Chat3.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717032117&Signature=eXOibDIQ5X4SdTzVLCvupw%2Bao%2F4%3D','1','2023-01-03 10:04:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('c58ccf9a-327b-41c5-8ec2-29d4f72d4242','bd64eb6b-1993-4ad7-ad81-f70eab00d869',N'I felt very uncomfortable.','0','2023-01-03 10:05:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('fa3f0054-d040-4382-bb09-2e8b891c7363','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'From what I can see, You have been bitten by an three-chambered ant.','0','2023-01-03 10:06:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d5c5caa1-eaa5-41be-801f-389ebfa616bf','bd64eb6b-1993-4ad7-ad81-f70eab00d869',N'Is it dangerous, doctor?','0','2023-01-03 10:07:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('991e3701-45a6-4855-a12c-3ac786ee70f1','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'You should go to the clinic so that we can test and diagnose more accurately.','0','2023-01-03 10:08:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('3ee52c96-04b3-44bc-aede-3acbb4d6b1b8','bd64eb6b-1993-4ad7-ad81-f70eab00d869',N'Thank you doctor, I will come as soon as possible.','0','2023-01-03 10:09:00','2ba8b19e-5ad4-479b-9a7f-115edd5040b1');

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('52ee2e12-aa6d-4585-9b9a-3b4866538443','25fec2c5-87c1-4ec4-9014-9515e5d8863b',N'Hi Doctor!','0','2023-01-03 11:00:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('8582dc51-086c-4b79-abd5-4020d5e5fab2','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'May I help you?','0','2023-01-03 11:01:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('e2c643d1-26f3-4aff-abe1-4a889a16c60e','25fec2c5-87c1-4ec4-9014-9515e5d8863b',N'I have a fever, and I have red dots on my hands.','0','2023-01-03 11:02:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('91581f40-aaf3-4627-bc84-4c1382d3fec5','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'Can you send me a picture of the symptoms?','0','2023-01-03 11:03:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('a2577c88-0a4b-46fc-b9d3-52a6b811ff68','25fec2c5-87c1-4ec4-9014-9515e5d8863b',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Chat4.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717032141&Signature=x9aBT1jW%2BsG0%2FBb0kCcEY18Kxz0%3D','1','2023-01-03 11:04:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('89250d89-bb5e-4c6c-9b29-52cd015e5d73','25fec2c5-87c1-4ec4-9014-9515e5d8863b',N'I feel very tired in my body.','0','2023-01-03 11:05:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('81915ed6-c8b1-4e15-a2fb-56c92024803e','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'From what I can see, You may have had dengue fever.','0','2023-01-03 11:06:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('80457124-b084-4e67-9ec0-5c0b9968efda','25fec2c5-87c1-4ec4-9014-9515e5d8863b',N'Is it dangerous, doctor?','0','2023-01-03 11:07:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('5a2921f8-e9b3-4cd3-924f-60ebc75d04ed','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'You should go to the clinic so that we can test and diagnose more accurately.','0','2023-01-03 11:08:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('a78d79b3-1627-4c89-bd55-66dfd6ef0c59','25fec2c5-87c1-4ec4-9014-9515e5d8863b',N'Thank you doctor, I will come as soon as possible.','0','2023-01-03 11:09:00','42c753c5-f0c2-4e93-865e-1314cfc611c6');

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('3e34c58e-3c6d-466f-b947-68f0f4253c6d','abac9d62-375e-42e9-898d-648864d34985',N'Hi Doctor!','0','2023-01-03 12:00:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('02946e5a-c88c-4631-ad15-6b5d6fffad0d','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'May I help you?','0','2023-01-03 12:01:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('a017849e-699a-440d-8c89-7126e77f7213','abac9d62-375e-42e9-898d-648864d34985',N'These days my legs are aching all the time.','0','2023-01-03 12:02:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d60f29df-10dc-4611-9a5c-78a05580993b','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'Can you send me a picture of the symptoms?','0','2023-01-03 12:03:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('29c09e8f-e43c-433c-b866-840c68d0569c','abac9d62-375e-42e9-898d-648864d34985',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Chat5.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717032158&Signature=vnUkKU0wfyyHFldlHkcDmivQ4e8%3D','1','2023-01-03 12:04:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('b2756b85-1586-4377-83b1-8931aeff542c','abac9d62-375e-42e9-898d-648864d34985',N'My legs are often very painful in the morning and evening.','0','2023-01-03 12:05:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('87336db5-07f0-457a-b390-98403bde9cd2','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'From what I can see, Maybe you have arthritis.','0','2023-01-03 12:06:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d31f8f31-b3d9-407a-b4cc-a11313e56506','abac9d62-375e-42e9-898d-648864d34985',N'Is it dangerous, doctor?','0','2023-01-03 12:07:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('a0f86a9c-d319-4f87-9730-a1838d46f152','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'You should go to the clinic so that we can test and diagnose more accurately.','0','2023-01-03 12:08:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('c7980bcf-5c3e-484e-8dfe-a7aa231c66b8','abac9d62-375e-42e9-898d-648864d34985',N'Thank you doctor, I will come as soon as possible.','0','2023-01-03 12:09:00','b2cd704d-b5b3-4be8-b614-1c07bd7b0770');

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('bb0ddf3f-6ff9-4f55-b108-b4f2e38633ad','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848',N'Hi Doctor!','0','2023-01-03 13:00:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('0b9cbec9-eb7d-43d4-a500-bf42c909d3fe','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'May I help you?','0','2023-01-03 13:01:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('c91f7723-e5a2-4bf9-b12c-bf705e502a92','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848',N'Lately my back has been hurting a lot.','0','2023-01-03 13:02:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('287ccd21-4f4e-4447-a142-bfa58b879db0','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'Can you send me a picture of the symptoms?','0','2023-01-03 13:03:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('bbf7a8c2-4578-4ae8-a737-c00880330ef4','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Chat6.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717032183&Signature=gY%2BeJK45tJ0U51hO6AQSVVVy3Co%3D','1','2023-01-03 13:04:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('32f620d7-26a6-460d-b208-c36dd66ab966','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848',N'I felt very uncomfortable.','0','2023-01-03 13:05:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('8d44923a-db46-41a8-80d8-cfdcf74cc6bf','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'From what I can see, Maybe you have spondylolisthesis.','0','2023-01-03 13:06:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('6936015d-aa4d-4291-ae42-d233045faee7','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848',N'Is it dangerous, doctor?','0','2023-01-03 13:07:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('d42c460c-8748-44ff-9e07-da4b29d4e77f','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'You should go to the clinic so that we can test and diagnose more accurately.','0','2023-01-03 13:08:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('f2766433-69fe-4fb1-b114-ddb097214a0f','ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848',N'Thank you doctor, I will come as soon as possible.','0','2023-01-03 13:09:00','b04e49c9-1ada-43d7-a9df-1dfda469d9e0');

INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('278b6416-22f7-4501-8251-e04305d725c6','9248a7b5-d937-48db-abe6-8ffeb3bf0d0e',N'Hi Doctor!','0','2023-01-03 14:00:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('52d7299b-dccb-4e26-b863-e1a026525aef','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'May I help you?','0','2023-01-03 14:01:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('0f4eba08-04f0-4cb2-837b-e3799b295404','9248a7b5-d937-48db-abe6-8ffeb3bf0d0e',N'I have been coughing a lot lately.','0','2023-01-03 14:02:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('e6cd0bf9-78ce-4c96-9d6d-e64070020eb6','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'Can you send me a picture of the symptoms?','0','2023-01-03 14:03:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('749c4d37-e564-4fbc-8459-f30986b1e680','9248a7b5-d937-48db-abe6-8ffeb3bf0d0e',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Chat7.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717032205&Signature=sPBaw63rXMLEtSGeyhBqpJBGDsc%3D','1','2023-01-03 14:04:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('662923f6-64cc-48c8-95c6-f32baea23dc4','9248a7b5-d937-48db-abe6-8ffeb3bf0d0e',N'I feel very uncomfortable, it affects my life and work.','0','2023-01-03 14:05:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('23e94f56-1b61-494e-b4ae-fe94daa90409','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'From what I can see, You may be suffering from bronchitis.','0','2023-01-03 14:06:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('3b495c7a-c6da-49c5-b2e8-00cba9a5cf13','9248a7b5-d937-48db-abe6-8ffeb3bf0d0e',N'Is it dangerous, doctor?','0','2023-01-03 14:07:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('b622a907-1283-4dd9-930b-02548b4d7319','8edf474e-0fcb-49fc-9c9e-4e902c7472f2',N'You should go to the clinic so that we can test and diagnose more accurately.','0','2023-01-03 14:08:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
INSERT INTO [ChatMessages]  (ChatMessageID,UserID,Content,Type,CreatedAt,RoomID)
VALUES ('495954b4-6651-4a4b-8c3c-034532a778ee','9248a7b5-d937-48db-abe6-8ffeb3bf0d0e',N'Thank you doctor, I will come as soon as possible.','0','2023-01-03 14:09:00','2f645efd-70aa-4a2a-aaa9-244fa1b719bc');
