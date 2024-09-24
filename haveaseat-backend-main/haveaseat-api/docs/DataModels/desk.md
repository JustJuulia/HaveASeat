# Desk Model

The Desk model represents a desk in the database (stored in the `Desks` table).

## Fields

| Field           | Type      | Description                              | Required | Primary Key | Foreign Key |
|-----------------|-----------|------------------------------------------|----------|-------------|-------------|
| `Id`            | `bigint`  | The unique identifier of the Desk        | Yes      | Yes         | No          |
| `PositionX`     | `integer` | The X coordinate of the Desk             | No       | No          | No          |
| `PositionY`     | `integer` | The Y coordinate of the Desk             | No       | No          | No          |
| `ChairPosition` | `integer` | The position of the chair                | No       | No          | No          |
| `RoomId`        | `bigint`  | The id of Room which contains this Desk  | Yes      | No          | Yes         |

> **Note:** While the `PositionX`, `PositionY`, and `ChairPosition` fields are not required in the database, the program cannot work without them.

> **Note:** While there is no limit for the value of the `ChairPosition` field, it should take one of these values: **0**, **1**, **2**, **3**, which correspond to **top**, **right**, **bottom**, and **left**.

## Example

```
{
  "Id": 7,
  "PositionX": 6,
  "PositionY": 4,
  "ChairPosition": 0,
  "RoomId": 3
}
```

## Relationships

- **Room**: A desk belongs to one room. This is a many-to-one relationship with the [**Room**](room.md) model (stored in the `Rooms` table).

- **Reservations**: A desk can have multiple reservations on itself. This is a one-to-many relationship with the [**Reservation**](reservation.md) model (stored in the `Reservations` table).
