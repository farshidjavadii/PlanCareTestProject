## Overview

**Car Registration Management System** is a full-stack web application designed to manage vehicle information and monitor registration expiry status in real-time.  

- **Backend:** .NET 9
- **Frontend:** Angular 20 
- **Database:** In-memory data (can be extended to SQL Server or other databases)  
- **Features:**  
  - Retrieve a list of cars (`/`) (default: http://localhost:4200/)
  - Real-time registration status updates (`/registration`) using SignalR  (default : http://localhost:4200/registration)
  - Background service to check registration expiry  

---

## Features

### 1. Cars List (`/`)
- Fetch a list of cars using a **GET API**  
- Optional query parameter: `make`  

### 2. Registration Status (`/registration`)
- Background service checks registration expiry automatically  
- SignalR pushes real-time updates to the frontend table  

---

## Getting Start
docker compose up
