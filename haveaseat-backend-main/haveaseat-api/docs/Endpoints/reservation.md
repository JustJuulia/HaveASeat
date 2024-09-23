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
