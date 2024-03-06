from config import connection_string
import pyodbc
from data import Model, MachineLearning, DeepLearning, ModelAll,GetModelDtos
from datetime import datetime

connection = pyodbc.connect(connection_string)
sql = connection.cursor()

def GetAll():
    try:
        query = '''
            SELECT
                M."ModelID",
                M."ModelName",
                M."FileURL",
                M."Accuracy",
                M."IsActive",
                M."CreatedAt",
                M."UpdatedAt",
                M."MachineID",
                ML."MachineName",
                M."DeepID",
                DL."DeepName"
            FROM
                "Model" M
                JOIN "MachineLearning" ML ON M."MachineID" = ML."MachineID"
                JOIN "DeepLearning" DL ON M."DeepID" = DL."DeepID"
            ORDER BY UpdatedAt DESC
            '''
        sql.execute(query)
        rows = sql.fetchall()
        data = []
        for row in rows:
            model = ModelAll(
                ModelID=row[0].lower(),
                ModelName =row[1],
                Accuracy=row[3],
                IsActive=row[4],
                MachineLearning=MachineLearning(
                    MachineID=row[7].lower(),
                    MachineName=row[8]
                ),
                DeepLearning=DeepLearning(
                    DeepID=row[9].lower(),
                    DeepName=row[10]
                )
            )
            data.append(model.dict())
        return data
    except Exception as e:
        print("An error occurred:", str(e))
        return []

def GetByID(id):
    try:
        query = f'''
            SELECT
                M."ModelID",
                M."ModelName",
                M."FileURL",
                M."Accuracy",
                M."IsActive",
                M."CreatedAt",
                M."UpdatedAt",
                M."MachineID",
                ML."MachineName",
                M."DeepID",
                DL."DeepName"
            FROM
                "Model" M
                JOIN "MachineLearning" ML ON M."MachineID" = ML."MachineID"
                JOIN "DeepLearning" DL ON M."DeepID" = DL."DeepID"
            WHERE ModelID = '{id}'
            '''
        sql.execute(query)

        row = sql.fetchone()
        if row:
            model = Model(
                ModelID=row[0].lower(),
                ModelName = row[1],
                Accuracy=row[3],
                IsActive=row[4],
                CreatedAt=str(row[5]),
                UpdatedAt=str(row[6]),
                MachineLearning=MachineLearning(
                    MachineID=row[7].lower(),
                    MachineName=row[8]
                ),
                DeepLearning=DeepLearning(
                    DeepID=row[9].lower(),
                    DeepName=row[10]
                )
            )
            data = model.dict()
        else:
            data = None
        return data
    except Exception as e:
        print("An error occurred:", str(e))
        return []

def Create(ModelName, Accuracy, MachineID, DeepID, FileURL):
    try:
        query = "INSERT INTO Model (ModelName,FileURL,Accuracy,IsActive,CreatedAt,UpdatedAt,MachineID,DeepID) VALUES (?,?,?,?,?,?,?,?)"
        values = (ModelName,FileURL, Accuracy, False, datetime.now(), datetime.now(), MachineID, DeepID)
        sql.execute(query, values)
        connection.commit()
        count = sql.rowcount
        if count > 0:
            return True
        else:
            return False
    except Exception as e:
        print("An error occurred:", str(e))
        return False

def Update(ModelID,ModelName,Accuracy, MachineID, DeepID, FileURL):
    try:
        if FileURL is None:
            query = "UPDATE Model SET ModelName= ?, Accuracy = ?,MachineID= ?,DeepID=?,UpdatedAt =? WHERE ModelID = ?"
            values = (ModelName, Accuracy, MachineID, DeepID, datetime.now(), ModelID)
        else:
            query = "UPDATE Model SET ModelName= ?, Accuracy = ?,MachineID= ?,DeepID=? ,FileURL=?,UpdatedAt =? WHERE ModelID = ?"
            values = (ModelName, Accuracy, MachineID, DeepID, FileURL,datetime.now(), ModelID)
        sql.execute(query, values)
        connection.commit()
        count = sql.rowcount
        if count > 0:
            return True
        else:
            return False
    except Exception as e:
        print("An error occurred:", str(e))
        return False

def Delete(id):
    try:
        query = "DELETE FROM Model WHERE ModelID = ?"
        values = (id,)
        sql.execute(query, values)
        connection.commit()
        count = sql.rowcount
        if count > 0:
            return True
        else:
            return False
    except Exception as e:
        print("An error occurred:", str(e))
        return False

def GetActive():
    try:
        query = f'''
            SELECT M.ModelID,M.FileURL,DL.DeepName
            FROM
                "Model" M
                JOIN "DeepLearning" DL ON M."DeepID" = DL."DeepID"
            WHERE IsActive = 1
            '''
        sql.execute(query)
        row = sql.fetchone()
        if row:
            data = GetModelDtos(ModelID=row[0].lower(),FileUrl=row[1],DeepName=row[2])
        else:
            data = None
        return data
    except Exception as e:
        print("An error occurred:", str(e))
        return []
def GetModelUrl(id):
    try:
        query = f'''
            SELECT M.ModelID,M.FileURL,DL.DeepName
            FROM
                "Model" M
                JOIN "DeepLearning" DL ON M."DeepID" = DL."DeepID"
            WHERE ModelID = '{id}'
            '''
        sql.execute(query)
        row = sql.fetchone()
        if row:
            data = GetModelDtos(ModelID=row[0].lower(),FileUrl=row[1],DeepName=row[2])
        else:
            data = None
        return data
    except Exception as e:
        print("An error occurred:", str(e))
        return []
def ActiveModel(id):
    try:
        query = f'''
                UPDATE Model SET IsActive = 1 WHERE ModelID = '{id}'
                UPDATE Model SET IsActive = 0 WHERE ModelID <> '{id}'
                '''
        sql.execute(query)
        connection.commit()
        count = sql.rowcount
        if count > 0:
            return True
        else:
            return False
    except Exception as e:
        print("An error occurred:", str(e))
        return False