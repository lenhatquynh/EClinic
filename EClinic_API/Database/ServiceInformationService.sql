USE ServiceInformationService

GO

INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('e360722f-7405-4278-a4b2-17497036cef0',N'Multispeciality');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('af645b86-0d57-400d-ac87-9fa2a780408f',N'Pediatrics');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('8739f592-43bd-42f6-a5e6-a4986feccad0',N'Obstetrics');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('af144812-f2b6-4272-9cf6-c1e3af08e81a',N'Musculoskeletal system');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('02684979-45c5-4e99-b18f-2741b24f00ff',N'Cardiology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('0faa39fa-6021-4eb7-982a-2bf40e0670d9',N'Endocrinology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('a1043784-0289-4511-a73b-38848cece653',N'Dermatology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('90efa17f-952a-4d89-836a-51030a9e6564',N'Gastroenterology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('58e1c920-fa14-4b6a-83fb-74d5673cb3bc',N'Oncology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('f32124ca-afc7-43dd-9a6e-a06b5444bc55',N'Nutrition');

INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('2c3c44c2-9961-44cf-a909-aa8511f0edae',N'Respiratory');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('94e9d778-417c-4611-ba96-af88958447e7',N'Geriatrics');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('f8db42e9-dda4-4815-b8d6-b322470efe46',N'Thoracic vascular');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('0b2f57b5-165a-4d67-bd3a-b3cde1dd6d14',N'Andrology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('50b45d48-1475-4d7b-b80d-ba2813cd6a40',N'Ophthalmology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('c15b26f8-c997-45e1-b532-c873293bd999',N'General Medical');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('ab429bdf-99db-4c2b-b50b-cf5b6fb4126a',N'Medical ultrasound');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('6bcf1139-aa3c-4007-8afe-d39935b50bce',N'Otorhinolaryngology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('81a9cc52-7b67-4a61-8bcc-3782be771fce',N'Psychiatry');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('57d380f2-9ce4-4d07-9100-37b0dc0e32af',N'Nephro - Urology');

INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('0231f164-2680-42f5-84fb-5f07428eaddd',N'Neurology');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('f3575ab5-1d36-426c-888c-6415324c3f0d',N'Infectious disease');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('4b6f23f7-a4e2-47d8-a933-83d52ba8d9b7',N'Physiotherapy');
INSERT INTO [Specialization] (SpecializationID,SpecializationName)
VALUES ('5f0d1591-e9f8-49fd-af4a-902b8937bb6d',N'Traditional medicine');

GO

INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('25d316bd-0e90-4dd1-9292-4b8768d5b88a',N'Urea','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('114630fc-e624-4394-ba60-4db570b85ff0',N'Creatinine','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('78039832-49ee-49c6-81bc-52b68ca0099f',N'Glucose','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('56a38f38-7308-4090-8f7e-5792f67cb988',N'LDL cholest','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('cd77ca4d-d176-4291-94db-581ea1f35c50',N'HDL cholest','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('92030993-601f-45a5-8928-630478327211',N'Triglycerid','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('f65e4bc0-7c8d-468e-b5e8-677b8730f6ea',N'GGT','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('f48cb785-d2ba-4b1c-a998-6a9879507536',N'Cholesterol','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('6ad9c924-d863-4f21-99b8-75b0362169f7',N'SGOT','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('13fe8194-c907-4a93-ac72-854b83ce0148',N'SGPT','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('eecfb5d0-8e94-47cb-8b00-87872435b299',N'Uric acid','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('787d3700-531b-4cfc-8b9f-8f0e2a35d285',N'Total urinalysis','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('b12ea29d-9e41-451a-b6c9-90d1f2aa0c5d',N'Blood formula','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('bcb0ce5a-86dc-422e-85cc-9139367948e8',N'Quick test for covid','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('1f29d1ab-53a4-4ce2-99b6-9408e5498cfe',N'Liver enzymes SGOT','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('4b89e839-1473-4a25-ac47-94f4ea93330e',N'Liver enzyme SGPT','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('89a8a7ae-297f-4c55-952e-9c797e3f9b21',N'Uric Acid (Gout Disease)','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('e09ade4f-a162-4e98-9762-a3033096ff87',N'Creatinine (Kidney Function)','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('ab1e2054-60e3-4b59-a798-aaa6e67d962c',N'AFP','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('72f66228-ed29-403e-baa7-affec9ad0a39',N'Phosphatase Alkaline','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('35be4d95-08fe-4e6f-831f-b62758805848',N'Total Bilirubin','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('502a18b2-2544-407e-af01-baec5d2bbaf1',N'Albumin','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('30a2df8e-da3d-4ff7-b8d2-c03e775302a8',N'Free PSA','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('d1908a5b-e16d-4a17-b12a-c50340e7fd86',N'TSH','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('0b0242b7-91c6-4a69-b613-cc225379df57',N'T3 Free','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('343a02f6-9cc3-43c3-bfa2-cdb703d8d5bf',N'T4 Free','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('14ce0267-c79a-4bf5-88fe-cee40167326d',N'CEA','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('94ca6431-fada-433e-993d-d08a272d360a',N'CA 125 (Ovaries)','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('18d74bea-ce1f-42b8-8ece-d13968a734da',N'CA 19-9','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('55603712-f847-40b6-a4a3-d78a4c18418b',N'Anti-HCV Hepatitis C Anti-HCV','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('4b405963-128b-47dc-b380-d8736281f8e2',N'CYFRA 21-1 (Cytokeratins - 19)','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('58e6afea-b322-4240-8811-dd24c6cd9130',N'HbA1C (Hemoglobin A1C)','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('8583273e-57b7-40f5-9592-ddeffc62e29b',N'Iron Serum (Iron)','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('d811efef-5c2c-44b3-bffb-e04f85098293',N'CA 72-4(Cancer antigen 72-4)','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('9144752c-4bb1-4035-9136-e5b258a7ff16',N'eGFR','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('68760298-52c1-460d-88e0-e66f4cf43c9c',N'Live Bilirubin','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('436f25cf-0c53-4d4e-a2b3-e99f8440c81f',N'Breast color ultrasound','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('e9ee8174-75f6-4c3a-9dfd-ef4192448908',N'Gynecological examination','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('03a7de85-6d10-4107-ae99-f111be4d02a3',N'Pap smear','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [Service] (ServiceID,ServiceName,Price,EstimatedTime,IsActive,SpecializationID,CreatedAt,UpdatedAt)
VALUES ('12ee212f-80ee-4a4d-ba64-f428e939d999',N'Check out the white blood','100000','60','1','e360722f-7405-4278-a4b2-17497036cef0','2023-01-01 09:00:00','2023-01-01 09:00:00');

GO

INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de',N'General test package',N'A general blood test is considered a convenient and fast method of checking health and is prescribed by most doctors in medical examination and treatment cases. Through the indicators from the test results, you will understand the current body condition and detect health problems. Moreover, you can also treat early diseases such as gout, liver and kidney dysfunction, diabetes, atherosclerosis or screen for some types of cancer such as breast cancer, cervical cancer, etc.',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package1.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326187&Signature=8WOr%2F7Z8xZszfoqGfeefIcvMpic%3D','600000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('e8d74a74-6b6a-47a3-a0e6-1e14d302960a',N'Quick test for covid-19',N'Quick test for covid-19',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package2.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326221&Signature=co36XA8mmU788EiuhMHyWVxwoEc%3D','100000','0','10','20','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56',N'Test package to check blood fat liver enzymes',N'The test package to check liver enzymes and blood fats was born with routine items such as liver enzymes, blood fats... to assess health status, thereby proactively adjusting diet and activities. Early detection of hidden diseases can lead to early treatment, improve cure rates and save costs. You can schedule a home sample collection and test results will be available the same day. After that, the doctor calls to discuss the results in detail, gives an appropriate lifestyle or if there is an abnormality, the doctor advises the patient to go to the hospital to perform some more in-depth diagnostic methods to Accurately detect the disease, thereby providing effective treatment.',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package3.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326233&Signature=BbxqccFD5k0YWx9uXm9JzRb7FfA%3D','300000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('1c468121-5123-4aad-8ff7-25e7c87d8a67',N'PCR test Covid - application form',N'PCR test Covid - application form',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package4.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326245&Signature=TYyd1QMrclhbjQxoStsGfiMLRhc%3D','200000','0','10','15','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252',N'Basic health check package',N'The basic health checkup package at Eclinic provides the most basic examination and testing items to help check the health status in a comprehensive way, which includes some important items such as blood tests, laboratory tests. liver function, kidney function, urinalysis, general internal examination, abdominal ultrasound, ...',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package5.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326258&Signature=6j3nsDO9TpTLoCjI4vbAbkjHhJU%3D','700000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('b03b17d3-8cf2-46a7-9fee-33fa55779619',N'Liver test package',N'The liver is one of the most important organs in the human body. The liver plays an important role in metabolism and a number of other functions in the body such as glycogen storage, plasma protein synthesis and detoxification. The test package focuses on assessing liver function by testing liver enzymes, testing for hepatitis B or assessing your immune status if you have been vaccinated, along with screening for liver cancer.',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package6.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326302&Signature=7JErGkqOmZR%2Fc6C06snSBaVKg04%3D','450000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c',N'Premium health checkup package',N'Besides providing basic examination and testing items such as blood tests, liver and kidney function tests, total urinalysis, general internal examination, abdominal ultrasound, ... Premium health checkup package There are also screening test items for common cancers such as liver cancer, stomach cancer, lung cancer..., as well as examination items and imaging techniques to provide general results. and the most complete about the current body condition.',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package7.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326314&Signature=jGl%2BlMf2MBscpjZYJS1ZlqwkpIk%3D','800000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('a5d3d2d7-1274-4447-80f8-42a074350fbc',N'Female cancer screening test',N'Female cancer screening is the detection of cancer at a very early stage, without any symptoms of the disease. Very early stage cancers are mostly cured by simple, low-cost methods, with very few side effects, without affecting function and aesthetics. In addition, cancer screening detects precancerous lesions, which are noncancerous lesions that are more likely to turn cancerous later in life.',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package8.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326333&Signature=2FsFgRJJdcW6gSLqgo3%2FFS6rW3M%3D','800000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('8ed52064-8ed1-42d4-aa67-44348c96c960',N'Male cancer screening test',N'Cancer is a disease that leads to rapid death. General examination and cancer screening to help screen 6 types of cancer, early detection of cancer markers for patients to intervene and treat promptly.',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package9.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326348&Signature=Fz%2BZAo8IBJ87OB6%2Bm8c4iiD9E34%3D', '600000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');
INSERT INTO [ServicePackage] (ServicePackageID,ServicePackageName,Description,Image,Price,Discount,TotalOrder,EstimatedTime,IsActive,CreatedAt,UpdatedAt)
VALUES ('0e09d0da-2471-4ffe-9156-46a21c86afcb',N'Women specialty examination package',N'Women specialty examination package',N'https://eclinicbucket.s3.ap-southeast-1.amazonaws.com/Image/Package10.jpg?AWSAccessKeyId=AKIAUBYK6ZN247UCBSWB&Expires=1717326366&Signature=Rtf%2BxDCIby6URMnVnSsvK9ew%2FFQ%3D','1000000','0','10','60','1','2023-01-01 09:00:00','2023-01-01 09:00:00');

GO

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','25d316bd-0e90-4dd1-9292-4b8768d5b88a');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','114630fc-e624-4394-ba60-4db570b85ff0');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','78039832-49ee-49c6-81bc-52b68ca0099f');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','56a38f38-7308-4090-8f7e-5792f67cb988');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','cd77ca4d-d176-4291-94db-581ea1f35c50');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','92030993-601f-45a5-8928-630478327211');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','f65e4bc0-7c8d-468e-b5e8-677b8730f6ea');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','f48cb785-d2ba-4b1c-a998-6a9879507536');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','6ad9c924-d863-4f21-99b8-75b0362169f7');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','13fe8194-c907-4a93-ac72-854b83ce0148');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','eecfb5d0-8e94-47cb-8b00-87872435b299');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','787d3700-531b-4cfc-8b9f-8f0e2a35d285');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('27e602df-1020-46ae-b9ad-1ddd2ac658de','b12ea29d-9e41-451a-b6c9-90d1f2aa0c5d');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('e8d74a74-6b6a-47a3-a0e6-1e14d302960a','bcb0ce5a-86dc-422e-85cc-9139367948e8');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','78039832-49ee-49c6-81bc-52b68ca0099f');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','cd77ca4d-d176-4291-94db-581ea1f35c50');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','92030993-601f-45a5-8928-630478327211');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','f65e4bc0-7c8d-468e-b5e8-677b8730f6ea');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','f48cb785-d2ba-4b1c-a998-6a9879507536');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','b12ea29d-9e41-451a-b6c9-90d1f2aa0c5d');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','1f29d1ab-53a4-4ce2-99b6-9408e5498cfe');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','4b89e839-1473-4a25-ac47-94f4ea93330e');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','89a8a7ae-297f-4c55-952e-9c797e3f9b21');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('f63d4de0-1373-422b-a8c6-23c7fa9c9e56','e09ade4f-a162-4e98-9762-a3033096ff87');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','25d316bd-0e90-4dd1-9292-4b8768d5b88a');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','114630fc-e624-4394-ba60-4db570b85ff0');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','78039832-49ee-49c6-81bc-52b68ca0099f');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','56a38f38-7308-4090-8f7e-5792f67cb988');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','cd77ca4d-d176-4291-94db-581ea1f35c50');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','92030993-601f-45a5-8928-630478327211');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','f65e4bc0-7c8d-468e-b5e8-677b8730f6ea');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','f48cb785-d2ba-4b1c-a998-6a9879507536');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','6ad9c924-d863-4f21-99b8-75b0362169f7');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','13fe8194-c907-4a93-ac72-854b83ce0148');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','eecfb5d0-8e94-47cb-8b00-87872435b299');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','787d3700-531b-4cfc-8b9f-8f0e2a35d285');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a0f83102-7fb5-425f-8d72-31b76b17f252','b12ea29d-9e41-451a-b6c9-90d1f2aa0c5d');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b03b17d3-8cf2-46a7-9fee-33fa55779619','f65e4bc0-7c8d-468e-b5e8-677b8730f6ea');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b03b17d3-8cf2-46a7-9fee-33fa55779619','6ad9c924-d863-4f21-99b8-75b0362169f7');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b03b17d3-8cf2-46a7-9fee-33fa55779619','13fe8194-c907-4a93-ac72-854b83ce0148');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b03b17d3-8cf2-46a7-9fee-33fa55779619','ab1e2054-60e3-4b59-a798-aaa6e67d962c');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','25d316bd-0e90-4dd1-9292-4b8768d5b88a');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','114630fc-e624-4394-ba60-4db570b85ff0');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','78039832-49ee-49c6-81bc-52b68ca0099f');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','56a38f38-7308-4090-8f7e-5792f67cb988');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','cd77ca4d-d176-4291-94db-581ea1f35c50');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','92030993-601f-45a5-8928-630478327211');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','f65e4bc0-7c8d-468e-b5e8-677b8730f6ea');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','72f66228-ed29-403e-baa7-affec9ad0a39');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','35be4d95-08fe-4e6f-831f-b62758805848');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','502a18b2-2544-407e-af01-baec5d2bbaf1');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','f48cb785-d2ba-4b1c-a998-6a9879507536');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','6ad9c924-d863-4f21-99b8-75b0362169f7');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','13fe8194-c907-4a93-ac72-854b83ce0148');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','eecfb5d0-8e94-47cb-8b00-87872435b299');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','30a2df8e-da3d-4ff7-b8d2-c03e775302a8');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','787d3700-531b-4cfc-8b9f-8f0e2a35d285');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','d1908a5b-e16d-4a17-b12a-c50340e7fd86');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','0b0242b7-91c6-4a69-b613-cc225379df57');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','343a02f6-9cc3-43c3-bfa2-cdb703d8d5bf');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','ab1e2054-60e3-4b59-a798-aaa6e67d962c');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','14ce0267-c79a-4bf5-88fe-cee40167326d');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','94ca6431-fada-433e-993d-d08a272d360a');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','18d74bea-ce1f-42b8-8ece-d13968a734da');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','55603712-f847-40b6-a4a3-d78a4c18418b');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','4b405963-128b-47dc-b380-d8736281f8e2');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','58e6afea-b322-4240-8811-dd24c6cd9130');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','8583273e-57b7-40f5-9592-ddeffc62e29b');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','d811efef-5c2c-44b3-bffb-e04f85098293');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','9144752c-4bb1-4035-9136-e5b258a7ff16');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','b12ea29d-9e41-451a-b6c9-90d1f2aa0c5d');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('b00b0d4c-b1a1-42f6-9c1a-3d5b5c53648c','68760298-52c1-460d-88e0-e66f4cf43c9c');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a5d3d2d7-1274-4447-80f8-42a074350fbc','ab1e2054-60e3-4b59-a798-aaa6e67d962c');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a5d3d2d7-1274-4447-80f8-42a074350fbc','14ce0267-c79a-4bf5-88fe-cee40167326d');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a5d3d2d7-1274-4447-80f8-42a074350fbc','94ca6431-fada-433e-993d-d08a272d360a');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a5d3d2d7-1274-4447-80f8-42a074350fbc','18d74bea-ce1f-42b8-8ece-d13968a734da');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a5d3d2d7-1274-4447-80f8-42a074350fbc','4b405963-128b-47dc-b380-d8736281f8e2');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('a5d3d2d7-1274-4447-80f8-42a074350fbc','d811efef-5c2c-44b3-bffb-e04f85098293');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('8ed52064-8ed1-42d4-aa67-44348c96c960','ab1e2054-60e3-4b59-a798-aaa6e67d962c');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('8ed52064-8ed1-42d4-aa67-44348c96c960','30a2df8e-da3d-4ff7-b8d2-c03e775302a8');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('8ed52064-8ed1-42d4-aa67-44348c96c960','14ce0267-c79a-4bf5-88fe-cee40167326d');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('8ed52064-8ed1-42d4-aa67-44348c96c960','18d74bea-ce1f-42b8-8ece-d13968a734da');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('8ed52064-8ed1-42d4-aa67-44348c96c960','4b405963-128b-47dc-b380-d8736281f8e2');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('8ed52064-8ed1-42d4-aa67-44348c96c960','d811efef-5c2c-44b3-bffb-e04f85098293');

INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('0e09d0da-2471-4ffe-9156-46a21c86afcb','436f25cf-0c53-4d4e-a2b3-e99f8440c81f');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('0e09d0da-2471-4ffe-9156-46a21c86afcb','e9ee8174-75f6-4c3a-9dfd-ef4192448908');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('0e09d0da-2471-4ffe-9156-46a21c86afcb','03a7de85-6d10-4107-ae99-f111be4d02a3');
INSERT INTO [ServicePackageItem] (ServicePackageID,ServiceID)
VALUES ('0e09d0da-2471-4ffe-9156-46a21c86afcb','12ee212f-80ee-4a4d-ba64-f428e939d999');
