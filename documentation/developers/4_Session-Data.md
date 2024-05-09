# Session state 

The web app needs to maintain/store user data across requests - specifically modifications to comparator sets. 
Given these actions can be performed by any user (authenticated and non-authenticated) options for storing this data were limited.

Cookies store was considered, however their size should be kept to a minimum with most browsers restrict cookie size to 4096 bytes. 
Whilst there are options to work around this, it would introduce dependencies on client side scripting.

Session state is being used for storage of user data while the user browses a web app, more information can be found [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-8.0#session-state).

Several configuration options have been implemented;
1. In-memory
2. Cosmos 
3. Redis

### In-memory
This is the default option when no configuration has been set.

### Cosmos
The follow settings will enable Cosmos Db to be used as the backing data store. 

```
"SessionData" : {
    "Using" : "Cosmos",
    "Settings" : {
        "ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
        "ContainerName": "[INSERT CONTAINER NAME VALUE]",
        "DatabaseName": "[INSERT DATABASE NAME VALUE]"
    }
}
```

### Redis
The follow settings will enable Redis to be used as the backing data store.

```
"SessionData" : {
    "Using" : "Redis",
    "Settings" : {
        "ConnectionString" : "[INSERT CONNECTION STRING VALUE]",
        "InstanceName": "[INSERT INSTANCE NAME VALUE]"
    }
}
```