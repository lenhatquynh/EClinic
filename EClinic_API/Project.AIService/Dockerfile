FROM python:3.10.11

RUN apt-get update && \
    apt-get install -y unixodbc unixodbc-dev && \
    apt-get install -y curl && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql17
RUN apt-get update && apt-get install ffmpeg libsm6 libxext6  -y
WORKDIR /app

COPY Project.AIService/requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

COPY Project.AIService/app /app/app

EXPOSE 8000

CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8000"]
