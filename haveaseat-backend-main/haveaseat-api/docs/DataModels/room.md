# Room Model

The Room model represents a room in the database (stored in the `Rooms` table).

## Fields

| Field       | Type      | Description                       | Required | Max Length | Primary Key |
|-------------|-----------|-----------------------------------|----------|------------|-------------|
| `Id`        | `bigint`  | The unique identifier of the Room | Yes      | -          | Yes         |
| `Name`      | `varchar` | The name of the Room              | No       | 4          | No          |


> **Note:** While the `Name` field is not required in the database, it is highly recommended to provide a name for better identification and usability.
## Example

```
{
  "Id": 1,
  "Name": "2.7"
}
```

## Relationships

- **Cells**: A room can have multiple cells inside. This is a one-to-many relationship with the [**Cell**](cell.md) model (stored in the `Cells` table).

- **Desks**: A room can have multiple desks inside. This is a one-to-many relationship with the [**Desk**](desk.md) model (stored in the `Desks` table).
