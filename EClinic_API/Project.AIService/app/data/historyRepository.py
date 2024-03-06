from config import connection_string
import pyodbc
from datetime import datetime
from data import MachineLearning,DeepLearning,PredictionHistoryALL,PredictionHistory,PaginationResponseHeader,PaginationResponse
connection = pyodbc.connect(connection_string)
sql = connection.cursor()
def GetAll(PageIndex, PageSize):
    try:
        query = f'''
            SELECT 
                P."PredictID",
                P."Result",
                P."PredictTime",
                M."ModelName"
            FROM
                "PredictionHistory" P
                JOIN "Model" M ON M."ModelID" = P."ModelID"
            ORDER BY PredictTime DESC
            OFFSET {(PageIndex - 1) * PageSize} ROWS
            FETCH NEXT {PageSize} ROWS ONLY
            '''
        count_query = '''
            SELECT COUNT(*) FROM "PredictionHistory"
            '''
        sql.execute(query)
        rows = sql.fetchall()
        data = []
        for row in rows:
            history = PredictionHistoryALL(
                PredictID=row[0].lower(),
                PredictTime=str(row[2]),
                Result=row[1],
                ModelName=row[3]
            )
            data.append(history)
        sql.execute(count_query)
        total_count = sql.fetchone()[0]
        total_pages = (total_count + PageSize - 1) // PageSize

        has_previous = PageIndex > 1
        has_next = PageIndex < total_pages

        response_header = PaginationResponseHeader(
            PageIndex=PageIndex,
            PageSize=PageSize,
            TotalCount=total_count,
            TotalPages=total_pages,
            HasPrevious=has_previous,
            HasNext=has_next
        )
        response = PaginationResponse(
            PaginationResponseHeader=response_header,
            Data=data
        )

        return response

    except Exception as e:
        print("An error occurred:", str(e))
        return []

def GetByID(id):
    try:
        query = f'''
                SELECT
                    P."PredictID",
                    P."Note",
                    P."Result",
                    P."PredictTime",
                    M."MachineID",
                    ML."MachineName",
                    M."DeepID",
                    DL."DeepName",
                    M."ModelName"
                FROM
                    "PredictionHistory" P
					JOIN "Model" M ON M."ModelID" = P."ModelID"
                    JOIN "MachineLearning" ML ON M."MachineID" = ML."MachineID"
                    JOIN "DeepLearning" DL ON M."DeepID" = DL."DeepID"
                WHERE PredictID = '{id}'
                '''
        sql.execute(query)
        row = sql.fetchone()
        if row:
            history = PredictionHistory(
                    PredictID=row[0].lower(),
                    Note=row[1],
                    Result=row[2],
                    PredictTime=str(row[3]),
                    MachineLearning=MachineLearning(
                        MachineID=row[4].lower(),
                        MachineName=row[5]
                    ),
                    DeepLearning=DeepLearning(
                        DeepID=row[6].lower(),
                        DeepName=row[7]
                    ),
                    ModelName =row[8]
                )
            data = history.dict()
        else:
            data = None
        return data
    except Exception as e:
        print("An error occurred:", str(e))
        return []
def Create(Note,Result,ModelID):
    try:
        query = "INSERT INTO PredictionHistory (Note,Result,PredictTime,ModelID) VALUES (?, ?, ?, ?)"
        values = (Note,Result,datetime.now(),ModelID)
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