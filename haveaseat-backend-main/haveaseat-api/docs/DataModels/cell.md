# Cell Model

The Cell model represents a cell in the database (stored in the `Cells` table).

## Fields

| Field       | Type      | Description                              | Required | Max Length | Primary Key | Foreign Key |
|-------------|-----------|------------------------------------------|----------|------------|-------------|-------------|
| `Id`        | `bigint`  | The unique identifier of the Cell        | Yes      | -          | Yes         | No          |
| `PositionX` | `integer` | The X coordinate of the Cell             | No       | -          | No          | No          |
| `PositionY` | `integer` | The Y coordinate of the Cell             | No       | -          | No          | No          |
| `Border`    | `varchar` | The style of the wall on map             | No       | 23         | No          | No          |
| `RoomId`    | `bigint`  | The id of Room which contains this Cell  | Yes      | -          | No          | Yes         |

> **Note:** While the `PositionX`, `PositionY`, and `Border` fields are not required in the database, the program cannot work without them.

## Example

```
{
  "Id": 1,
  "PositionX": 1,
  "PositionY": 1,
  "Border": "-2px -2px 0px 2px black",
  "RoomId": 1
}
```

## Relationships

- **Room**: A cell belongs to one room. This is a many-to-one relationship with the [**Room**](room.md) model (stored in the `Rooms` table).