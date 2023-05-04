# DigitalPlanner
Сервис для ведения заметок.<br />

### Функционал:
0. Лендинговая страничка. Если пользователь попытается получить доступ к приватной информации через меню, то его перебросит на страничку авторизации.<br />

![alt text](https://github.com/marie-frostova/DigitalPlanner/blob/master/images/Снимок.PNG?raw=true)

1. Авторизация пользователя. Если нет аккаунта, его можно создать.<br />

![alt text](https://github.com/marie-frostova/DigitalPlanner/blob/master/images/Снимок4.PNG?raw=true)

2. В раскладке календаря можно получить доступ заметкам, привязанным к конкретной дате.<br />

![alt text](https://github.com/marie-frostova/DigitalPlanner/blob/master/images/Снимок2.PNG?raw=true)

3. Редактор текста работает в парадигме WYSIWYM (What You See Is What You Mean).<br />

![alt text](https://github.com/marie-frostova/DigitalPlanner/blob/master/images/Снимок3.PNG?raw=true)

### Чтобы запустить этот проект, вам нужно проделать следующие шаги:
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
