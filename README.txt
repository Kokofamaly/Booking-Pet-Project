Booking Pet Project

Описание: Минимальный сервис для бронирования мастер-классов с использованием ASP.NET Core Minimal API.

Технологии: ASP.NET Core 9, EF Core, SQLite, Minimal API, Swagger, Cookie Authentication.

Функционал:

- Авторизация пользователей (логин/логаут)
- CRUD для мастер-классов (только для админа)
- Запись на мастер-классы (только для юзеров)
- Список участников мастер-класса (админ)
- Список записей пользователя (юзер)
- DTO для безопасного обмена данными между клиентом и сервером

Как запустить:

1. Клонировать репозиторий
2. dotnet restore
3. dotnet build
4. dotnet run

Открыть браузер по адресу: http://localhost:{порт}

Тестирование:

Swagger: http://localhost:{порт}/swagger
Форма логина: /login
Эндпоинты для юзера: /user/*
Эндпоинты для админа: /admin/*

Примеры запросов через Swagger или Postman:

Авторизация: POST /auth/login с form-data: email, password
Получить все мастер-классы (юзер): GET /user/master-class
Создать мастер-класс (админ): POST /admin/master-class/create с JSON CreateMasterClassDto
Записаться на мастер-класс (юзер): POST /user/master-class/{id}/sign-up
Подтвердить запись (юзер): PUT /user/master-class/{id}/confirm
Отменить запись (юзер): PUT /user/master-class/{id}/cancel
Получить участников (админ): GET /admin/master-class/{id}/participants

Тестовые данные (seeded):

Админ: admin@site.com / 1234
Юзер: user@site.com / 1234