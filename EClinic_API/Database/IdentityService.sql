USE [IdentityService]

GO
/*
INSERT INTO [Specializations] (SpecializationID,SpecializationName)
VALUES ('e360722f-7405-4278-a4b2-17497036cef0',N'Đa Khoa');
*/
GO

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('75364b86-380c-42fc-a0ff-56eac258d824','admintest',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','AD','2023-07-01 08:00:00','2023-07-01 08:00:00');

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('5b4f6db5-3eb0-4eb2-886b-07bfee043579','adminkhang',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','AD','2023-07-02 08:00:00','2023-07-02 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('7d36545e-d590-47a5-be2c-04843182d4b8','adminquynh',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','AD','2023-07-03 08:00:00','2023-07-03 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('857fce86-2f07-43e1-a808-1197dd107acc','adminthang',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','AD','2023-07-04 08:00:00','2023-07-04 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('9fbc9031-5f8f-4dd9-ac48-1a10ac2bbfb5','admincanh',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','AD','2023-07-05 08:00:00','2023-07-05 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('03eca33c-12fe-49d3-9ad9-3d582a9bc95a','adminsang',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','AD','2023-07-06 08:00:00','2023-07-06 08:00:00');

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('99b8a1ab-1302-4443-9ef7-95fae52b4938','expertkhang',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','EP','2023-07-07 08:00:00','2023-07-07 08:00:00');

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('22849f83-a5b4-49eb-93ed-e2d942254521','doctortest',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-08 08:00:00','2023-07-08 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('8edf474e-0fcb-49fc-9c9e-4e902c7472f2','anhtuan123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-09 08:00:00','2023-07-09 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('b514b384-ed9c-4d68-90ae-4f437bdee6b9','bichdao123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-10 08:00:00','2023-07-10 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('f8f4c1f6-b620-42e6-9da9-54438bbd13c5','quangnam123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-11 08:00:00','2023-07-11 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('5145193c-51dc-4fed-8aa9-8adc068b9c69','quyquyen123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-12 08:00:00','2023-07-12 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('5a504858-ec5d-4a7b-9c07-9302e6964bd9','thihang123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-13 08:00:00','2023-07-13 08:00:00');

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('b0ed6e7d-5824-49ee-a025-8de3b677a5fc','vankhanh123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-14 08:00:00','2023-07-14 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('7fdd10b3-2070-47f5-9414-ab93ade7fd5e','anhtuyet123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-15 08:00:00','2023-07-15 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('a156cf24-3cbb-43d7-a76b-d09c842deeda','bachtuyet123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-16 08:00:00','2023-07-16 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('162d22a0-8124-4e17-842e-f725c823aa23','trungdung123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-17 08:00:00','2023-07-17 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('246a5605-cac0-4736-9446-10053bead807','duytoan123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-18 08:00:00','2023-07-18 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('f9afc6da-14e9-4949-bff2-159d1a374bda','minhtruong123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-19 08:00:00','2023-07-19 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('8baefb8b-794e-4d67-ad9b-1e3c747c110a','anhtuyet234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-20 08:00:00','2023-07-20 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('7cfcd120-4e00-4ebd-8ba8-239e9b67bc65','bichngoc123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-21 08:00:00','2023-07-21 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('c6dca19e-ca50-4521-863d-379cbd120013','ngocphuoc123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-22 08:00:00','2023-07-22 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('2268c972-4fb2-4a01-8dea-43341f1e6931','thithao234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','DT','2023-07-23 08:00:00','2023-07-23 08:00:00');


INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('32ab71e0-a75d-4d39-8d5d-e66525477d48','supportertest',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','SP','2023-07-24 08:00:00','2023-07-24 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('b7bd24ed-941a-467a-81cb-b12b9bb1aed3','huutrong123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','SP','2023-07-25 08:00:00','2023-07-25 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('30c20bc2-e8e3-4052-815c-b1a50668e91c','trongnghia123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','SP','2023-07-26 08:00:00','2023-07-26 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('68b9aff7-97f9-46d1-8c9e-b6a497795713','huunhan123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','SP','2023-07-27 08:00:00','2023-07-27 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('8701c16a-714b-4317-a8e3-c22842b051c0','vankhanh234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','SP','2023-07-28 08:00:00','2023-07-28 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('c90d2651-857b-4694-bfa7-c3dd8a45031b','trongtin123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','SP','2023-07-29 08:00:00','2023-07-29 08:00:00');

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('63da4fe0-de4d-4c8e-b8c8-ec3202c20038','usertest',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-01 08:00:00','2023-08-01 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('42ce372a-7078-4a4c-93a3-c5000d58bff1','hoangbao123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-02 08:00:00','2023-08-02 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('c36a9563-e078-4826-a2eb-ce8d9e70eb64','nhatduc123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-03 08:00:00','2023-08-03 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('f5dfd1eb-e828-4903-aaa9-ddd60642ef0e','huutrong456',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-04 08:00:00','2023-08-04 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('bd64eb6b-1993-4ad7-ad81-f70eab00d869','minhhuy123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-05 08:00:00','2023-08-05 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('25fec2c5-87c1-4ec4-9014-9515e5d8863b','quoctrung123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-06 08:00:00','2023-08-06 08:00:00');

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('abac9d62-375e-42e9-898d-648864d34985','myanh123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-07 08:00:00','2023-08-07 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('ec3bdeb1-dfc1-442f-8b4b-68ee68cbc848','hoaiyen123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-08 08:00:00','2023-08-08 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('9248a7b5-d937-48db-abe6-8ffeb3bf0d0e','yennhi123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-09 08:00:00','2023-08-09 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('6cc6f27f-ac78-464e-a519-a56798e38ae9','thinhi123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-10 08:00:00','2023-08-10 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('3480d242-8d09-43e8-910d-adbd9cb84012','phuocdat123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-11 08:00:00','2023-08-11 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('be694904-b294-4c51-a0c4-db8ce8537dea','trihung123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-12 08:00:00','2023-08-12 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('bb974f0d-9383-4502-bb94-fc1d0fc3eab6','hoangdat123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','0','US','2023-08-13 08:00:00','2023-08-13 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('420f4420-7e8b-4100-943a-fededd5c4cde','thanhsang123',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','0','US','2023-08-14 08:00:00','2023-08-14 08:00:00');

INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('e304370f-3f72-44f1-af0e-6daaa9954ef8','ngocan234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-15 08:00:00','2023-08-15 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('6057ce03-8923-47b0-b0cf-8bcb07f06b44','tramanh234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-16 08:00:00','2023-08-16 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('9b0a2802-cac5-488f-843d-91d3adb99fbf','vietanh234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-17 08:00:00','2023-08-17 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('50e7bae7-3a74-4232-ab4c-a30214f6c6b9','quynhanh234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-18 08:00:00','2023-08-18 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('9cce93e4-f88e-47fa-93ce-a6ec0569f76b','ducanh234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-19 08:00:00','2023-08-19 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('bdd1b27d-5fbf-4a35-a5f4-b8cd47de498c','linhchi234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-20 08:00:00','2023-08-20 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('fe9c4099-9b05-45dd-a299-c408ba574878','mydung234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-21 08:00:00','2023-08-21 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('826e96cd-e68e-4e21-b17f-d5adc5545872','manhduy234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-22 08:00:00','2023-08-22 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('5a49af6d-19cb-4b15-9456-e3a1a9a083f7','thuyduong234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-23 08:00:00','2023-08-23 08:00:00');
INSERT INTO [Users] (UserID,UserName, PasswordSalt, PasswordHash,Enabled,RoleID,CreatedAt,UpdatedAt) 
VALUES ('ca064e23-1ce0-4ca8-b045-ea8cf23bbc8c','minhhang234',0xF8772DF93BB4293EB0D4D6B4C4B01DCC,'fBQ6kuS9N5pUsCciJtFS5jNhfD6GX8GoXhmmZULPW/4=','1','US','2023-08-24 08:00:00','2023-08-24 08:00:00');


GO

