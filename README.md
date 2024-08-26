
# .Net API auditing proof of concept

This project illustrates the use of [Audit.NET](https://github.com/thepirat000/Audit.NET) and [CorrelationId](https://github.com/stevejgordon/CorrelationId) to provide an audit trail within and between services.

## Running locally

### Running the service

1. ```cd AuditedApi```
2. ```dotnet restore```
3. ```dotnet run```

The API will start up on port 5001 by default.

### Calling the API

Using your favourite API client, call ```http://localhost:5001/api/Forecast```. Pass the correlation id using the **X-Correlation-ID** header.

### View the audit record

For the purposes of this PoC the audit output is located in the logs directory. The following is an example of the output produced:

```json
{
  "Action": {
    "TraceId": "0HN65MNPHOSNV:00000001",
    "HttpMethod": "GET",
    "ControllerName": "Forecast",
    "ActionName": "GetForecasts",
    "ActionParameters": {},
    "RequestUrl": "http://localhost:5001/api/Forecast",
    "IpAddress": "127.0.0.1",
    "ResponseStatus": "OK",
    "ResponseStatusCode": 200,
    "Headers": {
      "Accept": "*/*",
      "Connection": "close",
      "Host": "localhost:5001",
      "User-Agent": "Thunder Client (https://www.thunderclient.com)",
      "Accept-Encoding": "gzip, deflate, br",
      "X-Correlation-ID": "ABC-123"
    }
  },
  "EventType": "Forecast/GetForecasts (GET)",
  "Environment": {
    "UserName": "xxxx",
    "MachineName": "xxxxx",
    "DomainName": "xxxxxxx",
    "CallingMethodName": "AuditedApi.Contollers.ForecastController.GetForecasts()",
    "AssemblyName": "AuditedApi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
    "Culture": "xxx"
  },
  "StartDate": "2024-08-26T10:00:43.907147Z",
  "EndDate": "2024-08-26T10:00:43.930518Z",
  "Duration": 23
}
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
