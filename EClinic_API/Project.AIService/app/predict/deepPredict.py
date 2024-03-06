import cv2
import numpy as np
from tensorflow.keras.applications.vgg16 import VGG16, preprocess_input
from tensorflow.keras.applications.resnet50 import ResNet50, preprocess_input
from tensorflow.keras.applications.mobilenet import MobileNet, preprocess_input
def VGG16Predict(file):
    try:
        pic_size = 224
        image = cv2.resize(file, (pic_size, pic_size))
        image = preprocess_input(image)
        image = np.reshape(image, (1, pic_size, pic_size, 3))
        base_model = VGG16(weights='imagenet', include_top=False)
        features = base_model.predict(image)
        features = features.reshape((features.shape[0], 512*7*7))
        return features
    except Exception as e:
        print("An error occurred in VGG16Predict:", str(e))
        return None

def ResNet50Predict(file):
    try:
        pic_size = 224
        image = cv2.resize(file, (pic_size, pic_size))
        image = preprocess_input(image)
        image = np.reshape(image, (1, pic_size, pic_size, 3))
        base_model = ResNet50(weights='imagenet', include_top=False)
        features = base_model.predict(image)
        features = features.reshape((features.shape[0], 7*7*2048))
        return features
    except Exception as e:
        print("An error occurred in ResNet50Predict:", str(e))
        return None

def MobileNetPredict(file):
    try:
        pic_size = 224
        image = cv2.resize(file, (pic_size, pic_size))
        image = preprocess_input(image)
        image = np.reshape(image, (1, pic_size, pic_size, 3))
        base_model = MobileNet(weights='imagenet', include_top=False, input_shape=(pic_size, pic_size, 3))
        features = base_model.predict(image)
        features = features.reshape((features.shape[0], 1024*7*7))
        return features
    except Exception as e:
        print("An error occurred in MobileNetPredict:", str(e))
        return None
