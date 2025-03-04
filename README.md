# QuickPostApi

QuickPostApi is a web API built with ASP.NET Core 8.0. It provides endpoints for managing posts, authentication, and other related functionalities. The project uses various libraries and tools such as MediatR, NLog, OpenTelemetry, and Swagger for enhanced functionality and observability.

## Features

- **Global Exception Handling**: Middleware to handle exceptions globally.
- **Authentication and Authorization**: JWT-based authentication.
- **Logging**: NLog for logging.
- **OpenTelemetry**: Instrumentation for tracing.
- **Swagger**: API documentation and testing.

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Docker (optional, for containerization)

### Installation

1. Clone the repository:

git clone https://github.com/your-repo/QuickPostApi.git
cd QuickPostApi


2. Restore the dependencies:

dotnet restore

3. Build the project:

dotnet build

### Running the Application

1. Run the application:

dotnet run --project QuickPostApi

2. The API will be available at `https://localhost:5001` (or the configured port).

### Using Docker

1. Build the Docker image:

docker build -t quickpostapi .

2. Run the Docker container:

docker run -d -p 5000:80 quickpostapi

## Using Docker Compose

To run the application along with Prometheus and Grafana for monitoring, use Docker Compose:

1. Ensure Docker and Docker Compose are installed on your machine.

2. Build and start the services:

   ```bash
   docker-compose up --build
   ```

3. Access the services:

   - **QuickPostApi**: `http://localhost:8080`
   - **Prometheus**: `http://localhost:9090`
   - **Grafana**: `http://localhost:3000` (Default credentials: `admin` / `admin`)

4. Configure Grafana to use Prometheus as a data source to visualize metrics.

### Configuration

- **Prometheus**: The configuration file is located at `prometheus/prometheus.yml`.
- **Grafana**: Data is persisted in the `grafana-data` volume.

## Redis

- docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 -e REDIS_ARGS="--requirepass 123456" redis/redis-stack:latest

## Configuration

The application can be configured using the `appsettings.json` file. Ensure to set up the necessary configurations for logging, authentication, and other services.

## Documentation

Swagger is used for API documentation. Once the application is running, you can access the Swagger UI at `https://localhost:5001/swagger`.

## Project Structure

- **QuickPostApi**: Main API project.
- **Application**: Contains application logic and services.
- **Infrastructure**: Infrastructure-related code such as authentication.
- **Persistence**: Data access layer.

## Dependencies

- [MediatR](https://github.com/jbogard/MediatR)
- [NLog](https://github.com/NLog/NLog)
- [OpenTelemetry](https://github.com/open-telemetry/opentelemetry-dotnet)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [FluentValidation](https://fluentvalidation.net/)

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.



