# ForbiddenDate Model

The ForbiddenDate model represents a forbidden date in the database (stored in the `ForbiddenDates` table)

## Fields

| Field         | Type      | Description                                    | Required | Primary Key | Max Length | Alternate Key |
|---------------|-----------|------------------------------------------------|----------|-------------|------------|---------------|
| `Id`          | `bigint`  | The unique identifier of the ForbiddenDate     | Yes      | Yes         | -          | No            |
| `Description` | `varchar` | The description of the ForbiddenDate           | No       | No          | -          | No            |
| `Date`        | `date`    | The date which is forbidden                    | Yes      | No          | 140        | Yes           |


## Example

```
{
  "Id": 1,
  "Description": "Swieto niepodleglosci",
  "Date": "2024-11-11"
}
```

## Relationships

ForbiddenDate model has no relationships.