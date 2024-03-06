import sys
import os
auth_path = os.path.join(os.path.dirname(__file__), '..', 'auth')
data_path = os.path.join(os.path.dirname(__file__), '..', 'data')
sys.path.append(data_path)
sys.path.append(auth_path)
from fastapi import APIRouter, Depends, status, HTTPException, Response
from fastapi.responses import JSONResponse
from auth_bearer import JWTBearer,Role
import deepLearningRepository as repository
from data import  DeepLearning
router = APIRouter(tags=['DeepLearning'])


@router.get('/DeepLearning/GetAll',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
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
@router.get('/DeepLearning/GetByID',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
async def GetByID(DeepID:str):
    try:
        data = repository.GetByID(DeepID)
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
@router.post('/DeepLearning/Create',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Create(DeepName):
    try:
        result =repository.Create(DeepName)
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
@router.put('/DeepLearning/Update',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Update(data: DeepLearning):
    try:
        result =repository.Update(data.DeepID,data.DeepName)
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
@router.delete('/DeepLearning/Delete',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
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