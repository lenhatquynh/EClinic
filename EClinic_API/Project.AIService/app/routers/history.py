import sys
import os
auth_path = os.path.join(os.path.dirname(__file__), '..', 'auth')
data_path = os.path.join(os.path.dirname(__file__), '..', 'data')
sys.path.append(data_path)
sys.path.append(auth_path)
from fastapi import APIRouter, Depends, status, HTTPException, Response,Header
from fastapi.responses import JSONResponse
from auth_bearer import JWTBearer,Role
import historyRepository as repository
from data import  DeepLearning
router = APIRouter(tags=['History'])

@router.get('/History/GetAll',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
async def GetAll(PageNumber: int = Header(...), PageSize: int = Header(...)):
    try:
        data = repository.GetAll(PageNumber, PageSize)
        data_list = [item.dict() for item in data.Data]
        res = {
            "isSuccess": True,
            "data": data_list
        }
        headers = {
            "X-Pagination": str(data.PaginationResponseHeader.dict())
        }
        
        return JSONResponse(res, headers=headers)
    except Exception as e:
        print("An error occurred aaa:", str(e))
        raise HTTPException(status_code=500, detail="Internal Server Error")
@router.get('/History/GetByID',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
async def GetByID(PredictID:str):
    try:
        data = repository.GetByID(PredictID)
        res = {
            "isSuccess": True,
            "data": data
        }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")          