# Map

## GetAllRooms

Retrievers all rooms and cells associated with them.

**Request example**:

```
http GET https://localhost:7023/api/Map/getAllRooms
Host: localhost:7023
```

**Method**: GET

**URL**: /api/Map/getAllRooms

**Responses**:

- **Status Code**: 200 OK

```
[
  {
    "id": 1,
    "name": "2.7",
    "cells": [
      {
        "id": 1,
        "positionX": 1,
        "positionY": 1,
        "border": "-2px -2px 0px 2px black"
      },
      {
        "id": 2,
        "positionX": 2,
        "positionY": 1,
        "border": "2px -2px 0px 2px black"
      },
      {
        "id": 3,
        "positionX": 1,
        "positionY": 2,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 4,
        "positionX": 2,
        "positionY": 2,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 5,
        "positionX": 1,
        "positionY": 3,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 6,
        "positionX": 2,
        "positionY": 3,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 7,
        "positionX": 1,
        "positionY": 4,
        "border": "-2px 2px 0px 2px black"
      },
      {
        "id": 8,
        "positionX": 2,
        "positionY": 4,
        "border": "4px 0px 0px 0px black"
      }
    ]
  },
  {
    "id": 2,
    "name": "2.8",
    "cells": [
      {
        "id": 9,
        "positionX": 3,
        "positionY": 1,
        "border": "-2px -2px 0px 2px black"
      },
      {
        "id": 10,
        "positionX": 4,
        "positionY": 1,
        "border": "2px -2px 0px 2px black"
      },
      {
        "id": 11,
        "positionX": 3,
        "positionY": 2,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 12,
        "positionX": 4,
        "positionY": 2,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 13,
        "positionX": 3,
        "positionY": 3,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 14,
        "positionX": 4,
        "positionY": 3,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 15,
        "positionX": 3,
        "positionY": 4,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 16,
        "positionX": 4,
        "positionY": 4,
        "border": "2px 2px 0px 2px black"
      }
    ]
  }
]
```

## GetAllMap

Retrieves all rooms, cells, and desks associated with them.

**Request example**:

```
http GET https://localhost:7023/api/Map/GetAllMap
Host: localhost:7023
```

**Method**: GET

**URL**: /api/Map/GetAllMap

**Responses**:

- **Status Code**: 200 OK

```
[
  {
    "id": 1,
    "name": "2.7",
    "cells": [
      {
        "id": 1,
        "positionX": 1,
        "positionY": 1,
        "border": "-2px -2px 0px 2px black"
      },
      {
        "id": 2,
        "positionX": 2,
        "positionY": 1,
        "border": "2px -2px 0px 2px black"
      },
      {
        "id": 3,
        "positionX": 1,
        "positionY": 2,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 4,
        "positionX": 2,
        "positionY": 2,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 5,
        "positionX": 1,
        "positionY": 3,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 6,
        "positionX": 2,
        "positionY": 3,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 7,
        "positionX": 1,
        "positionY": 4,
        "border": "-2px 2px 0px 2px black"
      },
      {
        "id": 8,
        "positionX": 2,
        "positionY": 4,
        "border": "4px 0px 0px 0px black"
      }
    ],
    "desks": [
      {
        "id": 1,
        "positionX": 1,
        "positionY": 1,
        "chairPosition": 0
      }
    ]
  },
  {
    "id": 2,
    "name": "2.8",
    "cells": [
      {
        "id": 9,
        "positionX": 3,
        "positionY": 1,
        "border": "-2px -2px 0px 2px black"
      },
      {
        "id": 10,
        "positionX": 4,
        "positionY": 1,
        "border": "2px -2px 0px 2px black"
      },
      {
        "id": 11,
        "positionX": 3,
        "positionY": 2,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 12,
        "positionX": 4,
        "positionY": 2,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 13,
        "positionX": 3,
        "positionY": 3,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 14,
        "positionX": 4,
        "positionY": 3,
        "border": "4px 0px 0px 0px black"
      },
      {
        "id": 15,
        "positionX": 3,
        "positionY": 4,
        "border": "-4px 0px 0px 0px black"
      },
      {
        "id": 16,
        "positionX": 4,
        "positionY": 4,
        "border": "2px 2px 0px 2px black"
      }
    ],
    "desks": [
      {
        "id": 2,
        "positionX": 4,
        "positionY": 1,
        "chairPosition": 0
      }
    ]
  }
]
```

## GetAllDesks

Retrievers all rooms and desks associated with them.

**Request example**:

```
http GET https://localhost:7023/api/Map/getAllDesks
Host: localhost:7023
```

**Method**: GET

**URL**: /api/Map/getAllDesks

**Responses**:

- **Status Code**: 200 OK

```
[
  {
    "id": 1,
    "name": "2.7",
    "desks": [
      {
        "id": 1,
        "positionX": 1,
        "positionY": 1,
        "chairPosition": 0
      }
    ]
  },
  {
    "id": 2,
    "name": "2.8",
    "desks": [
      {
        "id": 2,
        "positionX": 4,
        "positionY": 1,
        "chairPosition": 0
      }
    ]
  },
  {
    "id": 3,
    "name": "2.9",
    "desks": [
      {
        "id": 3,
        "positionX": 5,
        "positionY": 1,
        "chairPosition": 3
      },
      {
        "id": 4,
        "positionX": 6,
        "positionY": 1,
        "chairPosition": 1
      },
      {
        "id": 5,
        "positionX": 5,
        "positionY": 2,
        "chairPosition": 3
      },
      {
        "id": 6,
        "positionX": 6,
        "positionY": 2,
        "chairPosition": 1
      },
      {
        "id": 7,
        "positionX": 6,
        "positionY": 4,
        "chairPosition": 0
      }
    ]
  }
]
```

## AddNewDesk

Adds a new desk.

**Request example**:

```
http POST https://localhost:7023/api/Map/AddNewDesk
Host: localhost:7023
Content-Type: application/json

{
  "positionX": 2,
  "positionY": 1,
  "chairPosition": 1
}
```

> **Note:** The **positionX** and position **positionY** values must match the X and Y coordinates of one of the cells.

> **Note:** The available values for **chairPosition** are **0**, **1**, **2**, **3** which correspond to **top**, **right**, **bottom** and **left**.

**Method**: POST

**URL**: /api/Map/AddNewDesk

**Responses**:

- **Status Code**: 201 Created

```
  true
```

- **Status Code**: 400 BadRequest

```
{
  "error": "This position doesn't exist"
}
```

- **Status Code**: 400 BadRequest

```
{
  "error": "This position is already occupied"
}
```

## EditChairPositionByDeskPosition

Edits chair position of the desk.

**Request example**:

```
http POST https://localhost:7023/api/Map/EditChairPositionByDeskPosition?positionX=1&positionY=1&chairPosition=1
Host: localhost:7023
```

> **Note:** The **positionX** and position **positionY** values must match the X and Y coordinates of one of the cells.

> **Note:** The available values for **chairPosition** are **0**, **1**, **2**, **3** which correspond to **top**, **right**, **bottom** and **left**.

**Method**: POST

**URL**: /api/Map/EditChairPositionByDeskPosition

**Responses**:

- **Status Code**: 200 OK

```
  true
```

- **Status Code**: 500 InternalServerError

```
{
  "error": "Something went wrong"
}
```

## DeleteDesk

Deletes a desk.

```
http DELETE https://localhost:7023/api/Map/DeleteDesk
Host: localhost:7023
Content-Type: application/json

{
  "positionX": 1,
  "positionY": 1,
  "chairPosition": 1
}
```

> **Note:** The **positionX** and position **positionY** values must match the X and Y coordinates of one of the cells.

> **Note:** The **chairPosition** parameter is irrelevant. It exists because, from the frontend side, it is easier to send the existing desk object. However, creating an object only to take values necessary to delete the desk doesn’t seem to make much sense.

**Method**: DELETE

**URL**: /api/Map/DeleteDesk

**Responses**: 

- **Status Code**: 200 OK

```
  true
```

- **Status Code**: 400 BadRequest

```
{
  "error": "Not sent!"
}
```

- **Status Code**: 500 InternalServerError

```
{
  "error": "something went wrong"
}
```