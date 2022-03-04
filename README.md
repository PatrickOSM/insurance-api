# Insurance Policy API

## Overview

This project was created as part of the hiring process at superformula and will be utilized as a sample project for C# .Net 6.0 API implementation.

## Insurances

They're identified by their ids, which are unique GUID, and live under `/api/InsurancePolicy/{id}`.

All insurance policies have the following properties:

Field | Description
------|------------
id | The policy unique id.
firstName | Client name.
lastName | Client last name.
driversLicense | Drivers License #.
vehicleName | Name of the drivers vehicle.
VehicleModel | Model of the vehicle.
VehicleManufacturer | Manufacturer of the vehicle.
VehicleYear | Vehicle manufactor year, should be before 1995 to meet the "classic" status.
Street | US street address.
City | The ids of the item's comments, in ranked display order.
State | US state.
ZipCode | US postal code.
EffectiveDate | Data to define when the policy will start to take effect.
ExpirationDate | Validation of the insurance policy.
Premium | Amount of money for the insurance policy.

### Request example

```javascript
GET /InsurancePolicy
{
   "driversLicence": "749882582"
   "sortField": "VehicleYear"
   "ascending": true
   "expiredPolicies": true
}
```

### Response

```javascript
{
  "currentPage": 0,
  "totalPages": 1,
  "totalItems": 1,
  "result": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "firstName": "Timothy",
      "lastName": "Davis",
      "driversLicence": "594533707",
      "vehicleName": "Peugeot 406",
      "vehicleModel": "Peugeot Coupé",
      "vehicleManufacturer": "Peugeot",
      "vehicleYear": 1995,
      "street": "1000 Radio Park Drive",
      "city": "Augusta",
      "state": "GA",
      "zipCode": "30904",
      "effectiveDate": "2022-04-10T00:00:00.0000000-00:00",
      "expirationDate": "2022-04-10T00:00:00.0000000-00:00",
      "premium": 500000
    }
  ]
}
```
