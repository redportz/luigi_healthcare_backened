# Luigi-Healthcare-Backend

## Group Number: 4  
**Authors:**  
- Matthew Walker  
- Eric Zabala  
- Jacob Carter  
- Justin Cornell  
- Raad Darwish  
- Cade Parlato  

---

## 🧠 Setup the Local MySQL Database (No Cloud Required)

We've removed all personal data and cloud dependencies. You will now set up your own **local MySQL database** using the included script.

---

### ✅ Step 1: Install MySQL & MySQL Workbench

- **MySQL Community Server:**  
  [https://dev.mysql.com/downloads/mysql/](https://dev.mysql.com/downloads/mysql/)  
  ⚙️ During setup, change the **default port to `5114`**.

- **MySQL Workbench:**  
  [https://dev.mysql.com/downloads/workbench/](https://dev.mysql.com/downloads/workbench/)

---

### ✅ Step 2: Run the Included Database Script

1. Open **MySQL Workbench**.
2. Connect to:
   - **Host:** `localhost`
   - **Port:** `your port here`
   - **User:** `root` (or your configured user)
3. Click `File > Open SQL Script…`
   - Select the file: `script.sql`
4. Make sure the following lines are included at the top of the script (or run them manually first):
   ```sql
   CREATE DATABASE IF NOT EXISTS luigi_healthcare;
   USE luigi_healthcare;
5. Click the ⚡️ **Execute** button to run the script.

---


## 🧪 Running the Backend Locally

### Step 1: Install Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/)

---

### Step 2: Clone the Repository

Using [GitHub Desktop](https://desktop.github.com/):
- File > Clone Repository
- Choose this repo or paste the URL
- Click **Clone**

---

### Step 3: Open in VS Code
- Click **"Open in Visual Studio Code"** from GitHub Desktop  
  *or* open VS Code and go to **File > Open Folder...**

---

### Step 4: Add Environment Variables

- Create a file named `.env` at:
  HealthCareDBBackend/HealthCareDBBackend/.env

- Add your local SQL Server connection string like this:
  DB_CONNECTION_STRING=Server=localhost;Database=luigi_healthcare;User Id=your_username;Password=your_password;Encrypt=False;

- Save the file. The backend will use this connection string to access your local database created from `script.sql`.

---

### Step 5: Run the Backend

- Open Visual Studio Code
- Open the terminal by:
  - Clicking the Terminal icon
  - Or using the shortcut Ctrl + ~
- Run the backend with the following command:
  dotnet run --project ./HealthCareDBBackend/HealthCareDBBackend.csproj
- The backend should now be running locally and connected to your local SQL Server database using the sample data.
