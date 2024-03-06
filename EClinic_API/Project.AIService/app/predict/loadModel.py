import joblib
import os
def LoadModel(modelPath, input):
    try:
        path = os.path.join(os.getcwd(), modelPath)
        loaded_model = joblib.load(path)
        prediction = loaded_model.predict(input)
        predicted_label = prediction[0]
        return PredictResult(predicted_label)
    except Exception as e:
        print("An error occurred in LoadModel:", str(e))
        return "unknown"
def PredictResult(num):
    if num == 1:
        return "Monkeypox"
    elif num == 2:
        return "Chickenpox"
    elif num == 3:
        return "Measles"
    elif num == 4:
        return "Normal"
    else:
        return "unknown"
