# Tattletrail
Service to detect outages from background processes and services


## Endpoints:

### Get all monitors:
*GET* in green ```/api/v1/monitors``` - returns a JSON list of monitors active

### Create new monitor:

*POST* in red ```/api/v1/monitors```  - creates a monitor, takes an email to notifiy with, name, interval time in seconds

This call should contain body. Example:

```{
    "processname": "Process name",
    "intervaltime": 120,
    "subscribers": [
        "test@email.com",
        "test2@email.com"
    ]
}```

### Delete monitor:

*DELETE* in red ```/api/v1/monitors/{monitorid}``` - removes a monitor

### Get monitor details:

*GET* in green ```/api/v1/monitors/{monitorid}``` - returns info on an active monitor, when it was last heard from, in error state, etc.

### Checkin process activity

*GET* in green ```/api/v1/monitors/{monitorid}/checkin``` - API used by process to report that it's active
