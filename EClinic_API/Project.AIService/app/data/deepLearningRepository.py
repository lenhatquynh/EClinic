from config import connection_string
import pyodbc
from data import DeepLearning

connection = pyodbc.connect(connection_string)
sql = connection.cursor()

def GetAll():
    try:
        sql.execute("SELECT * FROM DeepLearning")
        rows = sql.fetchall()
        data = []
        for row in rows:
            deep_learning = DeepLearning(DeepID=row.DeepID.lower(), DeepName=row.DeepName)
            data.append(deep_learning.dict())
        return data
    except Exception as e:
        print("An error occurred while executing GetAll:", str(e))
        return None

def GetByID(id):
    try:
        sql.execute(f"SELECT * FROM DeepLearning WHERE DeepID = '{id}'")
        row = sql.fetchone()
        if row:
            machine_learning = DeepLearning(DeepID=row.DeepID.lower(), DeepName=row.DeepName)
            data = machine_learning.dict()
        else:
            data = None
        return data
    except Exception as e:
        print("An error occurred while executing GetByID:", str(e))
        return None

def Create(name):
    try:
        query = "INSERT INTO DeepLearning (DeepName) VALUES (?)"
        values = (name,)
        sql.execute(query, values)
        connection.commit()
        count = sql.rowcount
        if count > 0:
            return True
        else:
            return False
    except Exception as e:
        print("An error occurred while executing Create:", str(e))
        return False

def Update(id, name):
    try:
        query = "UPDATE DeepLearning SET DeepName = ? WHERE DeepID = ?"
        values = (name, id)
        sql.execute(query, values)
        connection.commit()
        count = sql.rowcount
        if count > 0:
            return True
        else:
            return False
    except Exception as e:
        print("An error occurred while executing Update:", str(e))
        return False

def Delete(id):
    try:
        query = "DELETE FROM DeepLearning WHERE DeepID = ?"
        values = (id,)
        sql.execute(query, values)
        connection.commit()
        count = sql.rowcount 
        if count > 0:
            return True
        else:
            return False
    except Exception as e:
        print("An error occurred while executing Delete:", str(e))
        return False
