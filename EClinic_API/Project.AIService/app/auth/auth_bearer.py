from fastapi import Request, HTTPException
from fastapi.security import HTTPBearer, HTTPAuthorizationCredentials
from enum import Enum
from typing import Optional
from auth_handler import decodeJWT

class Role(str, Enum):
    Admin = "Admin"
    Expert = "Expert"
    Doctor = "Doctor"

class JWTBearer(HTTPBearer):
    def __init__(self, auto_error: bool = True, roles: Optional[list[Role]] = None):
        super().__init__(auto_error=auto_error)
        self.required_roles = roles

    async def __call__(self, request: Request):
        credentials: HTTPAuthorizationCredentials = await super().__call__(request)
        if credentials:
            if not credentials.scheme == "Bearer":
                raise HTTPException(status_code=403, detail="Invalid authentication scheme.")
            if not self.verify_jwt(credentials.credentials):
                raise HTTPException(status_code=403, detail="Invalid token or expired token.")
            if self.required_roles and not self.verify_roles(credentials.credentials, self.required_roles):
                raise HTTPException(status_code=403, detail="Permission denied.")
            return credentials.credentials
        else:
            raise HTTPException(status_code=403, detail="Invalid authorization code.")

    def verify_jwt(self, jwtoken: str) -> bool:
        is_token_valid: bool = False
        try:
            payload = decodeJWT(jwtoken)
            if payload:
                is_token_valid = True
        except:
            payload = None
        return is_token_valid

    def verify_roles(self, jwtoken: str, required_roles: list[Role]) -> bool:
        is_role_valid: bool = False
        try:
            payload = decodeJWT(jwtoken) 
            if "role" in payload and payload["role"] in required_roles:
                is_role_valid = True
        except:
            payload = None
        return is_role_valid
