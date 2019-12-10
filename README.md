# Tattletrail
Service to detect outages from background processes and services


## Endpoints:

### Get all monitors:
![#006400](https://placehold.it/15/006400/000000?text=+) **GET** ```/api/v1/monitors``` - returns a JSON list of monitors active

### Create new monitor:

![#f03c15](https://placehold.it/15/f03c15/000000?text=+) **POST**  ```/api/v1/monitors```  - creates a monitor, takes an email to notifiy with, name, interval time in seconds

This call should contain body. Example:

```
{
    "processname": "Process name",
    "intervaltime": 120,
    "subscribers": [
        "test@email.com",
        "test2@email.com"
    ]
}
```

Response:

```
{
  "monitorid": "guid_monitor_id",
  "checkinurl": "http://somehost/api/v1/monitors/{monitorid}/checkin"
}

```

### Delete monitor:

![#f03c15](https://placehold.it/15/f03c15/000000?text=+) **DELETE** ```/api/v1/monitors/{monitorid}``` - removes a monitor

### Get monitor details:

![#006400](https://placehold.it/15/006400/000000?text=+) **GET**  ```/api/v1/monitors/{monitorid}``` - returns info on an active monitor, when it was last heard from, in error state, etc.

### Checkin process activity

![#006400](https://placehold.it/15/006400/000000?text=+) **GET**  ```/api/v1/monitors/{monitorid}/checkin``` - API used by process to report that it's active
