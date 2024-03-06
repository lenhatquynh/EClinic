from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
import uvicorn
from app.routers import machinelearning,deeplearning,model,predict,history


app = FastAPI()

origins = ["*"]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
    expose_headers=["X-Pagination"],
)

app.include_router(machinelearning.router)
app.include_router(deeplearning.router)
app.include_router(model.router)
app.include_router(history.router)
app.include_router(predict.router)


@app.get("/")
def root():
    return {"message": "Hello World AI Predict"}