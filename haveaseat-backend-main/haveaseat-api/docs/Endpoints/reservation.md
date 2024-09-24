# Reservation

## GetByEmail

Retrieves all reservations for the user with given email.

**Request example**:

```
http GET https://localhost:7023/api/Reservation/getByEmail/jKowalski@sagra.pl
Host: localhost:7023
```
> **Note:** The email parameter is case insensitive. For instance, JKOWALSKI@SAGRA.PL and jkowalski@sagra.pl will both return the same result.

**Method**: GET

**URL**: /api/Reservation/getByEmail

**Responses**:

- **Status Code**: 200 OK

```
[
  {
    "id": 2,
    "date": "2024-09-23",
    "desk": {
      "id": 1,
      "positionX": 1,
      "positionY": 1,
      "chairPosition": 0
    },
    "user": {
      "id": 4,
      "email": "jKowalski@sagra.pl",
      "name": "Jan",
      "surname": "Kowalski",
      "role": 1
    }
  },
  {
    "id": 3,
    "date": "2024-09-24",
    "desk": {
      "id": 1,
      "positionX": 1,
      "positionY": 1,
      "chairPosition": 0
    },
    "user": {
      "id": 4,
      "email": "jKowalski@sagra.pl",
      "name": "Jan",
      "surname": "Kowalski",
      "role": 1
    }
  },
  {
    "id": 4,
    "date": "2024-09-25",
    "desk": {
      "id": 2,
      "positionX": 4,
      "positionY": 1,
      "chairPosition": 0
    },
    "user": {
      "id": 4,
      "email": "jKowalski@sagra.pl",
      "name": "Jan",
      "surname": "Kowalski",
      "role": 1
    }
  }
]
```

- **Status Code**: 404 NotFound

```
{
  "message": "No reservations for given user",
  "userEmail": "jKowalski@sagra.pl"
}
```

## GetByDay

Retrieves all reservations on given day.

**Request example**:

```
https://localhost:7023/api/Reservation/getByDay/2024.09.23
Host: localhost:7023
```

> **Note:** Date must be in format YYYY.MM.DD for example 2024.11.30.

**Method**: GET

**URL**: /api/Reservation/getByDay

**Responses**:

- **Status Code**: 200 OK

```
[
  {
    "id": 2,
    "date": "2024-09-23",
    "desk": {
      "id": 1,
      "positionX": 1,
      "positionY": 1,
      "chairPosition": 0
    },
    "user": {
      "id": 4,
      "email": "jKowalski@sagra.pl",
      "name": "Jan",
      "surname": "Kowalski",
      "role": 1
    }
  },
  {
    "id":6,
    "date": "2024-09-23",
    "desk": {
      "id": 2,
      "positionX": 4,
      "positionY": 1,
      "chairPosition": 0
    },
    "user": {
      "id": 5,
      "email": "jNowak@sagra.pl",
      "name": "Jan",
      "surname": "Nowak",
      "role": 1
    }
  }
]
```

## GetAllUsersWithReservationByDay

Retrieve all users and reservation ids with reservations on a given day.

**Request example**:

```
http GET https://localhost:7023/api/Reservation/getAllUsersWithReservationByDay/2024.09.23
Host: localhost:7023
```
> **Note:** Date must be in format YYYY.MM.DD for example 2024.11.30.

**Method**: GET

**URL**: /api/Reservation/getAllUsersWithReservationByDay

**Responses**:

- **Status Code**: 200 OK

```
[
  {
    "id": 4,
    "email": "jKowalski@sagra.pl",
    "name": "Jan",
    "surname": "Kowalski",
    "role": 1,
    "reservationId": 2
  },
  {
    "id": 5,
    "email": "jNowak@sagra.pl",
    "name": "Jan",
    "surname": "Nowak",
    "role": 1,
    "reservationId": 6
  }
]
```

- **Status Code**: 400 BadRequest

```
{
  "error": "Not sent!"
}
```

## GetAllReservationsByDeskId

Retrieves all users with reservations on a desk that has a given ID.

**Request example**:

```
http GET https://localhost:7023/api/Reservation/getAllReservationsByDeskId/1
Host: localhost:7023
```

**Method**: GET

**URL**: /api/Reservation/getAllReservationsByDeskId

**Responses**:

- **StatusCode**: 200 OK

```
[
  {
    "date": "2024-09-19",
    "user": {
      "id": 6,
      "email": "jNowak@sagra.pl",
      "name": "Jan","
      "surname": "Nowak",
      "role": 1
    }
  },
  {
    "date": "2024-09-24",
    "user": {
      "id": 4,
      "email": "jKowalski@sagra.pl",
      "name": "Jan",
      "surname": "Kowalski",
      "role": 1
    }
  },
  {
    "date": "2024-09-23",
    "user": {
      "id": 4,
      "email": "jKowalski@sagra.pl",
      "name": "Jan",
      "surname": "Kowalski",
      "role": 1
    }
  }
]
```

## NewReservation

Adds a new reservation.

**Request example**:

```
http POST https://localhost:7023/api/Reservation/newReservation
Host: localhost:7023
Content-Type: application/json

{
  "date": "2024-10-20",
  "deskId": 6,
  "userId": 4
}
```

> **Note:** The **deskId** and **userId** must match the **desk** and **user** id in the database.

**Method**: POST

**URL**: /api/Authentication/register

**Responses**:

- **Status Code**: 201 Created

```
{
  "date": "2024-10-20",
  "deskId": 6,
  "userId": 4
}
```

- **Status Code**: 400 BadRequest

```
{
"error": "Not sent!"
}
```

- **Status Code**: 400 BadRequest

```
{
"error": "Reservation already exist!"
}
```

## Delete

Deletes a reservation.

**Request example**:

```
http DELETE https://localhost:7023/api/Reservation/delete/1
Host: localhost:7023
```

**Method**: DELETE

**URL**: /api/Reservation/delete

**Responses**:

- **Status Code**: 202 Accepted

```
true
```

- **Status Code**: 500 InternalServerError

```
{
  "error": "Something went wrong"
}
```