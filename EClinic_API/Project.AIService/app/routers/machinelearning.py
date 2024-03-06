import sys
import os
auth_path = os.path.join(os.path.dirname(__file__), '..', 'auth')
data_path = os.path.join(os.path.dirname(__file__), '..', 'data')
sys.path.append(data_path)
sys.path.append(auth_path)
from fastapi import APIRouter, Depends, status, HTTPException, Response
from fastapi.responses import JSONResponse
from auth_bearer import JWTBearer,Role
import machineLearningRepository as repository
from data import MachineLearning
router = APIRouter(tags=['MachineLearning'])

@router.get('/MachineLearning/GetAll',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
async def GetAll():
    try:
        data = repository.GetAll()
        res = {
            "isSuccess": True,
            "data": data
        }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")
@router.get('/MachineLearning/GetByID',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
async def GetByID(MachineID:str):
    try:
        data = repository.GetByID(MachineID)
        if data is None:
           res = {
            "isSuccess": False,
            "Message": "Not Found"
            }
        else:
            res = {
            "isSuccess": True,
            "data": data
            }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")          
@router.post('/MachineLearning/Create',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Create(MachineName):
    try:
        result =repository.Create(MachineName)
        if result is False:
            res = {
                "isSuccess": False,
                "Message": "Create error"
            }
            return JSONResponse(res)
        res = {
                "isSuccess": True,
                "Message": "Create Success"
            }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")   
@router.put('/MachineLearning/Update',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Update(data:MachineLearning):
    try:
        result =repository.Update(data.MachineID,data.MachineName)
        if result is False:
            res = {
                "isSuccess": False,
                "Message": "Update error"
            }
            return JSONResponse(res)
        res = {
                "isSuccess": True,
                "Message": "Update Success"
            }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")   
@router.delete('/MachineLearning/Delete',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Delete(MachineID:str):
    try:
        result =repository.Delete(MachineID)
        if result is False:
            res = {
                "isSuccess": False,
                "Message": "Delete error"
            }
            return JSONResponse(res)
        res = {
                "isSuccess": True,
                "Message": "Delete Success"
            }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")   