import sys
import os
import shutil
auth_path = os.path.join(os.path.dirname(__file__), '..', 'auth')
data_path = os.path.join(os.path.dirname(__file__), '..', 'data')
sys.path.append(data_path)
sys.path.append(auth_path)
from fastapi import APIRouter, Depends, status, HTTPException, Response, File, UploadFile, Form
from fastapi.responses import JSONResponse
from auth_bearer import JWTBearer,Role
import modelRepository as repository
from data import ModelDtos
router = APIRouter(tags=['Model'])

@router.get('/Model/GetAll',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
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
@router.get('/Model/GetByID',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
async def GetByID(ModelID:str):
    try:
        data = repository.GetByID(ModelID)
        res = {
            "isSuccess": True,
            "data": data
        }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")          
@router.post('/Model/Create',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Create(ModelName:str= Form(...),Accuracy:float= Form(...),MachineID:str= Form(...),DeepID:str= Form(...),file: UploadFile = File(...) ):
    try:
        FileUrl = '/app/app/model/' + file.filename
        save_path = os.path.join(os.getcwd(), FileUrl)

        with open(save_path, 'wb') as buffer:
            shutil.copyfileobj(file.file, buffer)
        result =repository.Create(ModelName,Accuracy,MachineID,DeepID,FileUrl)
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
@router.put('/Model/Update',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Update(ModelID:str=Form(...), ModelName:str= Form(...),Accuracy:float= Form(...),MachineID:str= Form(...),DeepID:str= Form(...),file: UploadFile = File(default=None) ):
    try:
        if file is not None:
            FileUrl = '/app/app/model/' + file.filename
            save_path = os.path.join(os.getcwd(), FileUrl)

            with open(save_path, 'wb') as buffer:
                shutil.copyfileobj(file.file, buffer)
        else:
            FileUrl =None
        result =repository.Update(ModelID,ModelName,Accuracy,MachineID,DeepID,FileUrl)
        if result is False:
            res = {
                "isSuccess": False,
                "Message": "Update error"
            }
        else:
            res = {
                "isSuccess": True,
                "Message": "Update Success"
            }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")   
@router.put('/Model/Active',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Active(ModelID):
    try:
        result =repository.ActiveModel(ModelID)
        if result is False:
            res = {
                "isSuccess": False,
                "Message": "Update error"
            }
            return JSONResponse(res)
        else:
            res = {
                "isSuccess": True,
                "Message": "Update Success"
            }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")   
@router.delete('/Model/Delete',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
def Delete(ModelID:str):
    try:
        result =repository.Delete(ModelID)
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