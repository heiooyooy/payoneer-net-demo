# **Order Web API Demo**

This demo was built by **Dejun (Darren) Tu**  for Payoneer interview only.

## **Technology Stack**

- **.NET 8**
- **[ASP.NET](http://ASP.NET) Core Web API**
- **Entity Framework Core 8**
- **Sqlite**
- **Serilog** (for logging)
- **xUnit** (for testing)
- **Moq** (for mocking in unit tests)
- **Swagger / OpenAPI** (for API documentation and testing)

## **Setup and Installation**

### **Prerequisites**

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or above

### **1. Clone the Repository**

```
git clone git@github.com:heiooyooy/payoneer-net-demo.git
cd payoneer-net-demo
```

## **Running the Application**

### **Running the API**

1. Navigate to the `payoneer-net-backend` project directory in your terminal.

2. Run the application with the following command:

   ```
   dotnet run
   ```

3. The API will be available at `https://localhost:xxxx` (e.g., `https://localhost:7123`).

4. You can access the **Swagger UI** for interactive API documentation and testing at `https://localhost:xxxx/swagger`.

## API Endpoints**

| **Method** | **Endpoint**       | **Description**                            | **Success Response** |
| ---------- | ------------------ | ------------------------------------------ | -------------------- |
| `POST`     | `/api/orders`      | Creates a new order.                       | `201 Created`        |
| `GET`      | `/api/orders`      | Retrieves a list of all orders with items. | `200 OK`             |
| `GET`      | `/api/orders/{id}` | Retrieves a single order by its `OrderId`. | `200 OK`             |

## Test data for convenience

Note: Since Id is the primary key so use the same data twice will create error, remember to update the guid for multiple runs.

```json
{
  "orderId": "3fa85f64-5717-4562-b3fc-2c963f66afa7",
  "customerName": "John Doe",
  "items": [
    {
      "productId": "1fa85f64-5717-4562-b3fc-2c963f66afa5",
      "quantity": 2
    },
    {
      "productId": "2fa85f64-5717-4562-b3fc-2c963f66afa5",
      "quantity": 1
    }
  ],
  "createdAt": "2025-06-14T12:00:00Z"
}
```

Or, use any IDE to call the apis from payoneer-net-backend.http file.

### **Running the Unit Tests and Integration Test**

To run the complete suite of unit and integration tests, navigate to the solution's root directory and run:

```
dotnet test
```