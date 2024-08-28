
# .Net API auditing proof of concept

![example workflow](https://github.com/ilroberts/AuditedApi/actions/workflows/dotnet.yml/badge.svg?branch=main)

This project illustrates the use of [Audit.NET](https://github.com/thepirat000/Audit.NET) and [CorrelationId](https://github.com/stevejgordon/CorrelationId) to provide an audit trail within and between services.

The project also illustrates the use of [Polly](https://www.pollydocs.org/index.html) as a relience library betwen the RequestService and the AuditedApi. It also makes use of [Simmy](https://github.com/Polly-Contrib/Simmy) to simulate faults in the RequestService.

## Running locally

### RequestService

#### Running the RequestService service

1. ```cd RequestService```
2. ```dotnet restore```
3. ```dotnet run```

#### Calling the API

Using your favourite API client, call ```http://localhost:5002/api/Forecast```.

### Audited API

#### Running the AuditedAPI service

1. ```cd AuditedApi```
2. ```dotnet restore```
3. ```dotnet run```

The API will start up on port 5001 by default.

#### Calling the RequestService API

Using your favourite API client, call ```http://localhost:5001/api/request```. This will act as a proxy for a call to the AuditedApi. The AuditedApi therefore needs to be running for this to work. The ```program.cs``` file in the RequestService project contains the Chaos configurations. This implementation followed a [blog post from Microsoft](https://devblogs.microsoft.com/dotnet/resilience-and-chaos-engineering/).

#### View the audit record

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
