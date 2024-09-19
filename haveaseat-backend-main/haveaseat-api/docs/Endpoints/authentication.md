# Authentication

## GetUserByEmail

Retrieves user information based on the provided email.

**Request example**:

```
http GET https://localhost:7023/api/Authentication/GetByEmail/jKowalski@sagra.pl
Host: localhost:7023
```

> **Note:** The email parameter is case insensitive. For instance, JKOWALSKI@SAGRA.PL and jkowalski@sagra.pl will both return the same result.

**Method**: GET

**URL**: /api/Authentication/GetUserByEmail

**Responses**:

- **Status Code**: 200 OK

```
{
  "id": 3,
  "email": "jKowalski@sagra.pl",
  "name": "Jan",
  "surname": "Kowalski",
  "role": 1
}
```
- **Status Code**: 404 Not Found

```
{
  "message": "No such user",
  "user": "jKowalski@sagra.pl"
}
```

## GetUserById

Retrieves user information based on the provided id.

**Request example**:

```
http GET https://localhost:7023/api/Authentication/GetById/3
Host: localhost:7023
```

**Method**: GET

**URL**: /api/Authentication/GetUserById

**Responses**:

- **Status Code**: 200 OK

```
{
  "id": 3,
  "email": "jKowalski@sagra.pl",
  "name": "Jan",
  "surname": "Kowalski",
  "role": 1
}
```

- **Status Code**: 404 Not Found

```
{
  "message": "No such user",
  "userId": 3
}
```

## Register

Registers a new user.

**Request example**:

```
http POST https://localhost:7023/api/Authentication/register
Host: localhost:7023
Content-Type: application/json

{
  "email": "jKowalski@sagra.pl",
  "password": "haslo",
  "name": "Jan",
  "surname": "Kowalski"
}
```

> **Note:** The password is hashed on the server side. It is likely to change in the further development process.

**Method**: POST

**URL**: /api/Authentication/register

**Responses**:

- **Status Code**: 201 Created

```
{
  "email": "jKowalski@sagra.pl",
  "password": "$2a$10$QQutEgeyvhy0fcfhw/ytNuj9m.XfzBPxOUBiL4O.P0tuuAV1eewlu",
  "name": "Jan",
  "surname": "Kowalski"
}
```

- **Status code**: 400 Bad Request

```
{
  "error": "User already exist!"
}
```

- **Status code**: 500 Internal Server Error

```
{
  "error": "An error with adding user to database!"
}
```

## Login

Logs an user.

**Request example**:

```
http POST https://localhost:7023/api/Authentication/Login
Host: localhost:7023
Content-Type: application/json

{
  "email": "jKowalski@sagra.pl",
  "password": "haslo"
}

```

> **Note:** The password is hashed on the server side. It is likely to change in the further development process.

> **Note:** The email parameter is case insensitive. For instance, JKOWALSKI@SAGRA.PL and jkowalski@sagra.pl will both return the same result.

**Method**: POST

**URL**: /api/Authentication/Login

**Responses**:

- **Status Code**: 202 Accepted

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
  "error": "Wrong email or password!"
}
```

- **Status Code**: 500 Internal Server Error

```
{
    "error": "An error with connecting to database!"
}
```