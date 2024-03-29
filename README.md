## Anonymous Notes with ASP.NET Using Blazor for Frontend

Project is a web application for collaborative note-taking that allows all users to create, read, and edit mutual notes.

ASP.NET is used for backend server operations and Blazor for frontend. The backend uses Entity Framework with PostgreSQL as a database. 

NUnit/BUnit is used for unit testing and Playwright is used for end-to-end testing.

For optimization purposes, Entity Framework operates in a non-tracking mode, which improves performance by reducing unnecessary overhead. 
Additionally, the pagination minimizes server load by fetching data on each page using small SQL queries for optimization.

SignalR is used to update the first page in real time. When new data is created/edited, it sends an request to the users who are currently viewing the front page to update the data.

#### The application was developed as a test task for a Junior Software Developer position.
### Task:
```
Create an application for storing and sharing notes.

Requirements: 
- All users work with shared notes. No authentication is required;
- Each user can create a note with a title and text;
- Each user can view a list of all notes created by all users;
- Each user can edit any note;
- User can search for notes by word from the title or text.
- Implementation on Blazor Server, EF Core, and PostgreSQL;
- It is necessary to minimise the load on the database;
- Coverage with integration tests and unit tests
```

> #### You may use the code or resources as you want.

<hr>

![image](https://github.com/TEGTO/AnonymousNotesSharingBlazor/assets/90476119/1b36ff3a-6ae9-4019-b520-e2fcb2c6ccc0)

<hr>

![image](https://github.com/TEGTO/AnonymousNotesSharingBlazor/assets/90476119/919274ad-f650-4353-8a73-f41ec9919713)

<hr>

![image](https://github.com/TEGTO/AnonymousNotesSharingBlazor/assets/90476119/12601050-829f-40db-9299-f52ed68fde3a)

<hr>

![image](https://github.com/TEGTO/AnonymousNotesSharingBlazor/assets/90476119/7149de0f-edb4-4b9a-add1-23bb429e1503)

