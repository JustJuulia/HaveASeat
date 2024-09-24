# User Model

The User model represents an user in the database (stored in the `Users` table).

## Fields

| Field      | Type      | Description                                    | Required | Primary Key | Max Length |
|------------|-----------|------------------------------------------------|----------|-------------|------------|
| `Id`       | `bigint`  | The unique identifier of the User              | Yes      | Yes         | -          |
| `Email`    | `varchar` | The email of the User                          | No       | No          | 45         |
| `Password` | `varchar` | The hashed password of the User                | No       | No          | 255        |
| `Name`     | `varchar` | The name of the User                           | No       | No          | 60         |
| `Surname`  | `varchar` | The surname of the User                        | No       | No          | 60         |
| `salt`     | `varchar` | The salt of the User's hashed password         | Yes      | No          | 255        |
| `Role`     | `integer` | The role of the User                           | No       | No          | -          |

> **Note:** While the `Email`, `Password`, `Name`, `Surname`, `Role` fields are not required in the database, the program cannot work without them.

> **Note:** While there is no limit for the valu of the `Role` field, it should take one of these values: **0**, **1** which correspond to **ADMIN**, **EMPLOYEE**, **bottom**, and **left**.

> **Note:** You can't set the `Role` value through an API request. It is set by default to one. To change it, you must approach the database directly.

## Example

```
{
  "Id": 3,
  "Email": "jKowalski@sagra.pl",
  "Password:" "$2a$10$QQutEgeyvhy0fcfhw/ytNuj9m.XfzBPxOUBiL4O.P0tuuAV1eewlu",
  "Name": "Jan",
  "Surname": "Kowalski",
  "salt": "$2a$10$QQutEgeyvhy0fcfhw/ytNu",
  "Role": 1
}
```

## Relationships

- **Reservation**: User can make multiple reservations. This is a one-to-many relationship with the [**Reservation**](reservation.md) model (stored in the `Reservations` table).
