from config import connection_string
import pyodbc
from data import MachineLearning

connection = pyodbc.connect(connection_string)
sql = connection.cursor()

def GetAll():
    try:
        sql.execute("SELECT * FROM MachineLearning")
        rows = sql.fetchall()
        data = []
        for row in rows:
            machine_learning = MachineLearning(MachineID=row.MachineID.lower(), MachineName=row.MachineName)
            data.append(machine_learning.dict())
        return data
    except Exception as e:
        print("An error occurred while executing GetAll:", str(e))
        return None

def GetByID(id):
    try:
        sql.execute(f"SELECT * FROM MachineLearning WHERE MachineID = '{id}'")
        row = sql.fetchone()
        if row:
            machine_learning = MachineLearning(MachineID=row.MachineID.lower(), MachineName=row.MachineName)
            data = machine_learning.dict()
        else:
            data = None
        return data
    except Exception as e:
        print("An error occurred while executing GetByID:", str(e))
        return None

def Create(name):
    try:
        query = "INSERT INTO MachineLearning (MachineName) VALUES (?)"
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
        query = "UPDATE MachineLearning SET MachineName = ? WHERE MachineID = ?"
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
        query = "DELETE FROM MachineLearning WHERE MachineID = ?"
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
