"""
Defines a utility class for rendering `input_schemas` as
appropriately-formatted Markdown tables:

```python
DictMarkdown(
    schema=input_schemas.gias[2024],
    key_column="Column Name",
    val_column="Data Type",
)
```

Each table (or set of tables) can then be displayed as, for example:

```python
from pipeline import input_schemas

print(str(DictMarkdown(input_schemas.workforce_census[2024], key_column="Column Name", val_column="Data Type")))
print(str(DictMarkdown(input_schemas.workforce_census_column_mappings[2024], key_column="Column From", val_column="Column To")))
print(str(DictMarkdown(input_schemas.workforce_census_column_eval[2024], key_column="Derived Column", val_column="Calculation")))
```
"""


class DictMarkdown:
    def __init__(
        self,
        schema: dict,
        key_column: str,
        val_column: str,
    ):
        self.key_column = key_column
        self.val_column = val_column
        self.data = [
            {
                key_column: column,
                val_column: data_type,
            }
            for column, data_type in schema.items()
        ]
        self.key_column_width = max(
            len(self.key_column), max(len(c[self.key_column]) for c in self.data)
        )
        self.val_column_width = max(
            len(self.val_column), max(len(c[self.val_column]) for c in self.data)
        )

    def __str__(self):
        lines = [
            f"| {self.key_column.ljust(self.key_column_width)} | {self.val_column.ljust(self.val_column_width)} |",
            f"|-{'-' * self.key_column_width}-|-{'-' * self.val_column_width}-|",
        ]
        for row in self.data:
            lines.append(
                f"| {row[self.key_column].ljust(self.key_column_width)} | {row[self.val_column].ljust(self.val_column_width)} |"
            )

        return "\n".join(lines)
