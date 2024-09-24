# Reservation Model

The Reservation model represents a reservation in the database (stored in the `Reservations` table).

## Fields

| Field    | Type      | Description                                    | Required | Primary Key | Foreign Key |
|----------|-----------|------------------------------------------------|----------|-------------|-------------|
| `Id`     | `bigint`  | The unique identifier of the Reservation       | Yes      | Yes         | No          |
| `Date`   | `date`    | The date on which the Desk is booked           | No       | No          | No          |
| `UserId` | `bigint`  | The id of the User who booked Desk             | Yes      | No          | Yes         |
| `DeskId` | `bigint`  | The id of the Desk on which reservation is for | Yes      | No          | Yes         |


> **Note:** While the `Date` field is not required in the database, the program cannot work without it.

## Example

```
{
  "Id": 2,
  "Date": "2024-09-23",
  "UserId": 4,
  "DeskId": 1
}
```

## Relationships

- **Desk**: A reservation is for one desk. This is a many-to-one relationship with the [**Desk**](desk.md) model (stored in the `Desks` table).

- **User**: A reservation is made by one user. This is a many-to-one relationship with the [**User**](user.md) model (stored in the `Users` table).
