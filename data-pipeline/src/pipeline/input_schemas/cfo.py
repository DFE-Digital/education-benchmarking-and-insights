cfo = {
    2021: {
        "Companies House Number": "string",
        "Title": "string",
        "Forename 1": "string",
        "Surname": "string",
        "Direct email address": "string",
    },
    2022: {
        "Companies House Number": "string",
        "Title": "string",
        "Forename 1": "string",
        "Surname": "string",
        "Direct email address": "string",
    },
    2023: {
        "Companies House Number": "string",
        "Title": "string",
        "Forename 1": "string",
        "Surname": "string",
        "Direct email address": "string",
    },
    "default": {
        "Companies House Number": "string",
        "CFO forename": "string",
        "CFO surname": "string",
        "CFO email": "string",
    },
}

cfo_column_mappings = {
    2021: {
        "Direct email address": "CFO email",
    },
    2022: {
        "Direct email address": "CFO email",
    },
    2023: {
        "Direct email address": "CFO email",
    },
    "default": {
        "CFO forename": "Forename 1",
        "CFO surname": "Surname",
    },
}

cfo_column_eval = {
    2021: {
        "CFO name": "`Title`.str.cat([`Forename 1`, `Surname`], sep=' ')",
    },
    2022: {
        "CFO name": "`Title`.str.cat([`Forename 1`, `Surname`], sep=' ')",
    },
    2023: {
        "CFO name": "`Title`.str.cat([`Forename 1`, `Surname`], sep=' ')",
    },
    "default": {
        "CFO name": "`Forename 1`.str.cat(`Surname`, sep=' ')",
    },
}
