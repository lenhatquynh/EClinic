import time
from typing import Dict
import jwt
# from decouple import config


# JWT_SECRET = config("secret_key")
# JWT_ALGORITHM = config("algorithm")
# JWT_ISSUER = config("issuer")
# JWT_AUDIENCE = config("audience")
JWT_SECRET ="EClinicH*q5yWs$L8RmBvKEeVgSc6Jjt4#2I3r!^G7UkXZ&(DhT"
JWT_ALGORITHM = ""
JWT_ISSUER = "FiveManDev"
JWT_AUDIENCE = "EClinic"
def token_response(token: str):
    return {
        "access_token": token
    }

def signJWT(user_id: str) -> Dict[str, str]:
    payload = {
        "user_id": user_id,
        "expires": time.time() + 600
    }
    token = jwt.encode(payload, JWT_SECRET, algorithm=JWT_ALGORITHM)

    return token_response(token)


def decodeJWT(token: str) -> dict:
    try:
        decoded_token = jwt.decode(token, JWT_SECRET, algorithms=["HS256"],issuer=JWT_ISSUER,audience=JWT_AUDIENCE)
        return decoded_token if decoded_token["exp"] >= time.time() else None
    except:
        return {}
