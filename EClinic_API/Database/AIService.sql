CREATE DATABASE AIService
GO
USE [AIService]

GO

CREATE TABLE [MachineLearning] (
    [MachineID] uniqueidentifier NOT NULL DEFAULT NEWID(),
    [MachineName] VARCHAR(255) NOT NULL
);

CREATE TABLE [Model] (
    [ModelID] uniqueidentifier NOT NULL DEFAULT NEWID(),
    [ModelName] VARCHAR(255) NOT NULL,
    [FileURL] VARCHAR(255) NOT NULL,
    [Accuracy] FLOAT NOT NULL,
    [IsActive] BIT NOT NULL,
    [CreatedAt] DATETIME NOT NULL,
    [UpdatedAt] DATETIME NOT NULL,
    [MachineID] uniqueidentifier NOT NULL,
    [DeepID] uniqueidentifier NOT NULL
);

CREATE TABLE [DeepLearning] (
    [DeepID] uniqueidentifier NOT NULL DEFAULT NEWID(),
    [DeepName] NVARCHAR(255) NOT NULL
);

CREATE TABLE [PredictionHistory] (
    [PredictID] uniqueidentifier NOT NULL DEFAULT NEWID(),
    [Note] NVARCHAR(255) NOT NULL,
    [Result] VARCHAR(255) NOT NULL,
    [PredictTime] DATETIME NOT NULL,
    [ModelID] uniqueidentifier NOT NULL
);

ALTER TABLE [dbo].[MachineLearning] ADD CONSTRAINT [PK_MachineLearning] PRIMARY KEY ([MachineID]);
ALTER TABLE [dbo].[Model] ADD CONSTRAINT [PK_Model] PRIMARY KEY ([ModelID]);
ALTER TABLE [dbo].[DeepLearning] ADD CONSTRAINT [PK_DeepLearning] PRIMARY KEY ([DeepID]);
ALTER TABLE [dbo].[PredictionHistory] ADD CONSTRAINT [PK_PredictionHistory] PRIMARY KEY ([PredictID]);

ALTER TABLE [dbo].[PredictionHistory] ADD CONSTRAINT [FK_PredictionHistory_Model] FOREIGN KEY ([ModelID]) REFERENCES [dbo].[Model] ([ModelID]);
ALTER TABLE [dbo].[Model] ADD CONSTRAINT [FK_Model_MachineLearning] FOREIGN KEY ([MachineID]) REFERENCES [dbo].[MachineLearning] ([MachineID]);
ALTER TABLE [dbo].[Model] ADD CONSTRAINT [FK_Model_DeepLearning] FOREIGN KEY ([DeepID]) REFERENCES [dbo].[DeepLearning] ([DeepID]);
GO


INSERT INTO [dbo].[DeepLearning]([DeepID],[DeepName])
VALUES ('cdf2376e-d944-4d54-8faa-00c43398368b','ResNet-50')
INSERT INTO [dbo].[DeepLearning]([DeepID],[DeepName])
VALUES ('46159922-0175-4408-80b2-68f0ef8f2f6a','VGG16')
INSERT INTO [dbo].[DeepLearning]([DeepID],[DeepName])
VALUES ('0f9f2229-b411-4d7b-a88c-f7d007215fd9','MobileNet')

GO

INSERT INTO [dbo].[MachineLearning]([MachineID],[MachineName])
VALUES ('a9351300-ef9a-441c-8c9d-048f0aa109ac','Random Forest Classification')
INSERT INTO [dbo].[MachineLearning]([MachineID],[MachineName])
VALUES ('84fc935e-ef19-4417-93f3-16634097ebac','K-Nearest Neighbors')
INSERT INTO [dbo].[MachineLearning]([MachineID],[MachineName])
VALUES ('f0c4aeaa-bb06-4927-821b-1b55fd1ecc11','Gaussian Naive Bayes')
INSERT INTO [dbo].[MachineLearning]([MachineID],[MachineName])
VALUES ('0aefd695-d080-4f2f-8b87-76d9864d3b6d','Decision Tree Classification')
INSERT INTO [dbo].[MachineLearning]([MachineID],[MachineName])
VALUES ('4056e726-c9fa-4f3f-be00-c84f79dc3609','Logistic Regression')
INSERT INTO [dbo].[MachineLearning]([MachineID],[MachineName])
VALUES ('4745a160-f27d-4fd4-af4b-d162fb9bfc73','AdaBoost Classifier')

GO

INSERT INTO [dbo].[Model]([ModelID],[ModelName],[FileURL],[Accuracy],[IsActive],[CreatedAt],[UpdatedAt],[MachineID],[DeepID])
VALUES ('7e50f977-3f64-4ede-92ae-003bad413f10','MobileNet_GaussianNB','/app/app/model/MobileNet_GaussianNB.pkl',79.3,0,'2023-06-15 12:52:11.087','2023-06-30 12:52:11.087','f0c4aeaa-bb06-4927-821b-1b55fd1ecc11','0f9f2229-b411-4d7b-a88c-f7d007215fd9')
INSERT INTO [dbo].[Model]([ModelID],[ModelName],[FileURL],[Accuracy],[IsActive],[CreatedAt],[UpdatedAt],[MachineID],[DeepID])
VALUES ('8524bb28-cbb4-4798-817d-17a7f42afd31','MobileNet_LogisticRegression','/app/app/model/MobileNet_LogisticRegression.pkl',96.2,1,'2023-06-17 12:52:11.087','2023-06-19 12:52:11.087','4056e726-c9fa-4f3f-be00-c84f79dc3609','0f9f2229-b411-4d7b-a88c-f7d007215fd9')
INSERT INTO [dbo].[Model]([ModelID],[ModelName],[FileURL],[Accuracy],[IsActive],[CreatedAt],[UpdatedAt],[MachineID],[DeepID])
VALUES ('4f4525f6-3402-47c9-9934-26de920122cb','MobileNet_RandomForestClassifier','/app/app/model/MobileNet_RandomForestClassifier.pkl',83.4,0,'2023-06-19 12:52:11.087','2023-06-30 12:52:11.087','a9351300-ef9a-441c-8c9d-048f0aa109ac','0f9f2229-b411-4d7b-a88c-f7d007215fd9')
INSERT INTO [dbo].[Model]([ModelID],[ModelName],[FileURL],[Accuracy],[IsActive],[CreatedAt],[UpdatedAt],[MachineID],[DeepID])
VALUES ('146ee539-a900-4988-b20e-3f6f27aad5fd','ResNet50_LogisticRegression','/app/app/model/ResNet50_LogisticRegression.pkl',76.2,0,'2023-06-21 12:52:11.087','2023-06-30 12:52:11.087','4056e726-c9fa-4f3f-be00-c84f79dc3609','cdf2376e-d944-4d54-8faa-00c43398368b')
INSERT INTO [dbo].[Model]([ModelID],[ModelName],[FileURL],[Accuracy],[IsActive],[CreatedAt],[UpdatedAt],[MachineID],[DeepID])
VALUES ('52a2a512-2a7e-4352-8f60-7b651e8a3eec','VGG16_LogisticRegression','/app/app/model/VGG16_LogisticRegression.pkl',91.7,0,'2023-06-22 12:52:11.087','2023-06-30 12:52:11.087','4056e726-c9fa-4f3f-be00-c84f79dc3609','46159922-0175-4408-80b2-68f0ef8f2f6a')
INSERT INTO [dbo].[Model]([ModelID],[ModelName],[FileURL],[Accuracy],[IsActive],[CreatedAt],[UpdatedAt],[MachineID],[DeepID])
VALUES ('129fa429-f5e4-4969-9f09-8a988c5eaa05','VGG16_RandomForestClassifier','/app/app/model/VGG16_RandomForestClassifier.pkl',80,0,'2023-06-23 12:52:11.087','2023-06-30 12:52:11.087','a9351300-ef9a-441c-8c9d-048f0aa109ac','46159922-0175-4408-80b2-68f0ef8f2f6a')

GO

INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('a9351300-ef9a-441c-8c9d-048f0aa109ac','The uploaded photo appears to be slightly blurry, which may affect the accuracy of the analysis. ','Monkeypox','2023-06-30 12:52:11.087','129fa429-f5e4-4969-9f09-8a988c5eaa05')
INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('84fc935e-ef19-4417-93f3-16634097ebac','Despite the blurriness, the photo depicts [description of the image] and provides some visual information for evaluation','Monkeypox','2023-06-29 12:52:11.087','129fa429-f5e4-4969-9f09-8a988c5eaa05')
INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('f0c4aeaa-bb06-4927-821b-1b55fd1ecc11','AI analysis suggests the presence of [predicted disease or condition], considering the available details in the photo.','Monkeypox','2023-06-28 12:52:11.087','129fa429-f5e4-4969-9f09-8a988c5eaa05')
INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('0aefd695-d080-4f2f-8b87-76d9864d3b6d','However, due to the image quality, it is important to exercise caution and consider potential limitations in the accuracy of the prediction.','Chickenpox','2023-06-27 12:52:11.087','8524bb28-cbb4-4798-817d-17a7f42afd31')
INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('4056e726-c9fa-4f3f-be00-c84f79dc3609','Requesting additional clear or high-resolution images or conducting further tests may be necessary to confirm the diagnosis.','Measles','2023-06-26 12:52:11.087','8524bb28-cbb4-4798-817d-17a7f42afd31')
INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('4745a160-f27d-4fd4-af4b-d162fb9bfc73','Considering the blurriness, it is recommended to verify the AI predictions through consultation with specialists or by seeking additional clinical information from the patient.','Measles','2023-06-25 12:52:11.087','8524bb28-cbb4-4798-817d-17a7f42afd31')
INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('7e50f977-3f64-4ede-92ae-003bad413f10','The patients medical history and relevant symptoms should be considered alongside the photo analysis to ensure a comprehensive evaluation.','Normal','2023-06-24 12:52:11.087','8524bb28-cbb4-4798-817d-17a7f42afd31')
INSERT INTO [dbo].[PredictionHistory]([PredictID],[Note],[Result],[PredictTime],[ModelID])
VALUES ('8524bb28-cbb4-4798-817d-17a7f42afd31','Follow-up with the patient is advised to gather additional data or conduct further diagnostic examinations as required.','Chickenpox','2023-06-23 12:52:11.087','8524bb28-cbb4-4798-817d-17a7f42afd31')


