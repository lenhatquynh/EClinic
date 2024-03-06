import sys
import os
auth_path = os.path.join(os.path.dirname(__file__), '..', 'auth')
data_path = os.path.join(os.path.dirname(__file__), '..', 'data')
predict_path = os.path.join(os.path.dirname(__file__), '..', 'predict')
sys.path.append(data_path)
sys.path.append(auth_path)
sys.path.append(predict_path)
from fastapi import APIRouter, Depends, status, HTTPException, Response, File, UploadFile, Form
from fastapi.responses import JSONResponse
from deepPredict import MobileNetPredict,ResNet50Predict,VGG16Predict
from loadModel import LoadModel
from auth_bearer import JWTBearer,Role
import modelRepository as repository
import historyRepository as historyRepository
import cv2
import numpy as np
router = APIRouter(tags=['Predict'])

# @router.get('/test',dependencies=[Depends(JWTBearer(role = Role.Admin))] )
@router.post('/AIPredict/DoctorPredict',dependencies=[Depends(JWTBearer(roles=[Role.Doctor]))] )
async def DoctorPredict(file: UploadFile = File(...), Note: str = Form(default=None)):
    try:
        if Note is None:
            Note = 'No content'
        model = repository.GetActive()
        path = os.path.join(os.getcwd(), model.FileUrl)
        input = None
        result = None
        image = cv2.imdecode(np.frombuffer(file.file.read(), np.uint8), cv2.IMREAD_COLOR)
        if model.DeepName =='ResNet-50':
            input = ResNet50Predict(image)
            result = LoadModel(path,input)
        elif model.DeepName =='VGG16':
            input = VGG16Predict(image)
            result = LoadModel(path,input)
        elif model.DeepName =='MobileNet':
            input = MobileNetPredict(image)
            result = LoadModel(path,input)
        else:
            input = ResNet50Predict(image)
            result = LoadModel(path,input)
            if result == 'unknown':
                input = VGG16Predict(image)
                result = LoadModel(path,input)
            if result == 'unknown':
                input = MobileNetPredict(image)
                result = LoadModel(path,input)
        historyRepository.Create(Note,result,model.ModelID)
        res = {
            "isSuccess": True,
            "data": result
        }
        return JSONResponse(res)
    except Exception as e:
        raise HTTPException(status_code=500, detail="Internal Server Error")
@router.post('/AIPredict/ExpertPredict',dependencies=[Depends(JWTBearer(roles=[Role.Expert]))] )
async def ExpertPredict(file: UploadFile = File(...), Note: str = Form(default=None),ModelID: str = Form(...)):
    try:
        if Note is None:
            Note = 'No content'
        model = repository.GetModelUrl(ModelID)
        path = os.path.join(os.getcwd(), model.FileUrl)
        input = None
        result = None
        image = cv2.imdecode(np.frombuffer(file.file.read(), np.uint8), cv2.IMREAD_COLOR)
        if model.DeepName =='ResNet-50':
            input = ResNet50Predict(image)
            result = LoadModel(path,input)
        elif model.DeepName =='VGG16':
            input = VGG16Predict(image)
            result = LoadModel(path,input)
        elif model.DeepName =='MobileNet':
            input = MobileNetPredict(image)
            result = LoadModel(path,input)
        else:
            input = ResNet50Predict(image)
            result = LoadModel(path,input)
            if result == 'unknown':
                input = VGG16Predict(image)
                result = LoadModel(path,input)
            if result == 'unknown':
                input = MobileNetPredict(image)
                result = LoadModel(path,input)
        historyRepository.Create(Note,result,model.ModelID)
        res = {
            "isSuccess": True,
            "data": result
        }
        return JSONResponse(res)
    except Exception as e:
        print(f"An exception occurred: {e}")
        raise HTTPException(status_code=500, detail="Internal Server Error")