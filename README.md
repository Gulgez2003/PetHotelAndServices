# PetHotel And Services

Welcome to PetHotel And Services! This project is a comprehensive solution designed to provide a range of services for pets and their owners, including boarding, grooming, and other pet-related services.

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Project Overview

PetHotel And Services is a web-based application that allows users to book various services for their pets. The application supports functionalities such as user registration, booking management, service scheduling, and payment processing.

## Features

- **User Authentication:** Secure user registration and login.
- **Pet Profiles:** Users can create and manage profiles for their pets.
- **Service Booking:** Users can book services like boarding, grooming, and daycare.
- **Payment Integration:** Secure payment processing for booked services.
- **Admin Panel:** Admin users can manage bookings, users, and services.
- **Notifications:** Email notifications for booking confirmations and updates.
- **Responsive Design:** User-friendly interface optimized for both desktop and mobile devices.

## Technologies Used

- **Backend:**
  - .NET Core
  - ASP.NET Core MVC
  - Entity Framework Core
- **Frontend:**
  - HTML5, CSS3, JavaScript
  - Bootstrap
- **Database:**
  - Microsoft SQL Server
- **Other:**
  - SendGrid (for email notifications)
  - Stripe (for payment processing)
  - Docker (for containerization)
  - Git (for version control)

## Installation

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker](https://www.docker.com/get-started) (optional, for containerization)

### Steps

1. **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/PetHotelAndServices.git
    cd PetHotelAndServices
    ```

2. **Set up the database:**

    Ensure your SQL Server is running and create a database for the project. Update the connection string in `appsettings.json`.

3. **Run database migrations:**

    ```bash
    dotnet ef database update
    ```

4. **Configure environment variables:**

    Set up environment variables for sensitive information such as API keys for SendGrid and Stripe.

5. **Run the application:**

    ```bash
    dotnet run
    ```

## Usage

1. **Register an account:** Create a new user account.
2. **Create pet profiles:** Add profiles for your pets.
3. **Book services:** Choose and book services for your pets.
4. **Manage bookings:** View and manage your bookings from the user dashboard.
5. **Admin panel:** Admin users can manage users, services, and bookings.

## Configuration

Update the `appsettings.json` file with your specific configuration settings, including the database connection string, API keys for third-party services, and other relevant settings.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
  },
  "SendGrid": {
    "ApiKey": "your_sendgrid_api_key"
  },
  "Stripe": {
    "SecretKey": "your_stripe_secret_key"
  }
}
