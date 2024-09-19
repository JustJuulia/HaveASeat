# Forbidden Dates

## GetAllForbiddenDates

Retrievers all forbidden dates.

**Request example**:

```
http GET https://localhost:7023/api/ForbiddenDate/getAllForbiddenDates
Host: localhost:7023
```

**Method**: GET

**URL**: /api/ForbiddenDate/getAllForbiddenDates

**Responses**:

- **Status Code**: 200 OK

```
[
  {
    "id": 1,
    "description": "Swieto niepodleglosci",
    "date": "2024-11-11"
  },
  {
    "id": 3,
    "description": "Dzien Zaduszny",
    "date": "2024-11-02"
  },
  {
    "id": 4,
    "description": "Pierwszy dzień świąt",
    "date": "2024-12-25"
  },
  {
    "id": 5,
    "description": "Drugi dzień świąt",
    "date": "2024-12-26"
  }
]
```

## GetByDate

Retrieves forbidden date information based on the provided date.

**Request example**:

```
http GET https://localhost:7023/api/ForbiddenDate/GetByDate/11.11.2024
Host: localhost:7023
```

**Method**: GET

**URL**: /api/ForbiddenDate/GetByDate

**Responses**:

- **Status Code**: 200 OK

```
{
  "id": 1,
  "description": "Swieto niepodleglosci",
  "date": "2024-11-11"
}
```

- **Status Code**: 404 Not Found

```
{
  "error": "Date is not Forbidden date!",
  "forbiddenDate": "2024-10-10"
}
```

## GetById

Retrieves forbidden date information based on the provided id.

**Request example**:

```
http GET https://localhost:7023/api/ForbiddenDate/GetById/1
Host: localhost:7023
```

**Method**: GET

**URL**: /api/ForbiddenDate/GetById

**Responses**:

- **Status Code**: 200 OK

```
{
  "id": 1,
  "description": "Swieto niepodleglosci",
  "date": "2024-11-11"
}
```

- **Status Code**: 404 Not Found

```
{
  "message": "Date is not Forbidden date!",
  "forbiddenDateId": 1
}
```

## AddForbiddenDate

Adds a new forbidden date.

**Request example**:

```
http POST https://localhost:7023/api/Authentication/AddForbiddenDate
Host: localhost:7023
Content-Type: application/json

{
  "description": "Dzień niepodległości",
  "date": "2024-11-11"
}
```

**Method**: POST

**URL**: /api/Authentication/AddForbiddenDate

**Responses**:

- **Status Code**: 201 Created

```
{
  "description": "Dzień niepodległości",
  "date": "2024-11-11"
}
```

- **Status Code**: 400 Bad Request

```
{
  "error": "Not send!"
}
```

- **Status Code**: 400 Bad Request

```
{
  "error": "Date already is forbidden"
}
```

- **Status Code**: 500 Internal Server Error

```
{
  "error": "An error with the database"
}
```

## EditForbiddenDate

Edits forbidden date.

**Request example**:

```
http POST https://localhost:7023/api/Authentication/EditForbiddenDate
Host: localhost:7023
Content-Type: application/json

{
  "description": "Dzień niepodległości",
  "date": "2024-11-11"
}
```

**Method**: POST

**URL**: /api/Authentication/EditForbiddenDate

**Responses**:

- **Status Code**: 200 OK

```
  true
```

- **Status Code**: 400 Bad Request

```
{
  "error": "Not send!"
}
```

- **Status Code**: 400 Bad Request

```
{
  "error": "Date is not forbidden"
}
```

- **Status Code**: 500 Internal Server Error

```
{
  "error": "Error with the database"
}
```

## Delete

Deletes forbidden date.

**Request example**:

```
http DELETE https://localhost:7023/api/ForbiddenDate/delete/11.11.2024
Host: localhost:7023
```

**Method**: DELETE

**URL**: /api/Authentication/delete
