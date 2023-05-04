# Enterprise Personnel Accounting
Сервис для учета сотрудников на предприятии.<br />
Пользователь может увидеть на главной странице всех сотрудников, выполнить поиск сотрудников по ФИО, позиции или отделу. Также есть возможность добавлять, удалять и редактировать сотрудников. Если у сотрудника есть менеджер или подчиненные это отражается в его данных. Также можно добавлять, удалять, редактировать отделы и позиции.<br />

### <a name="инструкция">Чтобы запустить этот проект, вам нужно проделать следующие шаги</a>
1. Скачать репозиторий себе на компьютер
2. В Microsoft SQL Manager Studio ( или где либо еще ) создать новую базу данных
3. В Visual Studio ( или другой IDE ) установить через менеджер пакетов:
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFramework
4. Открываем appsettings.json и в пустую строку `"DefaultConnection": ""` вписываем `Server=[server_name];Database=[database_name];Trusted_Connection=True;`
5. Tools -> Connect to Database... - подключиться к созданной ранее базе данных
6. Tools -> NuGet Package Manager -> Package Manager Console
7. В открывшейся консоли вводим следующие команды:
    - `Add-Migration [name]`
    - `Update-Database`
8. Если вы правильно проделали эти шаги, у вас проект должен запускаться