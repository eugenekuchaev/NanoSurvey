## Installation
Run the following command to start the application using Docker Compose:
```
docker-compose up
```
## API Endpoints
### GetQuestion Method
You can retrieve a question using the following URI:
http://localhost:50001/api/survey/surveys/1/questions/1  
Method: GET  

**Returns:**
```json
{
    "questionText": "How old are you?",
    "answers": [
        "18-24 years old",
        "25-34 years old",
        "35-44 years old",
        "45-54 years old",
        "55 years old and above"
    ]
}
```
### SaveResult Method
To save survey results, use the following URI:
http://localhost:50001/api/survey/save-result  
Method: POST  

**Request JSON:**
```json
{
    "surveyId": 1,
    "questionId": 1,
    "answerId": 1,
    "interviewId": 0 
}

```
**Returns:**
```json
{
    "nextQuestionId": 2,
    "interviewId": 1
}
```
