from pydantic import BaseModel,Field
from datetime import datetime
from typing import List
class MachineLearning(BaseModel):
    MachineID: str
    MachineName: str
class DeepLearning(BaseModel):
    DeepID: str
    DeepName: str
class Model(BaseModel):
    ModelID:str
    ModelName:str
    Accuracy: float
    IsActive: bool
    CreatedAt: str
    UpdatedAt: str
    MachineLearning: MachineLearning
    DeepLearning: DeepLearning
class ModelAll(BaseModel):
    ModelID:str
    ModelName:str
    Accuracy: float
    IsActive: bool
    MachineLearning: MachineLearning
    DeepLearning: DeepLearning
class PredictionHistoryALL(BaseModel):
    PredictID:str
    Result: str
    PredictTime: str
    ModelName:str
class PredictionHistory(BaseModel):
    PredictID:str
    Note:str
    Result: str
    PredictTime: str
    MachineLearning: MachineLearning
    DeepLearning: DeepLearning
    ModelName:str
class ModelDtos(BaseModel):
    ModelName:str
    Accuracy: float
    IsActive: bool
    CreatedAt: datetime
    UpdatedAt: datetime
    MachineID: str
    MachineID: str
class GetModelDtos(BaseModel):
    ModelID:str
    FileUrl:str
    DeepName:str
class PaginationResponseHeader(BaseModel):
    PageIndex:int
    PageSize:int
    TotalCount:int
    TotalPages:int
    HasPrevious:bool
    HasNext:bool
class PaginationResponse(BaseModel):
    PaginationResponseHeader: PaginationResponseHeader
    Data: List[PredictionHistoryALL]