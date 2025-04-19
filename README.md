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

## 🧠 Setup the Local Database (No Cloud Required)

We've removed all personal data and cloud dependencies. You will now **set up your own local SQL Server database** using the included script:

### ✅ Step 1: Install SQL Server & SSMS

- Download and install **SQL Server**:  
  [https://www.microsoft.com/en-us/sql-server/sql-server-downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

- Download and install **SQL Server Management Studio (SSMS)**:  
  [https://aka.ms/ssms](https://aka.ms/ssms)

---

### ✅ Step 2: Run the Included Database Script

1. Open SSMS and connect to **your local SQL Server**.
2. Right-click **Databases > New Database**  
   - Name it something like `luigi_healthcare`
3. Once created, click **File > Open > File...**
   - Select the file: [`script.sql`](.script.sql)
4. Make sure the new database is selected in the dropdown (top toolbar).
5. Click **Execute** to run the script.

✅ Your database is now fully set up with test accounts and sample data.

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
