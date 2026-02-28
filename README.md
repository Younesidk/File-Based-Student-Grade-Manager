# 📚 File-Based Student Grade Manager

A simple console application built in **C# / .NET 10** that lets you manage student records and grades, persisting all data to a local text file — no database needed.

> Personal project built for fun to practice file I/O, generics, and clean console app structure in C#.

---

## ✨ Features

| # | Feature |
|---|---------|
| 1 | View all students |
| 2 | Add a new student |
| 3 | View a student's grade average |
| 4 | Edit an existing student |
| 5 | Delete a student |
| 6 | Export all students to a CSV file |
| 0 | Exit |

---

## 🗂️ Project Structure

```
File-Based Student Grade Manager/
├── Program.cs          # Entry point — menu loop & user interaction
├── StudentService.cs   # CRUD operations & file persistence
├── StudentModel.cs     # Student data model & CSV serialization
└── students.txt        # Flat-file "database" (auto-created)
```

---

## 🧑‍🎓 Student Data

Each student record holds:

| Field | Type | Constraint |
|-------|------|------------|
| ID | `int` | Auto-incremented |
| First Name | `string` | Required |
| Last Name | `string` | Required |
| Age | `int` | 18 – 30 |
| Exam 1 | `double` | 0 – 20 |
| Exam 2 | `double` | 0 – 20 |
| Exam 3 | `double` | 0 – 20 |
| Average | `double` | Computed — `(E1 + E2 + E3) / 3` |

Records are stored in `students.txt` as comma-separated values:

```
1,John,Doe,21,18.5,16,19
2,Jane,Smith,22,20,17.5,15
```

---

## 📤 CSV Export

Choosing option **6** opens a **Save File Dialog** (Windows Forms) that lets you pick any location and file name on your machine. The exported `.csv` includes a header row followed by all student records:

```
id,FirstName,LastName,Age,Exam1,Exam2,Exam3
1,John,Doe,21,18.5,16,19
2,Jane,Smith,22,20,17.5,15
```

> ⚠️ The export writes to the file path you choose in the dialog — it does **not** overwrite `students.txt`.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run

```bash
git clone <repo-url>
cd "File-Based Student Grade Manager"
dotnet run
```

---

## 🛠️ Tech Stack

- **Language:** C# 14
- **Framework:** .NET 10
- **Storage:** Flat file (`students.txt`)
- **I/O:** `System.IO.File`
- **UI Dialog:** `System.Windows.Forms.SaveFileDialog` (CSV export)
