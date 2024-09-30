# Habit Tracker Console Application

This is a simple console-based Habit Tracker application built in C#.
The project is mostly for practice as I learn C#, but anyone is welcome to use it, experiment with it, and contribute if they wish!

## Features
- View all habits: Display a list of all tracked habits.
- View habit details: Display summary statistics for a specified habit (current streak, longest streak, % success, etc).
- Log daily habits: Record whether you completed each habit for the day.
- Add a new habit: Add a habit to your list of daily habits.
- Delete a habit: Remove a habit from your list.

This application is designed to be simple, with a basic CRUD (Create, Read, Update, Delete) structure.
It stores habit data using an SQLite database and allows users to interact with it through a text-based interface.

## Purpose

The primary purpose of this project is for me to practice C# concepts, including:

- Working with console applications
- Using MVC architecture
- Interacting with an SQLite database
- Implementing basic user input validation

While this is a learning project, anyone is free to:

- Download and use the code
- Fork the project and make improvements
- Submit pull requests with new features or bug fixes

## How to Run

#### Using Docker

1. Clone the repository:
    ```shell
   git clone https://github.com/jakegodsall/HabitTrackerCliApp.git
    ```
2. Navigate to the project directory
    ```shell
    cd HabitTrackerCliApp/
   ```
3. Build the Docker image:
   ```shell
   docker built -t habit-tracker .
    ```
4. Run the Docker container:
    ```shell
   docker run -it habit-tracker
    ```
   
#### Running Locally

1. Clone the repository:
    ```shell
   git clone https://github.com/jakegodsall/HabitTrackerCliApp.git
   ```
2. Navigate to the project directory:
    ```shell
    cd HabitTrackerCliApp/
   ``` 
3. Run the project:
    ```shell
   dotnet run
    ```

## Contributions

Contributions are welcome! Since this is a learning project, any improvements, feedback, or new ideas are appreciated. Feel free to:

- Open an issue to discuss potential changes or report bugs.
- Submit a pull request if you’d like to add functionality.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more details.

